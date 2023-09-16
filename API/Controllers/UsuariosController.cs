using API.Especificaciones;
using AutoMapper;
using Core.DTO;
using Core.Negocio.INegocio;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios(int pageNumber = 1, int pageSize = 10)
        {
            var parametros = new Parametros
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginaUsuarios = await _unidadTrabajo.Usuario.ObtenerTodosPaginado(parametros);
            return ResponseFactory.CreateSuccessResponse(200, paginaUsuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioNegocio.GetUsuarioById(id);
            if (usuario == null)
            {
                return ResponseFactory.CreateErrorResponse(404, "Usuario no encontrado");
            }
            return ResponseFactory.CreateSuccessResponse(200, usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            var creado = await _usuarioNegocio.CrearUsuario(usuarioDTO);

            if (creado)
            {
                return ResponseFactory.CreateSuccessResponse(201, "Usuario creado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(400, "No se pudo crear el usuario");
        }

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
