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
    [Route("api/Proyectos")]
    public class ProyectosController : ControllerBase
    {
        private readonly IProyectoNegocio _proyectoNegocio;
        private readonly IUnidadTrabajo _unidadTrabajo;
        public ProyectosController(IProyectoNegocio proyectoNegocio, IUnidadTrabajo unidadTrabajo)
        {
            _proyectoNegocio = proyectoNegocio;
            _unidadTrabajo = unidadTrabajo;
        }


        /// <summary>
        /// Obtiene todos los proyectos paginados.
        /// </summary>
        /// <param name="pageNumber">Número de la página actual (por defecto es 1).</param>
        /// <param name="pageSize">Tamaño de la página (por defecto es 10).</param>
        /// <returns>Una lista de proyectos paginados.</returns>
        /// <response code="200">Devuelve una lista de proyectos paginados.</response>
        /// <response code="400">Si hay un error en la solicitud o en el procesamiento.</response>
        /// <response code="404">si no se encuentra el proyecto.</response>
        [HttpGet]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> GetAllProyectos(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var proyectosDto = await _proyectoNegocio.GetAllProyectos(pageNumber, pageSize);

                if (proyectosDto == null || !proyectosDto.Any())
                {
                    return ResponseFactory.CreateSuccessResponse(404, "No se encontraron proyectos.");
                }

                return ResponseFactory.CreateSuccessResponse(200, proyectosDto);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(400, ex.Message);
            }
        }


        /// <summary>
        /// Obtiene un proyecto por su ID.
        /// </summary>
        /// <param name="id">ID único del proyecto.</param>
        /// <returns>
        ///     <para>Devuelve el proyecto con el ID especificado.</para>
        ///     <para>Devuelve un mensaje de error si el proyecto no se encuentra.</para>
        /// </returns>
        /// <response code="200">Devuelve el proyecto con el ID especificado.</response>
        /// <response code="401">Si un usuario que no ha iniciado sesión intenta acceder.</response>
        /// <response code="403">Si un usuario que no es administrador o consultor intenta ejecutar el endpoint.</response>
        /// <response code="404">Si el proyecto no fue encontrado.</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(ProyectoDto), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> GetProyectoById(int id)
        {
            try
            {
                var proyecto = await _proyectoNegocio.GetProyectoById(id);

                if (proyecto == null)
                {
                    return ResponseFactory.CreateErrorResponse(404, "El proyecto no fue encontrado");
                }

                return ResponseFactory.CreateSuccessResponse(200, proyecto);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(400, ex.Message);
            }
        }


        /// <summary>
        /// Obtiene proyectos por su estado.
        /// </summary>
        /// <param name="estado">Estado de los proyectos a buscar.</param>
        /// <returns>
        ///     <para>Devuelve una lista de proyectos que tienen el estado especificado.</para>
        ///     <para>Devuelve una lista vacía si no se encuentran proyectos con ese estado.</para>
        /// </returns>
        /// <response code="200">Devuelve la lista de proyectos con el estado especificado.</response>
        /// <response code="400">Si hay un error en la solicitud o en el procesamiento.</response>
        [HttpGet("proyectosPorEstado/{estado}")]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(List<ProyectoDto>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        public async Task<IActionResult> GetProyectosPorEstado(int estado)
        {
            try
            {
                var proyectos = await _proyectoNegocio.GetProyectosPorEstado(estado);
                return ResponseFactory.CreateSuccessResponse(200, proyectos);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(400, ex.Message);
            }
        }


        /// <summary>
        /// Crea un nuevo proyecto.Solo habilitado para los Administradores
        /// </summary>
        /// <param name="proyectoDto">Datos del proyecto a crear.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">Se ha creado el proyecto exitosamente.</response>
        /// <response code="400">Si ya existe un proyecto con el mismo nombre o no se pudo crear el proyecto.</response>
        /// <response code="403">El usuario no está autorizado.</response>
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        public async Task<IActionResult> CrearProyecto([FromBody] ProyectoReedDto proyectoDto)
        {
            try
            {
                var existeProyecto = await _unidadTrabajo.Proyecto.Existe(p => p.Nombre == proyectoDto.Nombre);

                if (existeProyecto)
                {
                    return ResponseFactory.CreateErrorResponse(400, "Ya existe un proyecto con ese nombre.");
                }

                var creado = await _proyectoNegocio.CrearProyecto(proyectoDto);

                if (creado)
                {
                    return ResponseFactory.CreateSuccessResponse(200, "Proyecto creado exitosamente");
                }

                return ResponseFactory.CreateErrorResponse(400, "No se pudo crear el Proyecto");
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, ex.Message);
            }
        }


        /// <summary>
        /// Actualiza un proyecto existente por su ID.Solo habilitado para los Administradores
        /// </summary>
        /// <param name="id">ID del proyecto a actualizar.</param>
        /// <param name="proyectoDto">Datos actualizados del proyecto.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">El proyecto se actualizó exitosamente.</response>
        /// <response code="400">Si el ID del proyecto no coincide, la información es incorrecta o el proyecto no pudo ser actualizado.</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiSuccessResponse), 500)]
        public async Task<IActionResult> ActualizarProyecto(int id, [FromBody] ProyectoDto proyectoDto)
        {
            try
            {
                if (id != proyectoDto.Id)
                {
                    return ResponseFactory.CreateErrorResponse(400, "Id del proyecto no coincide");
                }

                if (!ModelState.IsValid)
                {
                    return ResponseFactory.CreateErrorResponse(400, "Información incorrecta");
                }

                var actualizado = await _proyectoNegocio.ActualizarProyecto(proyectoDto);

                if (actualizado)
                {
                    return ResponseFactory.CreateSuccessResponse(200, "Proyecto actualizado con éxito");
                }
                else
                {
                    return ResponseFactory.CreateErrorResponse(400, "El Proyecto no pudo ser actualizado");
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, ex.Message);
            }
        }


        /// <summary>
        /// Elimina un proyecto por su ID. Solo habilitado para los Administradores
        /// </summary>
        /// <param name="id">ID del proyecto a eliminar.</param>
        /// <returns>El resultado de la operación.</returns>
        /// <response code="200">El proyecto se eliminó exitosamente.</response>
        /// <response code="401">Si un usuario que no ha iniciado sesión intenta acceder.</response>
        /// <response code="404">Si el proyecto no fue encontrado.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> EliminarProyecto(int id)
        {
            var eliminado = await _proyectoNegocio.EliminarProyecto(id);

            if (eliminado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Proyecto eliminado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(404, "Proyecto no encontrado");
        }

    }
}