using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Npgsql;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    internal class UsuarioDAO
    {

        ConexionBD conexionBD = new ConexionBD();

        public bool IniciarSesion(string usuario, string password)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"SELECT u.id_usuario,
                                      u.nombre_usuario,
                                      u.nombre_completo,
                                      r.nombre_rol
                                FROM usuario u
                                INNER JOIN rol r
                                    ON u.id_rol = r.id_rol
                                WHERE u.nombre_usuario = @usuario
                                    AND u.password = @password
                                    AND u.estado = TRUE";


                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@password", password);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ClaseSesion.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                    ClaseSesion.UsuarioActual = reader["nombre_usuario"].ToString();
                    ClaseSesion.RolActual = reader["nombre_rol"].ToString();

                    return true;
                }
                return false;
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
        // MOSTRAR TODOS LOS USUARIOS
        // =========================================================

        public DataTable MostrarUsuarios()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        u.id_usuario,
                        u.nombre_usuario,
                        u.nombre_completo,
                        r.nombre_rol,
                        u.estado
                    FROM usuario u
                    INNER JOIN rol r
                        ON u.id_rol = r.id_rol
                    ORDER BY u.nombre_usuario";

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
        // CARGAR ROLES PARA COMBOBOX
        // =========================================================

        public DataTable CargarRoles()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        id_rol,
                        nombre_rol
                    FROM rol
                    WHERE estado = TRUE
                    ORDER BY nombre_rol";

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
        // AGREGAR USUARIO
        // =========================================================

        public bool AgregarUsuario(
            string nombreUsuario,
            string nombreCompleto,
            string password,
            int idRol,
            bool estado)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    INSERT INTO usuario
                    (
                        nombre_usuario,
                        nombre_completo,
                        password,
                        id_rol,
                        estado
                    )
                    VALUES
                    (
                        @nombre_usuario,
                        @nombre_completo,
                        @password,
                        @id_rol,
                        @estado
                    )";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre_usuario",
                    nombreUsuario);

                cmd.Parameters.AddWithValue(
                    "@nombre_completo",
                    nombreCompleto);

                cmd.Parameters.AddWithValue(
                    "@password",
                    password);

                cmd.Parameters.AddWithValue(
                    "@id_rol",
                    idRol);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    estado);

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
        // EDITAR USUARIO
        // =========================================================

        public bool EditarUsuario(
            int idUsuario,
            string nombreUsuario,
            string nombreCompleto,
            string password,
            int idRol,
            bool estado)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE usuario
                    SET
                        nombre_usuario = @nombre_usuario,
                        nombre_completo = @nombre_completo,
                        password = @password,
                        id_rol = @id_rol,
                        estado = @estado
                    WHERE id_usuario = @id_usuario";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@nombre_usuario",
                    nombreUsuario);

                cmd.Parameters.AddWithValue(
                    "@nombre_completo",
                    nombreCompleto);

                cmd.Parameters.AddWithValue(
                    "@password",
                    password);

                cmd.Parameters.AddWithValue(
                    "@id_rol",
                    idRol);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    estado);

                cmd.Parameters.AddWithValue(
                    "@id_usuario",
                    idUsuario);

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
        // ELIMINAR USUARIO
        // =========================================================

        public bool EliminarUsuario(int idUsuario)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    UPDATE usuario
                    SET estado = FALSE
                    WHERE id_usuario = @id_usuario";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_usuario",
                    idUsuario);

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
        // BUSCAR USUARIO
        // =========================================================

        public DataTable BuscarUsuarios(
            string campo,
            string valor)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = "";

                switch (campo)
                {
                    case "nombre_usuario":

                        sql = @"
                            SELECT
                                u.id_usuario,
                                u.nombre_usuario,
                                u.nombre_completo,
                                r.nombre_rol,
                                u.estado
                            FROM usuario u
                            INNER JOIN rol r
                                ON u.id_rol = r.id_rol
                            WHERE u.nombre_usuario ILIKE @valor
                            ORDER BY u.nombre_usuario";

                        break;


                    case "nombre_completo":

                        sql = @"
                            SELECT
                                u.id_usuario,
                                u.nombre_usuario,
                                u.nombre_completo,
                                r.nombre_rol,
                                u.estado
                            FROM usuario u
                            INNER JOIN rol r
                                ON u.id_rol = r.id_rol
                            WHERE u.nombre_completo ILIKE @valor
                            ORDER BY u.nombre_usuario";

                        break;


                    case "nombre_rol":

                        sql = @"
                            SELECT
                                u.id_usuario,
                                u.nombre_usuario,
                                u.nombre_completo,
                                r.nombre_rol,
                                u.estado
                            FROM usuario u
                            INNER JOIN rol r
                                ON u.id_rol = r.id_rol
                            WHERE r.nombre_rol ILIKE @valor
                            ORDER BY u.nombre_usuario";

                        break;
                }

                if (string.IsNullOrEmpty(sql))
                    return tabla;

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
        // OBTENER USUARIO PARA EDITAR
        // =========================================================

        public DataTable ObtenerUsuario(int idUsuario)
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        u.id_usuario,
                        u.nombre_usuario,
                        u.nombre_completo,
                        u.password,
                        u.id_rol,
                        u.estado
                    FROM usuario u
                    WHERE u.id_usuario = @id_usuario";

                NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue(
                    "@id_usuario",
                    idUsuario);

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
    }
}