using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Services.Validadores;

namespace Services.Servicios
{
	public class ClienteServicio : IClienteServicio
	{
		private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public ClienteServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
		public async Task<Respuesta<Cliente>> Agregar(Cliente nuevaEntitidad)
		{
			ClienteValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevaEntitidad);

			if (!resultadoValidacion.IsValid)
			{
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());
			}

			await _unidadDeTrabajo.ClienteRepositorio.AgregarAsincrono(nuevaEntitidad);
			await _unidadDeTrabajo.CommitAsync();
		
			return new Respuesta<Cliente>{Ok = true, Mensaje = "Cliente creado con éxito", Datos = nuevaEntitidad};
		}

		public async Task<Respuesta<Cliente>> Actualizar(int entidadParaActualizarId, Cliente nuevosValoresEntidad)
		{
			ClienteValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevosValoresEntidad);

			if (!resultadoValidacion.IsValid)
			{
				//new Respuesta<Cliente>{Ok = false, Mensaje = resultadoValidacion.Errors[0].ErrorMessage.ToString(), Datos = null};
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());
				
			}

			Cliente ClienteParaActualizar = await _unidadDeTrabajo.ClienteRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId);

			if (ClienteParaActualizar == null)
				throw new ArgumentException("Id del cliente a actualizar inválido");

			ClienteParaActualizar.Nombre = nuevosValoresEntidad.Nombre;
			ClienteParaActualizar.Apellido = nuevosValoresEntidad.Apellido;
			ClienteParaActualizar.Cedula = nuevosValoresEntidad.Cedula;
			ClienteParaActualizar.Correo = nuevosValoresEntidad.Correo;
			ClienteParaActualizar.FechaNacimiento = nuevosValoresEntidad.FechaNacimiento;
			ClienteParaActualizar.Telefono = nuevosValoresEntidad.Telefono;
			ClienteParaActualizar.Direccion = nuevosValoresEntidad.Direccion;

			await _unidadDeTrabajo.CommitAsync();
			
			return new Respuesta<Cliente>{Ok = true, Mensaje = "Cliente actualizado con éxito", Datos = await _unidadDeTrabajo.ClienteRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId)};
			
		}

		public async Task<Respuesta<Cliente>> ObternerPorIdAsincrono(int id)
		{
			var obtenido = await _unidadDeTrabajo.ClienteRepositorio.ObtenerPorIdAsincrono(id);

			if (obtenido == null)
			{
				return new Respuesta<Cliente> { Ok = false, Mensaje = "Cliente no encontrado", Datos = obtenido };
			}
			else
			{
				return new Respuesta<Cliente> { Ok = true, Mensaje = "Cliente obtenido", Datos = obtenido };
			}
		}

		public async Task<Respuesta<IEnumerable<Cliente>>> ObternerTodosAsincrono()
		{
			
			return new Respuesta<IEnumerable<Cliente>>{Ok = true, Mensaje = "Clientes obtenidos", Datos = await _unidadDeTrabajo.ClienteRepositorio.ObtenerTodosAsincrono()};
		}

		public async Task<Respuesta<Cliente>> Remover(int entidadId)
		{
			Cliente cliente = await _unidadDeTrabajo.ClienteRepositorio.ObtenerPorIdAsincrono(entidadId);
			_unidadDeTrabajo.ClienteRepositorio.Remover(cliente);
			await _unidadDeTrabajo.CommitAsync();
			return new Respuesta<Cliente>{Ok = true, Mensaje = "Cliente eliminado", Datos = null};
		}
	}
}
