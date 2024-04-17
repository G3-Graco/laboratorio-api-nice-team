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
        public async Task<Respuesta<TipoMovimiento>> Actualizar(int id, TipoMovimiento tipoNuevo)
        {
            try
            {
                var respuesta = new Respuesta<TipoMovimiento>();
                if (id != tipoNuevo.Id) throw new ArgumentException("Los ids no son iguales");
                var tipo = await _unidadDeTrabajo.TipoMovimientoRepositorio.ObtenerPorIdAsincrono(id);
                tipo.Nombre = tipoNuevo.Nombre;
                tipo.Descripcion = tipoNuevo.Descripcion;
                await _unidadDeTrabajo.TipoMovimientoRepositorio.Actualizar(tipo);
                await _unidadDeTrabajo.CommitAsync();
                respuesta.Datos = tipo;
                respuesta.Ok = true;
                respuesta.Mensaje = "Modificación exitosa del tipo de movimiento";
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
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
            await _unidadDeTrabajo.TipoMovimientoRepositorio.AgregarAsincrono(tipo);
            await _unidadDeTrabajo.CommitAsync();
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

        public async Task<Respuesta<TipoMovimiento>> Remover(int id)
        {
            try
            {
                var respuesta = new Respuesta<TipoMovimiento>();
                var tipo = await _unidadDeTrabajo.TipoMovimientoRepositorio.ObtenerPorIdAsincrono(id);
                if (tipo == null) throw new ArgumentException("No existe un tipo de movimiento con tal id");
                var todos = await _unidadDeTrabajo.DocumentoRepositorio.ObtenerTodosAsincrono();
                if (todos.Any(x => x.IdTipo == id)) throw new ArgumentException("Ya existen movimientos con este tipo");
                _unidadDeTrabajo.TipoMovimientoRepositorio.Remover(tipo);
                await _unidadDeTrabajo.CommitAsync();
                respuesta.Datos = tipo;
                respuesta.Ok = true;
                respuesta.Mensaje = "Tipo de movimiento eliminado con éxito";
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}