using Core.DTO;

namespace API.Negocio.INegocio
{
    public interface IServicioNegocio
    {
        Task<IEnumerable<ServicioDTO>> GetAllServicios();
        Task<ServicioDTO> GetServicioById(int id);
        Task<bool> CrearServicio(ServicioDTO servicioDTO);
        Task<bool> ActualizarServicio(ServicioDTO servicioDTO);
        Task<bool> EliminarServicio(int id);
        Task<IEnumerable<ServicioDTO>> GetServiciosActivos();
    }
}
