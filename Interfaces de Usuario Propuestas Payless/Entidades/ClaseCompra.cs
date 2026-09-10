using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseCompra
    {
        public int IdCompra { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public bool Estado { get; set; }

        public int IdProveedor { get; set; }

        internal ClaseDetalleCompra ClaseDetalleCompra
        {
            get => default;
            set
            {
            }
        }
    }
}
