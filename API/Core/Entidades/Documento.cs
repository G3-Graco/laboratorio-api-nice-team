namespace Core.Entidades
{
    public class Documento
    {
        public int Id {get; set;}
        public byte[] documento {get; set;}
        public int Id_Tipo {get; set;}
        public virtual Tipo_Documento Tipo {get; set;}
        public int Id_Prestamo {get; set;}
        public virtual Prestamo prestamo {get; set;}
    }
}