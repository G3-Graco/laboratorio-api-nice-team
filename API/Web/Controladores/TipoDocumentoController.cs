using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces.Repositorios;
using Core.Interfaces.Servicios;
using Core.Respuestas;
using Infrastructure.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoDocumentoController : ControllerBase
    {
        private ITipoDocumentoServicio _servicio;
        public TipoDocumentoController(ITipoDocumentoServicio servicio)
        {
            _servicio = servicio;
        }

        /// <summary>
        /// Método para crear un tipo de documento
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Respuesta<TipoDocumento>>> Post([FromBody] TipoDocumento tipo) {
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
        /// Método para obtener todos los tipos de documentos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta<IEnumerable<TipoDocumento>>>> Get() {
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
        /// Método para obtener un tipo de documento
        /// </summary>
        /// <param name="id">La id del documento</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Respuesta<TipoDocumento>>> Get(int id) {
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
        /// Método para modificar un tipo de documento
        /// </summary>
        /// <param name="id">La id del tipo de documento</param>
        /// <param name="tipo">El nuevo objeto del tipo de documento</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Respuesta<TipoDocumento>>> Update(int id, [FromBody] TipoDocumento tipo) {
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
        /// Método para eliminar un tipo de documento
        /// </summary>
        /// <param name="id">La id del tipo de documento</param>
        /// <returns></returns>
        public async Task<ActionResult<Respuesta<TipoDocumento>>> Delete(int id) {
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