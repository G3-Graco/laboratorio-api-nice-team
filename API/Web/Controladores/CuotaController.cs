using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;

namespace Web.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuotaController : ControllerBase
    {
        private ICuotaServicio _servicio;
        public CuotaController(ICuotaServicio servicio)
        {
            _servicio = servicio;
        }


        /// <summary>
        /// Método para obtener consultar las cuotas de un préstamo.
        /// </summary>
        /// <returns>Respuesta con ienumerable de pagos</returns>
        [Authorize]
        [HttpGet("cuotasprestamo")]
        public async Task<ActionResult<Respuesta<IEnumerable<Cuota>>>> GetCuotasPretamo(int idusuariosesion, int idPrestamo) //query
        {
            try
            {
                var Respuesta = await _servicio.ConsultarCuotasPorPrestamo(idusuariosesion, idPrestamo);

                return Ok(Respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }


}
