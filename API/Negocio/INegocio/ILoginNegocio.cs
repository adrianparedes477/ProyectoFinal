using Core.DTO;

namespace API.Negocio.INegocio
{
    public interface ILoginNegocio
    {
        Task<UsuarioLoginDTO> AuthenticateCredentials(AuthenticateDto dto);
    }
}
