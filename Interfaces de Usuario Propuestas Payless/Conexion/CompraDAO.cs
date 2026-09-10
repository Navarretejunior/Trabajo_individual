using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    internal class CompraDAO
    {
        // ============================================================
        // MOSTRAR PROVEEDORES
        // ============================================================

        public DataTable MostrarProveedores()
        {
            DataTable tabla = new DataTable();

            ConexionBD conexionBD = new ConexionBD();
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                conexionBD.AbrirConexion();

                string consulta = @"
                    SELECT 
                        id_proveedor,
                        nombre
                    FROM proveedor
                    WHERE estado_proveedor = TRUE
                    ORDER BY nombre;
                ";

                using (NpgsqlCommand comando =
                    new NpgsqlCommand(consulta, conexion))
                using (NpgsqlDataAdapter adaptador =
                    new NpgsqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // ============================================================
        // OBTENER SIGUIENTE NUMERO DE COMPRA
        // ============================================================

        public int ObtenerSiguienteNumeroCompra()
        {
            ConexionBD conexionBD = new ConexionBD();
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                {
                    throw new Exception(
                        "No se pudo abrir la conexión con la base de datos.");
                }

                string consulta = @"
            SELECT COALESCE(MAX(id_compra), 0) + 1
            FROM compra;
        ";

                using (NpgsqlCommand comando =
                    new NpgsqlCommand(consulta, conexion))
                {
                    return Convert.ToInt32(
                        comando.ExecuteScalar());
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // ============================================================
        // MOSTRAR PRODUCTOS
        // ============================================================

        public DataTable MostrarProductos()
        {
            DataTable tabla = new DataTable();

            ConexionBD conexionBD = new ConexionBD();
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                conexionBD.AbrirConexion();

                string consulta = @"
                    SELECT
                        p.id_producto,
                        p.nombre,
                        c.nombre_categoria,
                        m.nombre_marca,
                        p.id_categoria,
                        p.id_marca
                    FROM producto p
                    INNER JOIN categoria c
                        ON p.id_categoria = c.id_categoria
                    INNER JOIN marca m
                        ON p.id_marca = m.id_marca
                    WHERE p.estado_producto = TRUE
                    ORDER BY p.nombre;
                ";

                using (NpgsqlCommand comando =
                    new NpgsqlCommand(consulta, conexion))
                using (NpgsqlDataAdapter adaptador =
                    new NpgsqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // ============================================================
        // MOSTRAR TALLAS DEL PRODUCTO
        // ============================================================

        public DataTable MostrarTallas(int idProducto)
        {
            DataTable tabla = new DataTable();

            ConexionBD conexionBD = new ConexionBD();
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                conexionBD.AbrirConexion();

                string consulta = @"
                    SELECT
                        id_producto_talla,
                        talla
                    FROM producto_talla
                    WHERE id_producto = @id_producto
                    ORDER BY CAST(talla AS INTEGER);
                ";

                using (NpgsqlCommand comando =
                    new NpgsqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@id_producto",
                        idProducto);

                    using (NpgsqlDataAdapter adaptador =
                        new NpgsqlDataAdapter(comando))
                    {
                        adaptador.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // ============================================================
        // OBTENER O CREAR PRODUCTO_TALLA
        // ============================================================

        private int ObtenerOCrearProductoTalla(
            int idProducto,
            string talla,
            NpgsqlConnection conexion,
            NpgsqlTransaction transaccion)
        {
            string consultaBuscar = @"
        SELECT id_producto_talla
        FROM producto_talla
        WHERE id_producto = @id_producto
        AND talla = @talla;
    ";

            using (NpgsqlCommand comandoBuscar =
                new NpgsqlCommand(
                    consultaBuscar,
                    conexion,
                    transaccion))
            {
                comandoBuscar.Parameters.AddWithValue(
                    "@id_producto",
                    idProducto);

                comandoBuscar.Parameters.AddWithValue(
                    "@talla",
                    talla);

                object resultado =
                    comandoBuscar.ExecuteScalar();

                if (resultado != null)
                {
                    return Convert.ToInt32(resultado);
                }
            }


            // ========================================================
            // SI NO EXISTE, CREAR LA RELACIÓN PRODUCTO + TALLA
            // ========================================================

            string consultaInsertar = @"
        INSERT INTO producto_talla
        (
            talla,
            id_producto
        )
        VALUES
        (
            @talla,
            @id_producto
        )
        RETURNING id_producto_talla;
    ";

            int idProductoTalla;

            using (NpgsqlCommand comandoInsertar =
                new NpgsqlCommand(
                    consultaInsertar,
                    conexion,
                    transaccion))
            {
                comandoInsertar.Parameters.AddWithValue(
                    "@talla",
                    talla);

                comandoInsertar.Parameters.AddWithValue(
                    "@id_producto",
                    idProducto);

                idProductoTalla =
                    Convert.ToInt32(
                        comandoInsertar.ExecuteScalar());
            }


            // ========================================================
            // CREAR INVENTARIO PARA LA TALLA
            // ========================================================

            string consultaInventario = @"
        INSERT INTO inventario
        (
            stock_actual,
            stock_minimo,
            id_producto_talla
        )
        VALUES
        (
            0,
            0,
            @id_producto_talla
        );
    ";

            using (NpgsqlCommand comandoInventario =
                new NpgsqlCommand(
                    consultaInventario,
                    conexion,
                    transaccion))
            {
                comandoInventario.Parameters.AddWithValue(
                    "@id_producto_talla",
                    idProductoTalla);

                comandoInventario.ExecuteNonQuery();
            }

            return idProductoTalla;
        }


        // ============================================================
        // REGISTRAR COMPRA
        // ============================================================

        public int RegistrarCompra(
            decimal total,
            int idProveedor,
            DataTable detalles)
        {
            ConexionBD conexionBD = new ConexionBD();
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                {
                    throw new Exception(
                        "No se pudo abrir la conexión con la base de datos.");
                }

                using (NpgsqlTransaction transaccion =
                    conexion.BeginTransaction())
                {
                    try
                    {
                        // ====================================================
                        // INSERTAR COMPRA
                        // ====================================================

                        string consultaCompra = @"
                    INSERT INTO compra
                    (
                        total,
                        id_proveedor
                    )
                    VALUES
                    (
                        @total,
                        @id_proveedor
                    )
                    RETURNING id_compra;
                ";

                        int idCompra;

                        using (NpgsqlCommand comando =
                            new NpgsqlCommand(
                                consultaCompra,
                                conexion,
                                transaccion))
                        {
                            comando.Parameters.AddWithValue(
                                "@total",
                                total);

                            comando.Parameters.AddWithValue(
                                "@id_proveedor",
                                idProveedor);

                            idCompra =
                                Convert.ToInt32(
                                    comando.ExecuteScalar());
                        }


                        // ====================================================
                        // INSERTAR DETALLES
                        // ====================================================

                        foreach (DataRow fila in detalles.Rows)
                        {
                            int idProducto =
                                Convert.ToInt32(
                                    fila["codigo"]);

                            string talla =
                                fila["talla"].ToString();


                            // ==================================================
                            // OBTENER O CREAR PRODUCTO + TALLA
                            // ==================================================

                            int idProductoTalla =
                                ObtenerOCrearProductoTalla(
                                    idProducto,
                                    talla,
                                    conexion,
                                    transaccion);


                            // ==================================================
                            // INSERTAR DETALLE DE COMPRA
                            // ==================================================

                            string consultaDetalle = @"
                        INSERT INTO detalle_compra
                        (
                            id_compra,
                            id_producto_talla,
                            cantidad,
                            precio_compra,
                            precio_venta,
                            subtotal
                        )
                        VALUES
                        (
                            @id_compra,
                            @id_producto_talla,
                            @cantidad,
                            @precio_compra,
                            @precio_venta,
                            @subtotal
                        );
                    ";

                            using (NpgsqlCommand comandoDetalle =
                                new NpgsqlCommand(
                                    consultaDetalle,
                                    conexion,
                                    transaccion))
                            {
                                comandoDetalle.Parameters.AddWithValue(
                                    "@id_compra",
                                    idCompra);

                                comandoDetalle.Parameters.AddWithValue(
                                    "@id_producto_talla",
                                    idProductoTalla);

                                comandoDetalle.Parameters.AddWithValue(
                                    "@cantidad",
                                    Convert.ToInt32(
                                        fila["cantidad"]));

                                comandoDetalle.Parameters.AddWithValue(
                                    "@precio_compra",
                                    Convert.ToDecimal(
                                        fila["precio_compra"]));

                                comandoDetalle.Parameters.AddWithValue(
                                    "@precio_venta",
                                    Convert.ToDecimal(
                                        fila["precio_venta"]));

                                comandoDetalle.Parameters.AddWithValue(
                                    "@subtotal",
                                    Convert.ToDecimal(
                                        fila["subtotal"]));

                                comandoDetalle.ExecuteNonQuery();
                            }


                            // ==================================================
                            // ACTUALIZAR PRECIO DE VENTA DEL PRODUCTO
                            // ==================================================

                            string consultaPrecioVenta = @"
                        UPDATE producto
                        SET precio_venta = @precio_venta
                        WHERE id_producto = @id_producto;
                    ";

                            using (NpgsqlCommand comandoPrecioVenta =
                                new NpgsqlCommand(
                                    consultaPrecioVenta,
                                    conexion,
                                    transaccion))
                            {
                                comandoPrecioVenta.Parameters.AddWithValue(
                                    "@precio_venta",
                                    Convert.ToDecimal(
                                        fila["precio_venta"]));

                                comandoPrecioVenta.Parameters.AddWithValue(
                                    "@id_producto",
                                    idProducto);

                                comandoPrecioVenta.ExecuteNonQuery();
                            }


                            // ==================================================
                            // ACTUALIZAR INVENTARIO
                            // ==================================================

                            string consultaInventario = @"
                        UPDATE inventario
                        SET
                            stock_actual = stock_actual + @cantidad,
                            fecha_actualizacion = CURRENT_TIMESTAMP
                        WHERE id_producto_talla = @id_producto_talla;
                    ";

                            using (NpgsqlCommand comandoInventario =
                                new NpgsqlCommand(
                                    consultaInventario,
                                    conexion,
                                    transaccion))
                            {
                                comandoInventario.Parameters.AddWithValue(
                                    "@cantidad",
                                    Convert.ToInt32(
                                        fila["cantidad"]));

                                comandoInventario.Parameters.AddWithValue(
                                    "@id_producto_talla",
                                    idProductoTalla);

                                comandoInventario.ExecuteNonQuery();
                            }
                        }


                        // ====================================================
                        // CONFIRMAR TRANSACCIÓN
                        // ====================================================

                        transaccion.Commit();

                        return idCompra;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // ============================================================
        // MOSTRAR COMPRAS
        // ============================================================

        public DataTable MostrarCompras()
        {
            DataTable tabla = new DataTable();

            ConexionBD conexionBD = new ConexionBD();
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                conexionBD.AbrirConexion();

                string consulta = @"
                    SELECT
                        c.id_compra AS ""ID"",
                        c.fecha AS ""Fecha"",
                        p.nombre AS ""Proveedor"",
                        c.total AS ""Total"",
                        c.estado AS ""Estado""
                    FROM compra c
                    INNER JOIN proveedor p
                        ON c.id_proveedor = p.id_proveedor
                    ORDER BY c.id_compra DESC;
                ";

                using (NpgsqlCommand comando =
                    new NpgsqlCommand(consulta, conexion))
                using (NpgsqlDataAdapter adaptador =
                    new NpgsqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // ============================================================
        // BUSCAR COMPRA POR ID
        // ============================================================

        public DataTable BuscarCompra(int idCompra)
        {
            DataTable tabla = new DataTable();

            ConexionBD conexionBD = new ConexionBD();
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                conexionBD.AbrirConexion();

                string consulta = @"
                    SELECT
                        c.id_compra AS ""ID"",
                        c.fecha AS ""Fecha"",
                        p.nombre AS ""Proveedor"",
                        c.total AS ""Total"",
                        c.estado AS ""Estado""
                    FROM compra c
                    INNER JOIN proveedor p
                        ON c.id_proveedor = p.id_proveedor
                    WHERE c.id_compra = @id_compra;
                ";

                using (NpgsqlCommand comando =
                    new NpgsqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@id_compra",
                        idCompra);

                    using (NpgsqlDataAdapter adaptador =
                        new NpgsqlDataAdapter(comando))
                    {
                        adaptador.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // ============================================================
        // MOSTRAR DETALLE DE COMPRA
        // ============================================================

        public DataTable MostrarDetalleCompra(int idCompra)
        {
            DataTable tabla = new DataTable();

            ConexionBD conexionBD = new ConexionBD();
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                conexionBD.AbrirConexion();

                string consulta = @"
                    SELECT
                        dc.id_detalle_compra AS ""ID"",
                        pr.nombre AS ""Producto"",
                        pt.talla AS ""Talla"",
                        dc.cantidad AS ""Cantidad"",
                        dc.precio_compra AS ""Precio Compra"",
                        dc.precio_venta AS ""Precio Venta"",
                        dc.subtotal AS ""Subtotal""
                    FROM detalle_compra dc
                    INNER JOIN producto_talla pt
                        ON dc.id_producto_talla =
                           pt.id_producto_talla
                    INNER JOIN producto pr
                        ON pt.id_producto =
                           pr.id_producto
                    WHERE dc.id_compra = @id_compra
                    ORDER BY dc.id_detalle_compra;
                ";

                using (NpgsqlCommand comando =
                    new NpgsqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue(
                        "@id_compra",
                        idCompra);

                    using (NpgsqlDataAdapter adaptador =
                        new NpgsqlDataAdapter(comando))
                    {
                        adaptador.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // ============================================================
        // ELIMINAR COMPRA
        // ============================================================

        public bool EliminarCompra(int idCompra)
        {
            ConexionBD conexionBD = new ConexionBD();
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                {
                    return false;
                }

                using (NpgsqlTransaction transaccion =
                    conexion.BeginTransaction())
                {
                    try
                    {
                        string consultaDetalle = @"
                            DELETE FROM detalle_compra
                            WHERE id_compra = @id_compra;
                        ";

                        using (NpgsqlCommand comandoDetalle =
                            new NpgsqlCommand(
                                consultaDetalle,
                                conexion,
                                transaccion))
                        {
                            comandoDetalle.Parameters.AddWithValue(
                                "@id_compra",
                                idCompra);

                            comandoDetalle.ExecuteNonQuery();
                        }


                        string consultaCompra = @"
                            DELETE FROM compra
                            WHERE id_compra = @id_compra;
                        ";

                        int filasAfectadas;

                        using (NpgsqlCommand comandoCompra =
                            new NpgsqlCommand(
                                consultaCompra,
                                conexion,
                                transaccion))
                        {
                            comandoCompra.Parameters.AddWithValue(
                                "@id_compra",
                                idCompra);

                            filasAfectadas =
                                comandoCompra.ExecuteNonQuery();
                        }


                        if (filasAfectadas == 0)
                        {
                            transaccion.Rollback();
                            return false;
                        }


                        transaccion.Commit();

                        return true;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }
    }
}
