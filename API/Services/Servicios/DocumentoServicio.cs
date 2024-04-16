using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using System.Net;
using System.Drawing;
using System.Diagnostics;

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

        public async Task<Respuesta<Documento>> Descargar(Documento documento)
        {
            if (documento.documento == null) throw new ArgumentException("El documento no puede ser nulo");
            var respuesta = new Respuesta<Documento>();
            try
            {
                var actual = await _unidadDeTrabajo.DocumentoRepositorio.ObtenerPorIdAsincrono(documento.Id);
                if (actual == null) throw new ArgumentException("El documento no se encuentra en la base de datos");
                var tipo = await _unidadDeTrabajo.TipoMovimientoRepositorio.ObtenerPorIdAsincrono(documento.IdTipo);
                var prestamo = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerPorIdAsincrono(documento.IdPrestamo);
                using (MemoryStream memoria = new MemoryStream(documento.documento))
                {
                    using (var imagen = Image.FromStream(memoria))
                    {
                        var archivo = $"C:\\Users\\user\\Downloads\\{tipo.Nombre + "_" + prestamo.IdCliente}";
                        imagen.Save(archivo + "png");
                        imagen.Save(archivo + "jpg");
                    }
                }
                respuesta.Datos = documento;
                respuesta.Ok = true;
                respuesta.Mensaje = $"Se logró descargar exitósamente en la dirección C:\\Users\\user\\Downloads\\";
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
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