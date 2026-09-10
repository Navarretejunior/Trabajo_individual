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
    public partial class AperturaCaja : Form
    {
        private AperturaCajaDAO aperturaCajaDAO;

        // Usuario que inició sesión
        private int idUsuario;
        private string nombreUsuario;

        public AperturaCaja(int idUsuario, string nombreUsuario)
        {
            InitializeComponent();
          
            aperturaCajaDAO = new AperturaCajaDAO();

            dtpFecha.Value = DateTime.Now;
            dtpHora.Value = DateTime.Now;


        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void AperturaCaja_Load(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Now;
            dtpHora.Value = DateTime.Now;

            dtpFecha.Value = DateTime.Now;
            dtpHora.Value = DateTime.Now;

            lblUsuario.Text = ClaseSesion.UsuarioActual;


        }

        private void btnAperturarCaja_Click(object sender, EventArgs e)
        {
            try
            {
                decimal montoInicial;
                decimal cambioDolar;

                if (!decimal.TryParse(txtMontoInicial.Text.Trim(), out montoInicial))
                {
                    MessageBox.Show(
                        "Ingrese un monto inicial válido.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtMontoInicial.Focus();
                    return;
                }

                if (montoInicial < 0)
                {
                    MessageBox.Show(
                        "El monto inicial no puede ser negativo.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtMontoInicial.Focus();
                    return;
                }

                if (!decimal.TryParse(txtCambioDolar.Text.Trim(), out cambioDolar))
                {
                    MessageBox.Show(
                        "Ingrese un cambio de dólar válido.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtCambioDolar.Focus();
                    return;
                }

                if (cambioDolar <= 0)
                {
                    MessageBox.Show(
                        "El cambio de dólar debe ser mayor que cero.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtCambioDolar.Focus();
                    return;
                }

                if (aperturaCajaDAO.ExisteCajaAbierta())
                {
                    MessageBox.Show(
                        "Ya existe una caja abierta.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // USUARIO AUTOMÁTICO DEL LOGIN
                int idUsuario = ClaseSesion.IdUsuario;

                bool resultado = aperturaCajaDAO.GuardarApertura(
                    montoInicial,
                    cambioDolar,
                    idUsuario);

                if (resultado)
                {
                    MessageBox.Show(
                        "La caja se aperturó correctamente.",
                        "Apertura de Caja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Caja frmCaja = new Caja();
                    frmCaja.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo aperturar la caja.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al aperturar la caja:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtMontoInicial.Clear();
            txtCambioDolar.Clear();

            dtpFecha.Value = DateTime.Now;
            dtpHora.Value = DateTime.Now;

            txtMontoInicial.Focus();
        }
    }
}
