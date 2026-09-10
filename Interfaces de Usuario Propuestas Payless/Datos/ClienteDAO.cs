using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    internal class ClienteDAO
    {
        ConexionBD conexionBD = new ConexionBD();

        // =========================================================
        // MOSTRAR TODOS LOS CLIENTES
        // =========================================================

        public DataTable MostrarClientes()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_cliente,
                        nombre,
                        telefono,
                        cedula,
                        direccion,
                        estado
                    FROM cliente
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
        // AGREGAR CLIENTE
        // =========================================================

        public bool AgregarCliente(ClaseCliente cliente)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    INSERT INTO cliente
                    (
                        nombre,
                        cedula,
                        telefono,
                        direccion,
                        estado
                    )
                    VALUES
                    (
                        @nombre,
                        @cedula,
                        @telefono,
                        @direccion,
                        @estado
                    )";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    cliente.Nombre);

                cmd.Parameters.AddWithValue(
                    "@cedula",
                    cliente.Cedula);

                cmd.Parameters.AddWithValue(
                    "@telefono",
                    cliente.Telefono);

                cmd.Parameters.AddWithValue(
                    "@direccion",
                    cliente.Direccion);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    cliente.Estado);

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
        // EDITAR CLIENTE
        // =========================================================

        public bool EditarCliente(ClaseCliente cliente)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE cliente
                    SET
                        nombre = @nombre,
                        cedula = @cedula,
                        telefono = @telefono,
                        direccion = @direccion,
                        estado = @estado
                    WHERE id_cliente = @id";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    cliente.Nombre);

                cmd.Parameters.AddWithValue(
                    "@cedula",
                    cliente.Cedula);

                cmd.Parameters.AddWithValue(
                    "@telefono",
                    cliente.Telefono);

                cmd.Parameters.AddWithValue(
                    "@direccion",
                    cliente.Direccion);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    cliente.Estado);

                cmd.Parameters.AddWithValue(
                    "@id",
                    cliente.IdCliente);

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
        // ELIMINAR CLIENTE
        // =========================================================

        public bool EliminarCliente(int idCliente)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE cliente
                    SET estado = FALSE
                    WHERE id_cliente = @id";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id",
                    idCliente);

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
        // BUSCAR POR NOMBRE
        // =========================================================

        public DataTable BuscarPorNombre(string nombre)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_cliente,
                        nombre,
                        telefono,
                        cedula,
                        direccion,
                        estado
                    FROM cliente
                    WHERE nombre ILIKE @nombre
                    ORDER BY nombre";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    "%" + nombre + "%");

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
        // BUSCAR POR CÉDULA
        // =========================================================

        public DataTable BuscarPorCedula(string cedula)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_cliente,
                        nombre,
                        telefono,
                        cedula,
                        direccion,
                        estado
                    FROM cliente
                    WHERE cedula ILIKE @cedula
                    ORDER BY nombre";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@cedula",
                    "%" + cedula + "%");

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
        // BUSCAR POR TELÉFONO
        // =========================================================

        public DataTable BuscarPorTelefono(string telefono)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_cliente,
                        nombre,
                        telefono,
                        cedula,
                        direccion,
                        estado
                    FROM cliente
                    WHERE telefono ILIKE @telefono
                    ORDER BY nombre";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@telefono",
                    "%" + telefono + "%");

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
        // BUSCAR POR ESTADO
        // =========================================================

        public DataTable BuscarClientes(bool estado)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_cliente,
                        nombre,
                        telefono,
                        cedula,
                        direccion,
                        estado
                    FROM cliente
                    WHERE estado = @estado
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
        // OBTENER CLIENTE PARA EDITAR
        // =========================================================

        public ClaseCliente ObtenerCliente(int idCliente)
        {
            ClaseCliente cliente = new ClaseCliente();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_cliente,
                        nombre,
                        telefono,
                        cedula,
                        direccion,
                        estado
                    FROM cliente
                    WHERE id_cliente = @id";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id",
                    idCliente);

                NpgsqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    cliente.IdCliente =
                        Convert.ToInt32(
                            reader["id_cliente"]);

                    cliente.Nombre =
                        reader["nombre"].ToString();

                    cliente.Cedula =
                        reader["cedula"].ToString();

                    cliente.Telefono =
                        reader["telefono"].ToString();

                    cliente.Direccion =
                        reader["direccion"].ToString();

                    cliente.Estado =
                        Convert.ToBoolean(
                            reader["estado"]);
                }
            }
            catch
            {
                return cliente;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return cliente;
        }

    }
}
