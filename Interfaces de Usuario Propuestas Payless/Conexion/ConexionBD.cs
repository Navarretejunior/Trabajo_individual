using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Conexion
{
    internal class ConexionBD
    {

        private readonly string cadenaConexion =
           "Host=localhost;Port=5432;Database=Payless individual;Username=postgres;Password=navarretejunior89";

        private NpgsqlConnection conexion;

        public ConexionBD()
        {
            conexion = new NpgsqlConnection(cadenaConexion);
        }

        public NpgsqlConnection ObtenerConexion()
        {
            return conexion;
        }

        public bool AbrirConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                    conexion.Open();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CerrarConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
                conexion.Close();
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
            return "Payless individual";
        }

        public string ObtenerUsuario()
        {
            return "postgres";
        }

        public string ObtenerPassword()
        {
            return "navarretejunior89";
        }


    }
}
