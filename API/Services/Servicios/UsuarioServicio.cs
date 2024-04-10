using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Services.Validadores;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Servicios
{
	public class UsuarioServicio : IUsuarioServicio
	{
		private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public UsuarioServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}

		public async Task<Respuesta<Usuario>> Actualizar(int entidadParaActualizarId, Usuario nuevosValoresEntidad)
		{
			UsuarioValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevosValoresEntidad);

			if (!resultadoValidacion.IsValid)
			{
				//new Respuesta<Cliente>{Ok = false, Mensaje = resultadoValidacion.Errors[0].ErrorMessage.ToString(), Datos = null};
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());

			}

			Usuario UsuarioParaActualizar = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId);

			if (UsuarioParaActualizar == null)
				throw new ArgumentException("Id del usuario a actualizar inválido");

			UsuarioParaActualizar.NombreUsuario = nuevosValoresEntidad.NombreUsuario;
			UsuarioParaActualizar.Contrasena = nuevosValoresEntidad.Contrasena;
			UsuarioParaActualizar.ClienteId = nuevosValoresEntidad.ClienteId;


			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Usuario> { Ok = true, Mensaje = "Usuario actualizado con éxito", Datos = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(entidadParaActualizarId) };

		}

		public async Task<Respuesta<Usuario>> Agregar(Usuario nuevaEntitidad)
		{
			UsuarioValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(nuevaEntitidad);

			if (!resultadoValidacion.IsValid)
			{
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());
			}

			await _unidadDeTrabajo.UsuarioRepositorio.AgregarAsincrono(nuevaEntitidad);
			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Usuario> { Ok = true, Mensaje = "Usuario creado con éxito", Datos = nuevaEntitidad };

		}

		public async Task<Respuesta<Usuario>> ObternerPorIdAsincrono(int id)
		{
			return new Respuesta<Usuario> { Ok = true, Mensaje = "Usuario obtenido", Datos = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(id) };
		}

		public async Task<Respuesta<IEnumerable<Usuario>>> ObternerTodosAsincrono()
		{
			return new Respuesta<IEnumerable<Usuario>> { Ok = true, Mensaje = "Usuarios obtenidos", Datos = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerTodosAsincrono() };
		}

		public async Task<Respuesta<Usuario>> Remover(int entidadId)
		{
			Usuario usuario = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(entidadId);
			_unidadDeTrabajo.UsuarioRepositorio.Remover(usuario);
			await _unidadDeTrabajo.CommitAsync();
			return new Respuesta<Usuario> { Ok = true, Mensaje = "Usuario eliminado", Datos = null };
		}

		public async Task<Respuesta<string>> IniciarSesion(string nombreusuario, string contrasena)
		{
			Usuario usuario = await _unidadDeTrabajo.UsuarioRepositorio.IniciarSesion(nombreusuario, contrasena);

			if (usuario == null)
			{
				return new Respuesta<string> { Ok = false, Mensaje = "Nombre de usuario y/o contraseña incorrecta.", Datos = string.Empty};
				
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("c2c3111663e00afe901d9c00ab169d36");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, usuario.NombreUsuario),
					new Claim("id", usuario.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddMinutes(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			string usuarioToken = tokenHandler.WriteToken(token);
			return new Respuesta<string> { Ok = true, Mensaje = "Inicio de sesión correcto.", Datos = usuarioToken };
		}

		public async Task<Respuesta<bool>> ComprobarTokenConId(string token, int idusuario)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
		
			var key = Encoding.ASCII.GetBytes("c2c3111663e00afe901d9c00ab169d36");
			tokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				IssuerSigningKey = new SymmetricSecurityKey(key),			
				ClockSkew = TimeSpan.Zero //quita margen de tolerancia
			}, out SecurityToken validatedToken); //out asigna token validado

			var jwtToken = (JwtSecurityToken)validatedToken;
			var idusuariotoken = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

			if (idusuario == idusuariotoken)
			{
				return new Respuesta<bool> { Ok = true, Mensaje = "El id del token es igual al del id a comparar", Datos = true };
			}
			else
			{
				return new Respuesta<bool> { Ok = false, Mensaje = "El id del token no es igual al del id a comparar", Datos = false };
			}
		}
	}
}
