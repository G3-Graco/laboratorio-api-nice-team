using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;

namespace Services.Servicios
{
    public class TipoMovimientoServicio : ITipoMovimientoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public TipoMovimientoServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
        public Task<Respuesta<TipoMovimiento>> Actualizar(int entidadParaActualizarId, TipoMovimiento nuevosValoresEntidad)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<TipoMovimiento>> Agregar(TipoMovimiento tipo)
        {
            var respuesta = new Respuesta<TipoMovimiento>();
            var todos = await _unidadDeTrabajo.TipoMovimientoRepositorio.ObtenerTodosAsincrono();
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

        public async Task<Respuesta<TipoMovimiento>> ObternerPorIdAsincrono(int id)
        {
            var respuesta = new Respuesta<TipoMovimiento>();
            var tipo = await _unidadDeTrabajo.TipoMovimientoRepositorio.ObtenerPorIdAsincrono(id);
            if (tipo == null)
            {
                respuesta.Datos = null;
                respuesta.Ok = false;
                respuesta.Mensaje = "Consulta fallida. No existe tipo de movimiento con tal id";
                return respuesta;
            }
            respuesta.Datos = tipo;
            respuesta.Ok = true;
            respuesta.Mensaje = "Consulta realizada con éxito";
            return respuesta;
        }

        public async Task<Respuesta<IEnumerable<TipoMovimiento>>> ObternerTodosAsincrono()
        {
            var todos = await _unidadDeTrabajo.TipoMovimientoRepositorio.ObtenerTodosAsincrono();
            var respuesta = new Respuesta<IEnumerable<TipoMovimiento>>() {
                Datos = todos, 
                Mensaje = "Obtención exitosa", 
                Ok = true
            };
            return respuesta;
        }

        public Task<Respuesta<TipoMovimiento>> Remover(int entidadId)
        {
            throw new NotImplementedException();
        }
    }
}