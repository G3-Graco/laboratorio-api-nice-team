using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
    public interface IPrestamoServicio : IBaseServicio<Prestamo>
    {
        Task<Respuesta<Documento>> GuardarDocumento(Documento documento);
        Task<Respuesta<IEnumerable<Documento>>> ConsultarDocumentos(int IdPrestamo);
        Task<Respuesta<IEnumerable<Prestamo>>> ConsultarPrestamosDeCliente(int IdCliente);
    }
}