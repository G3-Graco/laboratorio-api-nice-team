using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;

namespace Infrastructure.Repositorios
{
	public class PagoRepositorio : BaseRepositorio<Pago>, IPagoRepositorio
	{
		public PagoRepositorio(AppDbContext context) : base(context)
		{

		}
	}
}
