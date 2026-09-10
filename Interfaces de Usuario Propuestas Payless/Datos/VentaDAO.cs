using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    public class VentaDAO
    {

        private ConexionBD conexionBD = new ConexionBD();

        // =====================================================
        // 1. CARGAR CLIENTES
        // =====================================================
        public DataTable CargarClientes()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();
                string sql = @"
                SELECT id_cliente, nombre
                FROM cliente
                WHERE estado = TRUE
                ORDER BY nombre;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // =====================================================
        // 2. CARGAR PRODUCTOS
        // =====================================================
        public DataTable CargarProductos()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();
                string sql = @"
                SELECT 
                    p.id_producto,
                    p.nombre,
                    p.precio_venta,
                    c.nombre_categoria,
                    m.nombre_marca
                FROM producto p
                INNER JOIN categoria c ON p.id_categoria = c.id_categoria
                INNER JOIN marca m ON p.id_marca = m.id_marca
                WHERE p.estado_producto = TRUE
                ORDER BY p.nombre;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // =====================================================
        // 3. OBTENER INFORMACIÓN DEL PRODUCTO
        // =====================================================
        public DataTable ObtenerProducto(int idProducto)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();
                string sql = @"
                SELECT 
                    p.id_producto,
                    p.nombre,
                    p.precio_venta,
                    p.id_categoria,
                    c.nombre_categoria,
                    p.id_marca,
                    m.nombre_marca
                FROM producto p
                INNER JOIN categoria c ON p.id_categoria = c.id_categoria
                INNER JOIN marca m ON p.id_marca = m.id_marca
                WHERE p.id_producto = @idProducto;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // =====================================================
        // 4. CARGAR TALLAS DEL PRODUCTO
        // =====================================================
        public DataTable CargarTallas(int idProducto)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();
                string sql = @"
                SELECT 
                    pt.id_producto_talla,
                    pt.talla
                FROM producto_talla pt
                INNER JOIN inventario i ON pt.id_producto_talla = i.id_producto_talla
                WHERE pt.id_producto = @idProducto
                  AND i.stock_actual > 0
                ORDER BY pt.talla;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // =====================================================
        // 5. OBTENER STOCK DE PRODUCTO-TALLA
        // =====================================================
        public int ObtenerStockProductoTalla(int idProductoTalla)
        {
            int stock = 0;

            try
            {
                conexionBD.AbrirConexion();
                string sql = @"
                SELECT stock_actual
                FROM inventario
                WHERE id_producto_talla = @idProductoTalla;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idProductoTalla", idProductoTalla);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        stock = Convert.ToInt32(resultado);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return stock;
        }

        // =====================================================
        // 6. OBTENER PRECIO DE VENTA
        // =====================================================
        public decimal? ObtenerPrecioProducto(int idProducto)
        {
            try
            {
                conexionBD.AbrirConexion();
                string sql = @"
                SELECT precio_venta
                FROM producto
                WHERE id_producto = @idProducto;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado == null || resultado == DBNull.Value)
                    {
                        return null;
                    }

                    return Convert.ToDecimal(resultado);
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 7. OBTENER TIPO DE CAMBIO DE LA CAJA ABIERTA
        // =====================================================
        public decimal ObtenerTipoCambioActual()
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT tipo_cambio_dolar
            FROM caja
            WHERE estado_caja = 'Abierta'
            ORDER BY id_caja DESC
            LIMIT 1;";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    object res = cmd.ExecuteScalar();

                    return (res != null && res != DBNull.Value)
                        ? Convert.ToDecimal(res)
                        : 36.50m;
                }
            }
            catch
            {
                return 36.50m;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 8. GENERAR CÓDIGO DE VENTA
        // =====================================================
        public string GenerarCodigoVenta()
        {
            try
            {
                conexionBD.AbrirConexion();
                string sql = @"
                SELECT COALESCE(MAX(id_venta), 0) + 1
                FROM venta;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    int siguiente = Convert.ToInt32(cmd.ExecuteScalar());

                    return "V" + siguiente.ToString("D5");
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 1. OBTENER ID DE LA CAJA ABIERTA
        // =====================================================
        public int ObtenerIdCajaAbierta()
        {
            try
            {
                conexionBD.AbrirConexion();
                string sql = @"
                    SELECT id_caja 
                    FROM caja 
                    WHERE estado_caja = 'Abierta' 
                    ORDER BY id_caja DESC 
                    LIMIT 1;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion()))
                {
                    object res = cmd.ExecuteScalar();
                    return (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // =====================================================
        // 2. REGISTRAR VENTA Y FORMA DE PAGO (TRANSACCIÓN SQL)
        // =====================================================
        public bool RegistrarVentaConPago(
            string codigoVenta,
            int idCliente,
            int idUsuario,
            int idCaja,
            decimal subtotal,
            decimal iva,
            decimal total,
            string tipoPago,
            decimal montoCordobas,
            decimal montoDolares,
            decimal tipoCambio,
            decimal cambio,
            string tipoTarjeta,
            decimal montoTarjeta,
            DataTable detalleVenta)
        {
            try
            {
                conexionBD.AbrirConexion();
                NpgsqlConnection conexion = conexionBD.ObtenerConexion();

                // Transacción para garantizar consistencia atómica
                using (NpgsqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // A. Insertar Venta
                        string sqlVenta = @"
                            INSERT INTO venta 
                            (codigo_venta, fecha, subtotal, descuento, iva, total, estado, id_cliente, id_usuario, id_caja)
                            VALUES 
                            (@codigoVenta, CURRENT_TIMESTAMP, @subtotal, 0, @iva, @total, TRUE, @idCliente, @idUsuario, @idCaja)
                            RETURNING id_venta;";

                        int idVentaGenerado = 0;

                        using (NpgsqlCommand cmdVenta = new NpgsqlCommand(sqlVenta, conexion, transaccion))
                        {
                            cmdVenta.Parameters.AddWithValue("@codigoVenta", codigoVenta);
                            cmdVenta.Parameters.AddWithValue("@subtotal", subtotal);
                            cmdVenta.Parameters.AddWithValue("@iva", iva);
                            cmdVenta.Parameters.AddWithValue("@total", total);
                            cmdVenta.Parameters.AddWithValue("@idCliente", idCliente > 0 ? (object)idCliente : DBNull.Value);
                            cmdVenta.Parameters.AddWithValue("@idUsuario", idUsuario);
                            cmdVenta.Parameters.AddWithValue("@idCaja", idCaja);

                            idVentaGenerado = Convert.ToInt32(cmdVenta.ExecuteScalar());
                        }

                        // B. Insertar Detalle de Venta y actualizar Stock
                        foreach (DataRow fila in detalleVenta.Rows)
                        {
                            int idProductoTalla = Convert.ToInt32(fila["id_producto_talla"]);
                            int cantidad = Convert.ToInt32(fila["cantidad"]);
                            decimal precioUnitario = Convert.ToDecimal(fila["precio_venta"]);
                            decimal subtotalLinea = Convert.ToDecimal(fila["subtotal"]);

                            string sqlDetalle = @"
                                INSERT INTO detalle_venta 
                                (id_venta, id_producto_talla, cantidad, precio_unitario, subtotal)
                                VALUES 
                                (@idVenta, @idProductoTalla, @cantidad, @precio, @subtotal);";

                            using (NpgsqlCommand cmdDetalle = new NpgsqlCommand(sqlDetalle, conexion, transaccion))
                            {
                                cmdDetalle.Parameters.AddWithValue("@idVenta", idVentaGenerado);
                                cmdDetalle.Parameters.AddWithValue("@idProductoTalla", idProductoTalla);
                                cmdDetalle.Parameters.AddWithValue("@cantidad", cantidad);
                                cmdDetalle.Parameters.AddWithValue("@precio", precioUnitario);
                                cmdDetalle.Parameters.AddWithValue("@subtotal", subtotalLinea);
                                cmdDetalle.ExecuteNonQuery();
                            }

                            string sqlStock = @"
                                UPDATE inventario 
                                SET stock_actual = stock_actual - @cantidad
                                WHERE id_producto_talla = @idProductoTalla;";

                            using (NpgsqlCommand cmdStock = new NpgsqlCommand(sqlStock, conexion, transaccion))
                            {
                                cmdStock.Parameters.AddWithValue("@cantidad", cantidad);
                                cmdStock.Parameters.AddWithValue("@idProductoTalla", idProductoTalla);
                                cmdStock.ExecuteNonQuery();
                            }
                        }

                        // C. Insertar Registro de Pago en forma_pago
                        string sqlPago = @"
                            INSERT INTO forma_pago 
                            (tipo_pago, monto_cordobas, monto_dolares, tipo_cambio, cambio, tipo_tarjeta, monto_tarjeta, id_venta)
                            VALUES 
                            (@tipoPago, @montoCordobas, @montoDolares, @tipoCambio, @cambio, @tipoTarjeta, @montoTarjeta, @idVenta);";

                        using (NpgsqlCommand cmdPago = new NpgsqlCommand(sqlPago, conexion, transaccion))
                        {
                            cmdPago.Parameters.AddWithValue("@tipoPago", tipoPago);
                            cmdPago.Parameters.AddWithValue("@montoCordobas", montoCordobas);
                            cmdPago.Parameters.AddWithValue("@montoDolares", montoDolares);
                            cmdPago.Parameters.AddWithValue("@tipoCambio", tipoCambio);
                            cmdPago.Parameters.AddWithValue("@cambio", cambio);
                            cmdPago.Parameters.AddWithValue("@tipoTarjeta", (object)tipoTarjeta ?? DBNull.Value);
                            cmdPago.Parameters.AddWithValue("@montoTarjeta", montoTarjeta);
                            cmdPago.Parameters.AddWithValue("@idVenta", idVentaGenerado);

                            cmdPago.ExecuteNonQuery();
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

        // =====================================================
        // OBTENER DETALLE PARA FACTURA
        // =====================================================
        public DataTable ObtenerDetalleParaFactura(DataTable detalleVenta)
        {
            DataTable tabla = new DataTable();

            tabla.Columns.Add("producto", typeof(string));
            tabla.Columns.Add("talla", typeof(string));
            tabla.Columns.Add("cantidad", typeof(int));
            tabla.Columns.Add("precio_venta", typeof(decimal));
            tabla.Columns.Add("subtotal", typeof(decimal));

            try
            {
                conexionBD.AbrirConexion();

                foreach (DataRow fila in detalleVenta.Rows)
                {
                    int idProductoTalla =
                        Convert.ToInt32(
                            fila["id_producto_talla"]
                        );

                    string sql = @"
                SELECT
                    p.nombre AS producto,
                    pt.talla
                FROM producto_talla pt
                INNER JOIN producto p
                    ON pt.id_producto = p.id_producto
                WHERE pt.id_producto_talla = @idProductoTalla;";

                    using (NpgsqlCommand cmd =
                        new NpgsqlCommand(
                            sql,
                            conexionBD.ObtenerConexion()))
                    {
                        cmd.Parameters.AddWithValue(
                            "@idProductoTalla",
                            idProductoTalla
                        );

                        using (NpgsqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DataRow nuevaFila =
                                    tabla.NewRow();

                                nuevaFila["producto"] =
                                    reader["producto"].ToString();

                                nuevaFila["talla"] =
                                    reader["talla"].ToString();

                                nuevaFila["cantidad"] =
                                    Convert.ToInt32(
                                        fila["cantidad"]
                                    );

                                nuevaFila["precio_venta"] =
                                    Convert.ToDecimal(
                                        fila["precio_venta"]
                                    );

                                nuevaFila["subtotal"] =
                                    Convert.ToDecimal(
                                        fila["subtotal"]
                                    );

                                tabla.Rows.Add(nuevaFila);
                            }
                        }
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }
    }
}
