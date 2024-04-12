using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Services.Validadores;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

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

			var entidadagregada = await _unidadDeTrabajo.UsuarioRepositorio.AgregarAsincrono(nuevaEntitidad);
			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<Usuario> { Ok = true, Mensaje = "Usuario creado con éxito", Datos = entidadagregada };

		}

		public async Task<Respuesta<Usuario>> ObternerPorIdAsincrono(int id)
		{
			var obtenido = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(id);

			if (obtenido == null)
			{
				return new Respuesta<Usuario> { Ok = false, Mensaje = "Usuario no encontrado", Datos = obtenido };
			}
			else
			{
				return new Respuesta<Usuario> { Ok = true, Mensaje = "Usuario obtenido", Datos = obtenido };
			}
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
			var key = Encoding.ASCII.GetBytes("siberiaypaulamejoresamigasporsiempre");
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

		public async Task<Respuesta<ModeloRegistrarse>> Registrarse(ModeloRegistrarse modeloRegistrarse)
		{
			ModeloRegistrarseValidador validador = new();

			var resultadoValidacion = await validador.ValidateAsync(modeloRegistrarse);

			if (!resultadoValidacion.IsValid)
			{
				//new Respuesta<Cliente>{Ok = false, Mensaje = resultadoValidacion.Errors[0].ErrorMessage.ToString(), Datos = null};
				throw new ArgumentException(resultadoValidacion.Errors[0].ErrorMessage.ToString());


			}
			ClienteServicio clienteServicio = new (_unidadDeTrabajo);
			CuentaServicio cuentaServicio = new(_unidadDeTrabajo);

			var clienteAgregado = await clienteServicio.Agregar(new Cliente 
			{
				Id = 0,
				Nombre = modeloRegistrarse.Nombre,
				Apellido = modeloRegistrarse.Apellido,
				Cedula = modeloRegistrarse.Cedula,
				Correo = modeloRegistrarse.Correo,
				FechaNacimiento = modeloRegistrarse.FechaNacimiento,
				Telefono = modeloRegistrarse.Telefono,
				Direccion = modeloRegistrarse.Direccion
			});

			string numeroDeCuenta = modeloRegistrarse.Cedula.Substring(0, 7);
			var random = new Random();
			for( int i = 0; i < 5; i++ )
			{
				numeroDeCuenta += random.Next(9).ToString();
			}
			
			await cuentaServicio.Agregar(new Cuenta
			{
				Identificador = Int64.Parse(numeroDeCuenta),
				Saldo = 0,
				ClienteId = clienteAgregado.Datos.Id
			});

			await _unidadDeTrabajo.UsuarioRepositorio.AgregarAsincrono(new Usuario
			{
				Id = 0,
				NombreUsuario = modeloRegistrarse.NombreUsuario,
				Contrasena = modeloRegistrarse.Contrasena,
				ClienteId = clienteAgregado.Datos.Id
			});
			await _unidadDeTrabajo.CommitAsync();

			return new Respuesta<ModeloRegistrarse> { Ok = true, Mensaje = "Usuario registrado correctamente.", Datos = modeloRegistrarse };
		}
	}
}
