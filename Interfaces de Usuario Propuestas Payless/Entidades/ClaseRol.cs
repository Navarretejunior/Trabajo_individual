using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseRol
    {

        public int IdRol { get; set; }

        public string NombreRol { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public ClaseUsuario ClaseUsuario
        {
            get => default;
            set
            {
            }
        }
    }
}
