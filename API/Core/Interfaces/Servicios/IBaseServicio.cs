namespace Core.Interfaces.Servicios
{
    public interface IBaseServicio<Entitidad> where Entitidad : class
    {
        
        Task<Entitidad> ObternerPorIdAsincrono(int id);
        Task<IEnumerable<Entitidad>> ObternerTodosAsincrono();
        Task<Entitidad> Agregar(Entitidad nuevaEntitidad);
        Task<Entitidad> Actualizar(int entidadParaActualizarId, Entitidad nuevosValoresEntidad);
        Task Remover(int entidadId);
    }
}