namespace Core.Interfaces.Repositorios
{
    public interface IBaseRepositorio<TEntity> where TEntity : class
    {

        ValueTask<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task Update(TEntity entity);
        Task AddAsync(TEntity entity);


    }
}