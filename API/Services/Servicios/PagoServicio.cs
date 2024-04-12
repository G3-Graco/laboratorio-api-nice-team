using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using FluentValidation;
using Services.Validadores;

namespace Services.Servicios
{
	public class PagoServicio : IPagoServicio
	{
		private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public PagoServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
		public async Task<Respuesta<Pago>> Actualizar(int entidadParaActualizarId, Pago nuevosValoresEntidad)
		{
			PagoValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevosValoresEntidad);

			if (!resultadoValidacion.IsValid)
			{
				//new Respuesta<Cliente>{Ok = false, Mensaje = resultadoValidacion.Errors[0].ErrorMessage.ToString(), Datos = null};
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());

			}

			Pago PagoParaActualizar = await _unidadDeTrabajo.PagoRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId);

			if (PagoParaActualizar == null)
				throw new ArgumentException("Id del pago a actualizar inválido");

			PagoParaActualizar.CuentaIdentificador = nuevosValoresEntidad.CuentaIdentificador;
			PagoParaActualizar.CuotaId = nuevosValoresEntidad.CuotaId;


			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Pago> { Ok = true, Mensaje = "Pago actualizado con éxito", Datos = await _unidadDeTrabajo.PagoRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId) };

		}

		public async Task<Respuesta<Pago>> Agregar(Pago nuevaEntitidad)
		{
			PagoValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevaEntitidad);

			if (!resultadoValidacion.IsValid)
			{
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());
			}

			await _unidadDeTrabajo.PagoRepositorio.AgregarAsincrono(nuevaEntitidad);
			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Pago> { Ok = true, Mensaje = "Pago creado con éxito", Datos = nuevaEntitidad };

		}

		public async Task<Respuesta<Pago>> ObternerPorIdAsincrono(int id)
		{
			var obtenido = await _unidadDeTrabajo.PagoRepositorio.ObtenerPorIdAsincrono(id);

			if (obtenido == null)
			{
				return new Respuesta<Pago> { Ok = false, Mensaje = "Pago no encontrado", Datos = obtenido };
			}
			else
			{
				return new Respuesta<Pago> { Ok = true, Mensaje = "Pago obtenido", Datos = obtenido };
			}
		}

		public async Task<Respuesta<IEnumerable<Pago>>> ObternerTodosAsincrono()
		{
			return new Respuesta<IEnumerable<Pago>> { Ok = true, Mensaje = "Pagos obtenidos", Datos = await _unidadDeTrabajo.PagoRepositorio.ObtenerTodosAsincrono() };
		}

		public async Task<Respuesta<Pago>> Remover(int entidadId)
		{
			Pago pago = await _unidadDeTrabajo.PagoRepositorio.ObtenerPorIdAsincrono(entidadId);
			_unidadDeTrabajo.PagoRepositorio.Remover(pago);
			await _unidadDeTrabajo.CommitAsync();
			return new Respuesta<Pago> { Ok = true, Mensaje = "Pago eliminado", Datos = null };

		}
	}
}
