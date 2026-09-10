using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    public partial class SubProveedorEditar : Form
    {
        ProveedorDAO proveedorDAO = new ProveedorDAO();
        public SubProveedorEditar()
        {
            InitializeComponent();
        }

        private void SubProveedorEditar_Load(object sender, EventArgs e)
        {
            CargarProveedores();

            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = 0;

            txtCodigo.ReadOnly = true;
        }
        private void CargarProveedores()
        {
            cmbProveedor.DataSource =
                proveedorDAO.CargarProveedoresEditar();

            cmbProveedor.DisplayMember =
                "nombre";

            cmbProveedor.ValueMember =
                "id_proveedor";

            cmbProveedor.SelectedIndex = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un proveedor.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show(
                    "Seleccione un proveedor válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idProveedor =
                Convert.ToInt32(
                    cmbProveedor.SelectedValue);

            CargarProveedor(idProveedor);
        }
        private void CargarProveedor(int idProveedor)
        {
            ClaseProveedor proveedor =
                proveedorDAO.ObtenerProveedor(idProveedor);

            if (proveedor == null)
            {
                MessageBox.Show(
                    "No se encontró el proveedor.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            txtCodigo.Text =
                proveedor.IdProveedor.ToString();

            txtNombre.Text =
                proveedor.Nombre;

            txtTelefono.Text =
                proveedor.Telefono;

            txtCorreo.Text =
                proveedor.Correo;

            txtDireccion.Text =
                proveedor.Direccion;

            txtRuc.Text =
                proveedor.Ruc;

            cmbEstado.SelectedItem =
                proveedor.EstadoProveedor
                ? "Activo"
                : "Inactivo";
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            // ---------------------------------------------
            // Verificar que primero se haya buscado
            // ---------------------------------------------

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show(
                    "Primero busque un proveedor.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // ---------------------------------------------
            // Validar nombre
            // ---------------------------------------------

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre del proveedor.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNombre.Focus();
                return;
            }

            // ---------------------------------------------
            // Validar estado
            // ---------------------------------------------

            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione el estado del proveedor.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbEstado.Focus();
                return;
            }

            // ---------------------------------------------
            // Crear objeto proveedor
            // ---------------------------------------------

            ClaseProveedor proveedor =
                new ClaseProveedor();

            proveedor.IdProveedor =
                Convert.ToInt32(
                    txtCodigo.Text);

            proveedor.Nombre =
                txtNombre.Text.Trim();

            proveedor.Telefono =
                txtTelefono.Text.Trim();

            proveedor.Correo =
                txtCorreo.Text.Trim();

            proveedor.Direccion =
                txtDireccion.Text.Trim();

            proveedor.Ruc =
                txtRuc.Text.Trim();

            proveedor.EstadoProveedor =
                cmbEstado.SelectedItem.ToString()
                == "Activo";

            // ---------------------------------------------
            // Actualizar
            // ---------------------------------------------

            if (proveedorDAO.EditarProveedor(proveedor))
            {
                MessageBox.Show(
                    "Proveedor actualizado correctamente.",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo actualizar el proveedor.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}
