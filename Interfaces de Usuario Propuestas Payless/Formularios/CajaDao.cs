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
    internal class CajaDao
    {
        private readonly ConexionBD conexionBD;

        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public CajaDao()
        {
            conexionBD = new ConexionBD();
        }


        // ============================================================
        // 1. VERIFICAR SI EXISTE UNA CAJA ABIERTA
        // ============================================================

        public bool ExisteCajaAbierta(int idUsuario)
        {
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return false;

                string sql = @"
                    SELECT COUNT(*)
                    FROM caja
                    WHERE id_usuario = @id_usuario
                    AND estado_caja = 'Abierta';
                ";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                    return cantidad > 0;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // ============================================================
        // 2. OBTENER LA CAJA ABIERTA DEL USUARIO
        // ============================================================

        public ClaseCaja ObtenerCajaAbierta(int idUsuario)
        {
            ClaseCaja caja = null;

            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return null;

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

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            caja = ConvertirCaja(reader);
                        }
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return caja;
        }


        // ============================================================
        // 3. OBTENER CAJA POR ID
        // ============================================================

        public ClaseCaja ObtenerCajaPorId(int idCaja)
        {
            ClaseCaja caja = null;

            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return null;

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
                    WHERE id_caja = @id_caja;
                ";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@id_caja", idCaja);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            caja = ConvertirCaja(reader);
                        }
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return caja;
        }


        // ============================================================
        // 4. CONVERTIR LOS DATOS DE LA BD A CLASECAJA
        // ============================================================

        private ClaseCaja ConvertirCaja(NpgsqlDataReader reader)
        {
            ClaseCaja caja = new ClaseCaja();

            caja.IdCaja =
                Convert.ToInt32(reader["id_caja"]);

            caja.FechaApertura =
                Convert.ToDateTime(reader["fecha_apertura"]);

            if (reader["fecha_cierre"] != DBNull.Value)
            {
                caja.FechaCierre =
                    Convert.ToDateTime(reader["fecha_cierre"]);
            }
            else
            {
                caja.FechaCierre = null;
            }

            caja.SaldoInicial =
                Convert.ToDecimal(reader["saldo_inicial"]);

            caja.MontoEsperado =
                Convert.ToDecimal(reader["monto_esperado"]);

            caja.MontoArqueo =
                Convert.ToDecimal(reader["monto_arqueo"]);

            caja.Diferencia =
                Convert.ToDecimal(reader["diferencia"]);

            if (reader["saldo_final"] != DBNull.Value)
            {
                caja.SaldoFinal =
                    Convert.ToDecimal(reader["saldo_final"]);
            }
            else
            {
                caja.SaldoFinal = 0;
            }

            caja.TipoCambioDolar =
                Convert.ToDecimal(reader["tipo_cambio_dolar"]);

            caja.EstadoCaja =
                reader["estado_caja"].ToString();

            caja.IdUsuario =
                Convert.ToInt32(reader["id_usuario"]);

            return caja;
        }


        // ============================================================
        // 5. ABRIR CAJA
        // ============================================================

        public int AbrirCaja(ClaseCaja caja)
        {
            // Verificar si el usuario ya tiene una caja abierta
            if (ExisteCajaAbierta(caja.IdUsuario))
            {
                return 0;
            }

            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return 0;

                string sql = @"
                    INSERT INTO caja
                    (
                        fecha_apertura,
                        saldo_inicial,
                        monto_esperado,
                        monto_arqueo,
                        diferencia,
                        saldo_final,
                        tipo_cambio_dolar,
                        estado_caja,
                        id_usuario
                    )
                    VALUES
                    (
                        CURRENT_TIMESTAMP,
                        @saldo_inicial,
                        @saldo_inicial,
                        0,
                        0,
                        NULL,
                        @tipo_cambio_dolar,
                        'Abierta',
                        @id_usuario
                    )
                    RETURNING id_caja;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@saldo_inicial",
                        caja.SaldoInicial);

                    cmd.Parameters.AddWithValue(
                        "@tipo_cambio_dolar",
                        caja.TipoCambioDolar);

                    cmd.Parameters.AddWithValue(
                        "@id_usuario",
                        caja.IdUsuario);

                    return Convert.ToInt32(
                        cmd.ExecuteScalar());
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // ============================================================
        // 6. OBTENER SALDO INICIAL
        // ============================================================

        public decimal ObtenerSaldoInicial(int idCaja)
        {
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return 0;

                string sql = @"
                    SELECT saldo_inicial
                    FROM caja
                    WHERE id_caja = @id_caja;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        idCaja);

                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado == null ||
                        resultado == DBNull.Value)
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(resultado);
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // ============================================================
        // 7. OBTENER TOTAL DE INGRESOS
        // ============================================================

        public decimal ObtenerTotalIngresos(int idCaja)
        {
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return 0;

                string sql = @"
                    SELECT COALESCE(SUM(total), 0)
                    FROM venta
                    WHERE id_caja = @id_caja
                    AND estado = TRUE;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        idCaja);

                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado == null ||
                        resultado == DBNull.Value)
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(resultado);
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // ============================================================
        // 8. OBTENER TOTAL DE EGRESOS
        // ============================================================

        public decimal ObtenerTotalEgresos(int idCaja)
        {
            NpgsqlConnection conexion = conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return 0;

                string sql = @"
                    SELECT COALESCE(SUM(monto), 0)
                    FROM egreso_caja
                    WHERE id_caja = @id_caja;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        idCaja);

                    object resultado =
                        cmd.ExecuteScalar();

                    if (resultado == null ||
                        resultado == DBNull.Value)
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(resultado);
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // ============================================================
        // 9. CALCULAR MONTO ESPERADO
        // ============================================================

        public decimal CalcularMontoEsperado(int idCaja)
        {
            decimal saldoInicial =
                ObtenerSaldoInicial(idCaja);

            decimal ingresos =
                ObtenerTotalIngresos(idCaja);

            decimal egresos =
                ObtenerTotalEgresos(idCaja);

            return saldoInicial +
                   ingresos -
                   egresos;
        }


        // ============================================================
        // 10. REGISTRAR MOVIMIENTO DE CAJA
        // ============================================================

        public bool RegistrarMovimiento(
            string descripcion,
            decimal monto,
            int idCaja)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                return false;

            if (monto <= 0)
                return false;

            NpgsqlConnection conexion =
                conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return false;

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

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@descripcion",
                        descripcion);

                    cmd.Parameters.AddWithValue(
                        "@monto",
                        monto);

                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        idCaja);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // ============================================================
        // 11. OBTENER MOVIMIENTOS DE LA CAJA
        // ============================================================

        public DataTable ObtenerMovimientos(int idCaja)
        {
            DataTable tabla = new DataTable();

            NpgsqlConnection conexion =
                conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return tabla;

                string sql = @"
                    SELECT
                        id_egreso AS ""ID"",
                        descripcion AS ""Concepto"",
                        monto AS ""Monto"",
                        fecha AS ""Fecha y Hora""
                    FROM egreso_caja
                    WHERE id_caja = @id_caja
                    ORDER BY fecha DESC;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        idCaja);

                    using (NpgsqlDataAdapter adapter =
                        new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }


        // ============================================================
        // 12. REGISTRAR ARQUEO
        // ============================================================

        public bool RegistrarArqueo(
            int idCaja,
            decimal montoArqueo)
        {
            if (montoArqueo < 0)
                return false;

            decimal montoEsperado =
                CalcularMontoEsperado(idCaja);

            decimal diferencia =
                montoArqueo - montoEsperado;

            NpgsqlConnection conexion =
                conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return false;

                string sql = @"
                    UPDATE caja
                    SET
                        monto_esperado = @monto_esperado,
                        monto_arqueo = @monto_arqueo,
                        diferencia = @diferencia
                    WHERE id_caja = @id_caja
                    AND estado_caja = 'Abierta';
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@monto_esperado",
                        montoEsperado);

                    cmd.Parameters.AddWithValue(
                        "@monto_arqueo",
                        montoArqueo);

                    cmd.Parameters.AddWithValue(
                        "@diferencia",
                        diferencia);

                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        idCaja);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // ============================================================
        // 13. OBTENER DIFERENCIA
        // ============================================================

        public decimal ObtenerDiferencia(
            int idCaja,
            decimal montoArqueo)
        {
            decimal montoEsperado =
                CalcularMontoEsperado(idCaja);

            return montoArqueo - montoEsperado;
        }


        // ============================================================
        // 14. CERRAR CAJA
        // ============================================================

        public bool CerrarCaja(
            int idCaja,
            decimal montoArqueo)
        {
            if (montoArqueo < 0)
                return false;

            decimal montoEsperado =
                CalcularMontoEsperado(idCaja);

            decimal diferencia =
                montoArqueo - montoEsperado;

            NpgsqlConnection conexion =
                conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return false;

                string sql = @"
                    UPDATE caja
                    SET
                        fecha_cierre = CURRENT_TIMESTAMP,
                        monto_esperado = @monto_esperado,
                        monto_arqueo = @monto_arqueo,
                        diferencia = @diferencia,
                        saldo_final = @saldo_final,
                        estado_caja = 'Cerrada'
                    WHERE id_caja = @id_caja
                    AND estado_caja = 'Abierta';
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@monto_esperado",
                        montoEsperado);

                    cmd.Parameters.AddWithValue(
                        "@monto_arqueo",
                        montoArqueo);

                    cmd.Parameters.AddWithValue(
                        "@diferencia",
                        diferencia);

                    cmd.Parameters.AddWithValue(
                        "@saldo_final",
                        montoArqueo);

                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        idCaja);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // ============================================================
        // 15. ACTUALIZAR MONTO ESPERADO
        // ============================================================

        public bool ActualizarMontoEsperado(int idCaja)
        {
            decimal montoEsperado =
                CalcularMontoEsperado(idCaja);

            NpgsqlConnection conexion =
                conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return false;

                string sql = @"
                    UPDATE caja
                    SET monto_esperado = @monto_esperado
                    WHERE id_caja = @id_caja
                    AND estado_caja = 'Abierta';
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@monto_esperado",
                        montoEsperado);

                    cmd.Parameters.AddWithValue(
                        "@id_caja",
                        idCaja);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }
        }


        // ============================================================
        // 16. OBTENER ÚLTIMA CAJA DEL USUARIO
        // ============================================================

        public ClaseCaja ObtenerUltimaCaja(int idUsuario)
        {
            ClaseCaja caja = null;

            NpgsqlConnection conexion =
                conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return null;

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
                    ORDER BY id_caja DESC
                    LIMIT 1;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue(
                        "@id_usuario",
                        idUsuario);

                    using (NpgsqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            caja = ConvertirCaja(reader);
                        }
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return caja;
        }


        // ============================================================
        // 17. OBTENER TODAS LAS CAJAS
        // ============================================================

        public DataTable ObtenerCajas()
        {
            DataTable tabla = new DataTable();

            NpgsqlConnection conexion =
                conexionBD.ObtenerConexion();

            try
            {
                if (!conexionBD.AbrirConexion())
                    return tabla;

                string sql = @"
                    SELECT
                        c.id_caja AS ""ID Caja"",
                        c.fecha_apertura AS ""Fecha Apertura"",
                        c.fecha_cierre AS ""Fecha Cierre"",
                        c.saldo_inicial AS ""Saldo Inicial"",
                        c.monto_esperado AS ""Monto Esperado"",
                        c.monto_arqueo AS ""Monto Arqueo"",
                        c.diferencia AS ""Diferencia"",
                        c.saldo_final AS ""Saldo Final"",
                        c.tipo_cambio_dolar AS ""Tipo Cambio"",
                        c.estado_caja AS ""Estado"",
                        u.nombre_completo AS ""Usuario""
                    FROM caja c
                    INNER JOIN usuario u
                        ON c.id_usuario = u.id_usuario
                    ORDER BY c.id_caja DESC;
                ";

                using (NpgsqlCommand cmd =
                    new NpgsqlCommand(sql, conexion))
                {
                    using (NpgsqlDataAdapter adapter =
                        new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabla);
                    }
                }
            }
            finally
            {
                conexionBD.CerrarConexion();
            }

            return tabla;
        }
    }
}
