using AutoMapper;
using Core.DTO;
using Core.Negocio.INegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioNegocio _usuarioNegocio;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioNegocio usuarioNegocio, IMapper mapper)
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
            
            var actualizado = await _usuarioNegocio.ActualizarUsuario(usuarioDTO);

            if (actualizado)
            {
                return Ok(); 
            }

            return NotFound(); 
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
