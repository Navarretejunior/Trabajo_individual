using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseInventario
    {

        public int IdInventario { get; set; }

        public int StockActual { get; set; }

        public int StockMinimo { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public int IdProductoTalla { get; set; }

        
    }
}
