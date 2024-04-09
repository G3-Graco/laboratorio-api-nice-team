using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;

namespace Services.Servicios
{
    public class MovimientoServicio : IMovimientoServicio
    {
        public Task<Respuesta<Movimiento>> Actualizar(int entidadParaActualizarId, Movimiento nuevosValoresEntidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Movimiento>> Agregar(Movimiento nuevaEntitidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Movimiento>> ObternerPorIdAsincrono(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<IEnumerable<Movimiento>>> ObternerTodosAsincrono()
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Movimiento>> Remover(int entidadId)
        {
            throw new NotImplementedException();
        }

    }
}