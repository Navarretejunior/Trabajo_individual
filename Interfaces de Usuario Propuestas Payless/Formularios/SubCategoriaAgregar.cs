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
    public partial class SubCategoriaAgregar : Form
    {
        CategoriaDAO categoriaDAO = new CategoriaDAO();
        public SubCategoriaAgregar()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre de la categoría.");
                txtNombre.Focus();
                return;
            }

            ClaseCategoria categoria = new ClaseCategoria();

            categoria.NombreCategoria = txtNombre.Text.Trim();
            categoria.Descripcion = txtDescripcion.Text.Trim();

            categoria.Estado =
                cmbEstado.SelectedItem.ToString() == "Activo";

            if (categoriaDAO.AgregarCategoria(categoria))
            {
                MessageBox.Show(
                    "Categoría agregada correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarCampos();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo agregar la categoría.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SubCategoriaAgregar_Load(object sender, EventArgs e)
        {
            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = 0;
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
