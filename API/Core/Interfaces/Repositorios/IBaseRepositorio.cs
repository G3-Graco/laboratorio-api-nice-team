namespace Core.Interfaces.Repositorios
{
    public interface IBaseRepositorio<Entidad> where Entidad : class
    {

        ValueTask<Entidad> ObternerPorIdAsincrono(int id);
        Task<IEnumerable<Entidad>> ObternerTodosAsincrono();
        void Remover(Entidad entidad);
        void RemoverRango(IEnumerable<Entidad> entidades);
        Task Actualizar(Entidad entidad);
        Task AgregarAsincrono(Entidad entidad);


    }
}