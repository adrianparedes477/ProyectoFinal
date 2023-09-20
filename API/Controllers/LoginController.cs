using API.Helpers;
using API.Negocio.INegocio;
using Core.DTO;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILoginNegocio _loginNegocio;

        public LoginController(ILoginNegocio loginNegocio)
        {
            _loginNegocio = loginNegocio;
        }

        /// <summary>
        /// Logueo de usuario.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] AuthenticateDto dto)
        {
            var usuario = await _loginNegocio.AuthenticateCredentials(dto);

            if (usuario is null)
                return Unauthorized("Las credenciales son incorrectas");

            return Ok(usuario);
        }
    }
}
