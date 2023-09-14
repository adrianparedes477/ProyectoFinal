using API.Especificaciones;
using AutoMapper;
using Core.DTO;
using Core.Negocio.INegocio;
using Infraestructura.Data.Repositorio.IRepositorio;
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
            return Ok(paginaUsuarios);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioNegocio.GetUsuarioById(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            var creado = await _usuarioNegocio.CrearUsuario(usuarioDTO);

            if (creado)
            {
                return Ok("Usuario creado exitosamente");
            }

            return BadRequest("No se pudo crear el usuario");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {

            if (id != usuarioDTO.Id)
            {
                return BadRequest("Id del usuario no Coincide");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Informacion Incorrecta");
            }

            try
            {
                var actualizado = await _usuarioNegocio.ActualizarUsuario(usuarioDTO);

                if (actualizado)
                {
                    return Ok("Usuario actualizado con éxito");
                }
                else
                {
                    return BadRequest("El Usuario no pudo ser actualizado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var eliminado = await _usuarioNegocio.EliminarUsuario(id);

            if (eliminado)
            {
                return Ok("Usuario eliminado exitosamente");
            }

            return NotFound("Usuario no encontrado");
        }
    }

}
