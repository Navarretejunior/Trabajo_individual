using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
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
    public partial class Recuperacion_de_Cuenta : Form
    {
        public Recuperacion_de_Cuenta()
        {
            InitializeComponent();
            lblCodigo.Visible = false;
            txtCodigo.Visible = false;
            btnValidar.Visible = false;

            lblNuevaPassword.Visible = false;
            txtNuevaPassword.Visible = false;

            lblConfirmarPassword.Visible = false;
            txtConfirmarPassword.Visible = false;

            btnCambiarPassword.Visible = false;

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void Recuperacion_de_Cuenta_Load(object sender, EventArgs e)
        {
            txtCorreo.TabIndex = 0;
            btnEnviar.TabIndex = 1;
            btnValidar.TabIndex = 2;
            button3.TabIndex = 3;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            Login ventana = new Login();
            ventana.Show();

            this.Hide();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            RecuperacionDAO dao = new RecuperacionDAO();

            string correo = txtCorreo.Text.Trim();

            if (correo == "")
            {
                MessageBox.Show("Ingrese un correo.");
                return;
            }

            if (!dao.ExisteCorreo(correo))
            {
                MessageBox.Show("El correo no existe.");
                return;
            }

            string codigo = dao.GenerarCodigo();

            if (dao.GuardarCodigo(correo, codigo))
            {

                CorreoDAO correoDAO = new CorreoDAO();

                if (correoDAO.EnviarCodigo(correo, codigo))
                {
                    MessageBox.Show("Se envió un código a su correo.");

                    lblCodigo.Visible = true;
                    txtCodigo.Visible = true;
                    btnValidar.Visible = true;
                }
                else
                {
                    MessageBox.Show("No fue posible enviar el correo.");
                }
                //  MessageBox.Show("Su código es: " + codigo);

                lblCodigo.Visible = true;
                txtCodigo.Visible = true;
                btnValidar.Visible = true;
            }
        }


        private void btnValidar_Click(object sender, EventArgs e)
        {
            RecuperacionDAO dao = new RecuperacionDAO();

            if (dao.ValidarCodigo(txtCorreo.Text, txtCodigo.Text))
            {
                MessageBox.Show("Código correcto.");

                lblNuevaPassword.Visible = true;
                txtNuevaPassword.Visible = true;

                lblConfirmarPassword.Visible = true;
                txtConfirmarPassword.Visible = true;

                btnCambiarPassword.Visible = true;
            }
            else
            {
                MessageBox.Show("Código incorrecto o vencido.");
            }
        }

        private void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            RecuperacionDAO dao = new RecuperacionDAO();

            if (txtNuevaPassword.Text != txtConfirmarPassword.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            if (dao.CambiarPassword(txtCorreo.Text, txtNuevaPassword.Text))
            {
                MessageBox.Show("Contraseña actualizada.");

                Login ventana = new Login();
                ventana.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("No fue posible cambiar la contraseña.");
            }
        }
    }
}
