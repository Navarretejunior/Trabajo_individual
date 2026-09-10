using Interfaces_de_Usuario_Propuestas_Payless.Datos;
using System;
using System.Data;
using System.Windows.Forms;
using System;
using System.Data;
using System.Windows.Forms;

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    public partial class Pusuario : Form
    {
        // =========================================================
        // OBJETO DAO
        // =========================================================

        UsuarioDAO usuarioDAO = new UsuarioDAO();


        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public Pusuario()
        {
            InitializeComponent();

            // Cargar eventos
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Click += btnCancelar_Click;
            this.Load += Pusuario_Load;
        }


        // =========================================================
        // EVENTO LOAD
        // =========================================================

        private void Pusuario_Load(object sender, EventArgs e)
        {
            CargarRoles();
            CargarEstados();

            // Contraseñas ocultas
            txtContraseña.PasswordChar = '●';
            txtConfirmar.PasswordChar = '●';
        }


        // =========================================================
        // CARGAR ROLES
        // =========================================================

        private void CargarRoles()
        {
            try
            {
                DataTable tabla = usuarioDAO.CargarRoles();

                cmbRol.DataSource = tabla;
                cmbRol.DisplayMember = "nombre_rol";
                cmbRol.ValueMember = "id_rol";

                cmbRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los roles.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // =========================================================
        // CARGAR ESTADOS
        // =========================================================

        private void CargarEstados()
        {
            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = 0;
        }


        // =========================================================
        // BOTÓN GUARDAR
        // =========================================================

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // =================================================
                // OBTENER DATOS DE LOS CONTROLES
                // =================================================

                string nombreUsuario =
                    txtUsuario.Text.Trim();

                string nombreCompleto =
                    txtCompleto.Text.Trim();

                string password =
                    txtContraseña.Text;

                string confirmarPassword =
                    txtConfirmar.Text;

                string telefono =
                    txtTelefono.Text.Trim();

                string cedula =
                    txtCedula.Text.Trim();

                string gmail =
                    txtGmail.Text.Trim();


                // =================================================
                // VALIDAR NOMBRE DE USUARIO
                // =================================================

                if (string.IsNullOrWhiteSpace(nombreUsuario))
                {
                    MessageBox.Show(
                        "Ingrese el nombre del usuario.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtUsuario.Focus();
                    return;
                }


                // =================================================
                // VALIDAR NOMBRE COMPLETO
                // =================================================

                if (string.IsNullOrWhiteSpace(nombreCompleto))
                {
                    MessageBox.Show(
                        "Ingrese el nombre completo.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtCompleto.Focus();
                    return;
                }


                // =================================================
                // VALIDAR CONTRASEÑA
                // =================================================

                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show(
                        "Ingrese una contraseña.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtContraseña.Focus();
                    return;
                }


                // =================================================
                // CONFIRMAR CONTRASEÑA
                // =================================================

                if (password != confirmarPassword)
                {
                    MessageBox.Show(
                        "Las contraseñas no coinciden.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtConfirmar.Focus();
                    return;
                }


                // =================================================
                // VALIDAR ROL
                // =================================================

                if (cmbRol.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Seleccione un rol.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    cmbRol.Focus();
                    return;
                }


                // =================================================
                // VALIDAR ESTADO
                // =================================================

                if (cmbEstado.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Seleccione un estado.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    cmbEstado.Focus();
                    return;
                }


                // =================================================
                // OBTENER ID DEL ROL
                // =================================================

                int idRol =
                    Convert.ToInt32(cmbRol.SelectedValue);


                // =================================================
                // OBTENER ESTADO
                // =================================================

                bool estado =
                    cmbEstado.SelectedItem.ToString() == "Activo";


                // =================================================
                // GUARDAR MEDIANTE USUARIODAO
                // =================================================

                bool resultado =
                    usuarioDAO.AgregarUsuario(
                        nombreUsuario,
                        nombreCompleto,
                        password,
                        idRol,
                        estado);


                // =================================================
                // RESULTADO
                // =================================================

                if (resultado)
                {
                    MessageBox.Show(
                        "Usuario agregado correctamente.",
                        "Guardar usuario",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Indicar a la ventana Usuario
                    // que se agregó correctamente
                    this.DialogResult =
                        DialogResult.OK;

                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo agregar el usuario.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al guardar el usuario.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // =========================================================
        // BOTÓN CANCELAR
        // =========================================================

        private void btnCancelar_Click(object sender, EventArgs e)
        {



            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}