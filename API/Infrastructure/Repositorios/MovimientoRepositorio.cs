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

		public async ValueTask<IEnumerable<Movimiento>> ConsultarMovimientosDeUnaCuenta(Int64 idCuenta)
		{
			return await dbSet.Where(m => m.CuentaOrigenIdentificador == idCuenta || m.CuentaReceptoraIdentificador == idCuenta).ToListAsync();
		}

		public override async Task<IEnumerable<Movimiento>> ObtenerTodosAsincrono()
		{
			return await base.dbSet.Include(x => x.TipoMovimiento).ToListAsync();
		}
	}
}
