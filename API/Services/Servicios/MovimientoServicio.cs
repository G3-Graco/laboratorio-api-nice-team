using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Services.Servicios
{
    public class MovimientoServicio : IMovimientoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public MovimientoServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
        public Task<Respuesta<Movimiento>> Actualizar(int entidadParaActualizarId, Movimiento nuevosValoresEntidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Movimiento>> Agregar(Movimiento nuevaEntitidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Movimiento>> ObternerPorIdAsincrono(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<IEnumerable<Movimiento>>> ObternerTodosAsincrono()
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Movimiento>> Remover(int entidadId)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<Movimiento>> RealizarTransferencia(Movimiento movimiento)
        {
            try
            {
                var respuesta = new Respuesta<Movimiento>();
                if (movimiento?.Id == null) throw new ArgumentException("No existe un préstamo con tal id");
                var cuentaReceptora = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(movimiento.CuentaReceptoraIdentificador);
                if (cuentaReceptora == null) throw new ArgumentException("No existe una cuenta receptora con tal id");
                var cuentaOrigen = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(movimiento.CuentaOrigenIdentificador);
                if (cuentaOrigen == null) throw new ArgumentException("No existe una cuenta origen con tal id");
                if (movimiento.Monto > cuentaOrigen.Saldo) {
                    respuesta.Ok = false;
                    respuesta.Mensaje = "Transferencia denegada. El saldo de la cuenta es insuficiente";
                    respuesta.Datos = null;
                    return respuesta;
                }
                cuentaOrigen.Saldo -= movimiento.Monto;
                cuentaReceptora.Saldo += movimiento.Monto;
                await _unidadDeTrabajo.CuentaRepositorio.Actualizar(cuentaOrigen);
                await _unidadDeTrabajo.CuentaRepositorio.Actualizar(cuentaReceptora);
                await _unidadDeTrabajo.MovimientoRepositorio.AgregarAsincrono(movimiento);
                respuesta.Ok = true;
                respuesta.Mensaje = "Transferencia realizada con éxito";
                respuesta.Datos = movimiento;
                return respuesta;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Respuesta<Movimiento>> RealizarDeposito(Movimiento movimiento)
        {
            try
            {
                var respuesta = new Respuesta<Movimiento>();
                if (movimiento?.Id == null) throw new ArgumentException("No existe un préstamo con tal id");
                var cuenta = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(movimiento.CuentaReceptoraIdentificador);
                if (cuenta == null) throw new ArgumentException("No existe una cuenta receptora con tal id");
                cuenta.Saldo += movimiento.Monto;
                await _unidadDeTrabajo.CuentaRepositorio.Actualizar(cuenta);
                await _unidadDeTrabajo.MovimientoRepositorio.AgregarAsincrono(movimiento);
                respuesta.Ok = true;
                respuesta.Mensaje = "Déposito realizada con éxito";
                respuesta.Datos = movimiento;
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Respuesta<Movimiento>> RealizarRetiro(Movimiento movimiento)
        {
            try
            {
                var respuesta = new Respuesta<Movimiento>();
                if (movimiento?.Id == null) throw new ArgumentException("No existe un préstamo con tal id");
                var cuenta = await _unidadDeTrabajo.CuentaRepositorio.ObtenerPorIdAsincrono(movimiento.CuentaOrigenIdentificador);
                if (cuenta == null) throw new ArgumentException("No existe una cuenta con tal id");
                if (movimiento.Monto > cuenta.Saldo) {
                    respuesta.Ok = false;
                    respuesta.Mensaje = "Retiro denegado. El saldo de la cuenta es insuficiente";
                    respuesta.Datos = null;
                    return respuesta;
                }
                cuenta.Saldo -= movimiento.Monto;
                await _unidadDeTrabajo.CuentaRepositorio.Actualizar(cuenta);
                await _unidadDeTrabajo.MovimientoRepositorio.AgregarAsincrono(movimiento);
                respuesta.Ok = true;
                respuesta.Mensaje = "Transferencia realizada con éxito";
                respuesta.Datos = movimiento;
                return respuesta;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}