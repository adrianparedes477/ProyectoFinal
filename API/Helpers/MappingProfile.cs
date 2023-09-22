using AutoMapper;
using Core.Entidades;
using static Core.Entidades.Usuario;
using System.Globalization;
using Core.Modelos.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Proyecto, ProyectoDto>().ReverseMap();
        CreateMap<Servicio, ServicioDTO>().ReverseMap();
        CreateMap<Trabajo, TrabajoDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();

        CreateMap<Proyecto, ProyectoReedDto>().ReverseMap();
        CreateMap<Servicio, ServicioReedDTO>().ReverseMap();
        CreateMap<Trabajo, TrabajoReedDTO>().ReverseMap();
        CreateMap<Trabajo, TrabajoCrearDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioReedDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioLoginDTO>().ReverseMap();

        // Mapeo de Proyecto a ProyectoDto
        CreateMap<Proyecto, ProyectoDto>()
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.ToString()));

        // Mapeo de TrabajoDTO a Trabajo
        CreateMap<TrabajoDTO, Trabajo>()
        .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => DateTime.ParseExact(src.Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
        .ForMember(dest => dest.ValorHora, opt => opt.MapFrom(src => src.ValorHora))
        .ForMember(dest => dest.Costo, opt => opt.MapFrom(src => src.Costo));

        CreateMap<TrabajoActualizarDTO, Trabajo>()
       .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => DateTime.ParseExact(src.Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

        // Mapeo de Usuario a UsuarioDTO
        CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()));

        // Mapeo de UsuarioDTO a Usuario (para el registro de usuarios)
        CreateMap<UsuarioDTO, Usuario>()
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => Enum.Parse(typeof(TipoUsuario), src.Tipo)));

    }
}
