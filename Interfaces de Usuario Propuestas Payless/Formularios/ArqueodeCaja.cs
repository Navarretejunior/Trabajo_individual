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
    public partial class ArqueodeCaja : Form
    {
        private CajaDao cajaDao;
        private ClaseCaja cajaActual;
        public ArqueodeCaja()
        {
            InitializeComponent();

            cajaDao = new CajaDao();

            // El botón Guardar no tenía el evento conectado
            button1.Click += button1_Click;

            // Actualizar totales al cambiar cualquier cantidad
            numericUpDown19.ValueChanged += CalcularTotales;
            numericUpDown20.ValueChanged += CalcularTotales;
            numericUpDown21.ValueChanged += CalcularTotales;
            numericUpDown22.ValueChanged += CalcularTotales;
            numericUpDown23.ValueChanged += CalcularTotales;
            numericUpDown24.ValueChanged += CalcularTotales;

            numericUpDown43.ValueChanged += CalcularTotales;
            numericUpDown44.ValueChanged += CalcularTotales;
            numericUpDown45.ValueChanged += CalcularTotales;
            numericUpDown46.ValueChanged += CalcularTotales;
            numericUpDown47.ValueChanged += CalcularTotales;
            numericUpDown48.ValueChanged += CalcularTotales;
            numericUpDown49.ValueChanged += CalcularTotales;
            numericUpDown1.ValueChanged += CalcularTotales;

            numericUpDown39.ValueChanged += CalcularTotales;
            numericUpDown40.ValueChanged += CalcularTotales;
            numericUpDown41.ValueChanged += CalcularTotales;
            numericUpDown42.ValueChanged += CalcularTotales;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ArqueodeCaja_Load(object sender, EventArgs e)
        {
            try
            {
                txtUsuario.Text = ClaseSesion.UsuarioActual;
                txtUsuario.ReadOnly = true;

                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;

                CargarCajaActual();

                ConfigurarResultados();

                CalcularTotales(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el arqueo de caja:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // OBTENER CAJA ABIERTA
        // =========================================================

        private void CargarCajaActual()
        {
            cajaActual = cajaDao.ObtenerCajaAbierta(ClaseSesion.IdUsuario);

            if (cajaActual == null)
            {
                MessageBox.Show(
                    "No hay una caja abierta para el usuario actual.",
                    "Arqueo de Caja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
        }

        // =========================================================
        // CONFIGURAR CAMPOS DE RESULTADO
        // =========================================================

        private void ConfigurarResultados()
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;
            textBox9.ReadOnly = true;
            textBox10.ReadOnly = true;
            textBox11.ReadOnly = true;
            textBox12.ReadOnly = true;
            textBox13.ReadOnly = true;
            textBox14.ReadOnly = true;
            textBox15.ReadOnly = true;
            textBox16.ReadOnly = true;
            textBox17.ReadOnly = true;
            textBox18.ReadOnly = true;
        }

        // =========================================================
        // CALCULAR TODOS LOS TOTALES
        // =========================================================

        private void CalcularTotales(object sender, EventArgs e)
        {
            try
            {
                // =================================================
                // DÓLARES
                // =================================================

                decimal dolar100 =
                    numericUpDown20.Value * 100;

                decimal dolar50 =
                    numericUpDown22.Value * 50;

                decimal dolar10 =
                    numericUpDown24.Value * 10;

                decimal dolar20 =
                    numericUpDown23.Value * 20;

                decimal dolar5 =
                    numericUpDown21.Value * 5;

                decimal dolar1 =
                    numericUpDown19.Value * 1;

                decimal totalDolares =
                    dolar100 +
                    dolar50 +
                    dolar10 +
                    dolar20 +
                    dolar5 +
                    dolar1;

                // Mostrar subtotales
                textBox1.Text = dolar100.ToString("N2");
                textBox2.Text = dolar50.ToString("N2");
                textBox3.Text = dolar10.ToString("N2");
                textBox4.Text = dolar20.ToString("N2");
                textBox5.Text = dolar5.ToString("N2");
                textBox6.Text = dolar1.ToString("N2");

                label9.Text = "$ " + totalDolares.ToString("N2");


                // =================================================
                // BILLETES DE CÓRDOBAS
                // =================================================

                decimal cordoba1000 =
                    numericUpDown44.Value * 1000;

                decimal cordoba500 =
                    numericUpDown49.Value * 500;

                decimal cordoba200 =
                    numericUpDown48.Value * 200;

                decimal cordoba100 =
                    numericUpDown47.Value * 100;

                decimal cordoba50 =
                    numericUpDown46.Value * 50;

                decimal cordoba20 =
                    numericUpDown45.Value * 20;

                decimal cordoba10 =
                    numericUpDown43.Value * 10;

                decimal cordoba5 =
                    numericUpDown1.Value * 5;

                decimal totalBilletesCordoba =
                    cordoba1000 +
                    cordoba500 +
                    cordoba200 +
                    cordoba100 +
                    cordoba50 +
                    cordoba20 +
                    cordoba10 +
                    cordoba5;

                // Mostrar subtotales
                textBox7.Text = cordoba1000.ToString("N2");
                textBox8.Text = cordoba500.ToString("N2");
                textBox9.Text = cordoba200.ToString("N2");
                textBox10.Text = cordoba100.ToString("N2");
                textBox11.Text = cordoba50.ToString("N2");
                textBox12.Text = cordoba20.ToString("N2");
                textBox13.Text = cordoba10.ToString("N2");
                textBox14.Text = cordoba5.ToString("N2");

                label4.Text =
                    "C$ " + totalBilletesCordoba.ToString("N2");


                // =================================================
                // MONEDAS
                // =================================================

                decimal moneda5 =
                    numericUpDown42.Value * 5;

                decimal moneda1 =
                    numericUpDown41.Value * 1;

                decimal moneda050 =
                    numericUpDown40.Value * 0.50m;

                decimal moneda025 =
                    numericUpDown39.Value * 0.25m;

                decimal totalMonedas =
                    moneda5 +
                    moneda1 +
                    moneda050 +
                    moneda025;

                textBox15.Text = moneda5.ToString("N2");
                textBox16.Text = moneda1.ToString("N2");
                textBox17.Text = moneda050.ToString("N2");
                textBox18.Text = moneda025.ToString("N2");

                label5.Text =
                    "C$ " + totalMonedas.ToString("N2");


                // =================================================
                // CONVERSIÓN DE DÓLARES A CÓRDOBAS
                // =================================================

                decimal tipoCambio = 0;

                if (cajaActual != null)
                {
                    tipoCambio = cajaActual.TipoCambioDolar;
                }

                decimal conversionDolar = 0;

                if (tipoCambio > 0)
                {
                    conversionDolar =
                        totalDolares * tipoCambio;
                }

                label8.Text =
                    "C$ " + conversionDolar.ToString("N2");


                // =================================================
                // TOTAL GENERAL
                // =================================================

                decimal montoTotal =
                    conversionDolar +
                    totalBilletesCordoba +
                    totalMonedas;

                label6.Text =
                    "C$ " + montoTotal.ToString("N2");
            }
            catch
            {
                // Evitar que el formulario se cierre
                // mientras se modifican cantidades.
            }
        }


        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cajaActual == null)
                {
                    MessageBox.Show(
                        "No hay una caja abierta.",
                        "Arqueo de Caja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Volver a calcular para asegurarnos
                CalcularTotales(null, null);

                decimal totalDolares =
                    (numericUpDown20.Value * 100) +
                    (numericUpDown22.Value * 50) +
                    (numericUpDown24.Value * 10) +
                    (numericUpDown23.Value * 20) +
                    (numericUpDown21.Value * 5) +
                    numericUpDown19.Value;

                decimal totalBilletesCordoba =
                    (numericUpDown44.Value * 1000) +
                    (numericUpDown49.Value * 500) +
                    (numericUpDown48.Value * 200) +
                    (numericUpDown47.Value * 100) +
                    (numericUpDown46.Value * 50) +
                    (numericUpDown45.Value * 20) +
                    (numericUpDown43.Value * 10) +
                    (numericUpDown1.Value * 5);

                decimal totalMonedas =
                    (numericUpDown42.Value * 5) +
                    (numericUpDown41.Value * 1) +
                    (numericUpDown40.Value * 0.50m) +
                    (numericUpDown39.Value * 0.25m);

                decimal conversionDolar =
                    totalDolares *
                    cajaActual.TipoCambioDolar;

                decimal montoTotal =
                    conversionDolar +
                    totalBilletesCordoba +
                    totalMonedas;

                if (montoTotal < 0)
                {
                    MessageBox.Show(
                        "El monto del arqueo no puede ser negativo.",
                        "Arqueo de Caja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                bool resultado =
                    cajaDao.RegistrarArqueo(
                        cajaActual.IdCaja,
                        montoTotal);

                if (resultado)
                {
                    decimal diferencia =
                        cajaDao.ObtenerDiferencia(
                            cajaActual.IdCaja,
                            montoTotal);

                    MessageBox.Show(
                        "Arqueo guardado correctamente.\n\n" +
                        "Monto arqueado: C$ " +
                        montoTotal.ToString("N2") +
                        "\n\nDiferencia: C$ " +
                        diferencia.ToString("N2"),
                        "Arqueo de Caja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Caja ventana = new Caja();
                    ventana.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo guardar el arqueo.",
                        "Arqueo de Caja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar el arqueo:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
