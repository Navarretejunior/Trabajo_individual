using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    public class ClaseUsuario
    {





        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public string NombreCompleto { get; set; }

        public string Correo { get; set; }

        public string Password { get; set; }

        public bool Estado { get; set; }

        public string CodigoRecuperacion { get; set; }

        public DateTime? VenceCodigo { get; set; }

        public string Telefono { get; set; }

        public string Cedula { get; set; }

        public int IdRol { get; set; }


        // Usuario con múltiples validaciones en una sola línea
        /*public string Usuario
        {
            get => _username;
            set => _username =
                (!string.IsNullOrWhiteSpace(value) &&
                 value.Length >= 4 &&
                 value.Length <= 15 &&
                 !value.Contains(" ") &&
                 value.All(char.IsLetterOrDigit))
                ? value
                : throw new Exception("Usuario inválido: debe tener 4-15 caracteres, sin espacios y solo letras/números");
        }

        // Contraseña con múltiples validaciones en una sola línea
        public string Contraseña
        {
            get => _password;
            set => _password =
                (!string.IsNullOrWhiteSpace(value) &&
                 value.Length >= 6 &&
                 value.Length <= 40 &&
                 value.Any(char.IsUpper) &&   // al menos una mayúscula
                 value.Any(char.IsLower) &&   // al menos una minúscula
                 value.Any(char.IsDigit) &&   // al menos un número
                 value.Any(ch => "!@#$%^&*_-".Contains(ch)) && // carácter especial
                 !value.Contains(" "))        // sin espacios
                ? value
                : throw new Exception("Contraseña inválida: debe tener mayúscula, minúscula, número, símbolo y 6-40 caracteres");
        }*/

        internal ClaseVenta ClaseVenta
        {
            get => default;
            set
            {
            }
        }

        internal ClaseCaja ClaseCaja
        {
            get => default;
            set
            {
            }
        }

        internal ClaseLogin ClaseLogin
        {
            get => default;
            set
            {
            }
        }

        

        internal ClaseVenta ClaseVenta1
        {
            get => default;
            set
            {
            }
        }

       

        // Constructor
        



    }
}
