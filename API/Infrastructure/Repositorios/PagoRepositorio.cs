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

		public async ValueTask<IEnumerable<Pago>> ConsultarPagosDeUnaCuenta(Int64 idCuenta)
		{
			return await dbSet.Where(p => p.CuentaIdentificador == idCuenta).Include(x => x.CuentaOrigen).Include(x => x.CuotaPagada).ToListAsync();
		}

        public async ValueTask<IEnumerable<Pago>> ConsultarPagosDeUnPrestamo(int idPrestamo)
        {
            return await dbSet.Where(p => p.CuotaId == idPrestamo).Include(x => x.CuentaOrigen).Include(x => x.CuotaPagada).ToListAsync();
        }

        public override async Task<IEnumerable<Pago>> ObtenerTodosAsincrono()
		{
			return await base.dbSet.Include(x => x.CuentaOrigen).Include(x => x.CuotaPagada).ToListAsync();			
		}
	}
}
