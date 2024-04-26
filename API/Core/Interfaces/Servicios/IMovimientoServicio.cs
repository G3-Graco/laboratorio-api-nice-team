using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
	public interface IMovimientoServicio : IBaseServicio<Movimiento>
	{
		Task<Respuesta<Movimiento>> RealizarTransferencia(int idUsuarioSesion, Movimiento movimiento);
		Task<Respuesta<Movimiento>> RealizarDeposito(Movimiento movimiento);
		Task<Respuesta<Movimiento>> RealizarRetiro(int idUsuarioSesion, Movimiento movimiento);
		Task<Respuesta<IEnumerable<Movimiento>>> ConsultarMovimientosDeUnaCuenta(int idUsuarioSesion);

		Task<Respuesta<Movimiento>> RealizarMovimiento(int idUsuarioSesion, Movimiento movimiento);
	}
}
