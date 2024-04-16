using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
	public interface IClienteServicio : IBaseServicio<Cliente>
	{
		Task<Respuesta<Cliente>> ConsultarClienteValidado(int idUsuarioSesion);
	}
}
