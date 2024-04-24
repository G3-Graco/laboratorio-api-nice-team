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
    public class PlazoController : ControllerBase
    {
        private IPlazoServicio _servicio;
		public PlazoController(IPlazoServicio servicio)
		{
			_servicio = servicio;
		}
        /// <summary>
		/// Método para obtener lista de plazos
		/// </summary>
		/// <returns>Respuesta con objeto IEnumerable de plazos</returns>
		[HttpGet]
		public async Task<ActionResult<Respuesta<IEnumerable<Plazo>>>> Get()
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
		/// Método para obtener un plazo
		/// </summary>
		/// <returns>Respuesta con objeto plazo</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Respuesta<Plazo>>> Get(int id)
		{
			try
			{
				var Respuesta = await _servicio.ObternerPorIdAsincrono(id);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para crear un plazo
		/// </summary>
		/// <returns>Respuesta con objeto plazo</returns>
		[HttpPost]
		public async Task<ActionResult<Respuesta<Plazo>>> Post([FromBody] Plazo plazo)
		{
			try
			{
				var Respuesta = await _servicio.Agregar(plazo);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para actualizar datos de un plazo
		/// </summary>
		/// <returns>Respuesta con objeto plazo</returns>
		[HttpPut("{id}")]
		public async Task<ActionResult<Respuesta<Plazo>>> Put(int id, [FromBody] Plazo plazo)
		{
			try
			{
				var Respuesta = await _servicio.Actualizar(id, plazo);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para eliminar a un plazo
		/// </summary>
		/// <returns>Respuesta con mensaje de éxito de eliminado y datos null</returns>
		[HttpDelete]
		public async Task<ActionResult<Respuesta<Plazo>>> Delete(int id)
		{
			try
			{
				var Respuesta = await _servicio.Remover(id);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
    }
}