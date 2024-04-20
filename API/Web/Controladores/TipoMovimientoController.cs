using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoMovimientoController : ControllerBase
    {
        private ITipoMovimientoServicio _servicio;
        public TipoMovimientoController(ITipoMovimientoServicio servicio)
        {
            _servicio = servicio;
        }

        /// <summary>
        /// Método para crear un tipo de movimiento
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Respuesta<TipoMovimiento>>> Post([FromBody] TipoMovimiento tipo) {
            try
            {
                var respuesta = await _servicio.Agregar(tipo);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Método para obtener todos los tipos de movimientos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta<IEnumerable<TipoMovimiento>>>> Get() {
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

        /// <summary>
        /// Método para obtener un tipo de movimiento
        /// </summary>
        /// <param name="id">La id del movimiento</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta<TipoMovimiento>>> Get(int id) {
            try
            {
                var respuesta = await _servicio.ObternerPorIdAsincrono(id);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Método para modificar un tipo de movimiento
        /// </summary>
        /// <param name="id">La id del tipo de movimiento</param>
        /// <param name="tipo">El nuevo objeto del tipo de movimiento</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Respuesta<TipoMovimiento>>> Update(int id, [FromBody] TipoMovimiento tipo) {
            try
            {
                var respuesta = await _servicio.Actualizar(id, tipo);
                return respuesta;
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        /// <summary>
        /// Método para eliminar un tipo de movimiento
        /// </summary>
        /// <param name="id">La id del tipo de movimiento</param>
        /// <returns></returns>
        public async Task<ActionResult<Respuesta<TipoMovimiento>>> Delete(int id) {
            try
            {
                var respuesta = await _servicio.Remover(id);
                return respuesta;
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}