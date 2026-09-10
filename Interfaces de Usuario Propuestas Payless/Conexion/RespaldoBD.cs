using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Conexion
{
    internal class RespaldoBD
    {

        private ConexionBD conexionBD;
        private string rutaCarpetaRespaldos;

        private string rutaPgDump =
            @"C:\Program Files\PostgreSQL\16\bin\pg_dump.exe";

        private string rutaPsql =
            @"C:\Program Files\PostgreSQL\16\bin\psql.exe";

        public RespaldoBD()
        {
            conexionBD = new ConexionBD();

            // Carpeta Respaldos dentro del directorio
            // donde se ejecuta el programa
            rutaCarpetaRespaldos =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Respaldos");

            // Crear carpeta automáticamente
            if (!Directory.Exists(rutaCarpetaRespaldos))
            {
                Directory.CreateDirectory(rutaCarpetaRespaldos);
            }
        }

        // =====================================================
        // CREAR RESPALDO
        // =====================================================

        public bool CrearRespaldo(
    string nombrePersonalizado,
    out string rutaArchivoFinal)
        {
            rutaArchivoFinal = string.Empty;

            try
            {
                if (!File.Exists(rutaPgDump))
                {
                    return false;
                }

                if (!Directory.Exists(rutaCarpetaRespaldos))
                {
                    Directory.CreateDirectory(rutaCarpetaRespaldos);
                }

                string nombreArchivo =
                    $"{nombrePersonalizado}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.sql";

                rutaArchivoFinal =
                    Path.Combine(
                        rutaCarpetaRespaldos,
                        nombreArchivo);

                string host = conexionBD.ObtenerHost();
                int puerto = conexionBD.ObtenerPuerto();
                string baseDatos = conexionBD.ObtenerBaseDatos();
                string usuario = conexionBD.ObtenerUsuario();
                string password = conexionBD.ObtenerPassword();

                ProcessStartInfo proceso =
                    new ProcessStartInfo();

                proceso.FileName = rutaPgDump;

                proceso.Arguments =
                    $"-h \"{host}\" " +
                    $"-p {puerto} " +
                    $"-U \"{usuario}\" " +
                    $"-F p " +
                    $"-f \"{rutaArchivoFinal}\" " +
                    $"\"{baseDatos}\"";

                proceso.UseShellExecute = false;
                proceso.CreateNoWindow = true;
                proceso.RedirectStandardError = true;

                // Contraseña real de ConexionBD
                proceso.EnvironmentVariables["PGPASSWORD"] =
                    password;

                using (Process procesoPgDump =
                    new Process())
                {
                    procesoPgDump.StartInfo = proceso;

                    procesoPgDump.Start();

                    string error =
                        procesoPgDump.StandardError.ReadToEnd();

                    procesoPgDump.WaitForExit();

                    if (procesoPgDump.ExitCode != 0)
                    {
                        if (File.Exists(rutaArchivoFinal))
                        {
                            File.Delete(rutaArchivoFinal);
                        }

                        rutaArchivoFinal = string.Empty;

                        return false;
                    }
                }

                return File.Exists(rutaArchivoFinal);
            }
            catch
            {
                rutaArchivoFinal = string.Empty;
                return false;
            }
            finally
            {
                try
                {
                    conexionBD.CerrarConexion();
                }
                catch
                {
                }
            }
        }

        // =====================================================
        // OBTENER CONTRASEÑA DE POSTGRESQL
        // =====================================================

        private string ObtenerPasswordConexion()
        {
            try
            {
                conexionBD.AbrirConexion();

                NpgsqlConnection conexion =
                    conexionBD.ObtenerConexion();

                // Npgsql no permite recuperar la contraseña
                // después de crear la conexión.
                // Por eso se obtiene desde la cadena
                // de conexión de la clase ConexionBD.

                conexionBD.CerrarConexion();

                return ObtenerPasswordDesdeCadena();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                try
                {
                    conexionBD.CerrarConexion();
                }
                catch
                {
                }
            }
        }

        private string ObtenerPasswordDesdeCadena()
        {
            // IMPORTANTE:
            // Aquí debes colocar la misma contraseña que
            // utilizas en tu ConexionBD.
            return "LeonelF_241207";
        }

        // =====================================================
        // MOSTRAR RESPALDOS
        // =====================================================

        public DataTable MostrarRespaldos()
        {
            DataTable tabla = new DataTable();

            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Ruta");
            tabla.Columns.Add("Fecha");
            tabla.Columns.Add("Tamaño");

            try
            {
                if (!Directory.Exists(rutaCarpetaRespaldos))
                {
                    Directory.CreateDirectory(
                        rutaCarpetaRespaldos);
                }

                string[] archivos =
                    Directory.GetFiles(
                        rutaCarpetaRespaldos,
                        "*.sql");

                foreach (string archivo in archivos)
                {
                    FileInfo informacion =
                        new FileInfo(archivo);

                    DataRow fila =
                        tabla.NewRow();

                    fila["Nombre"] =
                        informacion.Name;

                    fila["Ruta"] =
                        informacion.FullName;

                    fila["Fecha"] =
                        informacion.LastWriteTime;

                    // REEMPLAZA LA LÍNEA: fila["Tamaño"] = informacion.Length; POR ESTAS DOS:
                    double mb = (double)informacion.Length / 1048576; // Convierte bytes a MB
                    fila["Tamaño"] = $"{mb:F1} MB"; // Guarda el texto formateado (Ej: "25.8 MB")


                    tabla.Rows.Add(fila);
                }
            }
            catch
            {
            }

            return tabla;
        }

        // =====================================================
        // ELIMINAR RESPALDO
        // =====================================================

        public bool EliminarRespaldo(
            string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    return false;
                }

                File.Delete(rutaArchivo);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // =====================================================
        // RESTAURAR RESPALDO
        // =====================================================
            public bool RestaurarRespaldo(string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    MessageBox.Show(
                        "El archivo de respaldo no existe.",
                        "Restauración",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }

                if (!File.Exists(rutaPsql))
                {
                    MessageBox.Show(
                        "No se encontró psql.exe en:\n" + rutaPsql,
                        "Error PostgreSQL",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return false;
                }

                // =====================================================
                // OBTENER DATOS DE CONEXIÓN
                // =====================================================

                string host = conexionBD.ObtenerHost();
                int puerto = conexionBD.ObtenerPuerto();
                string baseDatos = conexionBD.ObtenerBaseDatos();
                string usuario = conexionBD.ObtenerUsuario();
                string password = conexionBD.ObtenerPassword();

                // =====================================================
                // CERRAR CONEXIÓN ACTUAL
                // =====================================================

                conexionBD.CerrarConexion();

                // =====================================================
                // ELIMINAR EL ESQUEMA ACTUAL
                // =====================================================

                string cadenaConexion =
                    $"Host={host};" +
                    $"Port={puerto};" +
                    $"Database={baseDatos};" +
                    $"Username={usuario};" +
                    $"Password={password};";

                using (NpgsqlConnection conexionRestauracion =
                    new NpgsqlConnection(cadenaConexion))
                {
                    conexionRestauracion.Open();

                    string sqlLimpiar = @"
                DROP SCHEMA public CASCADE;
                CREATE SCHEMA public;
            ";

                    using (NpgsqlCommand comando =
                        new NpgsqlCommand(
                            sqlLimpiar,
                            conexionRestauracion))
                    {
                        comando.ExecuteNonQuery();
                    }

                    conexionRestauracion.Close();
                }

                // =====================================================
                // EJECUTAR PSQl
                // =====================================================

                ProcessStartInfo proceso =
                    new ProcessStartInfo();

                proceso.FileName = rutaPsql;

                proceso.Arguments =
                    $"--host=\"{host}\" " +
                    $"--port={puerto} " +
                    $"--username=\"{usuario}\" " +
                    $"--dbname=\"{baseDatos}\" " +
                    $"-v ON_ERROR_STOP=1 " +
                    $"--file=\"{rutaArchivo}\"";

                proceso.UseShellExecute = false;
                proceso.CreateNoWindow = true;

                proceso.RedirectStandardError = true;
                proceso.RedirectStandardOutput = true;

                proceso.EnvironmentVariables["PGPASSWORD"] =
                    password;

                using (Process procesoPsql =
                    new Process())
                {
                    procesoPsql.StartInfo = proceso;

                    procesoPsql.Start();

                    string salida =
                        procesoPsql.StandardOutput.ReadToEnd();

                    string error =
                        procesoPsql.StandardError.ReadToEnd();

                    procesoPsql.WaitForExit();

                    if (procesoPsql.ExitCode != 0)
                    {
                        MessageBox.Show(
                            "ERROR AL RESTAURAR:\n\n" +
                            error +
                            "\n\nCódigo de salida: " +
                            procesoPsql.ExitCode,
                            "Error PostgreSQL",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return false;
                    }
                }



                MessageBox.Show(
                    "El respaldo se restauró correctamente.",
                    "Restauración",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "ERROR EN LA RESTAURACIÓN:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
            finally
            {
                try
                {
                    conexionBD.CerrarConexion();
                }
                catch
                {
                }
            }
        }

        // =====================================================
        // DESCARGAR / COPIAR RESPALDO
        // =====================================================

        public bool CopiarRespaldo(
            string rutaOrigen,
            string rutaDestino)
        {
            try
            {
                if (!File.Exists(rutaOrigen))
                {
                    return false;
                }

                File.Copy(
                    rutaOrigen,
                    rutaDestino,
                    true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
