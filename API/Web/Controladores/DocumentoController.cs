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
        public DocumentoController(IWebHostEnvironment env, IDocumentoServicio servicio, IPrestamoServicio prestamos) {
            _env = env;
            _servicio = servicio;
            _servicioPrestamo = prestamos;
        }

        /// <summary>
        /// Método para ingresar documentos de un préstamo
        /// </summary>
        /// <returns></returns>
        [HttpPost("CargarArchivo")]
        public async Task<ActionResult<Respuesta<Documento>>> Post([FromBody] IFormFile archivo)
        {
            try
            {
                // var file = Request.Form.Files[0];
                var file = archivo;
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
                        var bytes = await _servicio.ConvertirAByte(RutaFullCompleta);
                        documento.Ubicacion = RutaFullCompleta;
                        documento.documento = bytes.Datos;
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
    }
}