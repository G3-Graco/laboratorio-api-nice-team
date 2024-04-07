using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;

namespace Infrastructure.Repositorios
{
	public class CuentaRepositorio : BaseRepositorio<Cuenta>, ICuentaRepositorio
	{
		public CuentaRepositorio(AppDbContext context) : base(context)
		{

		}
	}
}
