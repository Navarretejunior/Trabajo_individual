using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Interfaces_de_Usuario_Propuestas_Payless.Formularios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public partial class Caja : Form
    {
        private ConexionBD conexionBD;
        private ClaseCaja cajaActual;

        public Caja()
        {
            InitializeComponent();

            conexionBD = new ConexionBD();
        }

        private void Caja_Load(object sender, EventArgs e)
        {
            // =====================================================
            // NAVEGACIÓN
            // =====================================================

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

            // =====================================================
            // CARGAR CAJA
            // =====================================================

            CargarCaja();
        }

        private void CargarCaja()
        {
            try
            {
                string sql = @"
                    SELECT
                        id_caja,
                        fecha_apertura,
                        fecha_cierre,
                        saldo_inicial,
                        monto_esperado,
                        monto_arqueo,
                        diferencia,
                        saldo_final,
                        tipo_cambio_dolar,
                        estado_caja,
                        id_usuario
                    FROM caja
                    WHERE id_usuario = @id_usuario
                    AND estado_caja = 'Abierta'
                    ORDER BY id_caja DESC
                    LIMIT 1;
                ";

                if (!conexionBD.AbrirConexion())
                {
                    MessageBox.Show(
                        "No se pudo establecer conexión con la base de datos.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_usuario",
                        ClaseSesion.IdUsuario);

                    using (NpgsqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cajaActual = new ClaseCaja();

                            cajaActual.IdCaja =
                                Convert.ToInt32(reader["id_caja"]);

                            cajaActual.FechaApertura =
                                Convert.ToDateTime(reader["fecha_apertura"]);

                            cajaActual.FechaCierre =
                                reader["fecha_cierre"] == DBNull.Value
                                ? (DateTime?)null
                                : Convert.ToDateTime(reader["fecha_cierre"]);

                            cajaActual.SaldoInicial =
                                Convert.ToDecimal(reader["saldo_inicial"]);

                            cajaActual.MontoEsperado =
                                Convert.ToDecimal(reader["monto_esperado"]);

                            cajaActual.MontoArqueo =
                                Convert.ToDecimal(reader["monto_arqueo"]);

                            cajaActual.Diferencia =
                                Convert.ToDecimal(reader["diferencia"]);

                            cajaActual.SaldoFinal =
                                reader["saldo_final"] == DBNull.Value
                                ? 0
                                : Convert.ToDecimal(reader["saldo_final"]);

                            cajaActual.TipoCambioDolar =
                                Convert.ToDecimal(reader["tipo_cambio_dolar"]);

                            cajaActual.EstadoCaja =
                                reader["estado_caja"].ToString();

                            cajaActual.IdUsuario =
                                Convert.ToInt32(reader["id_usuario"]);
                        }
                        else
                        {
                            cajaActual = null;
                        }
                    }
                }

                conexionBD.CerrarConexion();

                if (cajaActual == null)
                {
                    MessageBox.Show(
                        "No hay una caja abierta para el usuario actual.",
                        "Caja",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LimpiarCaja();

                    return;
                }

                CargarDatosEnFormulario();
            }
            catch (Exception ex)
            {
                conexionBD.CerrarConexion();

                MessageBox.Show(
                    "Error al cargar la caja:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CargarDatosEnFormulario()
        {
            if (cajaActual == null)
                return;

            List<TextBox> textBoxes =
                ObtenerTextBox(this);

            if (textBoxes.Count >= 1)
                textBoxes[0].Text =
                    ClaseSesion.UsuarioActual;

            if (textBoxes.Count >= 2)
                textBoxes[1].Text =
                    cajaActual.SaldoInicial.ToString("N2");

            decimal ingresos =
                ObtenerIngresos();

            decimal egresos =
                ObtenerEgresos();

            decimal saldoFinal =
                cajaActual.SaldoInicial +
                ingresos -
                egresos;

            if (textBoxes.Count >= 3)
                textBoxes[2].Text =
                    ingresos.ToString("N2");

            if (textBoxes.Count >= 4)
                textBoxes[3].Text =
                    egresos.ToString("N2");

            if (textBoxes.Count >= 5)
                textBoxes[4].Text =
                    saldoFinal.ToString("N2");
        }

        // =========================================================
        // OBTENER TODOS LOS TEXTBOX DEL FORMULARIO
        // =========================================================

        private List<TextBox> ObtenerTextBox(Control control)
        {
            List<TextBox> resultado =
                new List<TextBox>();

            foreach (Control elemento in control.Controls)
            {
                if (elemento is TextBox)
                {
                    resultado.Add(
                        (TextBox)elemento);
                }

                if (elemento.HasChildren)
                {
                    resultado.AddRange(
                        ObtenerTextBox(elemento));
                }
            }

            resultado = resultado
                .OrderBy(x => x.TabIndex)
                .ToList();

            return resultado;
        }

        // =========================================================
        // OBTENER TOTAL DE INGRESOS
        // =========================================================

        private decimal ObtenerIngresos()
        {
            if (cajaActual == null)
                return 0;

            decimal resultado = 0;

            try
            {
                string sql = @"
                    SELECT COALESCE(SUM(total), 0)
                    FROM venta
                    WHERE id_caja = @id_caja
                    AND estado = TRUE;
                ";

                if (!conexionBD.AbrirConexion())
                    return 0;

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        cajaActual.IdCaja);

                    resultado =
                        Convert.ToDecimal(
                            cmd.ExecuteScalar());
                }

                conexionBD.CerrarConexion();
            }
            catch
            {
                conexionBD.CerrarConexion();
            }

            return resultado;
        }

        // =========================================================
        // OBTENER TOTAL DE EGRESOS
        // =========================================================

        private decimal ObtenerEgresos()
        {
            if (cajaActual == null)
                return 0;

            decimal resultado = 0;

            try
            {
                string sql = @"
                    SELECT COALESCE(SUM(monto), 0)
                    FROM egreso_caja
                    WHERE id_caja = @id_caja;
                ";

                if (!conexionBD.AbrirConexion())
                    return 0;

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        cajaActual.IdCaja);

                    resultado =
                        Convert.ToDecimal(
                            cmd.ExecuteScalar());
                }

                conexionBD.CerrarConexion();
            }
            catch
            {
                conexionBD.CerrarConexion();
            }

            return resultado;
        }

        // =========================================================
        // LIMPIAR INFORMACIÓN
        // =========================================================

        private void LimpiarCaja()
        {
            List<TextBox> textBoxes =
                ObtenerTextBox(this);

            foreach (TextBox txt in textBoxes)
            {
                txt.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {
            Menú_Principal ventana = new Menú_Principal();
            ventana.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Productos ventana = new Productos();
            ventana.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Proveedores ventana = new Proveedores();
            ventana.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Usuario ventana = new Usuario();
            ventana.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Cliente ventana = new Cliente();
            ventana.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Compras_nuevo ventana = new Compras_nuevo();
            ventana.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            inventario ventana = new inventario();
            ventana.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Credito ventana = new Credito();
            ventana.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Caja ventana = new Caja();
            ventana.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAperturadecaja_Click(object sender, EventArgs e)
        {
            AperturaCaja ventana =
                new AperturaCaja(
                    ClaseSesion.IdUsuario,
                    ClaseSesion.UsuarioActual);

            ventana.Show();

            this.Hide();
        }

        private void btnArqueodecaja_Click(object sender, EventArgs e)
        {
            if (cajaActual == null)
            {
                MessageBox.Show(
                    "No existe una caja abierta.",
                    "Caja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            ArqueodeCaja ventana =
                new ArqueodeCaja();

            ventana.Show();

            this.Hide();
        }

        private void btnCierredecaja_Click(object sender, EventArgs e)
        {
            if (cajaActual == null)
            {
                MessageBox.Show(
                    "No existe una caja abierta.",
                    "Caja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            CierredeCaja ventana =
                new CierredeCaja();

            ventana.Show();

            this.Hide();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            Mantenimiento ventana = new Mantenimiento();
            ventana.Show();
            this.Hide();
        }

        private void groupBox3_Enter_1(object sender, EventArgs e)
        {

        }

        private void btnGuardarMovimiento_Click(object sender, EventArgs e)
        {
            if (cajaActual == null)
            {
                MessageBox.Show(
                    "No existe una caja abierta.",
                    "Caja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            List<TextBox> textBoxes =
                ObtenerTextBox(this);

            if (textBoxes.Count < 7)
            {
                MessageBox.Show(
                    "No se encontraron todos los campos necesarios del formulario.",
                    "Caja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // =====================================================
            // CONCEPTO Y MONTO
            // =====================================================

            string concepto =
                textBoxes[5].Text.Trim();

            decimal monto;

            if (string.IsNullOrWhiteSpace(concepto))
            {
                MessageBox.Show(
                    "Ingrese el concepto del movimiento.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBoxes[5].Focus();

                return;
            }

            if (!decimal.TryParse(
                textBoxes[6].Text,
                out monto))
            {
                MessageBox.Show(
                    "Ingrese un monto válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBoxes[6].Focus();

                return;
            }

            if (monto <= 0)
            {
                MessageBox.Show(
                    "El monto debe ser mayor que cero.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBoxes[6].Focus();

                return;
            }

            // =====================================================
            // GUARDAR EN EGRESO_CAJA
            // =====================================================

            try
            {
                string sql = @"
                    INSERT INTO egreso_caja
                    (
                        descripcion,
                        monto,
                        fecha,
                        id_caja
                    )
                    VALUES
                    (
                        @descripcion,
                        @monto,
                        CURRENT_TIMESTAMP,
                        @id_caja
                    );
                ";

                if (!conexionBD.AbrirConexion())
                {
                    MessageBox.Show(
                        "No se pudo conectar con la base de datos.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(
                        sql,
                        conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@descripcion",
                        concepto);

                    cmd.Parameters.AddWithValue(
                        "@monto",
                        monto);

                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        cajaActual.IdCaja);

                    cmd.ExecuteNonQuery();
                }

                conexionBD.CerrarConexion();

                MessageBox.Show(
                    "Movimiento guardado correctamente.",
                    "Caja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                textBoxes[5].Clear();
                textBoxes[6].Clear();

                CargarCaja();
            }
            catch (Exception ex)
            {
                conexionBD.CerrarConexion();

                MessageBox.Show(
                    "Error al guardar el movimiento:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
