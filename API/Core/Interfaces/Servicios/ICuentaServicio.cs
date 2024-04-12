﻿using Core.Entidades;
using Core.Respuestas;

namespace Core.Interfaces.Servicios
{
	public interface ICuentaServicio : IBaseServicio<Cuenta>
	{
		Task<Respuesta<Cuenta>> ConsultarCuentaDeUnCliente(int idUsuarioSesion);
		Task<Respuesta<Cuenta>> ActualizarSaldo(int idCuenta, double nuevoSaldo);
	}
}
