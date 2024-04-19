using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

                var todos = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerTodosAsincrono();
                var lista = todos.ToList().FindAll(x => x.IdCliente == usuario.ClienteId);

                var respuesta = new Respuesta<IEnumerable<Prestamo>>() {
                    Datos = lista, 
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
                var prestamo = await _unidadDeTrabajo.DocumentoRepositorio.ObtenerPorIdAsincrono(IdPrestamo);
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

			//Aquí validación de que modeloSolicitudPrestamo.DocumentoIdentificacionPersonal y modeloSolicitudPrestamo.DocumentoComprobanteIngresos
			//sean documentos válidos.

			if (modeloSolicitudPrestamo.MontoTotalDeseado > (modeloSolicitudPrestamo.SueldoBasicoDelSolicitante * 3))
			{
				return new Respuesta<Prestamo> { Ok = false, Mensaje = "El monto solicitado supera el equivalente a tres sueldos básicos del solicitante.", Datos = null };
			}

			var PlazoIdeal = await _unidadDeTrabajo.PlazoRepositorio.ConsultarPlazoIdeal(modeloSolicitudPrestamo.NumeroCuotasDeseadas);

			//hacer metodo repositorio para buscar minimo cuotas y maximo cuotas para indicar el error abajo.

			if (PlazoIdeal == null)
			{
				return new Respuesta<Prestamo> { Ok = false, Mensaje = "El número de cuotas solicitadas es inválido, probar con otro número", Datos = null };
			}

			double cuotaMensual = (modeloSolicitudPrestamo.MontoTotalDeseado * PlazoIdeal.Porcentaje) / Math.Pow((1 - (1 + PlazoIdeal.Porcentaje)), (-1*modeloSolicitudPrestamo.NumeroCuotasDeseadas));


			var prestamoAgregado = await _unidadDeTrabajo.PrestamoRepostorio.AgregarAsincrono(new Prestamo
			{
				Id = 0,
				NumeroCuotas = modeloSolicitudPrestamo.NumeroCuotasDeseadas,
				MontoTotal = modeloSolicitudPrestamo.MontoTotalDeseado,
				CuotaMensual = cuotaMensual,
				Fecha = DateTime.Now,
				IdEstado = 1,
				IdCliente = usuario.ClienteId,
				IdPlazo = PlazoIdeal.Id
			});


            var cuotas = new List<Cuota>();
            for (int i = 0; i < modeloSolicitudPrestamo.NumeroCuotasDeseadas; i++)
            {
                cuotas.Add(new Cuota { Id = 0, IdPrestamo = prestamoAgregado.Id, Fecha = DateTime.Now.AddMonths(i + 1), Pago = cuotaMensual});
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

            double montopendiente = prestamo.MontoTotal - MontoPagado;

            return new Respuesta<double> { Ok = true, Mensaje = "Consulta realizada correctamente.", Datos = montopendiente };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}