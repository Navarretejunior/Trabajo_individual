using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless
{
    internal class ClaseEgresoCaja
    {

        public int IdEgreso { get; set; }

        public string Descripcion { get; set; }

        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        public int IdCaja { get; set; }
    }
}
