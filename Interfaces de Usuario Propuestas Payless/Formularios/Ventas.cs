
using System.Drawing.Printing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Interfaces_de_Usuario_Propuestas_Payless.Entidades;
//using static System.Net.WebRequestMethods;


namespace Interfaces_de_Usuario_Propuestas_Payless
{
    
    public partial class Ventas : Form
    {

        List<Producto> listaProductos = new List<Producto>();

        List<Clientes> listaCliente = new List<Clientes>();

        List<Venta> listaVentas = new List<Venta>();

        const decimal IVA_PORCENTAJE = 0.15m;

        const decimal TIPO_CAMBIO = 36.40m;

        int correlativoVenta = 1;

        int filaEditar = -1;

        string rutaVentas = "ventas.json";

        string rutaProductos = "productos.json";
        private Venta ventaActual;


        public Ventas()
        {
            InitializeComponent();
        }
       

        public class Producto
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public string Categoria { get; set; }
            public string Marca { get; set; }
            public int Talla { get; set; }
            public decimal Precio { get; set; }
            public int Stock { get; set; }
        }

        public class Clientes
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
        }

        public class DetalleVenta
        {
            public string CodigoProducto { get; set; }
            public string Producto { get; set; }
            public string Categoria { get; set; }
            public string Marca { get; set; }
            public int Talla { get; set; }
            public decimal PrecioVenta { get; set; }
            public int Cantidad { get; set; }
            public decimal Subtotal { get; set; }
        }

        public class Venta
        {
            public string CodigoVenta { get; set; }

            public string Cliente { get; set; }

            public DateTime Fecha { get; set; }

            public decimal Subtotal { get; set; }

            public decimal IVA { get; set; }

            public decimal Descuento { get; set; }

            public decimal Total { get; set; }

            public string FormaPago { get; set; }

            public decimal MontoCordobas { get; set; }

            public decimal MontoDolares { get; set; }

            public decimal Cambio { get; set; }

            public List<DetalleVenta> Detalles { get; set; }
        }









        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show(); this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show(); this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show(); this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
          
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show(); this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show(); this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Reportes_Venta ventana = new Reportes_Venta();
            ventana.Show(); this.Hide();

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Devoluciones_cs ventana = new Devoluciones_cs(); 
            ventana.Show();
            this.Hide();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label22_Click_1(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show(); this.Hide();
        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void CargarProductos()
        {
            if (File.Exists(rutaProductos))
            {
                string json =
                    File.ReadAllText(
                        rutaProductos);

                listaProductos =
                    JsonConvert.DeserializeObject<List<Producto>>(json);

                if (listaProductos == null)
                {
                    listaProductos =
                        new List<Producto>();
                }
            }
            else
            {
                listaProductos =
                    new List<Producto>()
                {
            new Producto()
            {
                Codigo="P001",
                Nombre="Zapato Escolar Negro",
                Categoria="Escolar",
                Marca="Bubble Gummers",
                Talla=30,
                Precio=1250,
                Stock=10
            },

            new Producto()
            {
                Codigo="P002",
                Nombre="Zapato Escolar Negro",
                Categoria="Escolar",
                Marca="Bubble Gummers",
                Talla=31,
                Precio=1250,
                Stock=8
            },

            new Producto()
            {
                Codigo="P003",
                Nombre="Tenis Deportivo Hombre",
                Categoria="Deportivo",
                Marca="Nike",
                Talla=40,
                Precio=2850,
                Stock=5
            },

            new Producto()
            {
                Codigo="P004",
                Nombre="Tenis Deportivo Hombre",
                Categoria="Deportivo",
                Marca="Nike",
                Talla=41,
                Precio=2850,
                Stock=7
            },

            new Producto()
            {
                Codigo="P005",
                Nombre="Sandalia Dama",
                Categoria="Casual",
                Marca="Skechers",
                Talla=37,
                Precio=1650,
                Stock=12
            }
                };

                GuardarProductos();
            }

            cmbProducto.Items.Clear();

            foreach (Producto p in listaProductos)
            {
                if (p.Stock > 0)
                {
                    if (!cmbProducto.Items.Contains(
                        p.Nombre))
                    {
                        cmbProducto.Items.Add(
                            p.Nombre);
                    }
                }
            }
        }

        private void GenerarCodigoVenta()
        {
            int siguiente = 1;

            if (listaVentas.Count > 0)
            {
                string ultimoCodigo =
                    listaVentas.Last().CodigoVenta;

                siguiente =
                    int.Parse(
                        ultimoCodigo.Substring(1)) + 1;
            }

            txtCodigoVenta.Text =
                "V" + siguiente.ToString("00000");
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            correlativoVenta++;

            GenerarCodigoVenta();

            dgvVenta.Rows.Clear();

            cmbCliente.SelectedIndex = -1;

            cmbProducto.SelectedIndex = -1;

            cmbTalla.Items.Clear();

            txtCodigoProducto.Clear();
            txtCategoria.Clear();
            txtMarca.Clear();
            txtPrecioVenta.Clear();
            txtCantidad.Clear();
            txtStockActual.Clear();

            txtSubtotal.Text = "0.00";
            txtIVA.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";

            txtMontoCordobas.Clear();
            txtMontoDolares.Clear();
            txtMontoTarjeta.Clear();
            txtMontoDolares.Clear();
        }



        private void Ventas_Load(object sender, EventArgs e)
        {
            CargarProductos();

            CargarCliente();

            GenerarCodigoVenta();

            CargarVentas();

            txtCantidad.KeyPress += SoloNumeros;

            txtMontoCordobas.KeyPress += SoloNumeros;

            txtMontoDolares.KeyPress += SoloNumeros;

            txtMontoTarjeta.KeyPress += SoloNumeros;

            txtCodigoVenta.ReadOnly = true;
            txtCodigoProducto.ReadOnly = true;
            txtCategoria.ReadOnly = true;
            txtMarca.ReadOnly = true;
            txtPrecioVenta.ReadOnly = true;
            txtStockActual.ReadOnly = true;

            txtSubtotal.Text = "0.00";
            txtIVA.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtTotal.Text = "0.00";

            rbEfectivo.Checked = true;

            cmbTipoTarjeta.Enabled = false;
            txtNumeroTarjeta.Enabled = false;
            txtMontoTarjeta.Enabled = false;

            cmbTipoTarjeta.Items.Add("Visa");
            cmbTipoTarjeta.Items.Add("MasterCard");
        }



        private void CargarCliente()
        {
            listaCliente.Clear();

            listaCliente.Add(new Clientes()
            {
                Id = 1,
                Nombre = "Juan Perez"
            });

            listaCliente.Add(new Clientes()
            {
                Id = 2,
                Nombre = "Maria Lopez"
            });

            listaCliente.Add(new Clientes()
            {
                Id = 3,
                Nombre = "Carlos Martinez"
            });

            listaCliente.Add(new Clientes()
            {
                Id = 4,
                Nombre = "Ana Gonzalez"
            });

            listaCliente.Add(new Clientes()
            {
                Id = 5,
                Nombre = "Jose Rodriguez"
            });

            cmbCliente.Items.Clear();

            foreach (Clientes c in listaCliente)
            {
                cmbCliente.Items.Add(c.Nombre);
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cmbProducto.SelectedIndex == -1)
                return;

            string nombreProducto = cmbProducto.Text;

            cmbTalla.Items.Clear();

            var tallas = listaProductos
                .Where(p => p.Nombre == nombreProducto && p.Stock > 0)
                .Select(p => p.Talla)
                .Distinct()
                .OrderBy(t => t);

            foreach (var talla in tallas)
            {
                cmbTalla.Items.Add(talla);
            }

            txtCodigoProducto.Clear();
            txtCategoria.Clear();
            txtMarca.Clear();
            txtPrecioVenta.Clear();
            txtStockActual.Clear();
        
    }

        private void cmbTalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedIndex == -1)
                return;

            if (cmbTalla.SelectedIndex == -1)
                return;

            string nombreProducto = cmbProducto.Text;

            int talla = Convert.ToInt32(cmbTalla.Text);

            Producto productoSeleccionado = listaProductos.FirstOrDefault(
                p => p.Nombre == nombreProducto &&
                     p.Talla == talla);

            if (productoSeleccionado != null)
            {
                txtCodigoProducto.Text = productoSeleccionado.Codigo;

                txtCategoria.Text = productoSeleccionado.Categoria;

                txtMarca.Text = productoSeleccionado.Marca;

                txtPrecioVenta.Text = productoSeleccionado.Precio.ToString("N2");

                txtStockActual.Text = productoSeleccionado.Stock.ToString();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Creas la instancia de la subpantalla
            NuevaVenta ventana = new NuevaVenta();

            // Con .Show() se abre sin bloquear ni ocultar el formulario principal
            ventana.Show();
            this.Show();
        }
        
        private void CargarVentas()
        {
            if (File.Exists(rutaVentas))
            {
                string json =
                    File.ReadAllText(rutaVentas);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    listaVentas =
                        JsonConvert.DeserializeObject<List<Venta>>(json);

                    if (listaVentas == null)
                    {
                        listaVentas =
                            new List<Venta>();
                    }
                }
            }
        }
        private void GuardarVentas()
        {
            string json =
                JsonConvert.SerializeObject(
                    listaVentas,
                    Formatting.Indented);

            File.WriteAllText(
                rutaVentas,
                json);
        }

        private void LimpiarProducto()
        {
            cmbProducto.SelectedIndex = -1;

            cmbTalla.Items.Clear();

            txtCodigoProducto.Clear();

            txtCategoria.Clear();

            txtMarca.Clear();

            txtPrecioVenta.Clear();

            txtCantidad.Clear();

            txtStockActual.Clear();
        }



        private void CalcularTotales()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                if (fila.Cells[8].Value != null)
                {
                    subtotal +=
                        Convert.ToDecimal(
                            fila.Cells[8].Value);
                }
            }

            decimal descuento =
                CalcularDescuento(subtotal);

            decimal iva =
                subtotal * 0.15m;

            decimal total =
                subtotal + iva - descuento;

            txtSubtotal.Text =
                subtotal.ToString("N2");

            txtDescuento.Text =
                descuento.ToString("N2");

            txtIVA.Text =
                iva.ToString("N2");

            txtTotal.Text =
                total.ToString("N2");
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvVenta.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una fila");
                return;
            }

            filaEditar = dgvVenta.CurrentRow.Index;

            txtCodigoProducto.Text =
                dgvVenta.Rows[filaEditar].Cells[0].Value.ToString();

            cmbProducto.Text =
                dgvVenta.Rows[filaEditar].Cells[1].Value.ToString();

            txtCategoria.Text =
                dgvVenta.Rows[filaEditar].Cells[2].Value.ToString();

            txtMarca.Text =
                dgvVenta.Rows[filaEditar].Cells[3].Value.ToString();

            cmbTalla.Text =
                dgvVenta.Rows[filaEditar].Cells[4].Value.ToString();

            txtPrecioVenta.Text =
                dgvVenta.Rows[filaEditar].Cells[5].Value.ToString();

            txtStockActual.Text =
                dgvVenta.Rows[filaEditar].Cells[6].Value.ToString();

            txtCantidad.Text =
                dgvVenta.Rows[filaEditar].Cells[7].Value.ToString();

            dgvVenta.Rows.RemoveAt(filaEditar);

            CalcularTotales();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvVenta.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una fila");
                return;
            }

            DialogResult respuesta =
                MessageBox.Show(
                    "¿Desea eliminar el producto?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                dgvVenta.Rows.RemoveAt(
                    dgvVenta.CurrentRow.Index);

                CalcularTotales();
            }



        }

        private decimal CalcularDescuento(decimal subtotal)
        {
            int totalCantidad = 0;

            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                if (fila.Cells[7].Value != null)
                {
                    totalCantidad +=
                        Convert.ToInt32(
                            fila.Cells[7].Value);
                }
            }

            if (totalCantidad >= 8)
            {
                return subtotal * 0.10m;
            }

            if (totalCantidad >= 5)
            {
                return subtotal * 0.08m;
            }

            if (totalCantidad >= 3)
            {
                return subtotal * 0.05m;
            }

            return 0;
        }


        private string ObtenerDescuento()
        {
            int totalCantidad = 0;

            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                if (fila.Cells[7].Value != null)
                {
                    totalCantidad +=
                        Convert.ToInt32(
                            fila.Cells[7].Value);
                }
            }

            if (totalCantidad >= 8)
                return "10%";

            if (totalCantidad >= 5)
                return "8%";

            if (totalCantidad >= 3)
                return "5%";

            return "0%";
        }

        private void rbEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEfectivo.Checked)
            {
                txtMontoCordobas.Enabled = true;
                txtMontoDolares.Enabled = true;

                cmbTipoTarjeta.Enabled = false;
                txtNumeroTarjeta.Enabled = false;
                txtMontoTarjeta.Enabled = false;

                cmbTipoTarjeta.SelectedIndex = -1;

                txtNumeroTarjeta.Clear();
                txtMontoTarjeta.Clear();
            }
        }

        private void rbTarjeta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTarjeta.Checked)
            {
                txtMontoCordobas.Enabled = false;
                txtMontoDolares.Enabled = false;

                txtMontoCordobas.Clear();
                txtMontoDolares.Clear();

                cmbTipoTarjeta.Enabled = true;
                txtNumeroTarjeta.Enabled = true;
                txtMontoTarjeta.Enabled = true;
            }
        }

        private void CalcularCambio()
        {
            decimal total =
        Convert.ToDecimal(txtTotal.Text);

            decimal cordobas = 0;
            decimal dolares = 0;

            decimal.TryParse(
                txtMontoCordobas.Text,
                out cordobas);

            decimal.TryParse(
                txtMontoDolares.Text,
                out dolares);

            decimal pagado =
                cordobas +
                (dolares * TIPO_CAMBIO);

            decimal cambio =
                pagado - total;

            if (cambio < 0)
                cambio = 0;

            txtCambio.Text =
                cambio.ToString("N2");
        }

        private void txtMontoCordobas_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
        }

        private void txtMontoDolares_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
           
        }

        private void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' &&
                txt.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private bool ValidarTarjeta()
        {
            if (cmbTipoTarjeta.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione el tipo de tarjeta");

                return false;
            }

            if (txtNumeroTarjeta.Text.Length != 16)
            {
                MessageBox.Show(
                    "La tarjeta debe tener 16 dígitos");

                return false;
            }

            if (!txtNumeroTarjeta.Text.All(char.IsDigit))
            {
                MessageBox.Show(
                    "Número de tarjeta inválido");

                return false;
            }

            decimal montoTarjeta;

            if (!decimal.TryParse(
                txtMontoTarjeta.Text,
                out montoTarjeta))
            {
                MessageBox.Show(
                    "Monto inválido");

                return false;
            }

            decimal total =
                Convert.ToDecimal(txtTotal.Text);

            if (montoTarjeta < total)
            {
                MessageBox.Show(
                    "El monto de la tarjeta no cubre el total.\n" +
                    "Faltan C$ " +
                    (total - montoTarjeta).ToString("N2"));

                return false;
            }

            return true;
        }

        private bool ValidarPagoEfectivo()
        {
          
            decimal total =
                Convert.ToDecimal(txtTotal.Text);

            decimal cordobas = 0;
            decimal dolares = 0;

            decimal.TryParse(
                txtMontoCordobas.Text,
                out cordobas);

            decimal.TryParse(
                txtMontoDolares.Text,
                out dolares);

            decimal pagado =
                cordobas +
                (dolares * TIPO_CAMBIO);

            if (pagado <= 0)
            {
                MessageBox.Show(
                    "Debe ingresar un monto de pago.");

                return false;
            }

            if (pagado < total)
            {
                MessageBox.Show(
                    "El monto ingresado es insuficiente.\n" +
                    "Faltan C$ " +
                    (total - pagado).ToString("N2"));

                return false;
            }

            return true;
        }
        

        private string ObtenerFormaPago()
        {
            if (rbEfectivo.Checked)
                return "Efectivo";

            if (rbTarjeta.Checked)
                return "Tarjeta";

            return "";
        }

        private bool ValidarPago()
        {
            if (rbEfectivo.Checked)
            {
                return ValidarPagoEfectivo();
            }

            if (rbTarjeta.Checked)
            {
                return ValidarTarjeta();
            }

            return false;
        }

        private List<DetalleVenta> ObtenerDetallesVenta()
        {
            List<DetalleVenta> detalles =
                new List<DetalleVenta>();

            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                DetalleVenta detalle =
                    new DetalleVenta();

                detalle.CodigoProducto =
                    fila.Cells[0].Value.ToString();

                detalle.Producto =
                    fila.Cells[1].Value.ToString();

                detalle.Categoria =
                    fila.Cells[2].Value.ToString();

                detalle.Marca =
                    fila.Cells[3].Value.ToString();

                detalle.Talla =
                    Convert.ToInt32(
                        fila.Cells[4].Value);

                detalle.PrecioVenta =
                    Convert.ToDecimal(
                        fila.Cells[5].Value);

                detalle.Cantidad =
                    Convert.ToInt32(
                        fila.Cells[7].Value);

                detalle.Subtotal =
                    Convert.ToDecimal(
                        fila.Cells[8].Value);

                detalles.Add(detalle);
            }

            return detalles;
        }
        private void ActualizarStock()
        {
            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                string codigo =
                    fila.Cells[0].Value.ToString();

                int cantidad =
                    Convert.ToInt32(
                        fila.Cells[7].Value);

                Producto producto =
                    listaProductos.FirstOrDefault(
                        p => p.Codigo == codigo);

                if (producto != null)
                {
                    producto.Stock =
                        producto.Stock - cantidad;

                    if (producto.Stock < 0)
                    {
                        producto.Stock = 0;
                    }
                    
                }
                
            }
            GuardarProductos();
        }
        private Venta CrearVenta()
        {
            Venta venta = new Venta();

            venta.CodigoVenta =
                txtCodigoVenta.Text;

            venta.Cliente =
                cmbCliente.Text;

            venta.Fecha =
                DateTime.Now;

            venta.Subtotal =
                Convert.ToDecimal(
                    txtSubtotal.Text);

            venta.IVA =
                Convert.ToDecimal(
                    txtIVA.Text);

            venta.Descuento =
                Convert.ToDecimal(
                    txtDescuento.Text);

            venta.Total =
                Convert.ToDecimal(
                    txtTotal.Text);

            venta.FormaPago =
                ObtenerFormaPago();

            decimal montoCordobas = 0;
            decimal montoDolares = 0;

            decimal.TryParse(
                txtMontoCordobas.Text,
                out montoCordobas);

            decimal.TryParse(
                txtMontoDolares.Text,
                out montoDolares);

            venta.MontoCordobas =
                montoCordobas;

            venta.MontoDolares =
                montoDolares;

            decimal cambio = 0;

            decimal.TryParse(
                txtCambio.Text,
                out cambio);

            venta.Cambio =
                cambio;

            venta.Detalles =
                ObtenerDetallesVenta();

            return venta;
        }
        private bool ValidarCliente()
        {
            if (cmbCliente.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un cliente");

                return false;
            }

            return true;
        }
        private bool ValidarDetalle()
        {
            int filas = 0;

            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                if (!fila.IsNewRow)
                    filas++;
            }

            if (filas == 0)
            {
                MessageBox.Show(
                    "Debe agregar productos");

                return false;
            }

            return true;
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            if(!ValidarCliente())
        return;

            if (!ValidarDetalle())
                return;

            if (!ValidarPago())
                return;

            Venta venta = CrearVenta();
            ventaActual = venta;

            listaVentas.Add(venta);
            GuardarVentas();

            ActualizarStock();
            CargarProductos();

            MessageBox.Show(
                "Venta guardada correctamente",
                "Éxito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            MostrarResumenVenta(venta);

            btnNuevaVenta.PerformClick();
        }

        private void GuardarProductos()
        {
            string json =
                JsonConvert.SerializeObject(
                    listaProductos,
                    Formatting.Indented);

            File.WriteAllText(
                rutaProductos,
                json);
        }

        private int ObtenerCantidadProductos()
        {
            int cantidad = 0;

            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                cantidad +=
                    Convert.ToInt32(
                        fila.Cells[7].Value);
            }

            return cantidad;
        }
        private void MostrarResumenVenta(Venta venta)
        {
            MessageBox.Show(
                "Código: " + venta.CodigoVenta +
                "\nCliente: " + venta.Cliente +
                "\nTotal: C$ " + venta.Total.ToString("N2") +
                "\nForma Pago: " + venta.FormaPago,
                "Resumen Venta");
        }

        private void btnImprimirFactura_Click(object sender, EventArgs e)
        {
            if (ventaActual == null)
            {
                MessageBox.Show(
                    "Primero debe guardar una venta");

                return;
            }

            printDialog1.Document = printDocument1;

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font titulo =
        new Font("Arial", 16, FontStyle.Bold);

            Font normal =
                new Font("Arial", 10);

            int y = 20;

            e.Graphics.DrawString(
                "PAYLESS",
                titulo,
                Brushes.Black,
                250,
                y);

            y += 40;

            e.Graphics.DrawString(
                "FACTURA",
                titulo,
                Brushes.Black,
                240,
                y);

            y += 50;

            e.Graphics.DrawString(
                "Venta: " +
                ventaActual.CodigoVenta,
                normal,
                Brushes.Black,
                20,
                y);

            y += 25;

            e.Graphics.DrawString(
                "Cliente: " +
                ventaActual.Cliente,
                normal,
                Brushes.Black,
                20,
                y);

            y += 25;

            e.Graphics.DrawString(
                "Fecha: " +
                ventaActual.Fecha.ToString(),
                normal,
                Brushes.Black,
                20,
                y);

            y += 40;

            e.Graphics.DrawString(
                "PRODUCTOS",
                titulo,
                Brushes.Black,
                20,
                y);

            y += 35;

            foreach (DetalleVenta item in ventaActual.Detalles)
            {
                string linea =
                    item.Producto +
                    " Talla:" +
                    item.Talla +
                    " Cant:" +
                    item.Cantidad +
                    " C$" +
                    item.Subtotal.ToString("N2");

                e.Graphics.DrawString(
                    linea,
                    normal,
                    Brushes.Black,
                    20,
                    y);

                y += 25;
            }

            y += 20;

            e.Graphics.DrawString(
                "Subtotal: C$ " +
                ventaActual.Subtotal.ToString("N2"),
                normal,
                Brushes.Black,
                20,
                y);

            y += 25;

            e.Graphics.DrawString(
                "IVA: C$ " +
                ventaActual.IVA.ToString("N2"),
                normal,
                Brushes.Black,
                20,
                y);

            y += 25;

            e.Graphics.DrawString(
                "Descuento: C$ " +
                ventaActual.Descuento.ToString("N2"),
                normal,
                Brushes.Black,
                20,
                y);

            y += 25;

            e.Graphics.DrawString(
                "TOTAL: C$ " +
                ventaActual.Total.ToString("N2"),
                titulo,
                Brushes.Black,
                20,
                y);

            y += 50;

            e.Graphics.DrawString(
                "Gracias por comprar en Payless",
                normal,
                Brushes.Black,
                20,
                y);
        }

        private void btnCargarVenta_Click(object sender, EventArgs e)
        {
             }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }
    }

}
