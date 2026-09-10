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

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    public partial class SubClienteAgregar : Form
    {
        public SubClienteAgregar()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar nombre
            if (string.IsNullOrWhiteSpace(q.Text))
            {
                MessageBox.Show("Ingrese el nombre del cliente.");
                q.Focus();
                return;
            }

            // Validar cédula
            if (string.IsNullOrWhiteSpace(txtcedula.Text))
            {
                MessageBox.Show("Ingrese el número de cédula.");
                txtcedula.Focus();
                return;
            }

            // Validar teléfono
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Ingrese el número de teléfono.");
                txtTelefono.Focus();
                return;
            }

            // Validar estado
            if (CBestado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un estado.");
                CBestado.Focus();
                return;
            }

            // Validar dirección
            if (string.IsNullOrWhiteSpace(richtxtDirreccion.Text))
            {
                MessageBox.Show("Ingrese la dirección.");
                richtxtDirreccion.Focus();
                return;
            }

            // Crear objeto cliente
            ClaseCliente cliente = new ClaseCliente();

            cliente.Nombre = q.Text.Trim();
            cliente.Cedula = txtcedula.Text.Trim();
            cliente.Telefono = txtTelefono.Text.Trim();
            cliente.Direccion = richtxtDirreccion.Text.Trim();
            cliente.Estado = CBestado.Text == "Activo";

            // Guardar cliente
            ClienteDAO dao = new ClienteDAO();

            if (dao.AgregarCliente(cliente))
            {
                MessageBox.Show(
                    "Cliente agregado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Limpiar campos
                q.Clear();
                txtcedula.Clear();
                txtTelefono.Clear();
                richtxtDirreccion.Clear();
                CBestado.SelectedIndex = -1;

                q.Focus();
            }
            else
            {
                MessageBox.Show(
                    "No fue posible agregar el cliente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }
    }
}
