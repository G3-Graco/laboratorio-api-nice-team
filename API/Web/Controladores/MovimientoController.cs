using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;

namespace Web.Controladores
{
	[ApiController]
	[Route("api/[controller]")]
	public class MovimientoController : ControllerBase
	{
		private IMovimientoServicio _servicio;
		private ITipoMovimientoServicio _servicioTipo;
		public MovimientoController(IMovimientoServicio servicio, ITipoMovimientoServicio servicioTipo)
		{
			_servicio = servicio;
			_servicioTipo = servicioTipo;
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
				var tipo = await _servicioTipo.ObternerPorIdAsincrono(movimiento.TipoMovimientoId);
				var respuesta = new Respuesta<Movimiento>();
				switch (tipo.Datos?.Nombre)
				{
					case "transferencia": 
						respuesta = await _servicio.RealizarTransferencia(movimiento);
						break;
					case "depósito": 
						respuesta = await _servicio.RealizarDeposito(movimiento);
						break;
					case "retiro":
						respuesta = await _servicio.RealizarRetiro(movimiento);
						break;
					default: 
						respuesta = await _servicio.Agregar(movimiento);
						break;
				}
				return Ok(respuesta);
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
		/// <summary>
		/// Método para obtener consultar los movimientos de una cuenta.
		/// </summary>
		/// <returns>Respuesta con ienumerable de movimientos</returns>
		[Authorize]
		[HttpGet("movimientoscuenta")]
		public async Task<ActionResult<Respuesta<IEnumerable<Movimiento>>>> PostMovimientosCuenta(int idusuariosesion) //query
		{
			try
			{
				var Respuesta = await _servicio.ConsultarMovimientosDeUnaCuenta(idusuariosesion);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

	}
}
