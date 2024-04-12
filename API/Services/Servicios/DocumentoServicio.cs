using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;

namespace Services.Servicios
{
    public class DocumentoServicio : IDocumentoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public DocumentoServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
        public Task<Respuesta<Documento>> Actualizar(int entidadParaActualizarId, Documento nuevosValoresEntidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Documento>> Agregar(Documento nuevaEntitidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Documento>> ConvertirAByte(string url)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Documento>> Descargar(Documento documento)
        {
            throw new NotImplementedException();
        }


        public Task<Respuesta<Documento>> ObternerPorIdAsincrono(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<IEnumerable<Documento>>> ObternerTodosAsincrono()
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Documento>> Remover(int entidadId)
        {
            throw new NotImplementedException();
        }

        

    }
}