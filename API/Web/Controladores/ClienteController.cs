using Microsoft.AspNetCore.Mvc;
using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Web.Helpers;

namespace Web.Controladores
{
	[ApiController]
	[Route("api/[controller]")]
	public class ClienteController : ControllerBase
	{
		private IClienteServicio _servicio;
		public ClienteController(IClienteServicio clienteServicio)
		{
			_servicio = clienteServicio;
		}

		/// <summary>
		/// Método para obtener lista de clientes
		/// </summary>
		/// <returns>Respuesta con objeto IEnumerable de clientes</returns>
		[Authorize]
		[HttpGet]
		public async Task<ActionResult<Respuesta<IEnumerable<Cliente>>>> Get()
		{
			try
			{
				var ClienteRespuesta = await _servicio.ObternerTodosAsincrono();

				return Ok(ClienteRespuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para obtener un cliente
		/// </summary>
		/// <returns>Respuesta con objeto cliente</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Respuesta<Cliente>>> Get(int id)
		{
			try
			{
				var ClienteRespuesta = await _servicio.ObternerPorIdAsincrono(id);

				return Ok(ClienteRespuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para crear un cliente
		/// </summary>
		/// <returns>Respuesta con objeto cliente</returns>
		[HttpPost]
		public async Task<ActionResult<Respuesta<Cliente>>> Post([FromBody] Cliente cliente)
		{
			try
			{
				var ClienteRespuesta = await _servicio.Agregar(cliente);

				return Ok(ClienteRespuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para actualizar datos de un cliente
		/// </summary>
		/// <returns>Respuesta con objeto cliente</returns>
		[HttpPut("{id}")]
		public async Task<ActionResult<Respuesta<Cliente>>> Put(int id, [FromBody] Cliente cliente)
		{
			try
			{
				var ClienteRespuesta = await _servicio.Actualizar(id, cliente);

				return Ok(ClienteRespuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para eliminar a un cliente
		/// </summary>
		/// <returns>Respuesta con mensaje de éxito de eliminado y datos null</returns>
		[HttpDelete]
		public async Task<ActionResult<Respuesta<Cliente>>> Delete(int id)
		{
			try
			{
				var ClienteRespuesta = await _servicio.Remover(id);

				return Ok(ClienteRespuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}


		}

		/// <summary>
		/// Método para obtener un cliente de manera validada.
		/// </summary>
		/// <returns>Respuesta con objeto cliente</returns>
		[Authorize]
		[HttpGet("consultarcliente")]
		public async Task<ActionResult<Respuesta<Cliente>>> GetConsultarCliente(int idusuariosesion)
		{
			try
			{
				var ClienteRespuesta = await _servicio.ConsultarClienteValidado(idusuariosesion);

				return Ok(ClienteRespuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
	}
}