namespace Core.Entidades
{
    public class Documento
    {
        public int Id {get; set;}
        public byte[] documento {get; set;}
        public int IdTipo {get; set;}
        public virtual TipoDocumento Tipo {get; set;}
        public int IdPrestamo {get; set;}
        public virtual Prestamo prestamo {get; set;}
    }
}