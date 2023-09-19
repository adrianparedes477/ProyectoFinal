using API.Especificaciones;
using AutoMapper;
using Core.DTO;
using Core.Negocio.INegocio;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUsuarios(int pageNumber = 1, int pageSize = 10)
        {
            var usuariosDto = await _usuarioNegocio.GetAllUsuarios(pageNumber, pageSize);
            return ResponseFactory.CreateSuccessResponse(200, usuariosDto);
        }


        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>El usuario con el ID especificado.</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioNegocio.GetUsuarioById(id);
            if (usuario == null)
            {
                return ResponseFactory.CreateSuccessResponse(404,"El Usuario no fue encontrado");
            }
            return ResponseFactory.CreateSuccessResponse(200, usuario);
        }


        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="usuarioDTO">Datos del usuario a crear.</param>
        /// <returns>Respuesta de estado de la creación.</returns>
        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioReedDTO usuarioDTO)
        {
            var existeUsuario = await _unidadTrabajo.Usuario.Existe(p => p.NombreCompleto == usuarioDTO.NombreCompleto);

            if (existeUsuario)
            {
                return ResponseFactory.CreateErrorResponse(400, "Ya existe un Usuario con ese nombre.");
            }
            var creado = await _usuarioNegocio.CrearUsuario(usuarioDTO);
            if (creado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Usuario creado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(400, "No se pudo crear el Usuario");
        }


        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="id">ID del usuario a actualizar.</param>
        /// <param name="usuarioDTO">Nuevos datos del usuario.</param>
        /// <returns>Respuesta de estado de la actualización.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.Id)
            {
                return ResponseFactory.CreateErrorResponse(400, "Id del usuario no coincide");
            }

            if (!ModelState.IsValid)
            {
                return ResponseFactory.CreateErrorResponse(400, "Informacion incorrecta");
            }

            try
            {
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
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>Respuesta de estado de la eliminación.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var eliminado = await _usuarioNegocio.EliminarUsuario(id);

            if (eliminado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Usuario eliminado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(404, "Usuario no encontrado");
        }

    }
}
