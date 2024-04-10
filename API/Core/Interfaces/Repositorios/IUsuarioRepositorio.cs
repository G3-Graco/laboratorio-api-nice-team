using Core.Entidades;

namespace Core.Interfaces.Repositorios
{
	public interface IUsuarioRepositorio : IBaseRepositorio<Usuario>
	{
		ValueTask<Usuario> IniciarSesion(string nombreusuario, string contrasena);
	}
}
