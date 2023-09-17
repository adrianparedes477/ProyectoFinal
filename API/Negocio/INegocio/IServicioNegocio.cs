using Core.DTO;

namespace API.Negocio.INegocio
{
    public interface IServicioNegocio
    {
        Task<IEnumerable<ServicioReedDTO>> GetAllServicios(int pageNumber, int pageSize);
        Task<ServicioReedDTO> GetServicioById(int id);
        Task<bool> CrearServicio(ServicioReedDTO servicioDTO);
        Task<bool> ActualizarServicio(ServicioDTO servicioDTO);
        Task<bool> EliminarServicio(int id);
        Task<IEnumerable<ServicioDTO>> GetServiciosActivos();
    }
}
