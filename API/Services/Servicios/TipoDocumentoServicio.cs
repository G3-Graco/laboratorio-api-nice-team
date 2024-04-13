using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;

namespace Services.Servicios
{
    public class TipoDocumentoServicio : ITipoDocumentoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public TipoDocumentoServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
        public Task<Respuesta<TipoDocumento>> Actualizar(int entidadParaActualizarId, TipoDocumento nuevosValoresEntidad)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<TipoDocumento>> Agregar(TipoDocumento tipo)
        {
            var respuesta = new Respuesta<TipoDocumento>();
            var todos = await _unidadDeTrabajo.TipoDocumentoRepositorio.ObtenerTodosAsincrono();
            if (todos.Any(x => x.Nombre == tipo.Nombre))
            {
                respuesta.Datos = null; 
                respuesta.Mensaje = "Creación denegada. Nombre ya existe";
                respuesta.Ok = false;
                return respuesta;
            }
            respuesta.Datos = tipo; 
            respuesta.Mensaje = "Creación realizada con éxito";
            respuesta.Ok = true;
            return respuesta;
        }

        public async Task<Respuesta<TipoDocumento>> ObternerPorIdAsincrono(int id)
        {
            var respuesta = new Respuesta<TipoDocumento>();
            var tipo = await _unidadDeTrabajo.TipoDocumentoRepositorio.ObtenerPorIdAsincrono(id);
            if (tipo == null)
            {
                respuesta.Datos = null;
                respuesta.Ok = false;
                respuesta.Mensaje = "Consulta fallida. No existe tipo de documento con tal id";
                return respuesta;
            }
            respuesta.Datos = tipo;
            respuesta.Ok = true;
            respuesta.Mensaje = "Consulta realizada con éxito";
            return respuesta;
        }

        public async Task<Respuesta<IEnumerable<TipoDocumento>>> ObternerTodosAsincrono()
        {
            var todos = await _unidadDeTrabajo.TipoDocumentoRepositorio.ObtenerTodosAsincrono();
            var respuesta = new Respuesta<IEnumerable<TipoDocumento>>() {
                Datos = todos, 
                Mensaje = "Obtención exitosa", 
                Ok = true
            };
            return respuesta;
        }

        public Task<Respuesta<TipoDocumento>> Remover(int entidadId)
        {
            throw new NotImplementedException();
        }

    }
}