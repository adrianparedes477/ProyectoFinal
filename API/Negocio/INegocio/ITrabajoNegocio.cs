using Core.DTO;

namespace API.Negocio.INegocio
{
    public interface ITrabajoNegocio
    {
        Task<IEnumerable<TrabajoDTO>> GetAllTrabajos();
        Task<TrabajoDTO> GetTrabajoById(int id);
        Task<bool> CrearTrabajo(TrabajoDTO trabajoDTO);
        Task<bool> ActualizarTrabajo(TrabajoDTO trabajoDTO);
        Task<bool> EliminarTrabajo(int id);
    }
}
