using Core.Entidades;

namespace Core.Interfaces.Repositorios
{
	public interface IPagoRepositorio : IBaseRepositorio<Pago>
	{
		
		ValueTask<IEnumerable<Pago>> ConsultarPagosDeUnaCuenta(Int64 idCuenta);
	}
}
