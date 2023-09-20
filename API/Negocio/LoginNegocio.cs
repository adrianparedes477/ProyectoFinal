using API.Helpers;
using API.Negocio.INegocio;
using Core.DTO;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.Extensions.Configuration;

namespace API.Negocio
{
    public class LoginNegocio : ILoginNegocio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private TokenJwtHelper _tokenJwtHelper;

        public LoginNegocio(IUnidadTrabajo unidadTrabajo, IConfiguration configuration)
        {
            _unidadTrabajo = unidadTrabajo;
            _tokenJwtHelper = new TokenJwtHelper(configuration);
        }

        public async Task<UsuarioLoginDTO> AuthenticateCredentials(AuthenticateDto dto)
        {
            var userCredentials = await _unidadTrabajo.Usuario.AuthenticateCredentials(dto);

            if (userCredentials is null)
                return null;

            var token = _tokenJwtHelper.GenerateToken(userCredentials);

            var usuario = new UsuarioLoginDTO()
            {
                NombreCompleto = userCredentials.NombreCompleto,
                Dni = userCredentials.Dni,
                Token = token
            };

            return usuario;
        }
    }
}
