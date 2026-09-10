using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Productos : Form
    {
        ProductoDAO productoDAO = new ProductoDAO();

        ClaseUsuario usuarioActual;

        private DataTable tablaProductos;

        public Productos()
        {
            InitializeComponent();

            ConfigurarDataGridView();
            ConfigurarComboBuscar();



            MostrarProductos();
        }



        private void ConfigurarDataGridView()
        {
            // No crear columnas automáticamente
            dgvProductos.AutoGenerateColumns = false;

            // No permitir editar directamente
            dgvProductos.ReadOnly = true;

            // Seleccionar fila completa
            dgvProductos.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            // Solo una fila seleccionada
            dgvProductos.MultiSelect = false;

            // No permitir agregar filas manualmente
            dgvProductos.AllowUserToAddRows = false;

            // Fuente
            dgvProductos.Font =
                new Font("Times New Roman", 12);

            dgvProductos.ColumnHeadersDefaultCellStyle.Font =
                new Font("Times New Roman", 12);

            // Limpiar columnas existentes
            dgvProductos.Columns.Clear();


            // =====================================================
            // CÓDIGO
            // =====================================================

            DataGridViewTextBoxColumn columnaID =
                new DataGridViewTextBoxColumn();

            columnaID.Name = "colID";
            columnaID.HeaderText = "Código";
            columnaID.DataPropertyName = "id_producto";
            columnaID.ReadOnly = true;
            columnaID.Width = 90;

            dgvProductos.Columns.Add(columnaID);


            // =====================================================
            // NOMBRE DEL PRODUCTO
            // =====================================================

            DataGridViewTextBoxColumn columnaNombre =
                new DataGridViewTextBoxColumn();

            columnaNombre.Name = "colNombre";
            columnaNombre.HeaderText = "Nombre del producto";
            columnaNombre.DataPropertyName = "nombre";
            columnaNombre.ReadOnly = true;
            columnaNombre.Width = 220;

            dgvProductos.Columns.Add(columnaNombre);


            // =====================================================
            // CATEGORÍA
            // =====================================================

            DataGridViewTextBoxColumn columnaCategoria =
                new DataGridViewTextBoxColumn();

            columnaCategoria.Name = "colCategoria";
            columnaCategoria.HeaderText = "Categoría";
            columnaCategoria.DataPropertyName = "categoria";
            columnaCategoria.ReadOnly = true;
            columnaCategoria.Width = 150;

            dgvProductos.Columns.Add(columnaCategoria);


            // =====================================================
            // MARCA
            // =====================================================

            DataGridViewTextBoxColumn columnaMarca =
                new DataGridViewTextBoxColumn();

            columnaMarca.Name = "colMarca";
            columnaMarca.HeaderText = "Marca";
            columnaMarca.DataPropertyName = "marca";
            columnaMarca.ReadOnly = true;
            columnaMarca.Width = 150;

            dgvProductos.Columns.Add(columnaMarca);


            // =====================================================
            // PROVEEDOR
            // =====================================================

            DataGridViewTextBoxColumn columnaProveedor =
                new DataGridViewTextBoxColumn();

            columnaProveedor.Name = "colProveedor";
            columnaProveedor.HeaderText = "Proveedor";
            columnaProveedor.DataPropertyName = "proveedor";
            columnaProveedor.ReadOnly = true;
            columnaProveedor.Width = 150;

            dgvProductos.Columns.Add(columnaProveedor);


            // =====================================================
            // ESTADO
            // =====================================================

            // IMPORTANTE:
            // NO usamos DataGridViewCheckBoxColumn.
            // Se utiliza una columna de texto para mostrar
            // "Activo" o "Inactivo".

            DataGridViewTextBoxColumn columnaEstado =
                new DataGridViewTextBoxColumn();

            columnaEstado.Name = "colEstado";
            columnaEstado.HeaderText = "Estado";
            columnaEstado.DataPropertyName = "estado_texto";
            columnaEstado.ReadOnly = true;
            columnaEstado.Width = 100;

            dgvProductos.Columns.Add(columnaEstado);
        }



        // =========================================================
        // CONFIGURAR COMBOBOX DE BÚSQUEDA
        // =========================================================

        private void ConfigurarComboBuscar()
        {
            cmdBuscarProducto.Items.Clear();

            cmdBuscarProducto.Items.Add("ID");
            cmdBuscarProducto.Items.Add("Nombre");
            cmdBuscarProducto.Items.Add("Categoría");
            cmdBuscarProducto.Items.Add("Marca");
            cmdBuscarProducto.Items.Add("Proveedor");
            cmdBuscarProducto.Items.Add("Estado");

            cmdBuscarProducto.SelectedIndex = -1;
        }


        private void MostrarProductos()
        {
            try
            {
                tablaProductos = productoDAO.MostrarProductos();

                if (tablaProductos == null)
                {
                    MessageBox.Show(
                        "No se pudieron cargar los productos.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (!tablaProductos.Columns.Contains("estado_texto"))
                {
                    tablaProductos.Columns.Add(
                        "estado_texto",
                        typeof(string));
                }

                foreach (DataRow fila in tablaProductos.Rows)
                {
                    bool estado = false;

                    if (fila["estado_producto"] != DBNull.Value)
                    {
                        estado = Convert.ToBoolean(
                            fila["estado_producto"]);
                    }

                    fila["estado_texto"] =
                        estado ? "Activo" : "Inactivo";
                }

                dgvProductos.DataSource = null;
                dgvProductos.DataSource = tablaProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los productos:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void Productos_Load(object sender, EventArgs e)
        {
            // Cargar los productos automáticamente
            MostrarProductos();

            // Deshabilitar todas las opciones
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

            // Habilitar opciones según el rol
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

        }



        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
            new Proveedores().Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            new Usuario().Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            new Cliente().Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos(); ventana.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal(); ventana.Show();
            this.Hide();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja(); ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito(); ventana.Show(); 
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
         
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas(); ventana.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo(); ventana.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario(); ventana.Show(); this.Hide();
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento(); ventana.Show(); this.Hide();
        }

        private void cmbBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Abrir subpantalla de agregar
            using (SubProductoAgregar formulario =
                   new SubProductoAgregar())
            {
                formulario.ShowDialog(this);
            }

            // Al cerrar la subpantalla,
            // volver a cargar los datos.
            MostrarProductos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
           /* string json = JsonConvert.SerializeObject(
        listaProductos,
        Formatting.Indented);

            File.WriteAllText("productos.json", json);

          MessageBox.Show("Productos guardados correctamente."); */
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // ============================================================
            // VERIFICAR QUE HAYA SELECCIONADO UN CRITERIO
            // ============================================================

            if (cmdBuscarProducto.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione una opción en 'Buscar por'.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            // ============================================================
            // VERIFICAR QUE HAYA PRODUCTOS
            // ============================================================

            if (tablaProductos == null ||
                tablaProductos.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay productos cargados.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            // ============================================================
            // OBTENER CRITERIO Y TEXTO
            // ============================================================

            string criterio =
                cmdBuscarProducto.SelectedItem.ToString();

            string texto =
                txtBuscarProducto.Text.Trim();


            // ============================================================
            // VALIDAR TEXTO
            // ============================================================

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show(
                    "Ingrese un valor para buscar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtBuscarProducto.Focus();

                return;
            }


            try
            {
                DataView vista =
                    new DataView(tablaProductos);

                string textoSeguro =
                    texto.Replace("'", "''");


                // ========================================================
                // BÚSQUEDA SEGÚN CRITERIO
                // ========================================================

                switch (criterio)
                {
                    // ====================================================
                    // ID
                    // ====================================================

                    case "ID":

                        int id;

                        if (!int.TryParse(
                            texto,
                            out id))
                        {
                            MessageBox.Show(
                                "El ID debe ser un número.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            txtBuscarProducto.Focus();

                            return;
                        }

                        vista.RowFilter =
                            $"id_producto = {id}";

                        break;


                    // ====================================================
                    // NOMBRE
                    // ====================================================

                    case "Nombre":

                        vista.RowFilter =
                            $"CONVERT(nombre, 'System.String') " +
                            $"LIKE '%{textoSeguro}%'";

                        break;


                    // ====================================================
                    // CATEGORÍA
                    // ====================================================

                    case "Categoría":

                        vista.RowFilter =
                            $"CONVERT(categoria, 'System.String') " +
                            $"LIKE '%{textoSeguro}%'";

                        break;


                    // ====================================================
                    // MARCA
                    // ====================================================

                    case "Marca":

                        vista.RowFilter =
                            $"CONVERT(marca, 'System.String') " +
                            $"LIKE '%{textoSeguro}%'";

                        break;


                    // ====================================================
                    // PROVEEDOR
                    // ====================================================

                    case "Proveedor":

                        vista.RowFilter =
                            $"CONVERT(proveedor, 'System.String') " +
                            $"LIKE '%{textoSeguro}%'";

                        break;


                    // ====================================================
                    // ESTADO
                    // ====================================================

                    case "Estado":

                        string estadoBuscado =
                            texto.ToLower();

                        if (estadoBuscado == "activo")
                        {
                            vista.RowFilter =
                                "estado_producto = TRUE";
                        }
                        else if (estadoBuscado == "inactivo")
                        {
                            vista.RowFilter =
                                "estado_producto = FALSE";
                        }
                        else
                        {
                            MessageBox.Show(
                                "Escriba Activo o Inactivo.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            txtBuscarProducto.Focus();

                            return;
                        }

                        break;
                }


                // ========================================================
                // MOSTRAR RESULTADOS
                // ========================================================

                dgvProductos.DataSource = vista;


                // ========================================================
                // SI NO HAY RESULTADOS
                // ========================================================

                if (vista.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron productos.",
                        "Resultado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al realizar la búsqueda:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarProducto ventana =
                new EditarProducto();

            ventana.ShowDialog(this);

            // Al regresar de la subpantalla,
            // actualizar el DataGrid.
            MostrarProductos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar selección
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            // Obtener ID
            if (dgvProductos.CurrentRow.Cells["colID"].Value
                == null)
            {
                MessageBox.Show(
                    "No se pudo obtener el producto seleccionado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }


            int idProducto =
                Convert.ToInt32(
                    dgvProductos.CurrentRow
                    .Cells["colID"]
                    .Value);


            // =====================================================
            // CONFIRMAR ELIMINACIÓN
            // =====================================================

            DialogResult respuesta =
                MessageBox.Show(
                    "¿Desea eliminar este producto?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);


            if (respuesta != DialogResult.Yes)
            {
                return;
            }


            // =====================================================
            // ELIMINAR
            // =====================================================

            try
            {
                bool eliminado =
                    productoDAO.EliminarProducto(
                        idProducto);


                if (eliminado)
                {
                    MessageBox.Show(
                        "Producto eliminado correctamente.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Actualizar DataGrid
                    MostrarProductos();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo eliminar el producto.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al eliminar el producto:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            Reporte_Productos ventana = new Reporte_Productos();
            ventana.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Categoria ventana = new Categoria();
            ventana.Show();
            this.Hide();
        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
            Marca ventana = new Marca();
            ventana.Show();
            this.Hide();
        }
    }
}
