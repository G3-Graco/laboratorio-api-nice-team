using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;

namespace Infrastructure.Repositorios
{
	public class TipoMovimientoRepositorio : BaseRepositorio<TipoMovimiento>, ITipoMovimientoRepositorio
	{
		public TipoMovimientoRepositorio(AppDbContext context) : base(context)
		{

		}
	}
}
