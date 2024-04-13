using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
	public interface IMovimientoServicio : IBaseServicio<Movimiento>
	{
		Task<Respuesta<IEnumerable<Movimiento>>> ConsultarMovimientosDeUnaCuenta(int idUsuarioSesion);
	}
}
