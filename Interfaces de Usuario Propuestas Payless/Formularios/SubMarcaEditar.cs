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
    public partial class SubMarcaEditar : Form
    {
        MarcaDAO marcaDAO = new MarcaDAO();

        public SubMarcaEditar()
        {
            InitializeComponent();
        }

        private void SubMarcaEditar_Load(object sender, EventArgs e)
        {
            // Cargar TODAS las marcas
            // activas e inactivas
            CargarMarcas();

            // Cargar estados
            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = -1;

            // El código no se modifica
            txtCodigo.ReadOnly = true;
        }

        private void CargarMarcas()
        {
            cmbMarca.DataSource =
              marcaDAO.CargarTodasLasMarcas();

            cmbMarca.DisplayMember =
                "nombre_marca";

            cmbMarca.ValueMember =
                "id_marca";

            cmbMarca.SelectedIndex = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbMarca.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione una marca.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                cmbMarca.SelectedValue?.ToString(),
                out int idMarca))
            {
                MessageBox.Show(
                    "La marca seleccionada no es válida.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            CargarMarca(idMarca);
        }
        private void CargarMarca(int idMarca)
        {
            ClaseMarca marca =
               marcaDAO.ObtenerMarca(idMarca);

            if (marca == null)
            {
                MessageBox.Show(
                    "No se encontró la marca.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            txtCodigo.Text =
                marca.IdMarca.ToString();

            txtNombre.Text =
                marca.NombreMarca;

            txtDescripcion.Text =
                marca.Descripcion;

            // Estado
            if (marca.Estado)
            {
                cmbEstado.SelectedItem = "Activo";
            }
            else
            {
                cmbEstado.SelectedItem = "Inactivo";
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show(
                    "Primero busque una marca.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre de la marca.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombre.Focus();

                return;
            }

            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione el estado de la marca.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbEstado.Focus();

                return;
            }

            ClaseMarca marca =
                new ClaseMarca();

            marca.IdMarca =
                Convert.ToInt32(
                    txtCodigo.Text);

            marca.NombreMarca =
                txtNombre.Text.Trim();

            marca.Descripcion =
                txtDescripcion.Text.Trim();

            marca.Estado =
                cmbEstado.SelectedItem
                .ToString() == "Activo";

            if (marcaDAO.EditarMarca(marca))
            {
                MessageBox.Show(
                    "Marca actualizada correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo actualizar la marca.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
