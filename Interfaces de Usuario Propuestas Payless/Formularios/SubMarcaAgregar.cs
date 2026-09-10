using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    public partial class SubMarcaAgregar : Form
    {
        MarcaDAO marcaDAO = new MarcaDAO();
        public SubMarcaAgregar()
        {
            InitializeComponent();
        }

        private void SubMarcaAgregar_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre de la marca.");

                txtNombre.Focus();
                return;
            }

            ClaseMarca marca = new ClaseMarca();

            marca.NombreMarca =
                txtNombre.Text.Trim();

            marca.Descripcion =
                txtDescripcion.Text.Trim();

            marca.Estado =
                cmbEstado.SelectedItem.ToString() == "Activo";

            if (marcaDAO.AgregarMarca(marca))
            {
                MessageBox.Show(
                    "Marca agregada correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarCampos();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo agregar la marca.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();

            cmbEstado.SelectedIndex = 0;

            txtNombre.Focus();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
