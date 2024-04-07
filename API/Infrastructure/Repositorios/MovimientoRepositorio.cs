using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;

namespace Infrastructure.Repositorios
{
	public class MovimientoRepositorio : BaseRepositorio<Movimiento>, IMovimientoRepositorio
	{
		public MovimientoRepositorio(AppDbContext context) : base(context)
		{

		}
	}
}
