using Core.Modelos.DTO;
using Core.Negocio.INegocio;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioNegocio _usuarioNegocio;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public UsuariosController(IUsuarioNegocio usuarioNegocio, IUnidadTrabajo unidadTrabajo)
        {
            _usuarioNegocio = usuarioNegocio;
            _unidadTrabajo = unidadTrabajo;
        }


        /// <summary>
        /// Obtiene todos los usuarios paginados.
        /// </summary>
        /// <param name="pageNumber">Número de página.</param>
        /// <param name="pageSize">Tamaño de la página.</param>
        /// <returns>Una lista paginada de usuarios.</returns>
        /// <response code="200">Devuelve la lista paginada de usuario.</response>
        /// <response code="400">Si no se encontraron usuario.</response>
        /// <response code="401">Si un usuario no autenticado intenta acceder.</response>
        /// <response code="500">Si ocurre un error interno del servidor.</response>
        [HttpGet]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(List<UsuarioDTO>), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 500)]
        public async Task<IActionResult> GetAllUsuarios(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var usuariosDto = await _usuarioNegocio.GetAllUsuarios(pageNumber, pageSize);

                if (usuariosDto == null || !usuariosDto.Any())
                {
                    return ResponseFactory.CreateSuccessResponse(404, "No se encontraron usuarios.");
                }

                return ResponseFactory.CreateSuccessResponse(200, usuariosDto);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, "Ocurrió un error al procesar la solicitud.", ex.Message);
            }
        }



        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID único del usuario.</param>
        /// <returns>El usuario con el ID especificado.</returns>
        /// <response code="200">Devuelve el usuario con el ID especificado.</response>
        /// <response code="401">Si un usuario que no ha iniciado sesión intenta ejecutar el endpoint.</response>
        /// <response code="403">Si un usuario que no es administrador o consultor intenta ejecutar el endpoint.</response>
        /// <response code="404">Si el usuario no fue encontrado.</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(UsuarioDTO), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        [ProducesResponseType(typeof(ApiErrorResponse), 500)]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _usuarioNegocio.GetUsuarioById(id);

                if (usuario == null)
                {
                    return ResponseFactory.CreateErrorResponse(404, "El Usuario no fue encontrado");
                }

                return ResponseFactory.CreateSuccessResponse(200, usuario);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, "Error interno del servidor: " + ex.Message);
            }
        }



        /// <summary>
        /// Crea un nuevo usuario(Administradores).
        /// </summary>
        /// <param name="usuarioDTO">Datos del usuario a crear.</param>
        /// <returns>
        ///     <response code="200">El usuario se creó exitosamente.</response>
        ///     <response code="400">Si el usuario ya existe o no se pudo crear.</response>
        ///     <response code="401">Si un usuario no autenticado intenta ejecutar el endpoint.</response>
        ///     <response code="500">Error interno del servidor.</response>
        /// </returns>
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(ApiSuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioReedDTO usuarioDTO)
        {
            try
            {
                var existeUsuario = await _unidadTrabajo.Usuario.Existe(p => p.NombreCompleto == usuarioDTO.NombreCompleto);

                if (existeUsuario)
                {
                    return ResponseFactory.CreateErrorResponse(StatusCodes.Status400BadRequest, "Ya existe un Usuario con ese nombre.");
                }

                var creado = await _usuarioNegocio.CrearUsuario(usuarioDTO);

                if (creado)
                {
                    return ResponseFactory.CreateSuccessResponse(StatusCodes.Status200OK, "Usuario creado exitosamente");
                }

                return ResponseFactory.CreateErrorResponse(StatusCodes.Status400BadRequest, "No se pudo crear el Usuario");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor: " + ex.Message);
            }
        }




        /// <summary>
        /// Actualiza un usuario existente(Administradores).
        /// </summary>
        /// <param name="id">ID del usuario a actualizar.</param>
        /// <param name="usuarioDTO">Nuevos datos del usuario.</param>
        /// <returns>Respuesta de estado de la actualización.</returns>
        /// <response code="200">Usuario actualizado exitosamente.</response>
        /// <response code="400">Si el ID del usuario no coincide, la información es incorrecta o el usuario no pudo ser actualizado.</response>
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
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                if (id != usuarioDTO.Id)
                {
                    return ResponseFactory.CreateErrorResponse(400, "Id del usuario no coincide");
                }

                if (!ModelState.IsValid)
                {
                    return ResponseFactory.CreateErrorResponse(400, "Informacion incorrecta");
                }

                var actualizado = await _usuarioNegocio.ActualizarUsuario(usuarioDTO);

                if (actualizado)
                {
                    return ResponseFactory.CreateSuccessResponse(200, "Usuario actualizado con éxito");
                }
                else
                {
                    return ResponseFactory.CreateErrorResponse(400, "El Usuario no pudo ser actualizado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }



        /// <summary>
        /// Elimina un usuario por su ID(Administradores).
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>Respuesta de estado de la eliminación.</returns>
        /// <response code="200">Usuario eliminado exitosamente.</response>
        /// <response code="400">Si hay un error en la solicitud o en el procesamiento.</response>
        /// <response code="401">Si un usuario que no ha iniciado sesión intenta ejecutar el endpoint.</response>
        /// <response code="403">El usuario no está autorizado.</response>
        /// <response code="404">Si el usuario no fue encontrado.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 401)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                var eliminado = await _usuarioNegocio.EliminarUsuario(id);

                if (eliminado)
                {
                    return ResponseFactory.CreateSuccessResponse(200, "Usuario eliminado exitosamente");
                }

                return ResponseFactory.CreateErrorResponse(404, "Usuario no encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }


    }
}
