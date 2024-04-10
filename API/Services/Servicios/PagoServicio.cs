using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;

namespace Services.Servicios
{
    public class PagoServicio : IPagoServicio
    {
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

        public Task<Respuesta<Pago>> RealizarPago(Pago pago)
        {
            throw new NotImplementedException();
        }

        // Pensar bien como hacer esta
        public Task<Respuesta<IEnumerable<Pago>>> ConsultarPagosPorPrestamo(int IdPrestamo)
        {
            throw new NotImplementedException();
        }

    }
}