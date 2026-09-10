using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseDetalleCompra
    {
        public int IdDetalleCompra { get; set; }

        public int IdCompra { get; set; }

        public int IdProductoTalla { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioCompra { get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal Subtotal { get; set; }
    }
}
