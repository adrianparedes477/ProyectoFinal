using AutoMapper;
using Core.Entidades;
using Core.DTO;
using static Core.Entidades.Usuario;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Proyecto, ProyectoDto>().ReverseMap();
        CreateMap<Servicio, ServicioDTO>().ReverseMap();
        CreateMap<Servicio, ServicioDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();

        // Mapeo de Proyecto a ProyectoDto
        CreateMap<Proyecto, ProyectoDto>()
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.ToString()));

        // Mapeo de Servicio a ServicioDTO
        CreateMap<Servicio, ServicioDTO>()
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado ? "Activo" : "No Activo"));

        // Mapeo de Trabajo a TrabajoDTO
        CreateMap<Trabajo, TrabajoDTO>()
            .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.NombreProyecto, opt => opt.MapFrom(src => src.Proyecto.Nombre))
            .ForMember(dest => dest.NombreServicio, opt => opt.MapFrom(src => src.Servicio.Descr));

        // Mapeo de Usuario a UsuarioDTO
        CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()));

        // Mapeo de UsuarioDTO a Usuario (para el registro de usuarios)
        CreateMap<UsuarioDTO, Usuario>()
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => Enum.Parse(typeof(TipoUsuario), src.Tipo)));

    }
}
