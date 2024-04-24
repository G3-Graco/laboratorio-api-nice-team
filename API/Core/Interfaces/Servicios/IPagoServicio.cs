using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
	public interface IPagoServicio : IBaseServicio<Pago>
	{
		Task<Respuesta<Pago>> RealizarPago(int idusuariosesion, Pago pago);
		Task<Respuesta<IEnumerable<Pago>>> ConsultarPagosPorPrestamo(int IdPrestamo);
		Task<Respuesta<IEnumerable<Pago>>> ConsultarPagosDeUnaCuenta(int idUsuarioSesion);

        Task<Respuesta<IEnumerable<Pago>>> ConsultarPagosPorPrestamoValidado(int idusuariosesion, int idPrestamo);
    }
}
