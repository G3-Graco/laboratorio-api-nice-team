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
    public class EstadoPrestamoServicio : IEstadoPrestamoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public EstadoPrestamoServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
        public Task<Respuesta<EstadoPrestamo>> Actualizar(int entidadParaActualizarId, EstadoPrestamo nuevosValoresEntidad)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<EstadoPrestamo>> Agregar(EstadoPrestamo estado)
        {
            var respuesta = new Respuesta<EstadoPrestamo>();
            try
            {
                if (estado == null) throw new ArgumentException("El estado no puede ser nulo");
                var nuevo = await _unidadDeTrabajo.EstadoPrestamoRepositorio.AgregarAsincrono(estado);
                if (nuevo == null) {
                    respuesta.Datos = null;
                    respuesta.Mensaje = "No se pudo crear el estado de préstamo";
                    respuesta.Ok = false;
                } else {
                    respuesta.Datos = nuevo;
                    respuesta.Ok = true;
                    respuesta.Mensaje = "Creación de estado de préstamo exitosa";
                }
                await _unidadDeTrabajo.CommitAsync();
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<Respuesta<EstadoPrestamo>> ObternerPorIdAsincrono(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<IEnumerable<EstadoPrestamo>>> ObternerTodosAsincrono()
        {
            var respuesta = new Respuesta<IEnumerable<EstadoPrestamo>>();
            try
            {
                var todos = await _unidadDeTrabajo.EstadoPrestamoRepositorio.ObtenerTodosAsincrono();
                respuesta.Mensaje = "obtención de estados de préstamo exitosa";
                respuesta.Datos = todos;
                respuesta.Ok = true;
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<Respuesta<EstadoPrestamo>> Remover(int entidadId)
        {
            throw new NotImplementedException();
        }
    }
}