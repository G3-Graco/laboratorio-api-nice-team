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
        public async Task<Respuesta<TipoDocumento>> Actualizar(int id, TipoDocumento tipoNuevo)
        {
            try
            {
                var respuesta = new Respuesta<TipoDocumento>();
                if (id != tipoNuevo.Id) throw new ArgumentException("Los ids no son iguales");
                var tipo = await _unidadDeTrabajo.TipoDocumentoRepositorio.ObtenerPorIdAsincrono(id);
                tipo.Nombre = tipoNuevo.Nombre;
                tipo.Descripcion = tipoNuevo.Descripcion;
                await _unidadDeTrabajo.TipoDocumentoRepositorio.Actualizar(tipo);
                await _unidadDeTrabajo.CommitAsync();
                respuesta.Datos = tipo;
                respuesta.Ok = true;
                respuesta.Mensaje = "Modificación exitosa del tipo de documento";
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
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
            await _unidadDeTrabajo.TipoDocumentoRepositorio.AgregarAsincrono(tipo);
            await _unidadDeTrabajo.CommitAsync();
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

        public async Task<Respuesta<TipoDocumento>> Remover(int id)
        {
            try
            {
                var respuesta = new Respuesta<TipoDocumento>();
                var tipo = await _unidadDeTrabajo.TipoDocumentoRepositorio.ObtenerPorIdAsincrono(id);
                if (tipo == null) throw new ArgumentException("No existe un tipo de documento con tal id");
                var todos = await _unidadDeTrabajo.DocumentoRepositorio.ObtenerTodosAsincrono();
                if (todos.Any(x => x.IdTipo == id)) throw new ArgumentException("Ya existen documentos con este tipo");
                _unidadDeTrabajo.TipoDocumentoRepositorio.Remover(tipo);
                await _unidadDeTrabajo.CommitAsync();
                respuesta.Datos = tipo;
                respuesta.Ok = true;
                respuesta.Mensaje = "Tipo de documento eliminado con éxito";
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}