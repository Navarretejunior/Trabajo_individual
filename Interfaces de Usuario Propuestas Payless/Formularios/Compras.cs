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
    public partial class Compras_nuevo : Form
    {
        CompraDAO compraDAO = new CompraDAO();
        private DataTable detallesCompra;

        public Compras_nuevo()
        {
            InitializeComponent();

            ConfigurarFormulario();
            CrearTablaDetalles();
            ConfigurarDataGridView();

            CargarProveedores();
            CargarProductos();

            MostrarNumeroCompra();
        }

        private void ConfigurarFormulario()
        {
            txtNoCompra.ReadOnly = true;

            txtSubtotal.ReadOnly = true;
            txtIVA.ReadOnly = true;
            txtTotalCompra.ReadOnly = true;

            cmbCategoria.Enabled = false;
            cmbMarca.Enabled = false;
            cmbTalla.Enabled = false;
            cmbCantidad.Enabled = false;

            // --------------------------------------------------------
            // FECHA
            // --------------------------------------------------------

            cmbFecha.Items.Clear();

            cmbFecha.Items.Add(
                DateTime.Now.ToString("dd/MM/yyyy"));

            cmbFecha.SelectedIndex = 0;


            // --------------------------------------------------------
            // CANTIDAD
            // --------------------------------------------------------

            cmbCantidad.Items.Clear();

            for (int i = 1; i <= 100; i++)
            {
                cmbCantidad.Items.Add(i);
            }

            cmbCantidad.SelectedIndex = -1;
        }

        private void MostrarNumeroCompra()
        {
            try
            {
                int numeroCompra =
                    compraDAO.ObtenerSiguienteNumeroCompra();

                txtNoCompra.Text = "N°" +
                    numeroCompra.ToString() ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo obtener el número de compra:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void CrearTablaDetalles()
        {
            detallesCompra = new DataTable();

            detallesCompra.Columns.Add(
                "id_producto_talla",
                typeof(int));

            detallesCompra.Columns.Add(
                "codigo",
                typeof(int));

            detallesCompra.Columns.Add(
                "producto",
                typeof(string));

            detallesCompra.Columns.Add(
                "categoria",
                typeof(string));

            detallesCompra.Columns.Add(
                "marca",
                typeof(string));

            detallesCompra.Columns.Add(
                "talla",
                typeof(string));

            detallesCompra.Columns.Add(
                "precio_compra",
                typeof(decimal));

            // Se conserva internamente.
            // NO se muestra en el DataGridView.
            detallesCompra.Columns.Add(
                "precio_venta",
                typeof(decimal));

            detallesCompra.Columns.Add(
                "cantidad",
                typeof(int));

            detallesCompra.Columns.Add(
                "subtotal",
                typeof(decimal));
        }

        private void ConfigurarDataGridView()
        {
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.ReadOnly = true;

            dataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.MultiSelect = false;

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(
                "Codigo",
                "Código");

            dataGridView1.Columns.Add(
                "Productos",
                "Productos");

            dataGridView1.Columns.Add(
                "Categoria",
                "Categoría");

            dataGridView1.Columns.Add(
                "Marca",
                "Marca");

            dataGridView1.Columns.Add(
                "Medida",
                "Medida");

            dataGridView1.Columns.Add(
                "PrecioCompra",
                "Precio compra");

            dataGridView1.Columns.Add(
                "Cantidad",
                "Cantidad");

            dataGridView1.Columns.Add(
                "Total",
                "Total");
        }

        private void CargarProveedores()
        {
            try
            {
                DataTable tabla =
                    compraDAO.MostrarProveedores();

                cmbProveedor.DataSource = tabla;

                cmbProveedor.DisplayMember =
                    "nombre";

                cmbProveedor.ValueMember =
                    "id_proveedor";

                cmbProveedor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los proveedores:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarProductos()
        {
            try
            {
                DataTable tabla =
                    compraDAO.MostrarProductos();

                cmbProducto.DataSource = tabla;

                cmbProducto.DisplayMember =
                    "nombre";

                cmbProducto.ValueMember =
                    "id_producto";

                cmbProducto.SelectedIndex = -1;
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



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menu_principal();
            ventana.Show();
            this.Hide();
        }

        private void label28_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Compras ventana = new Compras();
            ventana.show();
            this.Hide();
        }

        private void label26_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();

        }

        private void label29_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label30_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reporte_Compra ventana = new Reporte_Compra();
            ventana.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // ============================================================
            // VALIDAR PROVEEDOR
            // ============================================================

            if (cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un proveedor.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbProveedor.Focus();

                return;
            }


            // ============================================================
            // VALIDAR DETALLES
            // ============================================================

            if (detallesCompra.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Agregue al menos un producto a la compra.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // ============================================================
            // VALIDAR TOTAL
            // ============================================================

            if (!decimal.TryParse(
                txtTotalCompra.Text,
                out decimal total) ||
                total <= 0)
            {
                MessageBox.Show(
                    "El total de la compra no es válido.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // ============================================================
            // OBTENER PROVEEDOR
            // ============================================================

            int idProveedor =
                Convert.ToInt32(
                    cmbProveedor.SelectedValue);


            try
            {
                // ========================================================
                // GUARDAR COMPRA
                // ========================================================
                //
                // RegistrarCompra() inserta la compra en PostgreSQL
                // y devuelve el id_compra generado realmente.
                //
                // ========================================================

                int idCompra =
                    compraDAO.RegistrarCompra(
                        total,
                        idProveedor,
                        detallesCompra);


                // ========================================================
                // MOSTRAR ID REAL DE LA COMPRA
                // ========================================================

                txtNoCompra.Text =
                    idCompra.ToString();


                // ========================================================
                // MENSAJE DE CONFIRMACIÓN
                // ========================================================

                MessageBox.Show(
                    "Compra guardada correctamente.\n\n" +
                    "No. de compra: " +
                    idCompra,
                    "Compra",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                // ========================================================
                // AQUÍ IRÁ EL PDF
                // ========================================================
                //
                // Posteriormente podemos colocar:
                //
                // GenerarComprobantePDF(idCompra);
                //
                // El PDF utilizará este mismo idCompra.
                //
                // ========================================================


                // ========================================================
                // LIMPIAR FORMULARIO
                // ========================================================

                LimpiarFormulario();


                // ========================================================
                // MOSTRAR EL SIGUIENTE NÚMERO
                // ========================================================

                MostrarNumeroCompra();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar la compra:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }



        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedIndex == -1)
                return;

            DataRowView producto =
                cmbProducto.SelectedItem as DataRowView;

            if (producto == null)
                return;

            cmbCategoria.Text =
                producto["nombre_categoria"].ToString();

            cmbMarca.Text =
                producto["nombre_marca"].ToString();

            int idProducto =
                Convert.ToInt32(
                    producto["id_producto"]);

            CargarTallas(idProducto);
        }

        private void LimpiarFormulario()
        {
            detallesCompra.Clear();

            dataGridView1.Rows.Clear();

            cmbProveedor.SelectedIndex = -1;

            cmbProducto.SelectedIndex = -1;

            cmbCategoria.Text = "";

            cmbMarca.Text = "";

            cmbTalla.DataSource = null;

            cmbTalla.Enabled = false;

            cmbCantidad.SelectedIndex = -1;

            cmbCantidad.Enabled = false;

            txtPrecioCompra.Clear();

            txtPrecioVenta.Clear();

            txtSubtotal.Clear();

            txtIVA.Clear();

            txtTotalCompra.Clear();

            txtNoCompra.Clear();

            cmbFecha.Items.Clear();

            cmbFecha.Items.Add(
                DateTime.Now.ToString("dd/MM/yyyy"));

            cmbFecha.SelectedIndex = 0;
        }



        private void CargarTallas(int idProducto)
        {
            try
            {
                cmbTalla.DataSource = null;
                cmbTalla.Items.Clear();

                // Tallas disponibles para zapatos
                for (int talla = 15; talla <= 55; talla++)
                {
                    cmbTalla.Items.Add(talla);
                }

                cmbTalla.SelectedIndex = -1;
                cmbTalla.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las tallas:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cmbTalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTalla.SelectedIndex == -1)
                return;

            // Las tallas corresponden a zapatos
            // y se manejan mediante sistema europeo.

            cmbCantidad.Enabled = true;

            cmbCantidad.SelectedIndex = -1;
        }
        internal class Compras
        {
            internal void show()
            {
                throw new NotImplementedException();
            }

            internal void Show()
            {
                throw new NotImplementedException();
            }
        }

        internal class Menu_principal : Menú_Principal
        {
        }

        private void btnAgregarProductos_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un proveedor.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbProveedor.Focus();

                return;
            }

            if (cmbProducto.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbProducto.Focus();

                return;
            }

            if (cmbTalla.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione una talla.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbTalla.Focus();

                return;
            }

            if (cmbCantidad.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione una cantidad.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbCantidad.Focus();

                return;
            }

            if (!decimal.TryParse(
                txtPrecioCompra.Text,
                out decimal precioCompra) ||
                precioCompra <= 0)
            {
                MessageBox.Show(
                    "Ingrese un precio de compra válido.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPrecioCompra.Focus();

                return;
            }

            if (!decimal.TryParse(
                txtPrecioVenta.Text,
                out decimal precioVenta) ||
                precioVenta <= 0)
            {
                MessageBox.Show(
                    "Ingrese un precio de venta válido.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPrecioVenta.Focus();

                return;
            }

            DataRowView producto =
                cmbProducto.SelectedItem as DataRowView;

            if (producto == null)
                return;

            int idProducto =
                Convert.ToInt32(
                    producto["id_producto"]);

            string nombreProducto =
                producto["nombre"].ToString();

            string categoria =
                producto["nombre_categoria"].ToString();

            string marca =
                producto["nombre_marca"].ToString();

            string talla =
                cmbTalla.SelectedItem.ToString();

            int cantidad =
                Convert.ToInt32(
                    cmbCantidad.SelectedItem);

            decimal subtotal =
                precioCompra * cantidad;

            // ============================================================
            // GUARDAR DETALLE INTERNAMENTE
            // ============================================================

            DataRow fila =
                detallesCompra.NewRow();

            // Por ahora se deja en 0.
            // CompraDAO obtendrá/creará el id_producto_talla
            // al momento de guardar la compra.
            fila["id_producto_talla"] = 0;

            fila["codigo"] =
                idProducto;

            fila["producto"] =
                nombreProducto;

            fila["categoria"] =
                categoria;

            fila["marca"] =
                marca;

            fila["talla"] =
                talla;

            fila["precio_compra"] =
                precioCompra;

            fila["precio_venta"] =
                precioVenta;

            fila["cantidad"] =
                cantidad;

            fila["subtotal"] =
                subtotal;

            detallesCompra.Rows.Add(fila);

            MostrarDetalles();

            CalcularTotales();

            LimpiarDatosProducto();
        }

        private void MostrarDetalles()
        {
            dataGridView1.Rows.Clear();

            foreach (DataRow fila
                in detallesCompra.Rows)
            {
                dataGridView1.Rows.Add(
                    fila["codigo"],
                    fila["producto"],
                    fila["categoria"],
                    fila["marca"],
                    fila["talla"],
                    Convert.ToDecimal(
                        fila["precio_compra"])
                        .ToString("0.00"),
                    fila["cantidad"],
                    Convert.ToDecimal(
                        fila["subtotal"])
                        .ToString("0.00"));
            }
        }


        // ============================================================
        // CALCULAR TOTALES
        // ============================================================

        private void CalcularTotales()
        {
            decimal subtotal = 0;

            foreach (DataRow fila
                in detallesCompra.Rows)
            {
                subtotal +=
                    Convert.ToDecimal(
                        fila["subtotal"]);
            }


            // IVA 15%
            decimal iva =
                subtotal * 0.15m;


            decimal total =
                subtotal + iva;


            txtSubtotal.Text =
                subtotal.ToString("0.00");

            txtIVA.Text =
                iva.ToString("0.00");

            txtTotalCompra.Text =
                total.ToString("0.00");
        }


        // ============================================================
        // ELIMINAR
        // ============================================================

        private void btnEliminar_Click(
            object sender,
            EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un producto para eliminar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            int indice =
                dataGridView1.SelectedRows[0].Index;


            if (indice >= 0 &&
                indice < detallesCompra.Rows.Count)
            {
                detallesCompra.Rows.RemoveAt(indice);
            }


            MostrarDetalles();

            CalcularTotales();
        }

        private int ObtenerIdProducto(
            int idProductoTalla)
        {
            DataTable productos =
                compraDAO.MostrarProductos();


            foreach (DataRow producto
                in productos.Rows)
            {
                int idProducto =
                    Convert.ToInt32(
                        producto["id_producto"]);


                DataTable tallas =
                    compraDAO.MostrarTallas(
                        idProducto);


                foreach (DataRow talla
                    in tallas.Rows)
                {
                    int idTalla =
                        Convert.ToInt32(
                            talla["id_producto_talla"]);


                    if (idTalla ==
                        idProductoTalla)
                    {
                        return idProducto;
                    }
                }
            }


            return 0;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un producto para editar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            int indice =
                dataGridView1.SelectedRows[0].Index;


            if (indice < 0 ||
                indice >= detallesCompra.Rows.Count)
            {
                return;
            }


            DataRow fila =
                detallesCompra.Rows[indice];


            int idProductoTalla =
                Convert.ToInt32(
                    fila["id_producto_talla"]);


            int idProducto =
                ObtenerIdProducto(
                    idProductoTalla);


            if (idProducto == 0)
            {
                MessageBox.Show(
                    "No se encontró el producto.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }


            cmbProducto.SelectedValue =
                idProducto;


            CargarTallas(idProducto);


            cmbTalla.SelectedValue =
                idProductoTalla;


            cmbCantidad.Enabled = true;

            cmbCantidad.Text =
                fila["cantidad"].ToString();


            txtPrecioCompra.Text =
                Convert.ToDecimal(
                    fila["precio_compra"])
                    .ToString("0.00");


            txtPrecioVenta.Text =
                Convert.ToDecimal(
                    fila["precio_venta"])
                    .ToString("0.00");


            detallesCompra.Rows.RemoveAt(indice);


            MostrarDetalles();

            CalcularTotales();
        }

        private void LimpiarDatosProducto()
        {
            cmbProducto.SelectedIndex = -1;

            cmbCategoria.Text = "";

            cmbMarca.Text = "";

            cmbTalla.DataSource = null;

            cmbTalla.Enabled = false;

            cmbCantidad.SelectedIndex = -1;

            cmbCantidad.Enabled = false;

            txtPrecioCompra.Clear();

            txtPrecioVenta.Clear();
        }

        private void txtNoCompra_TextChanged(object sender, EventArgs e)
        {

        }
    }

}