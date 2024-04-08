namespace Core.Entidades
{
    public class Prestamo
    {
        public int Id {get; set;}
        public int NumeroCuotas {get; set;}
        public double MontoTotal {get; set;}
        public double CuotaMensual {get; set;}
        public DateTime Fecha {get; set;}
        public int IdEstado {get; set;}
        public virtual EstadoPrestamo? Estado {get; set;}
        public int IdCliente {get; set;}
        public virtual Cliente cliente {get; set;}
        public int IdPlazo {get; set;}
        public virtual Plazo? plazo {get; set;}
    }
}