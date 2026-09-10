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
    internal class MarcaDAO
    {
        ConexionBD conexionBD = new ConexionBD();

        // =========================================================
        // MOSTRAR TODAS LAS MARCAS
        // =========================================================
        public DataTable MostrarMarcas()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_marca,
                        nombre_marca,
                        descripcion,
                        estado
                    FROM marca
                    ORDER BY nombre_marca";

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
        // CARGAR MARCAS PARA EL COMBOBOX DE EDITAR
        // =========================================================
        public DataTable CargarMarcas()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_marca,
                        nombre_marca
                    FROM marca
                    WHERE estado = TRUE
                    ORDER BY nombre_marca";

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
        // AGREGAR MARCA
        // =========================================================
        public bool AgregarMarca(ClaseMarca marca)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    INSERT INTO marca
                    (
                        nombre_marca,
                        descripcion,
                        estado
                    )
                    VALUES
                    (
                        @nombre_marca,
                        @descripcion,
                        @estado
                    )";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre_marca",
                    marca.NombreMarca);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    marca.Descripcion);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    marca.Estado);

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
        // OBTENER UNA MARCA
        // =========================================================
        public ClaseMarca ObtenerMarca(int idMarca)
        {
            ClaseMarca marca = null;

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_marca,
                        nombre_marca,
                        descripcion,
                        estado
                    FROM marca
                    WHERE id_marca = @id_marca";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_marca",
                    idMarca);

                NpgsqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    marca = new ClaseMarca();

                    marca.IdMarca =
                        Convert.ToInt32(
                            reader["id_marca"]);

                    marca.NombreMarca =
                        reader["nombre_marca"].ToString();

                    marca.Descripcion =
                        reader["descripcion"] == DBNull.Value
                        ? ""
                        : reader["descripcion"].ToString();

                    marca.Estado =
                        Convert.ToBoolean(
                            reader["estado"]);
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

            return marca;
        }


        // =========================================================
        // EDITAR MARCA
        // =========================================================
        public bool EditarMarca(ClaseMarca marca)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE marca
                    SET
                        nombre_marca = @nombre_marca,
                        descripcion = @descripcion,
                        estado = @estado
                    WHERE id_marca = @id_marca";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre_marca",
                    marca.NombreMarca);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    marca.Descripcion);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    marca.Estado);

                cmd.Parameters.AddWithValue(
                    "@id_marca",
                    marca.IdMarca);

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
        // ELIMINAR / DESACTIVAR MARCA
        // =========================================================
        public bool EliminarMarca(int idMarca)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE marca
                    SET estado = FALSE
                    WHERE id_marca = @id_marca";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_marca",
                    idMarca);

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
                        id_marca,
                        nombre_marca,
                        descripcion,
                        estado
                    FROM marca
                    WHERE estado = @estado
                    ORDER BY nombre_marca";

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
        // CARGAR TODAS LAS MARCAS PARA EDITAR
        // INCLUYE ACTIVAS E INACTIVAS
        // =========================================================
        public DataTable CargarTodasLasMarcas()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                id_marca,
                nombre_marca
            FROM marca
            ORDER BY nombre_marca";

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
