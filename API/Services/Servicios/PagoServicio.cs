using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;

namespace Services.Servicios
{
    public class PagoServicio : IPagoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public PagoServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
        public Task<Respuesta<Pago>> Actualizar(int entidadParaActualizarId, Pago nuevosValoresEntidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Pago>> Agregar(Pago nuevaEntitidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Pago>> ObternerPorIdAsincrono(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<IEnumerable<Pago>>> ObternerTodosAsincrono()
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Pago>> Remover(int entidadId)
        {
            throw new NotImplementedException();
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

    }
}