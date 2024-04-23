using Core.Entidades;

namespace Core.Interfaces.Repositorios
{
    public interface ICuotaRepositorio : IBaseRepositorio<Cuota>
    {
        Task<List<Cuota>> AgregarVariosAsincrono(List<Cuota> cuotas);
        ValueTask<IEnumerable<Cuota>> ConsultarCuotasDeUnPrestamo(int idPrestamo);
        //ValueTask<IEnumerable<Cuota>> ConsultarCuotasDeUnCliente (int idCliente);
	}
}