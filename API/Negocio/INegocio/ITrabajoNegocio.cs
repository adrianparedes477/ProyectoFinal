using Core.Modelos.DTO;

namespace API.Negocio.INegocio
{
    public interface ITrabajoNegocio
    {
        Task<IEnumerable<TrabajoReedDTO>> GetAllTrabajos(int pageNumber, int pageSize);
        Task<TrabajoReedDTO> GetTrabajoById(int id);
        Task<bool> CrearTrabajo(TrabajoCrearDTO trabajoDTO, int proyectoId, int servicioId);
        Task<bool> ActualizarTrabajo(TrabajoActualizarDTO trabajoDTO);
        Task<bool> EliminarTrabajo(int id);
    }
}
