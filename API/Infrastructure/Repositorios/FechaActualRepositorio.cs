using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;

namespace Infrastructure.Repositorios
{
	public class FechaActualRepositorio : BaseRepositorio<FechaActual>, IFechaActualRepositorio
	{
		public FechaActualRepositorio(AppDbContext context) : base(context)
		{

		}
	}
}
