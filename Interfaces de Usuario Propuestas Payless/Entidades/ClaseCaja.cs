using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseCaja
    {

        public int IdCaja { get; set; }

        public DateTime FechaApertura { get; set; }

        public DateTime? FechaCierre { get; set; }

        public decimal SaldoInicial { get; set; }

        public decimal MontoEsperado { get; set; }

        public decimal MontoArqueo { get; set; }

        public decimal Diferencia { get; set; }

        public decimal SaldoFinal { get; set; }

        public decimal TipoCambioDolar { get; set; }

        public string EstadoCaja { get; set; }

        public int IdUsuario { get; set; }

        internal ClaseEgresoCaja ClaseEgresoCaja
        {
            get => default;
            set
            {
            }
        }

        internal ClaseVenta ClaseVenta
        {
            get => default;
            set
            {
            }
        }
    }
}
