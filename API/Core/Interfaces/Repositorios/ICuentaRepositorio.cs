using Core.Entidades;

namespace Core.Interfaces.Repositorios
{
	public interface ICuentaRepositorio : IBaseRepositorio<Cuenta>
	{
		ValueTask<Cuenta> ConsultarCuentaDeUnCliente(int idCliente);
		ValueTask<Cuenta> ObtenerPorIdAsincrono(Int64 id);
	}
}
