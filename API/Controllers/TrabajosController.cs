using API.Especificaciones;
using API.Negocio.INegocio;
using Core.DTO;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Trabajos")]
    public class TrabajosController : ControllerBase
    {
        private readonly ITrabajoNegocio _trabajoNegocio;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public TrabajosController(ITrabajoNegocio trabajoNegocio, IUnidadTrabajo unidadTrabajo)
        {
            _trabajoNegocio = trabajoNegocio;
            _unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrabajos(int pageNumber = 1, int pageSize = 10)
        {
            var parametros = new Parametros
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginaTrabajos = await _unidadTrabajo.Trabajo.ObtenerTodosPaginado(parametros, incluirPropiedades: "Proyecto,Servicio");
            return Ok(paginaTrabajos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrabajoById(int id)
        {
            var trabajo = await _trabajoNegocio.GetTrabajoById(id);

            if (trabajo == null)
            {
                return NotFound();
            }

            return Ok(trabajo);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CrearTrabajo(TrabajoDTO trabajoDTO)
        {
            try
            {
                await _trabajoNegocio.CrearTrabajo(trabajoDTO);
                return true;
            }
            catch (Exception ex)
            {
                // Maneja la excepción según tus necesidades
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarTrabajo(int id,[FromBody] TrabajoDTO trabajoDTO)
        {
            if (id != trabajoDTO.Id)
            {
                return BadRequest("Id del Trabajo no Coincide");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Informacion Incorrecta");
            }

            try
            {
                var actualizado = await _trabajoNegocio.ActualizarTrabajo(trabajoDTO);

                if (actualizado)
                {
                    return Ok("Trabajo actualizado con éxito");
                }
                else
                {
                    return BadRequest("El trabajo no pudo ser actualizado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> EliminarTrabajo(int id)
        {
            try
            {
                var eliminado = await _trabajoNegocio.EliminarTrabajo(id);

                if (!eliminado)
                {
                    return NotFound("Trabajo no encontrado");
                }

                return true;
            }
            catch (Exception ex)
            {
                // Maneja la excepción según tus necesidades
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }
    }
}
