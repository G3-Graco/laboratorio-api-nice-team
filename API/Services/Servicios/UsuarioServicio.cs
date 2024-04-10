using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Services.Validadores;

namespace Services.Servicios
{
	public class UsuarioServicio : IUsuarioServicio
	{
		private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public UsuarioServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}

		public async Task<Respuesta<Usuario>> Actualizar(int entidadParaActualizarId, Usuario nuevosValoresEntidad)
		{
			UsuarioValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevosValoresEntidad);

			if (!resultadoValidacion.IsValid)
			{
				//new Respuesta<Cliente>{Ok = false, Mensaje = resultadoValidacion.Errors[0].ErrorMessage.ToString(), Datos = null};
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());

			}

			Usuario UsuarioParaActualizar = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId);

			if (UsuarioParaActualizar == null)
				throw new ArgumentException("Id del usuario a actualizar inválido");

			UsuarioParaActualizar.NombreUsuario = nuevosValoresEntidad.NombreUsuario;
			UsuarioParaActualizar.Contrasena = nuevosValoresEntidad.Contrasena;
			UsuarioParaActualizar.ClienteId = nuevosValoresEntidad.ClienteId;


			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Usuario> { Ok = true, Mensaje = "Usuario actualizado con éxito", Datos = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId) };

		}

		public async Task<Respuesta<Usuario>> Agregar(Usuario nuevaEntitidad)
		{
			UsuarioValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevaEntitidad);

			if (!resultadoValidacion.IsValid)
			{
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());
			}

			await _unidadDeTrabajo.UsuarioRepositorio.AgregarAsincrono(nuevaEntitidad);
			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Usuario> { Ok = true, Mensaje = "Usuario creado con éxito", Datos = nuevaEntitidad };

		}

		public async Task<Respuesta<Usuario>> ObternerPorIdAsincrono(int id)
		{
			return new Respuesta<Usuario> { Ok = true, Mensaje = "Usuario obtenido", Datos = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(id) };
		}

		public async Task<Respuesta<IEnumerable<Usuario>>> ObternerTodosAsincrono()
		{
			return new Respuesta<IEnumerable<Usuario>> { Ok = true, Mensaje = "Usuarios obtenidos", Datos = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerTodosAsincrono() };
		}

		public async Task<Respuesta<Usuario>> Remover(int entidadId)
		{
			Usuario usuario = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(entidadId);
			_unidadDeTrabajo.UsuarioRepositorio.Remover(usuario);
			await _unidadDeTrabajo.CommitAsync();
			return new Respuesta<Usuario> { Ok = true, Mensaje = "Usuario eliminado", Datos = null };
		}
	}
}
