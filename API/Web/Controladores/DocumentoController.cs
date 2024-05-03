using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Web.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentoController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IDocumentoServicio _servicio;
        private readonly IPrestamoServicio _servicioPrestamo;
        private readonly ITipoDocumentoServicio _servicioTipo;
        public DocumentoController(IWebHostEnvironment env, 
        IDocumentoServicio servicio, IPrestamoServicio prestamos, 
        ITipoDocumentoServicio tipos) {
            _env = env;
            _servicio = servicio;
            _servicioPrestamo = prestamos;
            _servicioTipo = tipos;
        }

        /// <summary>
        /// Método para ingresar documentos de identidad de un préstamo
        /// </summary>
        /// <returns></returns>
        [HttpPost("CargarIdentidad")]
        public async Task<ActionResult<Respuesta<Documento>>> PostIdentidad()
        {
            try
            {
                var file = Request.Form.Files[0];
                // var file = archivo;
                var documento = new Documento();
                string NombreCarpeta = "/Documentos/";
                string RutaRaiz = _env.ContentRootPath;
                string RutaCompleta = RutaRaiz + NombreCarpeta;
                if (!Directory.Exists(RutaCompleta))
                {
                    Directory.CreateDirectory(RutaCompleta);
                }
                if (file.Length > 0)
                {
                    string NombreArchivo = file.FileName;
                    string RutaFullCompleta = Path.Combine(RutaCompleta, NombreArchivo);
                    using (var stream = new FileStream(RutaFullCompleta, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        var bytes = await _servicio.ConvertirAByte(file.OpenReadStream());
                        documento.Ubicacion = RutaFullCompleta;
                        documento.documento = bytes.Datos;
                        var lista = await _servicioTipo.ObternerTodosAsincrono();
                        documento.IdTipo = lista.Datos.First(x => x.Nombre == "Identificación").Id;
                        var partes = RutaFullCompleta.Split('.');
                        var indices = partes[^2];
                        documento.IdPrestamo = int.Parse(indices.Split('-')[^1]);
                        await _servicioPrestamo.GuardarDocumento(documento);
                    }
                }
                return Ok(documento);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Método para ingresar recibo de trabajo de un préstamo
        /// </summary>
        /// <returns></returns>
        [HttpPost("CargarTrabajo")]
        public async Task<ActionResult<Respuesta<Documento>>> PostTrabajo()
        {
            try
            {
                var file = Request.Form.Files[0];
                // var file = archivo;
                var documento = new Documento();
                string NombreCarpeta = "/Documentos/";
                string RutaRaiz = _env.ContentRootPath;
                string RutaCompleta = RutaRaiz + NombreCarpeta;
                if (!Directory.Exists(RutaCompleta))
                {
                    Directory.CreateDirectory(RutaCompleta);
                }
                if (file.Length > 0)
                {
                    string NombreArchivo = file.FileName;
                    string RutaFullCompleta = Path.Combine(RutaCompleta, NombreArchivo);
                    using (var stream = new FileStream(RutaFullCompleta, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        var bytes = await _servicio.ConvertirAByte(file.OpenReadStream());
                        documento.Ubicacion = RutaFullCompleta;
                        documento.documento = bytes.Datos;
                        var lista = await _servicioTipo.ObternerTodosAsincrono();
                        documento.IdTipo = lista.Datos.First(x => x.Nombre == "Recibo").Id;
                        var partes = RutaFullCompleta.Split('.');
                        var indices = partes[^2];
                        documento.IdPrestamo = int.Parse(indices.Split('-')[^1]);
                        await _servicioPrestamo.GuardarDocumento(documento);
                    }
                }
                return Ok(documento);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Método para crear un documento
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Respuesta<Documento>>> Post([FromBody] Documento documento) {
            try
            {
                var respuesta = await _servicioPrestamo.GuardarDocumento(documento);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Método para convertir un archivo a un arreglo de bytes
        /// </summary>
        /// <param name="url">La url del documento</param>
        /// <returns></returns>
        [HttpPost("Convertir/{url}")]
        public async Task<ActionResult<Respuesta<Byte[]>>> Post(string url)
        {
            try
            {
                var respuesta = await _servicio.ConvertirAByte(url);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Método para descargar un archivo de préstamos
        /// </summary>
        /// <param name="documento">El documento a descargar</param>
        /// <returns></returns>
        [HttpPost("Descargar")]
        public async Task<ActionResult<Respuesta<Documento>>> PostFile([FromBody] Documento documento)
        {
            try
            {
                var respuesta = await _servicio.Descargar(documento);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Método para obtener todos los documentos de un préstamo
        /// </summary>
        /// <param name="prestamoId">Id del préstamo</param>
        /// <returns></returns>
        [HttpGet("Prestamos/{prestamoId}")]
        public async Task<ActionResult<Respuesta<IEnumerable<Documento>>>> Get(int prestamoId)
        {
            try
            {
                var respuesta = await _servicioPrestamo.ConsultarDocumentos(prestamoId);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Método para obtener todos los documentos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta<IEnumerable<Documento>>>> Get()
        {
            try
            {
                var respuesta = await _servicio.ObternerTodosAsincrono();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}