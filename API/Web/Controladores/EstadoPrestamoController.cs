using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoPrestamoController : ControllerBase
    {
        private IEstadoPrestamoServicio _servicio;
		public EstadoPrestamoController(IEstadoPrestamoServicio servicio)
		{
			_servicio = servicio;
		}

        /// <summary>
        /// Método para obtener todos los estados de préstamos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta<IEnumerable<EstadoPrestamo>>>> Get()
		{
			try
			{
				var Respuesta = await _servicio.ObternerTodosAsincrono();

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

        /// <summary>
        /// Método para crear un estado de préstamo
        /// </summary>
        /// <param name="estado">El objeto de estado de préstamo</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Respuesta<EstadoPrestamo>>> Post([FromBody] EstadoPrestamo estado)
		{
			try
			{
                var Respuesta = await _servicio.Agregar(estado);

                return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
    }
}