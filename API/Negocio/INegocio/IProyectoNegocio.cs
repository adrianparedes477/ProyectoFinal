using Core.Modelos.DTO;

namespace API.Negocio.INegocio
{
    public interface IProyectoNegocio
    {
        Task<IEnumerable<ProyectoReedDto>> GetAllProyectos(int pageNumber, int pageSize);
        Task<ProyectoReedDto> GetProyectoById(int id);
        Task<bool> CrearProyecto(ProyectoReedDto proyectoDTO);
        Task<bool> ActualizarProyecto(ProyectoDto proyectoDTO);
        Task<bool> EliminarProyecto(int id);

        Task<IEnumerable<ProyectoReedDto>> GetProyectosPorEstado(int estado);
    }
}
