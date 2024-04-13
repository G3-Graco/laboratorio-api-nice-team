using Core.Entidades;

namespace Core.Interfaces.Repositorios
{
	public interface IMovimientoRepositorio : IBaseRepositorio<Movimiento>
	{
		ValueTask<IEnumerable<Movimiento>> ConsultarMovimientosDeUnaCuenta(Int64 idCuenta);
	}
}
