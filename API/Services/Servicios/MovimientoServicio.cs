using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using FluentValidation;
using Services.Validadores;

namespace Services.Servicios
{
	public class MovimientoServicio : IMovimientoServicio
	{
		private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public MovimientoServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
		public async Task<Respuesta<Movimiento>> Actualizar(int entidadParaActualizarId, Movimiento nuevosValoresEntidad)
		{
			MovimientoValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevosValoresEntidad);

			if (!resultadoValidacion.IsValid)
			{
				//new Respuesta<Cliente>{Ok = false, Mensaje = resultadoValidacion.Errors[0].ErrorMessage.ToString(), Datos = null};
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());

			}

			Movimiento MovimientoParaActualizar = await _unidadDeTrabajo.MovimientoRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId);

			if (MovimientoParaActualizar == null)
				throw new ArgumentException("Id del cliente a actualizar inválido");

			MovimientoParaActualizar.Monto = nuevosValoresEntidad.Monto;
			MovimientoParaActualizar.Descripcion = nuevosValoresEntidad.Descripcion;
			MovimientoParaActualizar.Fecha = nuevosValoresEntidad.Fecha;
			MovimientoParaActualizar.TipoMovimientoId = nuevosValoresEntidad.TipoMovimientoId;
			MovimientoParaActualizar.CuentaOrigenIdentificador = nuevosValoresEntidad.CuentaOrigenIdentificador;
			MovimientoParaActualizar.CuentaReceptoraIdentificador = nuevosValoresEntidad.CuentaReceptoraIdentificador;


			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Movimiento> { Ok = true, Mensaje = "Movimiento actualizado con éxito", Datos = await _unidadDeTrabajo.MovimientoRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId) };

		}

		public async Task<Respuesta<Movimiento>> Agregar(Movimiento nuevaEntitidad)
		{
			MovimientoValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevaEntitidad);

			if (!resultadoValidacion.IsValid)
			{
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());
			}

			var entidadagregada = await _unidadDeTrabajo.MovimientoRepositorio.AgregarAsincrono(nuevaEntitidad);
			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Movimiento> { Ok = true, Mensaje = "Movimiento creado con éxito", Datos = entidadagregada };

		}

		public async Task<Respuesta<Movimiento>> ObternerPorIdAsincrono(int id)
		{
			var obtenido = await _unidadDeTrabajo.MovimientoRepositorio.ObtenerPorIdAsincrono(id);

			if (obtenido == null)
			{
				return new Respuesta<Movimiento> { Ok = false, Mensaje = "Movimiento no encontrado", Datos = obtenido };
			}
			else
			{
				return new Respuesta<Movimiento> { Ok = true, Mensaje = "Movimiento obtenido", Datos = obtenido };
			}
		}

		public async Task<Respuesta<IEnumerable<Movimiento>>> ObternerTodosAsincrono()
		{
			return new Respuesta<IEnumerable<Movimiento>> { Ok = true, Mensaje = "Movimientos obtenidos", Datos = await _unidadDeTrabajo.MovimientoRepositorio.ObtenerTodosAsincrono() };
		}

		public async Task<Respuesta<Movimiento>> Remover(int entidadId)
		{
			Movimiento movimiento = await _unidadDeTrabajo.MovimientoRepositorio.ObtenerPorIdAsincrono(entidadId);
			_unidadDeTrabajo.MovimientoRepositorio.Remover(movimiento);
			await _unidadDeTrabajo.CommitAsync();
			return new Respuesta<Movimiento> { Ok = true, Mensaje = "Movimiento eliminado", Datos = null };
		}
	}
}
