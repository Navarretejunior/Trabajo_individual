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
    internal class CategoriaDAO
    {
        ConexionBD conexionBD = new ConexionBD();

        // =========================================================
        // MOSTRAR CATEGORÍAS
        // =========================================================

        public DataTable MostrarCategorias()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_categoria,
                        nombre_categoria,
                        descripcion,
                        estado
                    FROM categoria
                    ORDER BY nombre_categoria";

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
        // AGREGAR CATEGORÍA
        // =========================================================

        public bool AgregarCategoria(ClaseCategoria categoria)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    INSERT INTO categoria
                    (
                        nombre_categoria,
                        descripcion,
                        estado
                    )
                    VALUES
                    (
                        @nombre,
                        @descripcion,
                        @estado
                    )";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    categoria.NombreCategoria);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    categoria.Descripcion);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    categoria.Estado);

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
        // OBTENER CATEGORÍA POR ID
        // =========================================================

        public ClaseCategoria ObtenerCategoria(int idCategoria)
        {
            ClaseCategoria categoria = new ClaseCategoria();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_categoria,
                        nombre_categoria,
                        descripcion,
                        estado
                    FROM categoria
                    WHERE id_categoria = @id_categoria";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_categoria",
                    idCategoria);

                NpgsqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    categoria.IdCategoria =
                        Convert.ToInt32(
                            reader["id_categoria"]);

                    categoria.NombreCategoria =
                        reader["nombre_categoria"].ToString();

                    categoria.Descripcion =
                        reader["descripcion"].ToString();

                    categoria.Estado =
                        Convert.ToBoolean(
                            reader["estado"]);
                }
            }
            catch
            {
                return categoria;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return categoria;
        }

        // =========================================================
        // EDITAR CATEGORÍA
        // =========================================================

        public bool EditarCategoria(ClaseCategoria categoria)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE categoria
                    SET
                        nombre_categoria = @nombre,
                        descripcion = @descripcion,
                        estado = @estado
                    WHERE id_categoria = @id_categoria";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre",
                    categoria.NombreCategoria);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    categoria.Descripcion);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    categoria.Estado);

                cmd.Parameters.AddWithValue(
                    "@id_categoria",
                    categoria.IdCategoria);

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
        // ELIMINAR CATEGORÍA
        // =========================================================

        public bool EliminarCategoria(int idCategoria)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE categoria
                    SET estado = FALSE
                    WHERE id_categoria = @id_categoria";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_categoria",
                    idCategoria);

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
                        id_categoria,
                        nombre_categoria,
                        descripcion,
                        estado
                    FROM categoria
                    WHERE nombre_categoria ILIKE @nombre
                    ORDER BY nombre_categoria";

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
        // BUSCAR POR DESCRIPCIÓN
        // =========================================================

        public DataTable BuscarPorDescripcion(string descripcion)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_categoria,
                        nombre_categoria,
                        descripcion,
                        estado
                    FROM categoria
                    WHERE descripcion ILIKE @descripcion
                    ORDER BY nombre_categoria";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    "%" + descripcion + "%");

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

        public DataTable BuscarPorEstado(bool estado)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_categoria,
                        nombre_categoria,
                        descripcion,
                        estado
                    FROM categoria
                    WHERE estado = @estado
                    ORDER BY nombre_categoria";

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
        // CARGAR CATEGORÍAS ACTIVAS PARA COMBOBOX
        // =========================================================

        public DataTable CargarCategorias()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
            SELECT
                id_categoria,
                nombre_categoria
            FROM categoria
            ORDER BY nombre_categoria";

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
