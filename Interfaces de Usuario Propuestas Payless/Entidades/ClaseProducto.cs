using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public class ClaseProducto
    {

        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public decimal PrecioVenta { get; set; }

        public bool EstadoProducto { get; set; }

        public int IdCategoria { get; set; }

        public int IdMarca { get; set; }

        public int IdProveedor { get; set; }

        internal ClaseDetalleVenta ClaseDetalleVenta
        {
            get => default;
            set
            {
            }
        }

        internal ClaseDetalleCompra ClaseDetalleCompra
        {
            get => default;
            set
            {
            }
        }

        internal ClaseInventario ClaseInventario
        {
            get => default;
            set
            {
            }
        }
    }
}
