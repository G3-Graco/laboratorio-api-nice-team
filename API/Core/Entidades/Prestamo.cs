namespace Core.Entidades
{
    public class Prestamo
    {
        public int Id {get; set;}
        public int NumeroCuotas {get; set;}
        public double MontoTotal {get; set;}
        public double CuotaMensual {get; set;}
        public DateTime Fecha {get; set;}
        public int Id_Estado {get; set;}
        public virtual Estado_Prestamo Estado {get; set;}
        //colocar clienteid
        //colocar plazoid
    }
}