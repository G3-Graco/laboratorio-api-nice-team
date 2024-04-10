using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorios
{
	public class MovimientoRepositorio : BaseRepositorio<Movimiento>, IMovimientoRepositorio
	{
		public MovimientoRepositorio(AppDbContext context) : base(context)
		{

		}
		public override async Task<IEnumerable<Movimiento>> ObtenerTodosAsincrono()
		{
			return await base.dbSet.Include(x => x.TipoMovimiento).ToListAsync();
		}
	}
}
