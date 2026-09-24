using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Conexion
{
    internal class RecuperacionDAO
    {
        private ConexionBD conexionBD = new ConexionBD();

    

        public bool ExisteCorreo(string correo)
        {
            try
            {
                correo = correo.Trim();

                if (!conexionBD.AbrirConexion())
                {
                    return false;
                }

                string sql = @"
                    SELECT 1
                    FROM usuario
                    WHERE correo = @correo
                    LIMIT 1;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@correo",
                        NpgsqlDbType.Varchar,
                        correo);

                    using (NpgsqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        return reader.Read();
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError("ExisteCorreo", ex);
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        public string GenerarCodigo()
        {
            Random r = new Random();

            return r.Next(100000, 1000000).ToString();
        }

 
        public bool GuardarCodigo(
            string correo,
            string codigo)
        {
            try
            {
                correo = correo.Trim();
                codigo = codigo.Trim();

                if (!conexionBD.AbrirConexion())
                {
                    return false;
                }

                string sql = @"
                    UPDATE usuario
                    SET codigo_recuperacion = @codigo,
                        vence_codigo = CURRENT_TIMESTAMP + INTERVAL '5 minutes'
                    WHERE correo = @correo;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@codigo",
                        NpgsqlDbType.Varchar,
                        codigo);

                    cmd.Parameters.AddWithValue(
                        "@correo",
                        NpgsqlDbType.Varchar,
                        correo);

                    int filasAfectadas =
                        cmd.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                MostrarError("GuardarCodigo", ex);
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

 

        public bool ValidarCodigo(
            string correo,
            string codigo)
        {
            try
            {
                correo = correo.Trim();
                codigo = codigo.Trim();

                if (!conexionBD.AbrirConexion())
                {
                    return false;
                }

                string sql = @"
                    SELECT 1
                    FROM usuario
                    WHERE correo = @correo
                      AND codigo_recuperacion = @codigo
                      AND vence_codigo > CURRENT_TIMESTAMP
                    LIMIT 1;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@correo",
                        NpgsqlDbType.Varchar,
                        correo);

                    cmd.Parameters.AddWithValue(
                        "@codigo",
                        NpgsqlDbType.Varchar,
                        codigo);

                    object resultado =
                        cmd.ExecuteScalar();

                    return resultado != null;
                }
            }
            catch (Exception ex)
            {
                MostrarError("ValidarCodigo", ex);
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        public bool CambiarPassword(
            string correo,
            string password)
        {
            try
            {
                correo = correo.Trim();

                if (!conexionBD.AbrirConexion())
                {
                    return false;
                }

                string sql = @"
                    UPDATE usuario
                    SET password = @password,
                        codigo_recuperacion = NULL,
                        vence_codigo = NULL
                    WHERE correo = @correo;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@password",
                        NpgsqlDbType.Varchar,
                        password);

                    cmd.Parameters.AddWithValue(
                        "@correo",
                        NpgsqlDbType.Varchar,
                        correo);

                    int filasAfectadas =
                        cmd.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                MostrarError("CambiarPassword", ex);
                return false;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }



        private void MostrarError(
            string metodo,
            Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(
                "Error en " + metodo + ":\n\n" +
                ex.Message,
                "Error de recuperación",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Error
            );
        }
    }
}