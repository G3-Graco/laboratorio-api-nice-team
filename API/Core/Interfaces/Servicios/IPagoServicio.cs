using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
	public interface IPagoServicio : IBaseServicio<Pago>
	{
		Task<Respuesta<IEnumerable<Pago>>> ConsultarPagosDeUnaCuenta(int idUsuarioSesion);
	}
}
