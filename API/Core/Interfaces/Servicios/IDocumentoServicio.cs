using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
    public interface IDocumentoServicio : IBaseServicio<Documento>
    {
        Task<Respuesta<byte[]>> ConvertirAByte(string url);
        Task<Respuesta<byte[]>> ConvertirAByte(Stream archivo);
        Task<Respuesta<Documento>> Descargar(Documento documento);
    }
}