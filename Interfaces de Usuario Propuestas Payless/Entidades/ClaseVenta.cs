using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseVenta
    {

        public int IdVenta { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Descuento { get; set; }

        public decimal Iva { get; set; }

        public decimal Total { get; set; }

        public bool Estado { get; set; }

        public int IdCliente { get; set; }

        public int IdUsuario { get; set; }

        public int IdCaja { get; set; }

        

        internal ClaseDetalleVenta ClaseDetalleVenta
        {
            get => default;
            set
            {
            }
        }

        internal ClaseFormaPago ClaseFormaPago
        {
            get => default;
            set
            {
            }
        }
    }
}
