using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Services.Validadores;
using System.ComponentModel.DataAnnotations;

namespace Services.Servicios
{
	public class ClienteServicio : IClienteServicio
	{
		private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public ClienteServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
		public async Task<Cliente> Agregar(Cliente nuevaEntitidad)
		{
			ClienteValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevaEntitidad);

			if (!resultadoValidacion.IsValid)
			{
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());
			}

			await _unidadDeTrabajo.ClienteRepositorio.AgregarAsincrono(nuevaEntitidad);
			await _unidadDeTrabajo.CommitAsync();
		
			return nuevaEntitidad; //

		}

		public async Task<Cliente> Actualizar(int entidadParaActualizarId, Cliente nuevosValoresEntidad)
		{
			ClienteValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevosValoresEntidad);

			if (!resultadoValidacion.IsValid)
			{
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());
				
			}

			Cliente ClienteParaActualizar = await _unidadDeTrabajo.ClienteRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId);

			if (ClienteParaActualizar == null)
				throw new ArgumentException("Id del cliente a actualazar inválido");

			ClienteParaActualizar.Nombre = nuevosValoresEntidad.Nombre;
			ClienteParaActualizar.Apellido = nuevosValoresEntidad.Apellido;
			ClienteParaActualizar.Cedula = nuevosValoresEntidad.Cedula;
			ClienteParaActualizar.Correo = nuevosValoresEntidad.Correo;
			ClienteParaActualizar.FechaNacimiento = nuevosValoresEntidad.FechaNacimiento;
			ClienteParaActualizar.Telefono = nuevosValoresEntidad.Telefono;
			ClienteParaActualizar.Direccion = nuevosValoresEntidad.Direccion;

			await _unidadDeTrabajo.CommitAsync();

			return await _unidadDeTrabajo.ClienteRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId);
		}


		public async Task<Cliente> ObternerPorIdAsincrono(int id)
		{
			return await _unidadDeTrabajo.ClienteRepositorio.ObtenerPorIdAsincrono(id);
		}

		public async Task<IEnumerable<Cliente>> ObternerTodosAsincrono()
		{
			return await _unidadDeTrabajo.ClienteRepositorio.ObtenerTodosAsincrono();
		}

		public async Task Remover(int entidadId)
		{
			Cliente cliente = await _unidadDeTrabajo.ClienteRepositorio.ObtenerPorIdAsincrono(entidadId);
			_unidadDeTrabajo.ClienteRepositorio.Remover(cliente);
			await _unidadDeTrabajo.CommitAsync();
		}
	}
}
