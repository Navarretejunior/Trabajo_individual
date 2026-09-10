using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Conexion
{
    internal class RecuperacionDAO
    {

        ConexionBD conexionBD = new ConexionBD();

        public bool ExisteCorreo(string correo)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"SELECT * FROM usuario
                               WHERE correo = @correo";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@correo", correo);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                return reader.Read();
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

        public string GenerarCodigo()
        {
            Random r = new Random();
            return r.Next(100000, 999999).ToString();
        }

        public bool GuardarCodigo(string correo, string codigo)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"UPDATE usuario
                       SET codigo_recuperacion=@codigo,
                           vence_codigo=@fecha
                       WHERE correo=@correo";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now.AddMinutes(5));
                cmd.Parameters.AddWithValue("@correo", correo);

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

        public bool ValidarCodigo(string correo, string codigo)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"SELECT *
                       FROM usuario
                       WHERE correo=@correo
                       AND codigo_recuperacion=@codigo
                       AND vence_codigo > NOW()";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                return reader.Read();
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

        public bool CambiarPassword(string correo, string password)
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"UPDATE usuario
                       SET password=@password,
                           codigo_recuperacion=NULL,
                           vence_codigo=NULL
                       WHERE correo=@correo";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionBD.ObtenerConexion());

                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@correo", correo);

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




    }
}
