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

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class EditarProducto : Form
    {
        ProductoDAO productoDAO = new ProductoDAO();

        int idProductoSeleccionado = 0;
       

        public EditarProducto()
        {
            InitializeComponent();

           
        }

        //Cargar Formulario

        private void EditarProducto_Load(object sender, EventArgs e)
        {
            // Cargar productos
            DataTable productos =
                productoDAO.MostrarProductos();

            CBnombreP.DataSource = productos;
            CBnombreP.DisplayMember = "nombre";
            CBnombreP.ValueMember = "id_producto";

            CBnombreP.DropDownStyle =
                ComboBoxStyle.DropDown;

            CBnombreP.AutoCompleteMode =
                AutoCompleteMode.SuggestAppend;

            CBnombreP.AutoCompleteSource =
                AutoCompleteSource.ListItems;


            // Cargar categorías
            DataTable categorias =
                productoDAO.CargarCategorias();

            CBcategoria.DataSource = categorias;
            CBcategoria.DisplayMember =
                "nombre_categoria";

            CBcategoria.ValueMember =
                "id_categoria";


            // El código solamente se muestra
            txtCodigo.ReadOnly = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Buscar producto

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombreProducto =
               CBnombreP.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombreProducto))
            {
                MessageBox.Show(
                    "Seleccione o escriba un producto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;

            }
            DataTable resultado = productoDAO.BuscarPorNombre(nombreProducto);

            if (resultado.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No se encontró el producto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            DataRow producto =
                resultado.Rows[0];

            // Id del producto
            idProductoSeleccionado =
               Convert.ToInt32(
                   producto["id_producto"]);

            // Cargar nombre
            CBnombreP.Text =
                producto["nombre"].ToString();

            //Cargar categoria 
            string categoria = producto["categoria"].ToString();
            for(int i = 0;
                i<CBcategoria.Items.Count;
                i++)
            {
                DataRowView fila = CBcategoria.Items[i] as DataRowView;

                if(fila != null)
                {
                    string nombreCategoria =
                        fila["nombre_categoria"].ToString();
                    if(nombreCategoria.Equals(
                        categoria,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        CBcategoria.SelectedIndex = i;
                        break;
                    }
                }
            }

            //Cargar marca
            txtMarca.Text = producto["marca"].ToString();

            //Cargar proveedor
            txtProveedor.Text = producto["proveedor"].ToString();

            //Cargar codigo 
            txtCodigo.Text =
            producto["codigo"].ToString();


        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Validar producto
            if (idProductoSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Primero debe buscar un producto.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            //Validar nombre
            string nombre =
               CBnombreP.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show(
                    "El nombre del producto no puede estar vacío.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CBnombreP.Focus();
                return;
            }
            //Validar categoria
            if (CBcategoria.SelectedIndex == -1 ||
               CBcategoria.SelectedValue == null)
            {
                MessageBox.Show(
                    "Debe seleccionar una categoría.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                CBcategoria.Focus();
                return;
            }

            if (!int.TryParse(
                CBcategoria.SelectedValue.ToString(),
                out int idCategoria))
            {
                MessageBox.Show(
                    "La categoría seleccionada no es válida.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
           

            //Obtener marca
            DataTable marcas =
               productoDAO.CargarMarcas();

            DataRow filaMarca =
                marcas.AsEnumerable()
                .FirstOrDefault(
                    x =>
                    x["nombre_marca"]
                    .ToString()
                    .Equals(
                        txtMarca.Text.Trim(),
                        StringComparison.OrdinalIgnoreCase));

            if (filaMarca == null)
            {
                MessageBox.Show(
                    "La marca indicada no existe.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idMarca =
                Convert.ToInt32(
                    filaMarca["id_marca"]);

            //Obtener proveedor
            DataTable proveedores =
               productoDAO.CargarProveedores();

            DataRow filaProveedor =
                proveedores.AsEnumerable()
                .FirstOrDefault(
                    x =>
                    x["nombre"]
                    .ToString()
                    .Equals(
                        txtProveedor.Text.Trim(),
                        StringComparison.OrdinalIgnoreCase));

            if (filaProveedor == null)
            {
                MessageBox.Show(
                    "El proveedor indicado no existe.",
                    "Guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idProveedor =
                Convert.ToInt32(
                    filaProveedor["id_proveedor"]);

            //Crear objeto producto
            ClaseProducto producto =
                new ClaseProducto();

            producto.IdProducto =
                idProductoSeleccionado;

            producto.Nombre =
                nombre;

            producto.IdCategoria =
                idCategoria;

            producto.IdProveedor =
                idProveedor;

            producto.IdMarca =
                idMarca;


            if (  resultado)
            {
                MessageBox.Show(
                    "Producto actualizado correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarFormulario();

            }
        }

        //Limpiar
        private void LimpiarFormulario()
        {
            idProductoSeleccionado = 0;

            CBnombreP.SelectedIndex = -1;
            CBcategoria.SelectedIndex = -1;

            txtMarca.Clear();
            txtProveedor.Clear();
            txtCodigo.Clear();
           

            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        
        private void CBTalla_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();

            this.Close();

        }
    }
}
