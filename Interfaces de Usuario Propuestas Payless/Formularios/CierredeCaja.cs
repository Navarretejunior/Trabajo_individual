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
    public partial class CierredeCaja : Form
    {
        private CajaDao cajaDao;
        private ClaseCaja cajaActual;

        public CierredeCaja()
        {
            InitializeComponent();

            cajaDao = new CajaDao();

            // Asegurar que los botones ejecuten estos eventos
            button1.Click += button1_Click;
            button2.Click += button2_Click;
        }

        // =========================================================
        // OBTENER CAJA ABIERTA
        // =========================================================

        private void CargarCajaActual()
        {
            cajaActual =
                cajaDao.ObtenerCajaAbierta(
                    ClaseSesion.IdUsuario);

            if (cajaActual == null)
            {
                MessageBox.Show(
                    "No existe una caja abierta para el usuario actual.",
                    "Cierre de Caja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            CargarDatos();
        }

        // =========================================================
        // CARGAR DATOS EN EL FORMULARIO
        // =========================================================

        private void CargarDatos()
        {
            if (cajaActual == null)
                return;

            // -----------------------------------------------------
            // SALDO INICIAL
            // -----------------------------------------------------

            label3.Text =
                "C$ " +
                cajaActual.SaldoInicial.ToString("N2");


            // -----------------------------------------------------
            // INGRESOS
            // -----------------------------------------------------

            decimal ventas =
                cajaDao.ObtenerTotalIngresos(
                    cajaActual.IdCaja);

            label4.Text =
                "C$ " +
                ventas.ToString("N2");

            // Actualmente el DAO no tiene un método separado
            // para abonos de crédito.
            label7.Text =
                "C$ 0.00";


            // -----------------------------------------------------
            // EGRESOS
            // -----------------------------------------------------

            decimal egresos =
                cajaDao.ObtenerTotalEgresos(
                    cajaActual.IdCaja);

            // El DAO actual registra los egresos de caja
            // mediante egreso_caja.
            label9.Text =
                "C$ 0.00";

            label10.Text =
                "C$ " +
                egresos.ToString("N2");


            // -----------------------------------------------------
            // ARQUEO DE CAJA
            // -----------------------------------------------------

            decimal arqueo =
                cajaActual.MontoArqueo;

            label17.Text =
                "C$ " +
                arqueo.ToString("N2");


            // -----------------------------------------------------
            // SALDO FINAL
            // -----------------------------------------------------

            decimal saldoFinal =
                cajaActual.SaldoInicial +
                ventas -
                egresos;

            label18.Text =
                "C$ " +
                saldoFinal.ToString("N2");


            // -----------------------------------------------------
            // DIFERENCIA
            // -----------------------------------------------------

            decimal diferencia =
                arqueo -
                saldoFinal;

            if (diferencia < 0)
            {
                label19.Text =
                    "C$ " +
                    Math.Abs(diferencia).ToString("N2");

                label20.Text =
                    "C$ 0.00";
            }
            else
            {
                label19.Text =
                    "C$ 0.00";

                label20.Text =
                    "C$ " +
                    diferencia.ToString("N2");
            }


            // -----------------------------------------------------
            // EFECTIVO
            // -----------------------------------------------------

            // El arqueo guarda el monto total en córdobas,
            // por lo que este valor representa el efectivo
            // contabilizado en el arqueo.
            label24.Text =
                "C$ " +
                arqueo.ToString("N2");


            // El DAO actual no guarda por separado cuánto
            // corresponde exclusivamente a dólares.
            label26.Text =
                "C$ 0.00";


            // -----------------------------------------------------
            // TARJETA
            // -----------------------------------------------------

            // Actualmente no tenemos en CajaDao un método que
            // separe las ventas por forma de pago.
            label22.Text =
                "C$ 0.00";
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // -------------------------------------------------
                // VERIFICAR QUE EXISTA UNA CAJA ABIERTA
                // -------------------------------------------------

                if (cajaActual == null)
                {
                    MessageBox.Show(
                        "No existe una caja abierta para cerrar.",
                        "Cierre de Caja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                // -------------------------------------------------
                // OBTENER EL ARQUEO ACTUAL
                // -------------------------------------------------

                decimal montoArqueo =
                    cajaActual.MontoArqueo;


                // -------------------------------------------------
                // CONFIRMAR CIERRE
                // -------------------------------------------------

                DialogResult respuesta =
                    MessageBox.Show(
                        "¿Está seguro de que desea cerrar la caja?\n\n" +
                        "Monto del arqueo: C$ " +
                        montoArqueo.ToString("N2"),
                        "Cerrar Caja",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                {
                    return;
                }


                // -------------------------------------------------
                // CERRAR CAJA EN LA BASE DE DATOS
                // -------------------------------------------------

                bool resultado =
                    cajaDao.CerrarCaja(
                        cajaActual.IdCaja,
                        montoArqueo);


                if (resultado)
                {
                    MessageBox.Show(
                        "La caja se cerró correctamente.\n\n" +
                        "Monto final: C$ " +
                        montoArqueo.ToString("N2"),
                        "Cierre de Caja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);


                    // Volver a la pantalla principal de Caja
                    Caja ventana =
                        new Caja();

                    ventana.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo cerrar la caja.\n\n" +
                        "Es posible que la caja ya haya sido cerrada.",
                        "Cierre de Caja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cerrar la caja:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CierredeCaja_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCajaActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar el cierre de caja:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
