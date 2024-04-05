using System;

namespace Core.Entidades
{
    public class Cuota
    {
        public int Id {get; set;}
        public DateTime Fecha {get; set;}
        public DateTime FechaPago {get; set;}
        public int Id_Prestamo {get; set;}
        public virtual Prestamo prestamo {get; set;}
    }
}