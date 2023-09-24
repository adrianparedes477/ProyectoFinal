using API.Negocio.INegocio;
using Core.Modelos.DTO;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Servicios")]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioNegocio _servicioNegocio;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ServiciosController(IServicioNegocio servicioNegocio, IUnidadTrabajo unidadTrabajo)
        {
            _servicioNegocio = servicioNegocio;
            _unidadTrabajo = unidadTrabajo;
        }

        /// <summary>
        /// Obtiene todos los servicios paginados.
        /// </summary>
        /// <param name="pageNumber">Número de página.</param>
        /// <param name="pageSize">Tamaño de la página.</param>
        /// <returns>Lista de servicios paginados.</returns>
        /// <response code="200">Devuelve una lista de servicios paginados.</response>
        /// <response code="400">Si hay un error en la solicitud o en el procesamiento.</response>
        /// <response code="404">si no se encuentra el servicio.</response>
        [HttpGet]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(List<ServicioDTO>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        public async Task<IActionResult> GetAllServicios([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var servicioDto = await _servicioNegocio.GetAllServicios(pageNumber, pageSize);

                if (servicioDto == null || !servicioDto.Any())
                {
                    return ResponseFactory.CreateSuccessResponse(404, "No se encontraron servicios.");
                }
                return ResponseFactory.CreateSuccessResponse(200, servicioDto);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(400, ex.Message);
            }

        [HttpGet]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        public async Task<IActionResult> GetAllServicios(int pageNumber = 1, int pageSize = 10)
        {
            var servicioDto = await _servicioNegocio.GetAllServicios(pageNumber, pageSize);
            return ResponseFactory.CreateSuccessResponse(200, servicioDto);

        }


        /// <summary>
        /// Obtiene un servicio por su ID.
        /// </summary>
        /// <param name="id">ID del servicio.</param>
        /// <returns>El servicio correspondiente al ID proporcionado.</returns>

        /// <response code="200">Devuelve el servicio con el ID especificado.</response>
        /// <response code="401">Si un usuario que no ha iniciado sesión intenta acceder.</response>
        /// <response code="403">Si un usuario que no es administrador o consultor intenta ejecutar el endpoint.</response>
        /// <response code="404">Si el servicio no fue encontrado.</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> GetServicioById(int id)
        {
            try
            {
                var servicioDto = await _servicioNegocio.GetServicioById(id);

                if (servicioDto == null)
                {
                    return ResponseFactory.CreateErrorResponse(404, "El servicio no fue encontrado");
                }

                return ResponseFactory.CreateSuccessResponse(200, servicioDto);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(400, ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de servicios activos.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de servicios que están actualmente activos y disponibles para su uso.
        /// Si no hay servicios activos, se devuelve una lista vacía.
        /// </remarks>
        /// <returns>Una lista de servicios activos.</returns>
        /// <response code="200">Se devuelve la lista de servicios activos.</response>
        /// <response code="401">Si un usuario no autenticado intenta acceder.</response>
        /// <response code="403">Si un usuario no autorizado intenta acceder.</response>
        /// <response code="400">Si hay un error en la solicitud o en el procesamiento.</response>
        [HttpGet("activos")]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(List<ServicioDTO>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        public async Task<IActionResult> GetServiciosActivos()
        {
            try
            {
                var serviciosActivos = await _servicioNegocio.GetServiciosActivos();

                if (serviciosActivos == null || !serviciosActivos.Any())
                {
                    return ResponseFactory.CreateSuccessResponse(200, new List<ServicioDTO>());
                }

                return ResponseFactory.CreateSuccessResponse(200, serviciosActivos);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(400, ex.Message);
            }
        }

        /// <summary>
        /// Crea un nuevo servicio(Administradores).
        /// </summary>
        /// <param name="servicioDTO">Datos del nuevo servicio.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">Si el servicio se crea exitosamente.</response>
        /// <response code="400">Si no se pudo crear el servicio.</response>
        /// <response code="401">Si un usuario no autenticado intenta acceder.</response>
        /// <response code="403">Si un usuario no autorizado intenta acceder.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        [ProducesResponseType(typeof(ApiErrorResponse), 500)]
        public async Task<IActionResult> CrearServicio([FromBody] ServicioReedDTO servicioDTO)
        {
            try
            {
                var creado = await _servicioNegocio.CrearServicio(servicioDTO);

                if (creado)
                {
                    return ResponseFactory.CreateSuccessResponse(200, "Servicio creado exitosamente");
                }

                return ResponseFactory.CreateErrorResponse(400, "No se pudo crear el Servicio");
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un servicio existente por su ID(Administradores).
        /// </summary>
        /// <param name="id">ID del servicio a actualizar.</param>
        /// <param name="servicioDTO">Datos actualizados del servicio.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">Si el servicio se actualiza exitosamente.</response>
        /// <response code="400">Si el ID del servicio no coincide o la información es incorrecta.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> GetServicioById(int id)
        {
            var servicioDto = await _servicioNegocio.GetServicioById(id);

            if (servicioDto == null)
            {
                return ResponseFactory.CreateErrorResponse(404, "El servicio no fue encontrado");
            }

            return ResponseFactory.CreateSuccessResponse(200, servicioDto);
        }


        /// <summary>
        /// Obtiene servicios activos.
        /// </summary>
        /// <returns>Lista de servicios activos.</returns>
        [HttpGet("activos")]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        public async Task<IActionResult> GetServiciosActivos()
        {
            var serviciosActivos = await _servicioNegocio.GetServiciosActivos();
            return ResponseFactory.CreateSuccessResponse(200, serviciosActivos);
        }

        /// <summary>
        /// Crea un nuevo servicio.
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        public async Task<IActionResult> CrearServicio([FromBody] ServicioReedDTO servicioDTO)
        {
            var creado = await _servicioNegocio.CrearServicio(servicioDTO);

            if (creado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Servicio creado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(400, "No se pudo crear el Servicio");
        }

        /// <summary>
        /// Actualiza un servicio existente por su ID.
        /// </summary>

        [HttpPut("{id}")]
        [Authorize(Policy = "Administrador")]
        public async Task<IActionResult> ActualizarServicio(int id, [FromBody] ServicioDTO servicioDTO)
        {
            if (id != servicioDTO.Id)
            {
                return ResponseFactory.CreateErrorResponse(400, "Id del servicio no coincide");
            }

            if (!ModelState.IsValid)
            {
                return ResponseFactory.CreateErrorResponse(400, "Informacion incorrecta");
            }

            try
            {
                var actualizado = await _servicioNegocio.ActualizarServicio(servicioDTO);

                if (actualizado)
                {
                    return ResponseFactory.CreateSuccessResponse(200, "Servicio actualizado con éxito");
                }
                else
                {
                    return ResponseFactory.CreateErrorResponse(400, "El Servicio no pudo ser actualizado");
                }
            }
            catch (Exception ex)
            {

                return ResponseFactory.CreateErrorResponse(500, ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);

            }
        }


        /// <summary>

        /// Elimina un servicio por su ID(Administradores).
        /// </summary>
        /// <param name="id">ID del servicio a eliminar.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">Si el servicio se elimina exitosamente.</response>
        /// <response code="404">Si el servicio no se encuentra.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]

        /// Elimina un servicio por su ID.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Administrador")]

        public async Task<IActionResult> EliminarServicio(int id)
        {
            var eliminado = await _servicioNegocio.EliminarServicio(id);

            if (eliminado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Servicio eliminado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(404, "Servicio no encontrado");
        }

    }

}
