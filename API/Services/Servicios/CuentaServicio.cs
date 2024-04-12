using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Services.Validadores;

namespace Services.Servicios
{
	public class CuentaServicio : ICuentaServicio
	{
		private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public CuentaServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}

		public async Task<Respuesta<Cuenta>> Actualizar(int entidadParaActualizarId, Cuenta nuevosValoresEntidad)
		{
			CuentaValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevosValoresEntidad);

			if (!resultadoValidacion.IsValid)
			{
				//new Respuesta<Cliente>{Ok = false, Mensaje = resultadoValidacion.Errors[0].ErrorMessage.ToString(), Datos = null};
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());

			}

			Cuenta CuentaParaActualizar = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId);

			if (CuentaParaActualizar == null)
				throw new ArgumentException("Id de la cuenta a actualizar inválido");

			CuentaParaActualizar.Saldo = nuevosValoresEntidad.Saldo;
			CuentaParaActualizar.ClienteId = nuevosValoresEntidad.ClienteId;

			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Cuenta> { Ok = true, Mensaje = "Cuenta actualizada con éxito", Datos = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId) };

		}

		

		public async Task<Respuesta<Cuenta>> Agregar(Cuenta nuevaEntitidad)
		{
			CuentaValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevaEntitidad);

			if (!resultadoValidacion.IsValid)
			{
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());
			}

			await _unidadDeTrabajo.CuentaRepositorio.AgregarAsincrono(nuevaEntitidad);
			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Cuenta> { Ok = true, Mensaje = "Cuenta creada con éxito", Datos = nuevaEntitidad };
		}		

		public async Task<Respuesta<Cuenta>> ObternerPorIdAsincrono(int id)
		{
			var obtenido = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(id);

			if (obtenido == null)
			{
				return new Respuesta<Cuenta> { Ok = false, Mensaje = "Cuenta no encontrada", Datos = obtenido };
			}
			else
			{
				return new Respuesta<Cuenta> { Ok = true, Mensaje = "Cuenta obtenida", Datos = obtenido };
			}

		}

		public async Task<Respuesta<IEnumerable<Cuenta>>> ObternerTodosAsincrono()
		{
			return new Respuesta<IEnumerable<Cuenta>> { Ok = true, Mensaje = "Cuentas obtenidas", Datos = await _unidadDeTrabajo.CuentaRepositorio.ObtenerTodosAsincrono() };
		}

		public async Task<Respuesta<Cuenta>> Remover(int entidadId)
		{
			Cuenta cuenta = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(entidadId);
			_unidadDeTrabajo.CuentaRepositorio.Remover(cuenta);
			await _unidadDeTrabajo.CommitAsync();
			return new Respuesta<Cuenta> { Ok = true, Mensaje = "Cuenta eliminada", Datos = null };
		}
		public async Task<Respuesta<Cuenta>> ConsultarCuentaDeUnCliente(int idUsuarioSesion)
		{
			if (idUsuarioSesion == null)
			{
				throw new ArgumentException("No se ha encontrado una cuenta para este cliente");
			}
			var cuenta = await _unidadDeTrabajo.CuentaRepositorio.ConsultarCuentaDeUnCliente(idUsuarioSesion);

			if (cuenta == null)
				throw new ArgumentException("No se ha encontrado una cuenta para este cliente");

			return new Respuesta<Cuenta> { Ok = true, Mensaje = "Cuenta consultada", Datos = cuenta };

			
		}
		public async Task<Respuesta<Cuenta>> ActualizarSaldo(int idCuenta, double nuevoSaldo)
		{
			Cuenta CuentaParaActualizar = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(idCuenta);

			if (CuentaParaActualizar == null)
				throw new ArgumentException("Id de la cuenta a actualizar saldo es inválido");

			CuentaParaActualizar.Saldo = nuevoSaldo;

			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Cuenta> { Ok = true, Mensaje = "Saldo actualizado con éxito", Datos = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(idCuenta) };

		}
	}
}

