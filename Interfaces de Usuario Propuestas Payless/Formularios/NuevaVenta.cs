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
using static Interfaces_de_Usuario_Propuestas_Payless.Ventas;

namespace Interfaces_de_Usuario_Propuestas_Payless.Entidades
{
    public partial class NuevaVenta : Form
    {
        private VentaDAO ventaDAO;

        private int idProductoSeleccionado = 0;
        private int idProductoTallaSeleccionado = 0;

        private decimal precioVentaActual = 0;
        private decimal tipoCambioActual = 0;
        public NuevaVenta()
        {
            InitializeComponent();
            ventaDAO = new VentaDAO();
        }

        private void NuevaVenta_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarFormulario();

                CargarClientes();
                CargarProductos();
                CargarTipoCambio();
                GenerarCodigoVenta();

                CalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar la nueva venta:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarClientes()
        {
            try
            {
                DataTable clientes = ventaDAO.CargarClientes();

                CBcliente.DataSource = clientes;
                CBcliente.DisplayMember = "nombre";
                CBcliente.ValueMember = "id_cliente";
                CBcliente.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los clientes:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarProductos()
        {
            try
            {
                DataTable productos = ventaDAO.CargarProductos();

                CBproducto.DataSource = productos;
                CBproducto.DisplayMember = "nombre";
                CBproducto.ValueMember = "id_producto";
                CBproducto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los productos: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void MostrarUsuario()
        {
            lblUsuario.Text = ClaseSesion.UsuarioActual;
        }

       

        private void GenerarCodigoVenta()
        {
            try
            {
                txtCodigoVenta.Text = ventaDAO.GenerarCodigoVenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al generar el código de venta: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

       

        private void ConfigurarFormulario()
        {
            txtCodigoVenta.ReadOnly = true;
            txtPrecioVenta.ReadOnly = true;
            txtStockActual.ReadOnly = true;

            CBcategoria.Enabled = false;
            CBmarca.Enabled = false;

            CBcliente.SelectedIndex = -1;
            CBproducto.SelectedIndex = -1;
            CBTalla.SelectedIndex = -1;

            txtPrecioVenta.Clear();
            txtStockActual.Clear();

            txtCantidad.Text = "1";

            dgvDetalleVenta.AllowUserToAddRows = false;
            dgvDetalleVenta.AutoGenerateColumns = false;

            lblSubtotal.Text = "C$ 0.00";
            lblIVA.Text = "C$ 0.00";
            lblTotal.Text = "C$ 0.00";
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void CBproducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBproducto.SelectedIndex == -1)
                return;

            if (CBproducto.SelectedValue == null)
                return;

            try
            {
                if (CBproducto.SelectedValue is DataRowView)
                    return;

                idProductoSeleccionado =
                    Convert.ToInt32(CBproducto.SelectedValue);

                CargarInformacionProducto();
                CargarTallas();

                txtStockActual.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al seleccionar el producto:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarInformacionProducto()
        {
            DataTable producto =
                ventaDAO.ObtenerProducto(idProductoSeleccionado);

            if (producto.Rows.Count == 0)
                return;

            DataRow fila = producto.Rows[0];

            CBcategoria.Text =
                fila["nombre_categoria"].ToString();

            CBmarca.Text =
                fila["nombre_marca"].ToString();

            CargarPrecio();
        }

        private void CargarPrecio()
        {
            decimal? precio =
                ventaDAO.ObtenerPrecioProducto(idProductoSeleccionado);

            if (precio == null || precio.Value <= 0)
            {
                precioVentaActual = 0;
                txtPrecioVenta.Clear();

                MessageBox.Show(
                    "Este producto no tiene un precio de venta registrado.",
                    "Precio no disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            precioVentaActual = precio.Value;

            txtPrecioVenta.Text =
                precioVentaActual.ToString("N2");
        }

        private void CargarTallas()
        {
            try
            {
                CBTalla.DataSource = null;

                DataTable tallas =
                    ventaDAO.CargarTallas(idProductoSeleccionado);

                if (tallas.Rows.Count == 0)
                {
                    CBTalla.Enabled = false;

                    MessageBox.Show(
                        "Este producto no tiene tallas disponibles.",
                        "Sin stock",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                CBTalla.Enabled = true;

                CBTalla.DataSource = tallas;
                CBTalla.DisplayMember = "talla";
                CBTalla.ValueMember = "id_producto_talla";
                CBTalla.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las tallas:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarTipoCambio()
        {
            try
            {
                tipoCambioActual = ventaDAO.ObtenerTipoCambioActual();

                if (tipoCambioActual <= 0)
                {
                    tipoCambioActual = 0;
                }
            }
            catch
            {
                tipoCambioActual = 0;
            }
        }
       

        private void cmbTalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBTalla.SelectedIndex == -1)
                return;

            if (CBTalla.SelectedValue == null)
                return;

            try
            {
                if (CBTalla.SelectedValue is DataRowView)
                    return;

                idProductoTallaSeleccionado =
                    Convert.ToInt32(CBTalla.SelectedValue);

                CargarStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al seleccionar la talla:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarStock()
        {
            try
            {
                int stock =
                    ventaDAO.ObtenerStockProductoTalla(
                        idProductoTallaSeleccionado
                    );

                txtStockActual.Text = stock.ToString();

                if (stock <= 0)
                {
                    MessageBox.Show(
                        "Esta talla no tiene stock disponible.",
                        "Sin stock",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al obtener el stock:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarProducto())
                return;

            AgregarDetalle();
            CalcularTotales();
            LimpiarCampos();
        }

        // ==========================================================
        // VALIDAR PRODUCTO
        // ==========================================================
        private bool ValidarProducto()
        {
            if (CBproducto.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (CBTalla.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione una talla.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (precioVentaActual <= 0)
            {
                MessageBox.Show(
                    "El producto no tiene un precio de venta válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }


            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show(
                    "Ingrese una cantidad válida mayor que cero.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }
            if (!int.TryParse(
                txtStockActual.Text,
                out int stock))
            {
                MessageBox.Show(
                    "No se pudo obtener el stock disponible.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (stock <= 0)
            {
                MessageBox.Show(
                    "No hay stock disponible para esta talla.",
                    "Sin stock",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            if (cantidad > stock)
            {
                MessageBox.Show(
                    "La cantidad solicitada supera el stock disponible.\n\n" +
                    "Stock disponible: " + stock,
                    "Stock insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            return true;
        }

        private void AgregarDetalle()
        {
            int cantidad = Convert.ToInt32(txtCantidad.Text);

            decimal subtotal =
                CalcularSubtotalLinea(
                    precioVentaActual,
                    cantidad
                );

            dgvDetalleVenta.Rows.Add(
                idProductoTallaSeleccionado,
                CBproducto.Text,
                CBcategoria.Text,
                CBmarca.Text,
                CBTalla.Text,
                precioVentaActual,
                cantidad,
                subtotal
            );
        }

        private decimal CalcularSubtotalLinea(
           decimal precio,
           int cantidad)
        {
            return precio * cantidad;
        }

        private void CalcularTotales()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow fila in dgvDetalleVenta.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                // Usa "colSubtotal" en lugar de "Subtotal"
                if (fila.Cells["colSubtotal"].Value != null)
                {
                    subtotal += Convert.ToDecimal(fila.Cells["colSubtotal"].Value);
                }
            }

            decimal iva = subtotal * 0.15m;
            decimal total = subtotal + iva;

            lblSubtotal.Text = "C$ " + subtotal.ToString("N2");
            lblIVA.Text = "C$ " + iva.ToString("N2");
            lblTotal.Text = "C$ " + total.ToString("N2");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarUltimoDetalle();
            CalcularTotales();
        }

        private void EliminarUltimoDetalle()
        {
            if (dgvDetalleVenta.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay productos para eliminar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            dgvDetalleVenta.Rows.RemoveAt(
                dgvDetalleVenta.Rows.Count - 1
            );
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();

        }

        private void LimpiarCampos()
        {
            idProductoSeleccionado = 0;
            idProductoTallaSeleccionado = 0;
            precioVentaActual = 0;

            CBproducto.SelectedIndex = -1;

            CBcategoria.Text = "";
            CBmarca.Text = "";

            CBTalla.DataSource = null;
            CBTalla.Enabled = false;

            txtPrecioVenta.Clear();
            txtStockActual.Clear();

            txtCantidad.Text = "1";
        }

        private decimal ObtenerSubtotal()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow fila in dgvDetalleVenta.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                // Cambiar "Subtotal" por "colSubtotal" en la validación
                if (fila.Cells["colSubtotal"].Value != null)
                {
                    // Cambiar "Subtotal" por "colSubtotal" en la conversión
                    subtotal += Convert.ToDecimal(fila.Cells["colSubtotal"].Value);
                }
            }

            return subtotal;
        }

        private int ObtenerIdCliente()
        {
            if (CBcliente.SelectedIndex == -1)
                return 0;

            if (CBcliente.SelectedValue is DataRowView)
                return 0;

            return Convert.ToInt32(
                CBcliente.SelectedValue
            );
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (CBcliente.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un cliente.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (dgvDetalleVenta.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Debe agregar al menos un producto a la venta.",
                    "Venta vacía",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            decimal subtotal = ObtenerSubtotal();
            decimal iva = subtotal * 0.15m;
            decimal total = subtotal + iva;

            DataTable detalleVenta = CrearDetalleVenta();

            int idCliente = ObtenerIdCliente();

            int idCaja = ventaDAO.ObtenerIdCajaAbierta();

            if (idCaja <= 0)
            {
                MessageBox.Show(
                    "No existe una caja abierta.",
                    "Caja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // AQUÍ DEBES COLOCAR EL ID DEL USUARIO ACTUAL
            int idUsuario = 1;

            SubFormaPagoV formaPago = new SubFormaPagoV(
                txtCodigoVenta.Text,
                idCliente,
                idUsuario,
                idCaja,
                subtotal,
                iva,
                total,
                detalleVenta
            );

            DialogResult resultado =
                formaPago.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                Close();
            }

        }

        private DataTable CrearDetalleVenta()
        {
            DataTable tabla = new DataTable();

            tabla.Columns.Add("id_producto_talla", typeof(int));
            tabla.Columns.Add("cantidad", typeof(int));
            tabla.Columns.Add("precio_venta", typeof(decimal));
            tabla.Columns.Add("subtotal", typeof(decimal));

            foreach (DataGridViewRow fila in dgvDetalleVenta.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                DataRow nuevaFila = tabla.NewRow();

                // Asegúrate de usar "colID", "colCantidad", "colPrecio" y "colSubtotal"
                nuevaFila["id_producto_talla"] = Convert.ToInt32(fila.Cells["colIdProductoTalla"].Value);
                nuevaFila["cantidad"] = Convert.ToInt32(fila.Cells["colCantidad"].Value);
                nuevaFila["precio_venta"] = Convert.ToDecimal(fila.Cells["colPrecioVenta"].Value);
                nuevaFila["subtotal"] = Convert.ToDecimal(fila.Cells["colSubtotal"].Value);

                tabla.Rows.Add(nuevaFila);
            }

            return tabla;
        
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
