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
    public partial class Marca : Form
    {
        MarcaDAO marcaDAO = new MarcaDAO();
        public Marca()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void Marca_Load(object sender, EventArgs e)
        {
            dgvMarcas.AutoGenerateColumns = false;

            cmbBuscarPor.Items.Clear();

            cmbBuscarPor.Items.Add("Todas");
            cmbBuscarPor.Items.Add("Activas");
            cmbBuscarPor.Items.Add("Inactivas");

            cmbBuscarPor.SelectedIndex = 0;

            CargarMarcas();
        }
        private void CargarMarcas()
        {
            dgvMarcas.DataSource = null;
            dgvMarcas.DataSource = marcaDAO.MostrarMarcas();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            switch (cmbBuscarPor.Text)
            {
                case "Todas":

                    CargarMarcas();

                    break;

                case "Activas":

                    dgvMarcas.DataSource =
                        marcaDAO.BuscarPorEstado(true);

                    break;

                case "Inactivas":

                    dgvMarcas.DataSource =
                        marcaDAO.BuscarPorEstado(false);

                    break;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SubMarcaAgregar formulario =
               new SubMarcaAgregar();

            formulario.ShowDialog();

            CargarMarcas();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione una marca para editar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            SubMarcaEditar formulario =
                new SubMarcaEditar();

            formulario.ShowDialog();

            CargarMarcas();


        }

        

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione una marca.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idMarca =
                Convert.ToInt32(
                    dgvMarcas.CurrentRow
                    .Cells["colId"]
                    .Value);

            DialogResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de desactivar esta marca?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                if (marcaDAO.EliminarMarca(idMarca))
                {
                    MessageBox.Show(
                        "Marca desactivada correctamente.",
                        "Correcto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarMarcas();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo desactivar la marca.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
