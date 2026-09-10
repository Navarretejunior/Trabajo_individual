using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Mantenimiento : Form
    {
        private RespaldoBD respaldoBD = new RespaldoBD();

        // Guarda temporalmente el archivo seleccionado
        private string rutaArchivoSeleccionado = "";
        public Mantenimiento()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void Mantenimiento_Load(object sender, EventArgs e)
        {
            lblCaja.Enabled = false;
            lblProveedores.Enabled = false;
            lblProductos.Enabled = false;
            lblVenta.Enabled = false;
            lblCompras.Enabled = false;
            lblUsuarios.Enabled = false;


            lblCliente.Enabled = false;
            lblCredito.Enabled = false;
            lblInventario.Enabled = false;
            lblMantenimiento.Enabled = false;


            switch (ClaseSesion.RolActual)
            {
                case "Administrador":

                    lblCaja.Enabled = true;
                    lblCompras.Enabled = true;
                    lblVenta.Enabled = true;
                    lblUsuarios.Enabled = true;
                    lblMantenimiento.Enabled = true;
                    lblCliente.Enabled = true;
                    lblCredito.Enabled = true;
                    lblInventario.Enabled = true;
                    lblProveedores.Enabled = true;
                    lblProductos.Enabled = true;


                    break;

                case "Gerente":

                    lblCaja.Enabled = true;
                    lblCompras.Enabled = true;
                    lblVenta.Enabled = true;

                    break;

                case "Cajero":

                    lblCaja.Enabled = true;
                    lblVenta.Enabled = true;

                    break;
            }



            CargarRespaldos();

            // AQUÍ VA TU CÓDIGO:
            dgvRespaldos.DataSource = respaldoBD.MostrarRespaldos();

            // Código ultra corto para los tamaños (Ajustado por el orden de tus columnas)
            dgvRespaldos.Columns[0].Width = 240; // Nombre
            dgvRespaldos.Columns[2].Width = 140; // Fecha
            dgvRespaldos.Columns[3].Width = 80;  // Tamaño
            dgvRespaldos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Ruta (Ubicación)

        }

        private void CargarRespaldos()
        {
            try
            {
                dgvRespaldos.DataSource = null;

                dgvRespaldos.DataSource =
                    respaldoBD.MostrarRespaldos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los respaldos.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = "Respaldo_Sistema";

                string rutaArchivo;

                bool resultado =
                    respaldoBD.CrearRespaldo(
                        nombre,
                        out rutaArchivo);

                if (resultado)
                {
                    MessageBox.Show(
                        "El respaldo se creó correctamente.",
                        "Respaldo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarRespaldos();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo crear el respaldo.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al crear el respaldo.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialogo = new OpenFileDialog())
                {
                    dialogo.Title = "Seleccionar respaldo";

                    dialogo.Filter =
                        "Archivos SQL (*.sql)|*.sql|" +
                        "Todos los archivos (*.*)|*.*";

                    dialogo.Multiselect = false;

                    if (dialogo.ShowDialog() == DialogResult.OK)
                    {
                        rutaArchivoSeleccionado =
                            dialogo.FileName;

                        MessageBox.Show(
                            "Archivo seleccionado:\n\n" +
                            Path.GetFileName(
                                rutaArchivoSeleccionado),
                            "Archivo seleccionado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo seleccionar el archivo.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
           
                if (string.IsNullOrWhiteSpace(
                    rutaArchivoSeleccionado))
                {
                    rutaArchivoSeleccionado =
                        ObtenerRutaSeleccionada();
                }

                if (string.IsNullOrWhiteSpace(
                    rutaArchivoSeleccionado))
                {
                    MessageBox.Show(
                        "Seleccione primero un archivo de respaldo.",
                        "Restaurar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DialogResult confirmacion =
                    MessageBox.Show(
                        "¿Está seguro de restaurar este respaldo?\n\n" +
                        "La información actual de la base de datos " +
                        "puede verse afectada.",
                        "Confirmar restauración",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (confirmacion != DialogResult.Yes)
                    return;

                bool resultado =
                    respaldoBD.RestaurarRespaldo(
                        rutaArchivoSeleccionado);

                if (resultado)
                {
                    MessageBox.Show(
                        "El respaldo se restauró correctamente.",
                        "Restauración",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    rutaArchivoSeleccionado = "";

                    CargarRespaldos();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo restaurar el respaldo.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al restaurar el respaldo.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string ObtenerRutaSeleccionada()
        {
            if (dgvRespaldos.CurrentRow == null)
                return "";

            try
            {
                // Si tienes una columna llamada rutaArchivo
                if (dgvRespaldos.Columns.Contains(
                    "rutaArchivo"))
                {
                    object valor =
                        dgvRespaldos.CurrentRow
                        .Cells["rutaArchivo"].Value;

                    if (valor != null)
                        return valor.ToString();
                }

                // Si la ruta está en la columna Ruta
                if (dgvRespaldos.Columns.Contains(
                    "Ruta"))
                {
                    object valor =
                        dgvRespaldos.CurrentRow
                        .Cells["Ruta"].Value;

                    if (valor != null)
                        return valor.ToString();
                }
            }
            catch
            {
                return "";
            }

            return "";
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta =
                    ObtenerRutaSeleccionada();

                if (string.IsNullOrWhiteSpace(ruta))
                {
                    ruta = rutaArchivoSeleccionado;
                }

                if (string.IsNullOrWhiteSpace(ruta) ||
                    !File.Exists(ruta))
                {
                    MessageBox.Show(
                        "Seleccione un respaldo válido.",
                        "Descargar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                using (SaveFileDialog dialogo =
                    new SaveFileDialog())
                {
                    dialogo.Title =
                        "Guardar copia del respaldo";

                    dialogo.Filter =
                        "Archivo SQL (*.sql)|*.sql";

                    dialogo.FileName =
                        Path.GetFileName(ruta);

                    if (dialogo.ShowDialog() ==
                        DialogResult.OK)
                    {
                        File.Copy(
                            ruta,
                            dialogo.FileName,
                            true);

                        MessageBox.Show(
                            "El respaldo se guardó correctamente.",
                            "Descargar",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar el respaldo.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta =
                    ObtenerRutaSeleccionada();

                if (string.IsNullOrWhiteSpace(ruta))
                {
                    ruta = rutaArchivoSeleccionado;
                }

                if (string.IsNullOrWhiteSpace(ruta) ||
                    !File.Exists(ruta))
                {
                    MessageBox.Show(
                        "Seleccione primero un respaldo.",
                        "Eliminar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DialogResult confirmacion =
                    MessageBox.Show(
                        "¿Está seguro de eliminar este respaldo?\n\n" +
                        Path.GetFileName(ruta),
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (confirmacion != DialogResult.Yes)
                    return;

                bool resultado =
                    respaldoBD.EliminarRespaldo(ruta);

                if (resultado)
                {
                    MessageBox.Show(
                        "El respaldo se eliminó correctamente.",
                        "Eliminar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    rutaArchivoSeleccionado = "";

                    CargarRespaldos();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo eliminar el respaldo.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al eliminar el respaldo.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
