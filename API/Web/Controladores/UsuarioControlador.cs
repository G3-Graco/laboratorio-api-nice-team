using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controladores
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsuarioControlador : ControllerBase
	{
		private IUsuarioServicio _servicio;
		public UsuarioControlador(IUsuarioServicio servicio)
		{
			_servicio = servicio;
		}

		/// <summary>
		/// Método para obtener lista de usuarios
		/// </summary>
		/// <returns>Respuesta con objeto IEnumerable de usuarios</returns>
		[HttpGet]
		public async Task<ActionResult<Respuesta<IEnumerable<Usuario>>>> Get()
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
		/// Método para obtener un usuario
		/// </summary>
		/// <returns>Respuesta con objeto usuario</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Respuesta<Usuario>>> Get(int id)
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
		/// Método para crear un usuario
		/// </summary>
		/// <returns>Respuesta con objeto usuario</returns>
		[HttpPost]
		public async Task<ActionResult<Respuesta<Usuario>>> Post([FromBody] Usuario usuario)
		{
			try
			{
				var Respuesta = await _servicio.Agregar(usuario);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para actualizar datos de un usuario
		/// </summary>
		/// <returns>Respuesta con objeto usuario</returns>
		[HttpPut("{id}")]
		public async Task<ActionResult<Respuesta<Usuario>>> Put(int id, [FromBody] Usuario usuario)
		{
			try
			{
				var Respuesta = await _servicio.Actualizar(id, usuario);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		/// <summary>
		/// Método para eliminar a un usuario
		/// </summary>
		/// <returns>Respuesta con mensaje de éxito de eliminado y datos null</returns>
		[HttpDelete]
		public async Task<ActionResult<Respuesta<Usuario>>> Delete(int id)
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



		[HttpPost("iniciosesion")]
		public async Task<IActionResult> Login([FromBody] Usuario usuario)
		{
			try
			{
				var Respuesta = await _servicio.IniciarSesion(usuario.NombreUsuario, usuario.Contrasena);

				return Ok(Respuesta);
			}
			catch(Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}			
		}

		[HttpPost("registrarse")]
		public async Task<IActionResult> Registrarse([FromBody] ModeloRegistrarse modeloRegistrarse)
		{
			try
			{
				var Respuesta = await _servicio.Registrarse(modeloRegistrarse);

				return Ok(Respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

	}
}
