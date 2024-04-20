using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;

namespace Services.Servicios
{
    public class CuotaServicio : ICuotaServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public CuotaServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
        public Task<Respuesta<Cuota>> Actualizar(int entidadParaActualizarId, Cuota nuevosValoresEntidad)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<Cuota>> Agregar(Cuota cuota)
        {
            var prestamo = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerPorIdAsincrono(cuota.IdPrestamo);
            if (prestamo == null) throw new ArgumentException("No existe un préstamo con tal id");
            await _unidadDeTrabajo.CuotaRepositorio.AgregarAsincrono(cuota);
            await _unidadDeTrabajo.CommitAsync();
            var respuesta = new Respuesta<Cuota>() {
                Datos = cuota, 
                Ok = true, 
                Mensaje = "Creación de cuota exitosa"
            };
            return respuesta;
        }

        public async Task<Respuesta<IEnumerable<Cuota>>> ConsultarCuotasPorPrestamo(int idusuariosesion, int IdPrestamo)
        {
            if (idusuariosesion == null || idusuariosesion == 0)
            {
                throw new ArgumentException("Token inválido, vuelva a iniciar sesión");
            }

            Prestamo prestamo = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerPorIdAsincrono(IdPrestamo);

            Usuario usuario = await _unidadDeTrabajo.UsuarioRepositorio.ObtenerPorIdAsincrono(idusuariosesion);

            if (prestamo.IdCliente != usuario.ClienteId)
            {
                return new Respuesta<IEnumerable<Cuota>> { Ok = false, Mensaje = "Consulta inválida. No se puede consultar un préstamo que no pertenezca al usuario actual", Datos = null };
            }

            var cuotas = await _unidadDeTrabajo.CuotaRepositorio.ConsultarCuotasDeUnPrestamo(IdPrestamo);
            if (cuotas == null)
                throw new ArgumentException("No se ha encontrado movimientos para este préstamo");

            return new Respuesta<IEnumerable<Cuota>> { Ok = true, Mensaje = "Cuotas consultadas", Datos = cuotas };
        }

        public async Task<Respuesta<Cuota>> ObternerPorIdAsincrono(int id)
        {
            var respuesta = new Respuesta<Cuota>();
            var cuota = await _unidadDeTrabajo.CuotaRepositorio.ObtenerPorIdAsincrono(id);
            if (cuota == null) {
                respuesta.Datos = null;
                respuesta.Ok = false;
                respuesta.Mensaje = "Cuota no encontrada. No existe cuota alguna con tal id";
                return respuesta;
            }
            respuesta.Datos = cuota;
            respuesta.Ok = true;
            respuesta.Mensaje = "Cuota encontrada exitósamente";
            return respuesta;
        }

        public Task<Respuesta<IEnumerable<Cuota>>> ObternerTodosAsincrono()
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Cuota>> Remover(int entidadId)
        {
            throw new NotImplementedException();
        }
    }
}