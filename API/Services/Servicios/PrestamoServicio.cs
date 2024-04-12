using Core.Entidades;
using Core.Interfaces;
using Core.Interfaces.Servicios;
using Core.Respuestas;

namespace Services.Servicios
{
    public class PrestamoServicio : IPrestamoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
		public PrestamoServicio(IUnidadDeTrabajo unidadDeTrabajo)
		{
			_unidadDeTrabajo = unidadDeTrabajo;
		}
        public Task<Respuesta<Prestamo>> Actualizar(int entidadParaActualizarId, Prestamo nuevosValoresEntidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Prestamo>> Agregar(Prestamo nuevaEntitidad)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Prestamo>> ObternerPorIdAsincrono(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<IEnumerable<Prestamo>>> ObternerTodosAsincrono()
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta<Prestamo>> Remover(int entidadId)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<Documento>> GuardarDocumento(Documento documento) 
        {
            try
            {
                if (documento == null) throw new ArgumentException("El documento está vacío");
                var prestamo = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerPorIdAsincrono(documento.IdPrestamo);
                if (prestamo == null) throw new ArgumentException("El préstamo de este documento no existe");
                var tipo = await _unidadDeTrabajo.TipoDocumentoRepositorio.ObtenerPorIdAsincrono(documento.IdTipo);
                if (tipo == null) throw new ArgumentException("El tipo de este documento no existe");
                await _unidadDeTrabajo.DocumentoRepositorio.AgregarAsincrono(documento);
                await _unidadDeTrabajo.CommitAsync();
                var respuesta = new Respuesta<Documento>() {
                    Datos = documento, 
                    Ok = true, 
                    Mensaje = "Documento ha sido guardado correctamente"
                };
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task<Respuesta<IEnumerable<Prestamo>>> ConsultarPrestamosDeCliente(int IdCliente) 
        {    
            try
            {
                var cliente = await _unidadDeTrabajo.ClienteRepositorio.ObtenerPorIdAsincrono(IdCliente);
                if (cliente == null) throw new ArgumentException("No existe cliente con tal id");
                var todos = await _unidadDeTrabajo.PrestamoRepostorio.ObtenerTodosAsincrono();
                var lista = todos.ToList().FindAll(x => x.IdCliente == IdCliente);
                var respuesta = new Respuesta<IEnumerable<Prestamo>>() {
                    Datos = lista, 
                    Mensaje = "Prestamos encontrados exitósamente", 
                    Ok = true
                };
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Respuesta<IEnumerable<Documento>>> ConsultarDocumentos(int IdPrestamo)
        {
            try
            {
                var prestamo = await _unidadDeTrabajo.DocumentoRepositorio.ObtenerPorIdAsincrono(IdPrestamo);
                if (prestamo == null) throw new ArgumentException("No existe un préstamo con tal id");
                var todos = await _unidadDeTrabajo.DocumentoRepositorio.ObtenerTodosAsincrono();
                var lista = todos.ToList().FindAll(x => x.IdPrestamo == IdPrestamo);
                var respuesta = new Respuesta<IEnumerable<Documento>>() {
                    Datos = lista, 
                    Mensaje = "Los documentos fueron encontrados exitósamente", 
                    Ok = true
                };
                return respuesta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}