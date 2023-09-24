using API.Negocio.INegocio;
using Core.Modelos.DTO;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        /// <param name="pageNumber">Número de página.</param>
        /// <param name="pageSize">Tamaño de la página.</param>
        /// <returns>Una lista paginada de trabajos.</returns>
        /// <response code="200">Devuelve la lista paginada de trabajos.</response>
        /// <response code="204">Si no se encontraron trabajos.</response>
        /// <response code="401">Si un usuario no autenticado intenta acceder.</response>
        [HttpGet]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiSuccessResponse), 204)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        public async Task<IActionResult> GetAllTrabajos(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var trabajosDto = await _trabajoNegocio.GetAllTrabajos(pageNumber, pageSize);

                if (trabajosDto == null || trabajosDto.Any())
                {
                    return ResponseFactory.CreateSuccessResponse(200, trabajosDto);
                }
                else
                {
                    return ResponseFactory.CreateSuccessResponse(204, "No se encontraron trabajos.");
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(400, ex.Message);
            }
        }


        /// <summary>
        /// Obtiene un trabajo por su ID.
        /// </summary>
        /// <param name="id">ID único del trabajo.</param>
        /// <returns>
        ///     <para>Devuelve el trabajo con el ID especificado.</para>
        ///     <para>Devuelve un mensaje de error si el trabajo no se encuentra.</para>
        /// </returns>
        /// <response code="200">Devuelve el trabajo con el ID especificado.</response>
        /// <response code="401">Si un usuario que no ha iniciado sesión intenta ejecutar el endpoint.</response>
        /// <response code="403">Si un usuario que no es administrador o consultor intenta ejecutar el endpoint.</response>
        /// <response code="404">Si el trabajo no fue encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(TrabajoDTO), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        [ProducesResponseType(typeof(ApiErrorResponse), 500)]
        public async Task<IActionResult> GetTrabajoById(int id)
        {
            try
            {
                var trabajo = await _trabajoNegocio.GetTrabajoById(id);

                if (trabajo == null)
                {
                    return ResponseFactory.CreateErrorResponse(404, "El trabajo no fue encontrado");
                }

                return ResponseFactory.CreateSuccessResponse(200, trabajo);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, "Ocurrió un error al procesar la solicitud.", ex.Message);
            }
        }


        /// <summary>
        /// Crea un nuevo trabajo(Administradores).
        /// </summary>
        /// <param name="trabajoDto">Datos del trabajo a crear.</param>
        /// <param name="proyectoId">ID del proyecto al que pertenece el trabajo.</param>
        /// <param name="servicioId">ID del servicio asociado al trabajo.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">Se ha creado el trabajo exitosamente.</response>
        /// <response code="400">Si la solicitud es inválida o no se pudo crear el trabajo.</response>
        /// <response code="401">Si un usuario que no ha iniciado sesión intenta ejecutar el endpoint.</response>
        /// <response code="403">El usuario no está autorizado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        [ProducesResponseType(typeof(ApiErrorResponse), 500)]
        public async Task<IActionResult> CrearTrabajo([FromBody] TrabajoCrearDTO trabajoDto, [FromQuery] int proyectoId, [FromQuery] int servicioId)
        {
            try
            {
                var creado = await _trabajoNegocio.CrearTrabajo(trabajoDto, proyectoId, servicioId);

                if (creado)
                {
                    return ResponseFactory.CreateSuccessResponse(200, "Trabajo creado exitosamente");
                }

                return ResponseFactory.CreateErrorResponse(400, "No se pudo crear el Trabajo");
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, "Ocurrió un error al procesar la solicitud.", ex.Message);
            }
        }


        /// <summary>
        /// Actualiza un trabajo existente por su ID(Administradores).
        /// </summary>
        /// <param name="id">ID único del trabajo a actualizar.</param>
        /// <param name="trabajoDTO">Datos actualizados del trabajo.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">El trabajo se actualizó exitosamente.</response>
        /// <response code="400">Si el ID del trabajo no coincide o la información es incorrecta.</response>
        /// <response code="401">Si un usuario que no ha iniciado sesión intenta ejecutar el endpoint.</response>
        /// <response code="403">El usuario no está autorizado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        [ProducesResponseType(typeof(ApiErrorResponse), 500)]
        public async Task<IActionResult> ActualizarTrabajo(int id, [FromBody] TrabajoActualizarDTO trabajoDTO)
        {
            try
            {
                if (id != trabajoDTO.Id)
                {
                    return ResponseFactory.CreateErrorResponse(400, "Id del Trabajo no coincide");
                }

                if (!ModelState.IsValid)
                {
                    return ResponseFactory.CreateErrorResponse(400, "Información incorrecta");
                }


        [HttpGet]
        [Authorize(Policy = "AdminOrConsultor")]
        public async Task<IActionResult> GetAllTrabajos(int pageNumber = 1, int pageSize = 10)
        {
            var trabajosDto = await _trabajoNegocio.GetAllTrabajos(pageNumber, pageSize);
            return ResponseFactory.CreateSuccessResponse(200, trabajosDto);
        }

        /// <summary>
        /// Obtiene un trabajo por su ID.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrConsultor")]
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
        [Authorize(Policy = "Administrador")]
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
        [Authorize(Policy = "Administrador")]
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

        /// Elimina un trabajo por su ID(Administradores).
        /// </summary>
        /// <param name="id">ID único del trabajo a eliminar.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">El trabajo se eliminó exitosamente.</response>
        /// <response code="400">Si hay un error en la solicitud o en el procesamiento.</response>
        /// <response code="401">Si un usuario que no ha iniciado sesión intenta ejecutar el endpoint.</response>
        /// <response code="403">El usuario no está autorizado.</response>
        /// <response code="404">Si el trabajo no fue encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        [ProducesResponseType(typeof(ApiErrorResponse), 500)]
        public async Task<IActionResult> EliminarTrabajo(int id)
        {
            try
            {
                var eliminado = await _trabajoNegocio.EliminarTrabajo(id);

                if (eliminado)
                {
                    return ResponseFactory.CreateSuccessResponse(200, "Trabajo eliminado exitosamente");
                }

                return ResponseFactory.CreateErrorResponse(404, "Trabajo no encontrado");
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, "Error interno del servidor: " + ex.Message);
            }
        }


        /// Elimina un trabajo por su ID.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Administrador")]
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
