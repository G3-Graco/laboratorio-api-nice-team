using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entidades
{
    public class Plazo
    {
        public int Id { get; set; }
        public double Porcentaje { get; set; }
        public int MinimoCuotas { get; set; }
        public int MaximaCuotas { get; set; }
    }
}