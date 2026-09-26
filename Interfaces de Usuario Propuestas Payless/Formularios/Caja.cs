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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            txtUsuario.Text = ClaseSesion.UsuarioActual;
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
            txtUsuario.Clear();
            textBox4.Clear();
            textBox5.Clear();

            dataGridView1.Rows.Clear();
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

        private void CargarMovimientos()
        {
            if (cajaActual == null)
                return;

            try
            {
                dataGridView1.Rows.Clear();

                string query = @"
            SELECT descripcion, monto, fecha
            FROM egreso_caja
            WHERE id_caja = @id_caja
            ORDER BY fecha DESC;
        ";

                if (!conexionBD.AbrirConexion())
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos.");
                    return;
                }

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    query,
                    conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        cajaActual.IdCaja);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(
                                reader["descripcion"].ToString(),
                                "C$ " + Convert.ToDecimal(reader["monto"]).ToString("N2"),
                                Convert.ToDateTime(reader["fecha"])
                                    .ToString("dd/MM/yyyy HH:mm")
                            );
                        }
                    }
                }

                conexionBD.CerrarConexion();
            }
            catch (Exception ex)
            {
                conexionBD.CerrarConexion();

                MessageBox.Show(
                    "Error al cargar los movimientos:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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

            string concepto = textBox4.Text.Trim();
            decimal monto;

            if (string.IsNullOrWhiteSpace(concepto))
            {
                MessageBox.Show(
                    "Ingrese el concepto del movimiento.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBox4.Focus();
                return;
            }

            if (!decimal.TryParse(textBox5.Text, out monto))
            {
                MessageBox.Show(
                    "Ingrese un monto válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBox5.Focus();
                return;
            }

            if (monto <= 0)
            {
                MessageBox.Show(
                    "El monto debe ser mayor que cero.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBox5.Focus();
                return;
            }

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

                using (NpgsqlCommand cmd = new NpgsqlCommand(
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

                textBox4.Clear();
                textBox5.Clear();

                CargarMovimientos();
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
