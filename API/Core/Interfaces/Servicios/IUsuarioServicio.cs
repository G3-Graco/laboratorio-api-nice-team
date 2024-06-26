﻿using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
	public interface IUsuarioServicio : IBaseServicio<Usuario>
	{
		Task<Respuesta<RespuestaIniciarSesion>> IniciarSesion(string usuario, string contrasena);
		Task<Respuesta<ModeloRegistrarse>> Registrarse(ModeloRegistrarse modeloRegistrarse);
	}
}
