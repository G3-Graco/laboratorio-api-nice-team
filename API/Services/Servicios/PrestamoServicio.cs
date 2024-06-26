using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Services.Validadores;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Servicios
{
    public class PrestamoServicio : IPrestamoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public PrestamoServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
        public Task<Respuesta<Prestamo>> Actualizar(int entidadParaActualizarId, Prestamo nuevosValoresEntidad)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<Prestamo>> Agregar(Prestamo nuevaEntitidad)
		{
			PrestamoValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevaEntitidad);

			if (!resultadoValidacion.IsValid)
			{
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());
			}

			var entidadagregada = await _unidadDeTrabajo.PrestamoRepostorio.AgregarAsincrono(nuevaEntitidad);
			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Prestamo> { Ok = true, Mensaje = "Prestamo creado con éxito", Datos = entidadagregada };
		}

        public Task<Respuesta<Prestamo>> ObternerPorIdAsincrono(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<IEnumerable<Prestamo>>> ObternerTodosAsincrono()
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<Prestamo>> Remover(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<Documento>> GuardarDocumento(Documento documento) 
        {
            try
            {
                if (documento == null) throw new ArgumentException("El documento está vacío");
                var prestamo = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerPorIdAsincrono(documento.IdPrestamo);
                if (prestamo == null) throw new ArgumentException("El préstamo de este documento no existe");
                var tipo = await _unidadDeTrabajo.TipoDocumentoRepositorio.ObtenerPorIdAsincrono(documento.IdTipo);
                if (tipo == null) throw new ArgumentException("El tipo de este documento no existe");
                await _unidadDeTrabajo.DocumentoRepositorio.AgregarAsincrono(documento);
                await _unidadDeTrabajo.CommitAsync();
                var respuesta = new Respuesta<Documento>() {
                    Datos = documento, 
                    Ok = true, 
                    Mensaje = "Documento ha sido guardado correctamente"
                };
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task<Respuesta<IEnumerable<Prestamo>>> ConsultarPrestamosDeCliente(int idUsuarioSesion) 
        {    
            try
            {
				if (idUsuarioSesion == null || idUsuarioSesion == 0) //idUsuarioSesion == 0 si el usuario no inserta este dato en el query
				{
					throw new ArgumentException("Token inválido, vuelva a iniciar sesión");
				}

				Usuario usuario = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(idUsuarioSesion);

                var prestamosCliente = await _unidadDeTrabajo.PrestamoRepostorio.ConsultarPrestamosDeUnCliente(usuario.ClienteId);


                foreach (var prestamo in prestamosCliente)
                {
                    await ActualizarEstadoPrestamo(prestamo);
                }


                var respuesta = new Respuesta<IEnumerable<Prestamo>>() {
                    Datos = prestamosCliente, 
                    Mensaje = "Prestamos encontrados exitósamente", 
                    Ok = true
                };

                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Respuesta<IEnumerable<Documento>>> ConsultarDocumentos(int IdPrestamo)
        {
            try
            {
                var prestamo = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerPorIdAsincrono(IdPrestamo);
                if (prestamo == null) throw new ArgumentException("No existe un préstamo con tal id");
                var todos = await _unidadDeTrabajo.DocumentoRepositorio.ObtenerTodosAsincrono();
                var lista = todos.ToList().FindAll(x => x.IdPrestamo == IdPrestamo);
                var respuesta = new Respuesta<IEnumerable<Documento>>() {
                    Datos = lista, 
                    Mensaje = "Los documentos fueron encontrados exitósamente", 
                    Ok = true
                };
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Respuesta<Prestamo>> ConsultarPrestamoValidado(int idUsuarioSesion, int idPrestamo)
		{
			if (idUsuarioSesion == null || idUsuarioSesion == 0)
			{
				throw new ArgumentException("Token inválido, vuelva a iniciar sesión");
			}

			Prestamo prestamo = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerPorIdAsincrono(idPrestamo);

            await ActualizarEstadoPrestamo(prestamo);

			Usuario usuario = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(idUsuarioSesion);


            //actualizar prestamo estado (como no está en el alcance lo hago si me da tiempo) p

			if(prestamo.IdCliente != usuario.ClienteId)
			{
				return new Respuesta<Prestamo> { Ok = false, Mensaje = "Consulta inválida. No se puede consultar un préstamo que no pertenezca al usuario actual", Datos = null };
			}
			else
			{
				return new Respuesta<Prestamo> { Ok = true, Mensaje = "Préstamo obtenido con éxito", Datos = prestamo };

			}
		}

		public async Task<Respuesta<Prestamo>> SolicitarPrestamo(int idUsuarioSesion, ModeloSolicitudPrestamo modeloSolicitudPrestamo)
		{
            if (idUsuarioSesion == null || idUsuarioSesion == 0)
            {
                throw new ArgumentException("Token inválido, vuelva a iniciar sesión");
            }

            Usuario usuario = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(idUsuarioSesion);

			//Aquí validación de que modeloSolicitudPrestamo.DocumentoIdentificacionPersonal y modeloSolicitudPrestamo.DocumentoComprobanteIngresos sean documentos válidos.
			// ************** se realizó a nivel de blazor la validación de arriba *********

			//Ver los préstamos sin pagar y validar que no tenga prestamos atrasados
			IEnumerable<Prestamo> prestamos = await _unidadDeTrabajo.PrestamoRepostorio.ConsultarPrestamosDeUnCliente(usuario.ClienteId);

			List<Prestamo> prestamosNoPagados = new List<Prestamo>();

			foreach (Prestamo prestamo in prestamos)
			{
                int cuotasAtrasadas = 0;

				IEnumerable<Cuota> cuotasdeprestamo = await _unidadDeTrabajo.CuotaRepositorio.ConsultarCuotasDeUnPrestamo(prestamo.Id);

				Cuota cuotasinpagar = null;

				foreach (Cuota cuota in cuotasdeprestamo)
				{
                    if (cuota.FechaPago == DateTime.MinValue)
					{

						if (DateTime.Compare(cuota.Fecha, DateTime.UtcNow) < 0)
						{
							cuotasAtrasadas += 1;
						}
		
						cuotasinpagar = cuota;
					}
				}

				if (cuotasAtrasadas > 3)
				{
					return new Respuesta<Prestamo> { Ok = false, Mensaje = "Posee un préstamo con más de 3 cuotas atrasadas. No puede solicitar otro préstamo", Datos = null };
				}

				if (cuotasinpagar != null)
				{
					prestamosNoPagados.Add(prestamo);
				}
			}

            //Validar monto
            if (prestamosNoPagados.Count == 0)
            {
				if (modeloSolicitudPrestamo.MontoTotalDeseado > (modeloSolicitudPrestamo.SueldoBasicoDelSolicitante * 3))
				{
					return new Respuesta<Prestamo> { Ok = false, Mensaje = "El monto solicitado supera el equivalente a tres sueldos básicos del solicitante.", Datos = null };
				}
			}
            else
            {
                double montoTotalPrestamos = 0;
                foreach(Prestamo prestamo in prestamosNoPagados)
                {
                    montoTotalPrestamos += prestamo.MontoTotal;
                }

				if (modeloSolicitudPrestamo.MontoTotalDeseado + montoTotalPrestamos > (modeloSolicitudPrestamo.SueldoBasicoDelSolicitante * 3))
				{
					return new Respuesta<Prestamo> { Ok = false, Mensaje = "El monto solicitado sumado a el monto total de los préstamos actuales supera el equivalente a tres sueldos básicos del solicitante.", Datos = null };
				}
			}

			

            var PlazoIdeal = await _unidadDeTrabajo.PlazoRepositorio.ConsultarPlazoIdeal(modeloSolicitudPrestamo.NumeroCuotasDeseadas);

            if (PlazoIdeal == null)
            {
                int PlazoMinimo = await _unidadDeTrabajo.PlazoRepositorio.ConsultarPlazoMinimo();
                int PlazoMaximo = await _unidadDeTrabajo.PlazoRepositorio.ConsultarPlazoMaximo();
                return new Respuesta<Prestamo> { Ok = false, Mensaje = $"El número de cuotas solicitadas es inválido. Elegir un numero de cuotas entre {PlazoMinimo} y {PlazoMaximo}", Datos = null };
            }

            var P = modeloSolicitudPrestamo.MontoTotalDeseado;
            var I = PlazoIdeal.Porcentaje;
            var n = modeloSolicitudPrestamo.NumeroCuotasDeseadas;
            double cuotaMensual = (P * I) / (1 - Math.Pow((1 + I), (-n)));

            var prestamoAgregado = await _unidadDeTrabajo.PrestamoRepostorio.AgregarAsincrono(new Prestamo
            {
                Id = 0,
                NumeroCuotas = modeloSolicitudPrestamo.NumeroCuotasDeseadas,
                MontoTotal = modeloSolicitudPrestamo.MontoTotalDeseado,
                CuotaMensual = cuotaMensual,
                Fecha = DateTime.UtcNow,
                IdEstado = 1,
                IdCliente = usuario.ClienteId,
                IdPlazo = PlazoIdeal.Id
            });

            await _unidadDeTrabajo.CommitAsync();

            var cuotas = new List<Cuota>();
            for (int i = 0; i < modeloSolicitudPrestamo.NumeroCuotasDeseadas; i++)
            {
                cuotas.Add(new Cuota { Id = 0, IdPrestamo = prestamoAgregado.Id, Fecha = DateTime.UtcNow.AddMonths(i + 1), FechaPago = DateTime.MinValue, Pago = cuotaMensual });
            }

            await _unidadDeTrabajo.CuotaRepositorio.AgregarVariosAsincrono(cuotas);




            //despues de validarlos
            //aqui va agregar documentos relacionados al id de lo de arriba



            await _unidadDeTrabajo.CommitAsync();
            return new Respuesta<Prestamo> { Ok = true, Mensaje = "Prestamo creado con éxito", Datos = prestamoAgregado };


        }

		public async Task<Respuesta<double>> ConsultarMontoPendientePrestamo(int idusuariosesion, int IdPrestamo)
        {
            try
            {

            
            if (idusuariosesion == null || idusuariosesion == 0)
            {
                throw new ArgumentException("Token inválido, vuelva a iniciar sesión");
            }
            Prestamo prestamo = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerPorIdAsincrono(IdPrestamo);

			await ActualizarEstadoPrestamo(prestamo);

			Usuario usuario = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(idusuariosesion);

            if (prestamo.IdCliente != usuario.ClienteId)
            {
                return new Respuesta<double> { Ok = false, Mensaje = "Consulta inválida. No se puede consultar un préstamo que no pertenezca al usuario actual", Datos = 0 };
            }


            var pagos = await _unidadDeTrabajo.PagoRepositorio.ConsultarPagosDeUnPrestamo(IdPrestamo);

            double MontoPagado = 0;
            foreach (Pago pago in pagos)
            {
                MontoPagado += pago.CuotaPagada.Pago;
            }

            double montopendiente = (prestamo.CuotaMensual * prestamo.NumeroCuotas) - MontoPagado;

            return new Respuesta<double> { Ok = true, Mensaje = "Consulta realizada correctamente.", Datos = montopendiente };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public async Task ActualizarEstadoPrestamo(Prestamo prestamo)
        {
			Prestamo prestamoActualizar = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerPorIdAsincrono(prestamo.Id);

			
			IEnumerable<Cuota> cuotas = await _unidadDeTrabajo.CuotaRepositorio.ConsultarCuotasDeUnPrestamo(prestamo.Id);

			Cuota cuotaPendiente = null;
			Cuota cuotaAtrasada = null;

			foreach (Cuota cuota in cuotas)
			{
				if (cuota.FechaPago == DateTime.MinValue)
                {
                    if (DateTime.Compare(cuota.Fecha, DateTime.UtcNow) < 0)
                    {
                        cuotaAtrasada = cuota;
					}
                    else
                    {
						cuotaPendiente = cuota;
					}
                }
			}

			if (cuotaPendiente == null && cuotaAtrasada == null)
			{
                prestamo.IdEstado = 3;
			}
			if (cuotaPendiente != null)
            {
				prestamo.IdEstado = 1;
			}
			if (cuotaAtrasada != null)
			{
				prestamo.IdEstado = 2;
			}


            EstadoPrestamo estadoPrestamo = await _unidadDeTrabajo.EstadoPrestamoRepositorio.ObtenerPorIdAsincrono(prestamo.IdEstado);

            prestamo.Estado = estadoPrestamo;

            await _unidadDeTrabajo.CommitAsync();

		}

	}
}