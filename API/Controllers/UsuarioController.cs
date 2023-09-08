using AutoMapper;
using Core.DTO;
using Core.Negocio.INegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioNegocio _usuarioNegocio;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioNegocio usuarioNegocio, IMapper mapper)
        {
            _usuarioNegocio = usuarioNegocio;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var usuarios = await _usuarioNegocio.GetAllUsuarios();
            return Ok(usuarios);
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
            // Llama al método de negocio para actualizar un usuario
            var actualizado = await _usuarioNegocio.ActualizarUsuario(usuarioDTO);

            if (actualizado)
            {
                return Ok(); // Indica que la operación fue exitosa
            }

            return NotFound(); // Indica que no se encontró el usuario o no se pudo actualizar
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
