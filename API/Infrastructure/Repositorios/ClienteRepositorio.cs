using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;

namespace Infrastructure.Repositorios
{
	public class ClienteRepositorio : BaseRepositorio<Cliente>, IClienteRepositorio
	{
		public ClienteRepositorio(AppDbContext context) : base(context)
		{

		}
	}
}
