using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless
{       
    public partial class Categoria : Form
    {
        CategoriaDAO categoriaDAO = new CategoriaDAO();
        
        public Categoria()
        {
            InitializeComponent();
        }

        private void Categoria_Load(object sender, EventArgs e)
        {
            CargarCategorias();

            cmbBuscarPor.Items.Clear();

            cmbBuscarPor.Items.Add("Todas");
            cmbBuscarPor.Items.Add("Activas");
            cmbBuscarPor.Items.Add("Inactivas");

            cmbBuscarPor.SelectedIndex = 0;
        }

        private void CargarCategorias()
        {
            dgvCategorias.DataSource = categoriaDAO.MostrarCategorias();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            switch (cmbBuscarPor.Text)
            {
                case "Todas":
                    CargarCategorias();
                    break;

                case "Activas":
                    dgvCategorias.DataSource =
                        categoriaDAO.BuscarPorEstado(true);
                    break;

                case "Inactivas":
                    dgvCategorias.DataSource =
                        categoriaDAO.BuscarPorEstado(false);
                    break;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SubCategoriaAgregar formulario = new SubCategoriaAgregar();

            formulario.ShowDialog();

            CargarCategorias();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            SubCategoriaEditar formulario = new SubCategoriaEditar();

            formulario.ShowDialog();

            CargarCategorias();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }

            int idCategoria = Convert.ToInt32(
                dgvCategorias.CurrentRow.Cells["colId"].Value
            );

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de desactivar esta categoría?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta == DialogResult.Yes)
            {
                if (categoriaDAO.EliminarCategoria(idCategoria))
                {
                    MessageBox.Show("Categoría desactivada correctamente.");
                    CargarCategorias();
                }
                else
                {
                    MessageBox.Show("No se pudo desactivar la categoría.");
                }
            }
        }
    }
}
