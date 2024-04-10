using Microsoft.AspNetCore.Mvc;
using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;

namespace Web.Controladores
{
	[ApiController]
	[Route("api/[controller]")]
	public class ClienteControlador : ControllerBase
	{
		private IClienteServicio _servicio;
		public ClienteControlador(IClienteServicio clienteServicio)
		{
			_servicio = clienteServicio;
		}

		/// <summary>
		/// Método para obtener lista de clientes
		/// </summary>
		/// <returns>Respuesta con objeto IEnumerable de clientes</returns>
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
				return BadRequest(ex.Message);
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
				return BadRequest(ex.Message);
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
				return BadRequest(ex.Message);
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
				return BadRequest(ex.Message);
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
				return BadRequest(ex.Message);
			}
		}
	}
}