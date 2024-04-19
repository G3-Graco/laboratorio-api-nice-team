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
        public DocumentoController(IWebHostEnvironment env) {
            _env = env;
        }

        /// <summary>
        /// Método para ingresar documentos de un préstamo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Task<bool> Post()
        {
            bool resultado = false;
            var file = Request.Form.Files[0];
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
                    resultado = true;
                }
            }
            return Task.FromResult(resultado);
        }
    }
}