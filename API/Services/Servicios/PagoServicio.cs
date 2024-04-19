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

			var entidadagregada = await _unidadDeTrabajo.PagoRepositorio.AgregarAsincrono(nuevaEntitidad);
			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Pago> { Ok = true, Mensaje = "Pago creado con éxito", Datos = entidadagregada };

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

        public async Task<Respuesta<Pago>> RealizarPago(Pago pago)
        {
            try
            {
                var respuesta = new Respuesta<Pago>();
                var cuota = await _unidadDeTrabajo.CuotaRepositorio.ObtenerPorIdAsincrono(pago.CuotaId);
                if (cuota == null) throw new ArgumentException("No existe una cuota con tal id");
                var cuenta = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(pago.CuentaIdentificador);
                if (cuenta == null) throw new ArgumentException("No existe una cuenta con tal id");
                if (cuenta.Saldo < cuota.Pago) {
                    respuesta.Datos = null;
                    respuesta.Ok = false;
                    respuesta.Mensaje = "Pago denegado. El saldo de su cuenta es insuficiente";
                    return respuesta;
                }
                cuota.FechaPago = DateTime.Now;
                cuenta.Saldo -= cuota.Pago;
                await _unidadDeTrabajo.CuotaRepositorio.Actualizar(cuota);
                await _unidadDeTrabajo.CuentaRepositorio.Actualizar(cuenta);
                await _unidadDeTrabajo.CommitAsync();
                respuesta.Datos = pago;
                respuesta.Ok = true;
                respuesta.Mensaje = "Pago realizado con éxito";
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Respuesta<IEnumerable<Pago>>> ConsultarPagosPorPrestamo(int IdPrestamo)
        {
            var prestamo = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerPorIdAsincrono(IdPrestamo);
            if (prestamo == null) throw new ArgumentException("No existe un préstamo con tal id");
            var todos = await _unidadDeTrabajo.CuotaRepositorio.ObtenerTodosAsincrono();
            var listaCuotas = todos.ToList().FindAll(x => x.IdPrestamo == IdPrestamo);
            var listaPagos = new List<Pago>();
            foreach (var cuota in listaCuotas) {
                var pago = await _unidadDeTrabajo.PagoRepositorio.ObtenerPorIdAsincrono(cuota.Id);
                listaPagos.Add(pago);
            }
            var respuesta = new Respuesta<IEnumerable<Pago>>() {
                Datos = listaPagos, 
                Mensaje = "Obtención de préstamos exitosa", 
                Ok = true
            };
            return respuesta;
        }

		public async Task<Respuesta<IEnumerable<Pago>>> ConsultarPagosDeUnaCuenta(int idUsuarioSesion)
		{
			if (idUsuarioSesion == null || idUsuarioSesion == 0)
			{
				throw new ArgumentException("Token inválido, vuelva a iniciar sesión");
			}

			Usuario usuario = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(idUsuarioSesion);

			Cuenta cuenta = await _unidadDeTrabajo.CuentaRepositorio.ConsultarCuentaDeUnCliente(usuario.ClienteId);

			if (cuenta == null)
				throw new ArgumentException("No se ha encontrado una cuenta para este cliente");

			var pagos = await _unidadDeTrabajo.PagoRepositorio.ConsultarPagosDeUnaCuenta(cuenta.Identificador);
			if (pagos == null)
				throw new ArgumentException("No se ha encontrado movimientos para este cliente");

			return new Respuesta<IEnumerable<Pago>> { Ok = true, Mensaje = "Pagos consultados", Datos = pagos };
		}

        public async Task<Respuesta<IEnumerable<Pago>>> ConsultarPagosPorPrestamoValidado(int idusuariosesion, int idPrestamo)
        {
            if (idusuariosesion == null || idusuariosesion == 0)
            {
                throw new ArgumentException("Token inválido, vuelva a iniciar sesión");
            }

            Prestamo prestamo = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerPorIdAsincrono(idPrestamo);

            Usuario usuario = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(idusuariosesion);

            if (prestamo.IdCliente != usuario.ClienteId)
            {
                return new Respuesta<IEnumerable<Pago>> { Ok = false, Mensaje = "Consulta inválida. No se puede consultar un préstamo que no pertenezca al usuario actual", Datos = null};
            }

            var pagos = await _unidadDeTrabajo.PagoRepositorio.ConsultarPagosDeUnPrestamo(idPrestamo);
            if (pagos == null)
                throw new ArgumentException("No se ha encontrado movimientos para este préstamo");

            return new Respuesta<IEnumerable<Pago>> { Ok = true, Mensaje = "Pagos consultados", Datos = pagos };
        }
    }
}