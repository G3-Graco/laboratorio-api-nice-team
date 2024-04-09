using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
    public interface IBaseServicio<Entidad> where Entidad : class
    {
        
        Task<Respuesta<Entidad>> ObternerPorIdAsincrono(int id);
        Task<Respuesta<IEnumerable<Entidad>>> ObternerTodosAsincrono();
        Task<Respuesta<Entidad>> Agregar(Entidad nuevaEntitidad);
        Task<Respuesta<Entidad>> Actualizar(int entidadParaActualizarId, Entidad nuevosValoresEntidad);
        Task<Respuesta<Entidad>> Remover(int entidadId);
    }
}