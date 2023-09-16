using API.Especificaciones;
using API.Negocio.INegocio;
using Core.DTO;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Response;
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
            return ResponseFactory.CreateSuccessResponse(200, paginaTrabajos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrabajoById(int id)
        {
            var trabajo = await _trabajoNegocio.GetTrabajoById(id);

            if (trabajo == null)
            {
                return ResponseFactory.CreateErrorResponse(404, "Trabajo no encontrado");
            }

            return ResponseFactory.CreateSuccessResponse(200, trabajo);
        }

        [HttpPost]
        public async Task<IActionResult> CrearTrabajo([FromBody] TrabajoDTO trabajoDTO)
        {
            try
            {
                await _trabajoNegocio.CrearTrabajo(trabajoDTO);
                return ResponseFactory.CreateSuccessResponse(200, "Trabjo creado exitosamente");
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarTrabajo(int id, [FromBody] TrabajoDTO trabajoDTO)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTrabajo(int id)
        {
            try
            {
                var eliminado = await _trabajoNegocio.EliminarTrabajo(id);

                if (!eliminado)
                {
                    return ResponseFactory.CreateErrorResponse(404, "Trabajo no encontrado");
                }

                return ResponseFactory.CreateSuccessResponse(200, true);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, "Error interno del servidor: " + ex.Message);
            }
        }

    }
}
