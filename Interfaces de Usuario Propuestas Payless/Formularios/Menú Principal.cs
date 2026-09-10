using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Menú_Principal : Form
    {

        public Menú_Principal()
        {
            InitializeComponent();
        }
        private void FormMenu_Load(object sender, EventArgs e)
        {
        }
        private void label15_Click(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Login ventana = new Login();
                ventana.Show();

                this.Hide();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de Salir de la aplicacion de escritorio?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();

                this.Hide();
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {


           

        }

        private void label17_Click(object sender, EventArgs e)
        {

            
        }
        

        private void label20_Click(object sender, EventArgs e)
        {


            

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Menú_Principal_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "Usuario: " +  ClaseSesion.UsuarioActual;
            lblRol.Text = "Rol: " + ClaseSesion.RolActual;

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

        private void lblCaja_Click(object sender, EventArgs e)
        {
            
        }

        private void lblVenta_Click(object sender, EventArgs e)
        {


            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();    
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {
            
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void lblVenta_Click_1(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void lblDevoluciones_Click(object sender, EventArgs e)
        {
            
        }

        private void lblCredito_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void lblCaja_Click_1(object sender, EventArgs e)
        {

            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void lblUsuarios_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void lblCliente_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void lblProveedores_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void lblProductos_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            

            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }
    }
    
}
