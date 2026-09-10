using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    class ClaseCliente
    {

        public int IdCliente { get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Cedula { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public bool Estado { get; set; }

        internal ClaseVenta ClaseVenta
        {
            get => default;
            set
            {
            }
        }

        
    }
}
