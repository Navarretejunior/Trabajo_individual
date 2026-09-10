using Interfaces_de_Usuario_Propuestas_Payless.Conexion;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    internal class AperturaCajaDAO
    {
        private ConexionBD conexionBD;

        public AperturaCajaDAO()
        {
            conexionBD = new ConexionBD();
        }

        // Verificar si ya existe una caja abierta
        public bool ExisteCajaAbierta()
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT COUNT(*)
                    FROM caja
                    WHERE estado_caja = 'Abierta'";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    sql, conexionBD.ObtenerConexion()))
                {
                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                    return cantidad > 0;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // Guardar la apertura de caja
        public bool GuardarApertura(
            decimal saldoInicial,
            decimal tipoCambioDolar,
            int idUsuario)
        {
            try
            {
                conexionBD.AbrirConexion();

                // No permitir abrir otra caja si ya existe una abierta
                string verificar = @"
                    SELECT COUNT(*)
                    FROM caja
                    WHERE estado_caja = 'Abierta'";

                using (NpgsqlCommand cmdVerificar = new NpgsqlCommand(
                    verificar, conexionBD.ObtenerConexion()))
                {
                    int cantidad = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                    if (cantidad > 0)
                    {
                        return false;
                    }
                }


                string sql = @"
                    INSERT INTO caja
                    (
                        saldo_inicial,
                        monto_esperado,
                        monto_arqueo,
                        diferencia,
                        tipo_cambio_dolar,
                        estado_caja,
                        id_usuario
                    )
                    VALUES
                    (
                        @saldoInicial,
                        @montoEsperado,
                        0,
                        0,
                        @tipoCambioDolar,
                        'Abierta',
                        @idUsuario
                    )";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                   sql, conexionBD.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue(
                        "@saldoInicial", saldoInicial);

                    cmd.Parameters.AddWithValue(
                        "@montoEsperado", saldoInicial);

                    cmd.Parameters.AddWithValue(
                        "@tipoCambioDolar", tipoCambioDolar);

                    cmd.Parameters.AddWithValue(
                        "@idUsuario", idUsuario);

                    int filas = cmd.ExecuteNonQuery();

                    return filas > 0;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // Obtener la caja que está abierta
        public DataTable ObtenerCajaAbierta()
        {
            DataTable tabla = new DataTable();

            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT
                        c.id_caja,
                        c.fecha_apertura,
                        c.saldo_inicial,
                        c.monto_esperado,
                        c.monto_arqueo,
                        c.diferencia,
                        c.saldo_final,
                        c.tipo_cambio_dolar,
                        c.estado_caja,
                        c.id_usuario,
                        u.nombre_completo
                    FROM caja c
                    INNER JOIN usuario u
                        ON c.id_usuario = u.id_usuario
                    WHERE c.estado_caja = 'Abierta'
                    ORDER BY c.id_caja DESC
                    LIMIT 1";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    sql, conexionBD.ObtenerConexion()))
                {
                    using (NpgsqlDataAdapter adapter =
                           new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabla);
                    }
                }

                return tabla;
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }

        // Obtener el ID de la caja abierta
        public int ObtenerIdCajaAbierta()
        {
            try
            {
                conexionBD.AbrirConexion();

                string sql = @"
                    SELECT id_caja
                    FROM caja
                    WHERE estado_caja = 'Abierta'
                    ORDER BY id_caja DESC
                    LIMIT 1";

                using (NpgsqlCommand cmd = new NpgsqlCommand(
                    sql, conexionBD.ObtenerConexion()))
                {
                    object resultado = cmd.ExecuteScalar();

                    if (resultado == null)
                        return 0;

                    return Convert.ToInt32(resultado);
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }
    }
}

