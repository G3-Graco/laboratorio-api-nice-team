using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorios
{
	public class CuentaRepositorio : BaseRepositorio<Cuenta>, ICuentaRepositorio
	{
		public CuentaRepositorio(AppDbContext context) : base(context)
		{

		}

		public async ValueTask<Cuenta> ConsultarCuentaDeUnCliente(int idCliente)
		{
			return await dbSet.Where(c => c.ClienteId.Equals(idCliente)).FirstOrDefaultAsync();
		}
	}
}
