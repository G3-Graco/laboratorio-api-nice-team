using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
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
        public DocumentoController(IWebHostEnvironment env, IDocumentoServicio servicio) {
            _env = env;
            _servicio = servicio;
        }

        /// <summary>
        /// Método para ingresar documentos de un préstamo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Respuesta<Documento>>> Post()
        {
            try
            {
                var file = Request.Form.Files[0];
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
    }
}