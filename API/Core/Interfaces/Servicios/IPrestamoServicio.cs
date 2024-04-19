using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
    public interface IPrestamoServicio : IBaseServicio<Prestamo>
    {
		Task<Respuesta<Prestamo>> ConsultarPrestamoValidado (int idUsuarioSesion, int idPrestamo);
		Task<Respuesta<Prestamo>> SolicitarPrestamo(int idUsuarioSesion, ModeloSolicitudPrestamo modeloSolicitudPrestamo);
        Task<Respuesta<Documento>> GuardarDocumento(Documento documento);
        Task<Respuesta<IEnumerable<Documento>>> ConsultarDocumentos(int IdPrestamo);
        Task<Respuesta<IEnumerable<Prestamo>>> ConsultarPrestamosDeCliente(int IdCliente);

        Task<Respuesta<Double>> ConsultarMontoPendientePrestamo(int idusuariosesion, int IdPrestamo);
    }
}