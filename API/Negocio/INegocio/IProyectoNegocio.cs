using Core.DTO;

namespace API.Negocio.INegocio
{
    public interface IProyectoNegocio
    {
        Task<IEnumerable<ProyectoDto>> GetAllProyectos();
        Task<ProyectoDto> GetProyectoById(int id);
        Task<bool> CrearProyecto(ProyectoDto proyectoDTO);
        Task<bool> ActualizarProyecto(ProyectoDto proyectoDTO);
        Task<bool> EliminarProyecto(int id);

        Task<IEnumerable<ProyectoDto>> GetProyectosPorEstado(int estado);
    }
}
