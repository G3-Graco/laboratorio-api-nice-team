using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorios
{
	public class PagoRepositorio : BaseRepositorio<Pago>, IPagoRepositorio
	{
		public PagoRepositorio(AppDbContext context) : base(context)
		{

		}
		public override async Task<IEnumerable<Pago>> ObtenerTodosAsincrono()
		{
			return await base.dbSet.Include(x => x.CuentaOrigen).Include(x => x.CuotaPagada).ToListAsync();			
		}
	}
}
