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
            throw new NotImplementedException();
        }

        // Pensar bien como hacer esta
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