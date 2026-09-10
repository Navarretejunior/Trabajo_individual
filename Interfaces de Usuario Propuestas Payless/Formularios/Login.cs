using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
//using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
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
    public partial class Login: Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnSesion_Click(object sender, EventArgs e)
        {


            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            if (usuario == "")
            {
                MessageBox.Show("Ingrese el usuario.");
                txtUsuario.Focus();
                return;
            }

            if (contraseña == "")
            {
                MessageBox.Show("Ingrese la contraseña.");
                txtContraseña.Focus();
                return;
            }


            UsuarioDAO usuarioDAO = new UsuarioDAO();

            if (usuarioDAO.IniciarSesion(usuario, contraseña))
            {
                MessageBox.Show("Bienvenido " + ClaseSesion.RolActual + " " + ClaseSesion.UsuarioActual);

                Menú_Principal ventana = new Menú_Principal();
                ventana.Show();
                this.Hide();
            }

            else 
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }











            /* switch (usuario)
             {
                 case "Leonel":
                     if (contraseña == "leonel123")
                     {
                         ClaseSesion.UsuarioActual = "Leonel";
                         ClaseSesion.RolActual = "ADMIN";
                     }
                     else { MessageBox.Show("Contraseña incorrecta"); return; }
                     break;

                 case "Kelly":
                     if (contraseña == "keling123")
                     {
                         ClaseSesion.UsuarioActual = "Keling";
                         ClaseSesion.RolActual = "KELING";
                     }
                     else { MessageBox.Show("Contraseña incorrecta"); return; }
                     break;

                 case "Paola":
                     if (contraseña == "paola123")
                     {
                         ClaseSesion.UsuarioActual = "Paola";
                         ClaseSesion.RolActual = "PAOLA";
                     }
                     else { MessageBox.Show("Contraseña incorrecta"); return; }
                     break;

                 case "Felipe":
                     if (contraseña == "felipe123")
                     {
                         ClaseSesion.UsuarioActual = "Felipe";
                         ClaseSesion.RolActual = "FELIPE";
                     }
                     else { MessageBox.Show("Contraseña incorrecta"); return; }
                     break;

                 case "Yubelkis":
                     if (contraseña == "yubelkis123")
                     {
                         ClaseSesion.UsuarioActual = "Yubelkis";
                         ClaseSesion.RolActual = "YUBELKIS";
                     }
                     else { MessageBox.Show("Contraseña incorrecta"); return; }
                     break;

                 default:
                     MessageBox.Show("Usuario no existe");
                     return;
             }

             MessageBox.Show("Bienvenido " + ClaseSesion.UsuarioActual + "!");
             this.Hide();
             new Menú_Principal().Show();*/
        }   
       

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Recuperacion_de_Cuenta ventana = new Recuperacion_de_Cuenta();
            ventana.Show();
            this.Hide();
        }

        private void btnProbarConexion_Click(object sender, EventArgs e)
        {
           
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
