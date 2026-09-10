using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless
{



    public partial class Cliente : Form
    {
        ClienteDAO clienteDAO = new ClienteDAO(); // 👈 CONEXIÓN AQUÍ


        public Cliente()
        {
            InitializeComponent();
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                DGVtabla1.DataSource = clienteDAO.MostrarClientes();
                AjustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message);

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
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();

            this.Hide();
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            DGVtabla1.DataSource = clienteDAO.MostrarClientes();

            CmbBusqueda.Items.Clear();

            CmbBusqueda.Items.Add("Nombre");
            CmbBusqueda.Items.Add("Cédula");
            CmbBusqueda.Items.Add("Teléfono");

            CmbBusqueda.SelectedIndex = 0;

            DGVtabla1.DataSource = clienteDAO.MostrarClientes();
        }




        private void label15_Click(object sender, EventArgs e)
        {
            new Productos().Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label14_Click_1(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label15_Click_1(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label17_Click_1(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Devoluciones_cs ventana = new Devoluciones_cs();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label20_Click_1(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


        }


        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void CBbusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CBestado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCargar_Click(object sender, EventArgs e)
        {

        }

        private void DGVtabla1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregar_Click_2(object sender, EventArgs e)
        {

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtcedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void AjustarColumnas()
        {
            DGVtabla1.Columns[0].Width = 200;
            DGVtabla1.Columns[1].Width = 150;
            DGVtabla1.Columns[2].Width = 150;
            DGVtabla1.Columns[3].Width = 150;
            DGVtabla1.Columns[4].Width = 150;
        }

        private void btnAgregar_Click_3(object sender, EventArgs e)
        {
            SubClienteAgregar ventana = new SubClienteAgregar();
            ventana.Show();
            this.Hide();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarCliente ventana = new EditarCliente();
            ventana.Show();
            this.Hide();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(DGVtabla1.CurrentRow.Cells["id_cliente"].Value);

            bool eliminado = clienteDAO.EliminarCliente(id);

            if (eliminado)
                MessageBox.Show("Cliente eliminado");

            DGVtabla1.DataSource = clienteDAO.MostrarClientes();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (CmbBusqueda.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Selecciona una opción para buscar.",
                    "Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string opcion = CmbBusqueda.Text.Trim();
            string dato = txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(dato))
            {
                MessageBox.Show(
                    "Escribe el dato que deseas buscar.",
                    "Búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtBuscar.Focus();
                return;
            }

            try
            {
                switch (opcion)
                {
                    case "Nombre":

                        DGVtabla1.DataSource =
                            clienteDAO.BuscarPorNombre(dato);

                        break;

                    case "Cédula":

                        DGVtabla1.DataSource =
                            clienteDAO.BuscarPorCedula(dato);

                        break;

                    case "Teléfono":

                        DGVtabla1.DataSource =
                            clienteDAO.BuscarPorTelefono(dato);

                        break;

                    default:

                        MessageBox.Show(
                            "Selecciona Nombre, Cédula o Teléfono.",
                            "Búsqueda",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                }

                AjustarColumnas();

                if (DGVtabla1.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No se encontraron clientes.",
                        "Búsqueda",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar clientes:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
        }

        private void label26_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void lblProveedores_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
                ventana.Show();
            this.Hide();
        }
    }
}
