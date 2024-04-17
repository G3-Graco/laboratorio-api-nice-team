using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;

namespace Web.Controladores
{
	[ApiController]
	[Route("api/[controller]")]
	public class CuentaController : ControllerBase
	{
		private ICuentaServicio _servicio;
		public CuentaController(ICuentaServicio servicio)
		{
			_servicio = servicio;
		}

		/// <summary>
		/// Método para obtener lista de cuentas
		/// </summary>
		/// <returns>Respuesta con objeto IEnumerable de cuentas</returns>
		[HttpGet]
		public async Task<ActionResult<Respuesta<IEnumerable<Cuenta>>>> Get()
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
		/// Método para obtener una cuenta
		/// </summary>
		/// <returns>Respuesta con objeto cuenta</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Respuesta<Cuenta>>> Get(int id)
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
		/// Método para crear una cuenta
		/// </summary>
		/// <returns>Respuesta con objeto cuenta</returns>
		[HttpPost]
		public async Task<ActionResult<Respuesta<Cuenta>>> Post([FromBody] Cuenta cuenta)
		{
			try
			{
				var Respuesta = await _servicio.Agregar(cuenta);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para actualizar datos de una cuenta
		/// </summary>
		/// <returns>Respuesta con objeto cuenta</returns>
		[HttpPut("{id}")]
		public async Task<ActionResult<Respuesta<Cuenta>>> Put(int id, [FromBody] Cuenta cuenta)
		{
			try
			{
				var Respuesta = await _servicio.Actualizar(id, cuenta);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para eliminar a una cuenta
		/// </summary>
		/// <returns>Respuesta con mensaje de éxito de eliminado y datos null</returns>
		[HttpDelete]
		public async Task<ActionResult<Respuesta<Cuenta>>> Delete(int id)
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
		/// Método para obtener una cuenta de un cliente en específico
		/// </summary>
		/// <returns>Respuesta con objeto cuenta</returns>
		[HttpGet("cuentacliente")]
		public async Task<ActionResult<Respuesta<Cuenta>>> GetCuentaCliente(int idusuariosesion) //query
		{
			try
			{
				var Respuesta = await _servicio.ConsultarCuentaDeUnCliente(idusuariosesion); 

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para obtener una cuenta de un cliente en específico
		/// </summary>
		/// <returns>Respuesta con objeto cuenta</returns>
		[Authorize]
		[HttpPost("cuentacliente")]
		public async Task<ActionResult<Respuesta<Cuenta>>> PostCuentaCliente(int idusuariosesion) //query
		{
			try
			{
				var Respuesta = await _servicio.ConsultarCuentaDeUnCliente(idusuariosesion);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para obtener actualizar el saldo de la cuenta de un cliente en específico
		/// </summary>
		/// <returns>Respuesta con objeto cuenta</returns>
		[Authorize]
		[HttpPost("actualizarsaldo")]
		public async Task<ActionResult<Respuesta<Cuenta>>> PostActualizarSaldo(int idusuariosesion, double nuevoSaldo) //query
		{
			try
			{
				var Respuesta = await _servicio.ActualizarSaldo(idusuariosesion, nuevoSaldo);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}
