using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public class ClaseMarca
    {

        public int IdMarca { get; set; }

        public string NombreMarca { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        internal ClaseProducto ClaseProducto
        {
            get => default;
            set
            {
            }
        }
    }
}
