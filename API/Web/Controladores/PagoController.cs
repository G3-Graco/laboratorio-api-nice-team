using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Helpers;

namespace Web.Controladores
{
	[ApiController]
	[Route("api/[controller]")]
	public class PagoController : ControllerBase
	{
		private IPagoServicio _servicio;
		public PagoController(IPagoServicio servicio)
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

		/// <summary>
		/// Método para obtener consultar los pagos de una cuenta.
		/// </summary>
		/// <returns>Respuesta con ienumerable de pagos</returns>
		[Authorize]
		[HttpGet("pagoscuenta")]
		public async Task<ActionResult<Respuesta<IEnumerable<Pago>>>> GetPagosCuenta(int idusuariosesion) //query
		{
			try
			{
				var Respuesta = await _servicio.ConsultarPagosDeUnaCuenta(idusuariosesion);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

        /// <summary>
        /// Método para obtener consultar los pagos realizados de un préstamo.
        /// </summary>
        /// <returns>Respuesta con ienumerable de pagos</returns>
        [Authorize]
        [HttpGet("pagosprestamo")]
        public async Task<ActionResult<Respuesta<IEnumerable<Pago>>>> GetPagosPretamo(int idusuariosesion, int idPrestamo) //query
        {
            try
            {
                var Respuesta = await _servicio.ConsultarPagosPorPrestamoValidado(idusuariosesion, idPrestamo);

                return Ok(Respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
