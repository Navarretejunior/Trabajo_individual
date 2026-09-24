using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Conexion
{
    internal class ConexionBD
    {
        private readonly string cadenaConexion =
            "Host=localhost;" +
            "Port=5432;" +
            "Database=PAYLESS BD;" +
            "Username=postgres;" +
            "Password=navarretejunior98";

        private NpgsqlConnection conexion;

        public ConexionBD()
        {
            conexion = new NpgsqlConnection(cadenaConexion);
        }

        // Obtener la conexión
        public NpgsqlConnection ObtenerConexion()
        {
            return conexion;
        }

        // Abrir conexión
        public bool AbrirConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error al conectar: " + ex.Message
                );

                return false;
            }
        }

        // Cerrar conexión
        public void CerrarConexion()
        {
            if (conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
        }

        // Datos necesarios para los respaldos

        public string ObtenerHost()
        {
            return "localhost";
        }

        public int ObtenerPuerto()
        {
            return 5432;
        }

        public string ObtenerBaseDatos()
        {
            return "PAYLESS BD";
        }

        public string ObtenerUsuario()
        {
            return "postgres";
        }

        public string ObtenerPassword()
        {
            return "navarretejunior98";
        }
    }
}
