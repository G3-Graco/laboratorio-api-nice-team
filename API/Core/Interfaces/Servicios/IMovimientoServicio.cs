using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
	public interface IMovimientoServicio : IBaseServicio<Movimiento>
	{
		Task<Respuesta<Movimiento>> RealizarTransferencia(Movimiento movimiento);
		Task<Respuesta<Movimiento>> RealizarDeposito(Movimiento movimiento);
		Task<Respuesta<Movimiento>> RealizarRetiro(Movimiento movimiento);
		Task<Respuesta<IEnumerable<Movimiento>>> ConsultarMovimientosDeUnaCuenta(int idUsuarioSesion);
	}
}
