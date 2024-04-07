using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;

namespace Infrastructure.Repositorios
{
	public class UsuarioRepositorio : BaseRepositorio<Usuario>, IUsuarioRepositorio
	{
		public UsuarioRepositorio(AppDbContext context) : base(context)
		{

		}
	}
}
