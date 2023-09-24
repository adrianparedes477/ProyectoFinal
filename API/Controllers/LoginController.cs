using API.Helpers;
using API.Negocio.INegocio;
using Core.Modelos.DTO;
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
        /// Realiza el inicio de sesión de un usuario.
        /// </summary>
        /// <param name="dto">Credenciales del usuario.</param>
        /// <returns>Token de autenticación.</returns>
        /// <response code="200">Inicio de sesión exitoso. Devuelve el token de autenticación.</response>
        /// <response code="400">Solicitud incorrecta. Si el DTO es nulo o no es válido.</response>
        /// <response code="401">Credenciales inválidas. Si las credenciales proporcionadas son incorrectas.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] AuthenticateDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Solicitud incorrecta. Asegúrese de proporcionar credenciales válidas.");
            }

            var usuario = await _loginNegocio.AuthenticateCredentials(dto);

            if (usuario is null)
            {
                return Unauthorized("Credenciales inválidas. Asegúrese de proporcionar credenciales correctas.");
            }

            return Ok(usuario);
        }


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
