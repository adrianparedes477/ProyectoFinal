using API.Especificaciones;
using API.Negocio.INegocio;
using Core.DTO;
using Core.Entidades;
using Core.Negocio;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Obtiene todos los trabajos paginados.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllTrabajos(int pageNumber = 1, int pageSize = 10)
        {
            var trabajosDto = await _trabajoNegocio.GetAllTrabajos(pageNumber, pageSize);
            return ResponseFactory.CreateSuccessResponse(200, trabajosDto);
        }

        /// <summary>
        /// Obtiene un trabajo por su ID.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrabajoById(int id)
        {
            var trabajo = await _trabajoNegocio.GetTrabajoById(id);

            if (trabajo == null)
            {
                return ResponseFactory.CreateSuccessResponse(404,"El proyecto no fue encontrado");
            }

            return ResponseFactory.CreateSuccessResponse(200, trabajo);
        }

        /// <summary>
        /// Crea un nuevo trabajo.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearTrabajo([FromBody] TrabajoCrearDTO trabajoDto, [FromQuery] int proyectoId, [FromQuery] int servicioId)
        {
            var creado = await _trabajoNegocio.CrearTrabajo(trabajoDto, proyectoId, servicioId);

            if (creado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Trabajo creado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(400, "No se pudo crear el Trabajo");
        }

        /// <summary>
        /// Actualiza un trabajo existente por su ID.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarTrabajo(int id, [FromBody] TrabajoActualizarDTO trabajoDTO)
        {
            if (id != trabajoDTO.Id)
            {
                return ResponseFactory.CreateErrorResponse(400, "Id del Trabajo no coincide");
            }

            if (!ModelState.IsValid)
            {
                return ResponseFactory.CreateErrorResponse(400, "Información incorrecta");
            }

            try
            {
                var actualizado = await _trabajoNegocio.ActualizarTrabajo(trabajoDTO);

                if (actualizado)
                {
                    return ResponseFactory.CreateSuccessResponse(200, "Trabajo actualizado con éxito");
                }
                else
                {
                    return ResponseFactory.CreateErrorResponse(400, "El trabajo no pudo ser actualizado");
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, "Error interno del servidor: " + ex.Message);
            }
        }

        /// <summary>
        /// Elimina un trabajo por su ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTrabajo(int id)
        {
            var eliminado = await _trabajoNegocio.EliminarTrabajo(id);

            if (eliminado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Trabajo eliminado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(404, "Trabajo no encontrado");
        }

    }
}
