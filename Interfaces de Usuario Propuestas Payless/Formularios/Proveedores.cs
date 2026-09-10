
using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using Newtonsoft.Json;
using Npgsql;
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
using static Interfaces_de_Usuario_Propuestas_Payless.ClaseProveedor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Proveedores : Form
    {
        ProveedorDAO proveedorDAO = new ProveedorDAO();

        private DataTable tablaProveedores;

        public Proveedores()
        {
            InitializeComponent();

            //ConfigurarDataGridView();
            // ConfigurarComboBuscar();

            //CargarProveedores();

            // Conectar el botón Buscar con su evento
            btnBuscar.Click += btnBuscar_Click;

            // Cargar el evento del formulario
            this.Load += Proveedores_Load;
        }





        private void ConfigurarComboBuscar()
        {

        }

        private void Proveedores_Load(object sender, EventArgs e)
        {


            cmbBuscar.Items.Clear();

            cmbBuscar.Items.Add("Todos");
            cmbBuscar.Items.Add("Activos");
            cmbBuscar.Items.Add("Inactivos");

            cmbBuscar.SelectedIndex = 0;

            CargarProveedores();



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

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click_1(object sender, EventArgs e)
        {

        }

        private void label17_Click_1(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click_1(object sender, EventArgs e)
        {


        }

        private void label9_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
        }

        private void label27_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label28_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SubProovedoresAgregar ventana = new SubProovedoresAgregar();
            ventana.Show(); this.Hide();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            SubProveedorEditar ventana = new SubProveedorEditar();
            ventana.Show(); this.Hide();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


        }

        private void btnCargar_Click(object sender, EventArgs e)
        {

        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        private void CargarProveedores()
        {
            try
            {
                DataTable tabla = proveedorDAO.MostrarProveedores();

                if (tabla == null || tabla.Columns.Count == 0)
                {
                    MessageBox.Show("No se encontraron datos de proveedores.");
                    return;
                }

                DGVtabla1.DataSource = tabla;

                if (DGVtabla1.Columns.Contains("estado"))
                {
                    DGVtabla1.Columns["estado"].HeaderText = "Estado";
                }

                AjustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores:\n\n" + ex.Message);
            }
        }


        private void AjustarColumnas()
        {
            if (DGVtabla1.Columns.Contains("id_proveedor"))
                DGVtabla1.Columns["id_proveedor"].Width = 80;

            if (DGVtabla1.Columns.Contains("nombre"))
                DGVtabla1.Columns["nombre"].Width = 200;

            if (DGVtabla1.Columns.Contains("direccion"))
                DGVtabla1.Columns["direccion"].Width = 250;

            if (DGVtabla1.Columns.Contains("ruc"))
                DGVtabla1.Columns["ruc"].Width = 150;

            if (DGVtabla1.Columns.Contains("estado"))
            {
                DGVtabla1.Columns["estado"].Width = 100;
                DGVtabla1.Columns["estado"].HeaderText = "Estado";
            }
        }



        private void label30_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void dgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (DGVtabla1.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un proveedor.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);


                return;
            }


            int idProveedor =
                Convert.ToInt32(
                    DGVtabla1.CurrentRow.Cells["id_proveedor"].Value);


            string nombre =
                DGVtabla1.CurrentRow.Cells["nombre"].Value.ToString();


            DialogResult respuesta =
                MessageBox.Show(
                    "¿Está seguro de eliminar al proveedor:\n\n" +
                    nombre + "?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);


            if (respuesta == DialogResult.Yes)
            {
                bool eliminado =
                    proveedorDAO.EliminarProveedor(idProveedor);


                if (eliminado)
                {
                    MessageBox.Show(
                        "Proveedor eliminado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);


                    CargarProveedores();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo eliminar el proveedor.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

            }
        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                string filtro = cmbBuscar.Text.Trim();

                DataTable tabla;

                if (filtro == "Todos")
                {
                    tabla = proveedorDAO.MostrarProveedores();
                }
                else if (filtro == "Activos")
                {
                    tabla = proveedorDAO.BuscarPorEstado(true);
                }
                else if (filtro == "Inactivos")
                {
                    tabla = proveedorDAO.BuscarPorEstado(false);
                }
                else
                {
                    return;
                }

                DGVtabla1.DataSource = tabla;

                if (DGVtabla1.Columns.Contains("estado"))
                {
                    DGVtabla1.Columns["estado"].HeaderText = "Estado";
                }

                AjustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
    }

}






    



