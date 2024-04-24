using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
    public interface ICuotaServicio : IBaseServicio<Cuota>
    {
        Task<Respuesta<IEnumerable<Cuota>>> ConsultarCuotasPorPrestamo(int idusuariosesion, int IdPrestamo);
		Task<Respuesta<IEnumerable<Cuota>>> ConsultarCuotasPagablesCliente(int idusuariosesion);
	}
}