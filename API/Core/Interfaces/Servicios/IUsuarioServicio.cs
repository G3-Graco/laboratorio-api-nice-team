using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
	public interface IUsuarioServicio : IBaseServicio<Usuario>
	{
		Task<Respuesta<string>> IniciarSesion(string usuario, string contrasena);

		Task<Respuesta<bool>> ComprobarTokenConId(string token, int idusuario);
	}
}
