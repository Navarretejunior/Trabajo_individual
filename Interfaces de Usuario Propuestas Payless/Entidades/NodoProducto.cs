using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Interfaces_de_Usuario_Propuestas_Payless.Ventas;

namespace Interfaces_de_Usuario_Propuestas_Payless.Entidades
{
    public class NodoProducto
    {
        public Producto Producto { get; set; }

        public NodoProducto Izquierdo { get; set; }

        public NodoProducto Derecho { get; set; }

        public NodoProducto(Producto producto)
        {
            Producto = producto;
            Izquierdo = null;
            Derecho = null;
        }
    }
}
