using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
	public interface IPagoServicio : IBaseServicio<Pago>
	{
		Task<Respuesta<Pago>> RealizarPago(Pago pago);
		Task<Respuesta<IEnumerable<Pago>>> ConsultarPagosPorPrestamo(int IdPrestamo);
	}
}
