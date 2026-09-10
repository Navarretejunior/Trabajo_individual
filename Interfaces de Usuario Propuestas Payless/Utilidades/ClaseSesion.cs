using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseSesion
    {

        public static int IdUsuario { get; set; }
        
        public static string UsuarioActual { get; set; }

        public static string RolActual { get; set; }

       /* public static string UsuarioActual;
        public static string RolActual;*/

        public ClaseUsuario ClaseUsuario
        {
            get => default;
            set
            {
            }
        }
    }
}
