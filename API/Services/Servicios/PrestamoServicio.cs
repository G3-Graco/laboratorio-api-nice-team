using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;

namespace Services.Servicios
{
    public class PrestamoServicio : IPrestamoServicio
    {
        public Task<Respuesta<Prestamo>> Actualizar(int entidadParaActualizarId, Prestamo nuevosValoresEntidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Prestamo>> Agregar(Prestamo nuevaEntitidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Prestamo>> ObternerPorIdAsincrono(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<IEnumerable<Prestamo>>> ObternerTodosAsincrono()
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Prestamo>> Remover(int entidadId)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Documento>> GuardarDocumento(Documento documento) 
        {
            var respuesta = new Respuesta<Documento>();
            throw new NotImplementedException();
        }

        public Task<Respuesta<IEnumerable<Prestamo>>> ConsultarPrestamosDeCliente(int IdCliente) {
            throw new NotImplementedException();
        }

        public Task<Respuesta<IEnumerable<Documento>>> ConsultarDocumentos(int IdPrestamo)
        {
            throw new NotImplementedException();
        }
    }
}