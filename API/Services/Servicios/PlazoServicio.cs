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
    public class PlazoServicio : IPlazoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public PlazoServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
        public Task<Respuesta<Plazo>> Actualizar(int id, Plazo plazo)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<Plazo>> Agregar(Plazo plazo)
        {
            try
            {
                var nuevo = await _unidadDeTrabajo.PlazoRepositorio.AgregarAsincrono(plazo);
                await _unidadDeTrabajo.CommitAsync();
                var respuesta = new Respuesta<Plazo>() {
                    Mensaje = "Plazo creado exitósamente", 
                    Ok = true, 
                    Datos = nuevo
                };
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Respuesta<Plazo>> ObternerPorIdAsincrono(int id)
        {
            try
            {
                var respuesta = new Respuesta<Plazo>();
                var plazo = await _unidadDeTrabajo.PlazoRepositorio.ObtenerPorIdAsincrono(id);
                if (plazo == null)
                {
                    respuesta.Mensaje = "No existe un plazo con tal id";
                    respuesta.Ok = false;
                    respuesta.Datos = null;
                } else {
                    respuesta.Mensaje = "Plazo encontrado exitósamente";
                    respuesta.Ok = true;
                    respuesta.Datos = plazo;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Respuesta<IEnumerable<Plazo>>> ObternerTodosAsincrono()
        {
            try
            {
                var todos = await _unidadDeTrabajo.PlazoRepositorio.ObtenerTodosAsincrono();
                var respuesta = new Respuesta<IEnumerable<Plazo>>() {
                    Datos = todos, 
                    Ok = true, 
                    Mensaje = "Plazos encontrados exitósamente"
                };
            return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<Respuesta<Plazo>> Remover(int entidadId)
        {
            throw new NotImplementedException();
        }
    }
}