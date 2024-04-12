using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controladores
{
	[ApiController]
	[Route("api/[controller]")]
	public class MovimientoControlador : ControllerBase
	{
		private IMovimientoServicio _servicio;
		public MovimientoControlador(IMovimientoServicio servicio)
		{
			_servicio = servicio;
		}

		/// <summary>
		/// Método para obtener lista de movimientos
		/// </summary>
		/// <returns>Respuesta con objeto IEnumerable de movimientos</returns>
		[HttpGet]
		public async Task<ActionResult<Respuesta<IEnumerable<Movimiento>>>> Get()
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
		/// Método para obtener un movimiento
		/// </summary>
		/// <returns>Respuesta con objeto movimiento</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Respuesta<Movimiento>>> Get(int id)
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
		/// Método para crear un movimiento
		/// </summary>
		/// <returns>Respuesta con objeto movimiento</returns>
		[HttpPost]
		public async Task<ActionResult<Respuesta<Movimiento>>> Post([FromBody] Movimiento movimiento)
		{
			try
			{
				var Respuesta = await _servicio.Agregar(movimiento);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para actualizar datos de un movimiento
		/// </summary>
		/// <returns>Respuesta con objeto movimiento</returns>
		[HttpPut("{id}")]
		public async Task<ActionResult<Respuesta<Movimiento>>> Put(int id, [FromBody] Movimiento movimiento)
		{
			try
			{
				var Respuesta = await _servicio.Actualizar(id, movimiento);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para eliminar a un movimiento
		/// </summary>
		/// <returns>Respuesta con mensaje de éxito de eliminado y datos null</returns>
		[HttpDelete]
		public async Task<ActionResult<Respuesta<Movimiento>>> Delete(int id)
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
