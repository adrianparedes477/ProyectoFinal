using API.Helpers;
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

        private TokenJwtHelper _tokenJwtHelper;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public LoginController(IUnidadTrabajo unidadTrabajo, IConfiguration configuration)
        {
            _unidadTrabajo = unidadTrabajo;
            _tokenJwtHelper = new TokenJwtHelper(configuration);
        }

        /// <summary>
        /// Logueo de usuario.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthenticateDto dto)
        {
            var userCredentials = await _unidadTrabajo.Usuario.AuthenticateCredentials(dto);
            if (userCredentials is null) return Unauthorized("Las credenciales son incorrectas");

            var token = _tokenJwtHelper.GenerateToken(userCredentials);

            var usuario = new UsuarioLoginDTO()
            {
                NombreCompleto = userCredentials.NombreCompleto,
                Dni = userCredentials.Dni,
                Token = token
            };


            return Ok(usuario);

        }
    }
}
