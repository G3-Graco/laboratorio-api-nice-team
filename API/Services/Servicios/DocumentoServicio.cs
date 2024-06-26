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

        public async Task<Respuesta<byte[]>> ConvertirAByte(Stream archivo)
        {
            var respuesta = new Respuesta<Byte[]>();
            try
            {
                using (MemoryStream memoria = new MemoryStream())
                {
                    archivo.CopyTo(memoria);
                    respuesta.Ok = true;
                    respuesta.Datos = memoria.ToArray();
                    respuesta.Mensaje = "Obtención de bytes del stream exitosa";
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

        public async Task<Respuesta<IEnumerable<Documento>>> ObternerTodosAsincrono()
        {
            try
            {
                var todos = await _unidadDeTrabajo.DocumentoRepositorio.ObtenerTodosAsincrono();
                var respuesta = new Respuesta<IEnumerable<Documento>>() {
                    Datos = todos, 
                    Mensaje = "Se obtuvieron los documentos exitósamente", 
                    Ok = true
                };
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Respuesta<Documento>> Remover(int id)
        {
            try
            {
                var respuesta = new Respuesta<Documento>();
                var documento = await _unidadDeTrabajo.DocumentoRepositorio.ObtenerPorIdAsincrono(id);
                if (documento == null) throw new ArgumentException("El documento no se encuentra en la base de datos");
                _unidadDeTrabajo.DocumentoRepositorio.Remover(documento);
                await _unidadDeTrabajo.CommitAsync();
                respuesta.Datos = documento;
                respuesta.Ok = true;
                respuesta.Mensaje = "Eliminación de documento exitosa";
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}