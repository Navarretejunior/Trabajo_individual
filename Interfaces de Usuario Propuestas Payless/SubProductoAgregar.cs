using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static Interfaces_de_Usuario_Propuestas_Payless;
//using static Interfaces_de_Usuario_Propuestas_Payless.Ventas;


namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class SubProductoAgregar : Form
    {
        ProductoDAO DAO = new ProductoDAO();
        public SubProductoAgregar()
        {
            InitializeComponent();
        }

        private void SubProductoAgregar_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarMarcas();
            CargarProveedores();
            CargarTallas();

        }
        private void CargarCategorias()
        {
            cmbCategoria.DataSource = DAO.CargarCategorias();
            cmbCategoria.DisplayMember = "nombre_categoria";
            cmbCategoria.ValueMember = "id_categoria";
            cmbCategoria.SelectedIndex = -1;
        }
        private void CargarMarcas()
        {
            cmbMarca.DataSource = DAO.CargarMarcas();
            cmbMarca.DisplayMember = "nombre_marca";
            cmbMarca.ValueMember = "id_marca";
            cmbMarca.SelectedIndex = -1;
        }
        private void CargarProveedores()
        {
            cmbProveedor.DataSource = DAO.CargarProveedores();
            cmbProveedor.DisplayMember = "nombre";
            cmbProveedor.ValueMember = "id_proveedor";
            cmbProveedor.SelectedIndex = -1;
        }
        private void CargarTallas()
        {
            cmbTalla.Items.Clear();

            for (int i = 30; i <= 45; i++)
            {
                cmbTalla.Items.Add(i.ToString());
            }
            cmbTalla.SelectedIndex = -1;
        }
        private bool ValidarCampos()
        {
            // Validar nombre
            if (string.IsNullOrWhiteSpace(txtNombredelProducto.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre del producto",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombredelProducto.Focus();
                return false;
            }

          

            // Validar categoría
            if (cmbCategoria.SelectedIndex == -1 ||
                cmbCategoria.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione una categoría",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbCategoria.Focus();
                return false;
            }

            // Validar marca
            if (cmbMarca.SelectedIndex == -1 ||
                cmbMarca.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione una marca",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbMarca.Focus();
                return false;
            }

            // Validar proveedor
            if (cmbProveedor.SelectedIndex == -1 ||
                cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione un proveedor",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbProveedor.Focus();
                return false;
            }

            // Validar talla
            if (cmbTalla.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(cmbTalla.Text))
            {
                MessageBox.Show(
                    "Seleccione una talla",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbTalla.Focus();
                return false;
            }

            // Todos los campos son válidos
            return true;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (!ValidarCampos())
                return;

            try
            {
                ClaseProducto producto = new ClaseProducto();

                // Nombre
                producto.Nombre =
                    txtNombredelProducto.Text.Trim();

                // Producto nuevo = activo
                producto.EstadoProducto = true;


                // Categoría
                if (!int.TryParse(
                    cmbCategoria.SelectedValue?.ToString(),
                    out int idCategoria))
                {
                    MessageBox.Show(
                        "Seleccione una categoría válida.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    cmbCategoria.Focus();
                    return;
                }

                producto.IdCategoria = idCategoria;


                // Marca
                if (!int.TryParse(
                    cmbMarca.SelectedValue?.ToString(),
                    out int idMarca))
                {
                    MessageBox.Show(
                        "Seleccione una marca válida.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    cmbMarca.Focus();
                    return;
                }

                producto.IdMarca = idMarca;


                // Proveedor
                if (!int.TryParse(
                    cmbProveedor.SelectedValue?.ToString(),
                    out int idProveedor))
                {
                    MessageBox.Show(
                        "Seleccione un proveedor válido.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    cmbProveedor.Focus();
                    return;
                }

                producto.IdProveedor = idProveedor;


                // Talla
                string talla = cmbTalla.Text.Trim();


                


                // Stock mínimo
                int stockMinimo = 5;


                // Guardar
                bool resultado =
                    DAO.AgregarProducto(
                        producto,
                       
                      
                        stockMinimo);


                if (resultado)
                {
                    MessageBox.Show(
                        "Producto guardado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo guardar el producto.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al guardar el producto:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }
        private void LimpiarCampos()

        { txtNombredelProducto.Clear();
            

            cmbCategoria.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            cmbTalla.SelectedIndex = -1;
            txtNombredelProducto.Focus();

        }
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        
    }
}

   
