using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Controladores
{
	[ApiController]
	[Route("api/[controller]")]
	public class PagoControlador : ControllerBase
	{
		private IPagoServicio _servicio;
		public PagoControlador(IPagoServicio servicio)
		{
			_servicio = servicio;
		}

		/// <summary>
		/// Método para obtener lista de pagos
		/// </summary>
		/// <returns>Respuesta con objeto IEnumerable de pagos</returns>
		[HttpGet]
		public async Task<ActionResult<Respuesta<IEnumerable<Pago>>>> Get()
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
		/// Método para obtener un pago
		/// </summary>
		/// <returns>Respuesta con objeto pago</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Respuesta<Pago>>> Get(int id)
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
		/// Método para crear un pago
		/// </summary>
		/// <returns>Respuesta con objeto pago</returns>
		[HttpPost]
		public async Task<ActionResult<Respuesta<Pago>>> Post([FromBody] Pago pago)
		{
			try
			{
				var Respuesta = await _servicio.Agregar(pago);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para actualizar datos de un pago
		/// </summary>
		/// <returns>Respuesta con objeto pago</returns>
		[HttpPut("{id}")]
		public async Task<ActionResult<Respuesta<Pago>>> Put(int id, [FromBody] Pago pago)
		{
			try
			{
				var Respuesta = await _servicio.Actualizar(id, pago);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para eliminar a un pago
		/// </summary>
		/// <returns>Respuesta con mensaje de éxito de eliminado y datos null</returns>
		[HttpDelete]
		public async Task<ActionResult<Respuesta<Pago>>> Delete(int id)
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
