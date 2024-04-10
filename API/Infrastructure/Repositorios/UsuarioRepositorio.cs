using Core.Entidades;
using Core.Interfaces.Repositorios;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorios
{
	public class UsuarioRepositorio : BaseRepositorio<Usuario>, IUsuarioRepositorio
	{
		public UsuarioRepositorio(AppDbContext context) : base(context)
		{

		}

		public virtual async ValueTask<Usuario> IniciarSesion(string nombreusuario, string contrasena)
		{
			return await dbSet.Where(u => u.NombreUsuario.Equals(nombreusuario) && u.Contrasena.Equals(contrasena)).FirstOrDefaultAsync();

		}
	}
}
