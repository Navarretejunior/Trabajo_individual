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
    internal class ProveedorDAO
    {


        private ConexionBD conexionBD = new ConexionBD();


        // =========================================================
        // MOSTRAR TODOS LOS PROVEEDORES
        // =========================================================

        public DataTable MostrarProveedores()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                     SELECT
                    id_proveedor,
                     nombre,
        direccion,
        estado_proveedor,
        ruc
    FROM proveedor
    ORDER BY nombre";

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion());

                da.Fill(tabla);
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // CARGAR PROVEEDORES ACTIVOS
        // =========================================================
        // Este método sirve, por ejemplo, para cargar proveedores
        // activos en el ComboBox de Agregar Producto.
        // =========================================================

        public DataTable CargarProveedores()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_proveedor,
                        nombre
                    FROM proveedor
                    WHERE estado_proveedor = TRUE
                    ORDER BY nombre";

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion());

                da.Fill(tabla);
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // AGREGAR PROVEEDOR
        // =========================================================

        public bool AgregarProveedor(ClaseProveedor proveedor)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    INSERT INTO proveedor
                    (
                        nombre,
                        telefono,
                        correo,
                        direccion,
                        estado_proveedor,
                        ruc,
                        fecha_registro
                    )
                    VALUES
                    (
                        @nombre,
                        @telefono,
                        @correo,
                        @direccion,
                        @estado_proveedor,
                        @ruc,
                        CURRENT_TIMESTAMP
                    )";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    proveedor.Nombre);

                cmd.Parameters.AddWithValue(
                    "@telefono",
                    proveedor.Telefono);

                cmd.Parameters.AddWithValue(
                    "@correo",
                    proveedor.Correo);

                cmd.Parameters.AddWithValue(
                    "@direccion",
                    proveedor.Direccion);

                cmd.Parameters.AddWithValue(
                    "@estado_proveedor",
                    proveedor.EstadoProveedor);

                cmd.Parameters.AddWithValue(
                    "@ruc",
                    proveedor.Ruc);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Error al agregar proveedor:\n\n" + ex.Message,
                    "Error PostgreSQL",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);

                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // =========================================================
        // EDITAR PROVEEDOR
        // =========================================================

        public bool EditarProveedor(ClaseProveedor proveedor)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            UPDATE proveedor
            SET
                nombre = @nombre,
                telefono = @telefono,
                correo = @correo,
                direccion = @direccion,
                estado_proveedor = @estado_proveedor,
                ruc = @ruc
            WHERE id_proveedor = @id_proveedor";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    proveedor.Nombre);

                cmd.Parameters.AddWithValue(
                    "@telefono",
                    proveedor.Telefono);

                cmd.Parameters.AddWithValue(
                    "@correo",
                    proveedor.Correo);

                cmd.Parameters.AddWithValue(
                    "@direccion",
                    proveedor.Direccion);

                cmd.Parameters.AddWithValue(
                    "@estado_proveedor",
                    proveedor.EstadoProveedor);

                cmd.Parameters.AddWithValue(
                    "@ruc",
                    proveedor.Ruc);

                cmd.Parameters.AddWithValue(
                    "@id_proveedor",
                    proveedor.IdProveedor);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // =========================================================
        // ELIMINAR PROVEEDOR
        // =========================================================
        // Eliminación lógica.
        // No borra físicamente el registro de la BD.
        // =========================================================

        public bool EliminarProveedor(int idProveedor)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE proveedor
                    SET estado_proveedor = FALSE
                    WHERE id_proveedor = @id_proveedor";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_proveedor",
                    idProveedor);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // =========================================================
        // BUSCAR PROVEEDORES
        // =========================================================

        public DataTable BuscarProveedores(
            string campo,
            string valor)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                // Solo permitimos campos conocidos.
                // Esto evita enviar cualquier nombre de columna
                // directamente desde el ComboBox.
                string columna;

                switch (campo)
                {
                    case "nombre":
                        columna = "nombre";
                        break;

                    case "telefono":
                        columna = "telefono";
                        break;

                    case "correo":
                        columna = "correo";
                        break;

                    case "direccion":
                        columna = "direccion";
                        break;

                    case "ruc":
                        columna = "ruc";
                        break;

                    default:
                        columna = "nombre";
                        break;
                }

                string sql = $@"
                    SELECT
                        id_proveedor,
                        nombre,
                    
                        direccion,
                        estado_proveedor,
                        ruc
                    FROM proveedor
                    WHERE CAST({columna} AS TEXT) ILIKE @valor
                    ORDER BY nombre";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@valor",
                    "%" + valor + "%");

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // =========================================================
        // OBTENER PROVEEDOR
        // =========================================================
        // Se utiliza principalmente para la pantalla EditarProveedor.
        // =========================================================

        public ClaseProveedor ObtenerProveedor(int idProveedor)
        {
            ClaseProveedor proveedor = null;

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                id_proveedor,
                nombre,
                telefono,
                correo,
                direccion,
                estado_proveedor,
                ruc,
                fecha_registro
            FROM proveedor
            WHERE id_proveedor = @id_proveedor";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_proveedor",
                    idProveedor);

                NpgsqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    proveedor = new ClaseProveedor();

                    proveedor.IdProveedor =
                        Convert.ToInt32(reader["id_proveedor"]);

                    proveedor.Nombre =
                        reader["nombre"].ToString();

                    proveedor.Telefono =
                        reader["telefono"] == DBNull.Value
                        ? ""
                        : reader["telefono"].ToString();

                    proveedor.Correo =
                        reader["correo"] == DBNull.Value
                        ? ""
                        : reader["correo"].ToString();

                    proveedor.Direccion =
                        reader["direccion"] == DBNull.Value
                        ? ""
                        : reader["direccion"].ToString();

                    proveedor.EstadoProveedor =
                        Convert.ToBoolean(
                            reader["estado_proveedor"]);

                    proveedor.Ruc =
                        reader["ruc"] == DBNull.Value
                        ? ""
                        : reader["ruc"].ToString();

                    proveedor.FechaRegistro =
                        Convert.ToDateTime(
                            reader["fecha_registro"]);
                }

                reader.Close();
            }
            catch
            {
                return null;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return proveedor;

        }


        // =========================================================
        // BUSCAR POR ESTADO
        // =========================================================

        public DataTable BuscarPorEstado(bool estado)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_proveedor,
                        nombre,
                        telefono,
                        correo,
                        direccion,
                        estado_proveedor,
                        ruc
                    FROM proveedor
                    WHERE estado_proveedor = @estado
                    ORDER BY nombre";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@estado",
                    estado);

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(cmd);

                da.Fill(tabla);
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }

        // =========================================================
        // CARGAR TODOS LOS PROVEEDORES PARA EDITAR
        // Incluye activos e inactivos
        // =========================================================
        public DataTable CargarProveedoresEditar()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                id_proveedor,
                nombre
            FROM proveedor
            ORDER BY nombre";

                NpgsqlDataAdapter da =
                    new NpgsqlDataAdapter(
                        sql,
                        conexionBD.ObtenerConexion());

                da.Fill(tabla);
            }
            catch
            {
                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


       
        
    }
    
}
