using Core.Entidades;

namespace Core.Interfaces.Repositorios
{
    public interface IPrestamoRepostorio : IBaseRepositorio<Prestamo>
    {
		ValueTask<IEnumerable<Prestamo>> ConsultarPrestamosDeUnCliente(int idCliente);
	}
}