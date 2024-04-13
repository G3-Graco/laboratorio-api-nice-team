using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
    public interface IPrestamoServicio : IBaseServicio<Prestamo>
    {
		Task<Respuesta<Prestamo>> ConsultarPrestamoValidado (int idUsuarioSesion, int idPrestamo);
		Task<Respuesta<Prestamo>> SolicitarPrestamo(int idUsuarioSesion, ModeloSolicitudPrestamo modeloSolicitudPrestamo);
	}
}