using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using System.Net;

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

        public async Task<Respuesta<byte[]>> ConvertirAByte(string url)
        {
            var respuesta = new Respuesta<byte[]>();
            try
            {
                using (WebClient cliente = new WebClient())
                {
                    Uri uri = new Uri(url);
                    byte[] bytes = await cliente.DownloadDataTaskAsync(uri);
                    respuesta.Datos = bytes;
                    respuesta.Ok = true;
                    respuesta.Mensaje = "Obtención de bytes de la imagen exitosa";
                    return respuesta;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
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