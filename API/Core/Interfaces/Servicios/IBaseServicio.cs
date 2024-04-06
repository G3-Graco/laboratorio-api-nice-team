namespace Core.Interfaces.Servicios
{
    public interface IBaseServicio<Entity> where Entity : class
    {
        
        Task<Entity> GetById(int id);
        Task<IEnumerable<Entity>> GetAll();
        Task<Entity> Create(Entity newEntity);
        Task<Entity> Update(int entidadToBeUpdatedId, Entity newEntidadValues);
        Task Delete(int entityId);
    }
}