using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using FluentValidation;
using Services.Validadores;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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

        public async Task<Respuesta<Movimiento>> RealizarRetiro(Movimiento movimiento)
        {
            try
            {
                var respuesta = new Respuesta<Movimiento>();
                if (movimiento == null) throw new ArgumentException("El movimiento no pude ser nulo");
                var cuenta = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(movimiento.CuentaOrigenIdentificador);
                if (cuenta == null) throw new ArgumentException("No existe una cuenta con tal id");
                if (movimiento?.TipoMovimientoId == null || movimiento.TipoMovimientoId <= 0) throw new ArgumentException("El movimiento carece de tipo");
                var tipo = await _unidadDeTrabajo.TipoMovimientoRepositorio.ObtenerPorIdAsincrono(movimiento.TipoMovimientoId);
                if (tipo == null) throw new ArgumentException("No existe un tipo con tal id");
                if (movimiento.Monto > cuenta.Saldo) {
                    respuesta.Ok = false;
                    respuesta.Mensaje = "Retiro denegado. El saldo de la cuenta es insuficiente";
                    respuesta.Datos = null;
                    return respuesta;
                }
                cuenta.Saldo -= movimiento.Monto;
                await _unidadDeTrabajo.CuentaRepositorio.Actualizar(cuenta);
                await _unidadDeTrabajo.MovimientoRepositorio.AgregarAsincrono(movimiento);
                await _unidadDeTrabajo.CommitAsync();
                respuesta.Ok = true;
                respuesta.Mensaje = "Transferencia realizada con éxito";
                respuesta.Datos = movimiento;
                return respuesta;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Respuesta<Movimiento>> RealizarDeposito(Movimiento movimiento)
        {
            try
            {
                var respuesta = new Respuesta<Movimiento>();
                if (movimiento == null) throw new ArgumentException("El movimiento no puede ser nulo");
                var cuenta = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(movimiento.CuentaReceptoraIdentificador);
                if (cuenta == null) throw new ArgumentException("No existe una cuenta receptora con tal id");
                if (movimiento?.TipoMovimientoId == null || movimiento.TipoMovimientoId <= 0) throw new ArgumentException("El movimiento carece de tipo");
                var tipo = await _unidadDeTrabajo.TipoMovimientoRepositorio.ObtenerPorIdAsincrono(movimiento.TipoMovimientoId);
                if (tipo == null) throw new ArgumentException("No existe un tipo con tal id");
                cuenta.Saldo += movimiento.Monto;
                await _unidadDeTrabajo.CuentaRepositorio.Actualizar(cuenta);
                await _unidadDeTrabajo.MovimientoRepositorio.AgregarAsincrono(movimiento);
                await _unidadDeTrabajo.CommitAsync();
                respuesta.Ok = true;
                respuesta.Mensaje = "Déposito realizada con éxito";
                respuesta.Datos = movimiento;
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Respuesta<Movimiento>> RealizarTransferencia(Movimiento movimiento)
        {
            try
            {
                var respuesta = new Respuesta<Movimiento>();
                if (movimiento == null) throw new ArgumentException("El movimiento no puede estar nulo");
                var cuentaReceptora = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(movimiento.CuentaReceptoraIdentificador);
                if (cuentaReceptora == null) throw new ArgumentException("No existe una cuenta receptora con tal id");
                var cuentaOrigen = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(movimiento.CuentaOrigenIdentificador);
                if (cuentaOrigen == null) throw new ArgumentException("No existe una cuenta origen con tal id");
                if (movimiento?.TipoMovimientoId == null || movimiento.TipoMovimientoId <= 0) throw new ArgumentException("El movimiento carece de tipo");
                var tipo = await _unidadDeTrabajo.TipoMovimientoRepositorio.ObtenerPorIdAsincrono(movimiento.TipoMovimientoId);
                if (tipo == null) throw new ArgumentException("No existe un tipo con tal id");
                if (movimiento.Monto > cuentaOrigen.Saldo) {
                    respuesta.Ok = false;
                    respuesta.Mensaje = "Transferencia denegada. El saldo de la cuenta es insuficiente";
                    respuesta.Datos = null;
                    return respuesta;
                }
                cuentaOrigen.Saldo -= movimiento.Monto;
                cuentaReceptora.Saldo += movimiento.Monto;
                await _unidadDeTrabajo.CuentaRepositorio.Actualizar(cuentaOrigen);
                await _unidadDeTrabajo.CuentaRepositorio.Actualizar(cuentaReceptora);
                await _unidadDeTrabajo.MovimientoRepositorio.AgregarAsincrono(movimiento);
                await _unidadDeTrabajo.CommitAsync();
                respuesta.Ok = true;
                respuesta.Mensaje = "Transferencia realizada con éxito";
                respuesta.Datos = movimiento;
                return respuesta;

            }
            catch (Exception e)
            {
                throw e;
            }
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

		public async Task<Respuesta<IEnumerable<Movimiento>>> ConsultarMovimientosDeUnaCuenta(int idUsuarioSesion)
		{
			if (idUsuarioSesion == null || idUsuarioSesion == 0)
			{
				throw new ArgumentException("Token inválido, vuelva a iniciar sesión");
			}

			Usuario usuario = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(idUsuarioSesion);

			Cuenta cuenta = await _unidadDeTrabajo.CuentaRepositorio.ConsultarCuentaDeUnCliente(usuario.ClienteId);
			if (cuenta == null)
				throw new ArgumentException("No se ha encontrado una cuenta para este cliente");

			var movimientos = await _unidadDeTrabajo.MovimientoRepositorio.ConsultarMovimientosDeUnaCuenta(cuenta.Identificador);
			if (movimientos == null)
				throw new ArgumentException("No se ha encontrado movimientos para este cliente");

			return new Respuesta<IEnumerable<Movimiento>> { Ok = true, Mensaje = "Movimientos consultados", Datos = movimientos };

		}

		public Task<Respuesta<Movimiento>> RealizarMovimiento(int idUsuarioSesion, Movimiento movimiento)
		{
			throw new NotImplementedException();
		}
	}
}
