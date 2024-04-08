namespace Core.Respuestas
{
    public class Respuesta<Entidad>
    {
        public bool Ok { get; set; }
        public string? Mensaje { get; set; }
        public Entidad? Datos { get; set; }
    }
}