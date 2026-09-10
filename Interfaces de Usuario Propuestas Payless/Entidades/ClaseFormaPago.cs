using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseFormaPago
    {
        public int IdPago { get; set; }

        public string TipoPago { get; set; }

        public decimal MontoCordobas { get; set; }

        public decimal MontoDolares { get; set; }

        public decimal TipoCambio { get; set; }

        public decimal Cambio { get; set; }

        public string TipoTarjeta { get; set; }

        public decimal MontoTarjeta { get; set; }

        public int IdVenta { get; set; }





    }
}
