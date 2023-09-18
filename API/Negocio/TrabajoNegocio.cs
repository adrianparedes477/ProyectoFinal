using API.Especificaciones;
using API.Negocio.INegocio;
using AutoMapper;
using Core.DTO;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace API.Negocio
{
    public class TrabajoNegocio : ITrabajoNegocio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        private readonly ITrabajoRepositorio _trabajoRepositorio;

        public TrabajoNegocio(IUnidadTrabajo unidadTrabajo, IMapper mapper, ITrabajoRepositorio trabajoRepositorio)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _trabajoRepositorio = trabajoRepositorio;
        }

        public async Task<IEnumerable<TrabajoReedDTO>> GetAllTrabajos(int pageNumber, int pageSize)
        {
            var parametros = new Parametros
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginaTrabajos = await _unidadTrabajo.Trabajo.ObtenerTodosPaginado(parametros, incluirPropiedades: "Proyecto,Servicio");

            return paginaTrabajos.Select(trabajo =>
            {
                var trabajoDto = _mapper.Map<TrabajoReedDTO>(trabajo);
                trabajoDto.Proyecto = _mapper.Map<ProyectoReedDto>(trabajo.Proyecto);
                trabajoDto.Servicio = _mapper.Map<ServicioReedDTO>(trabajo.Servicio);
                return trabajoDto;
            });
        }


        public async Task<TrabajoReedDTO> GetTrabajoById(int id)
        {
            var trabajo = await _unidadTrabajo.Trabajo.GetByIdWithPropertiesAsync(id);

            var trabajoDto = _mapper.Map<TrabajoReedDTO>(trabajo);
            trabajoDto.Proyecto = _mapper.Map<ProyectoReedDto>(trabajo.Proyecto);
            trabajoDto.Servicio = _mapper.Map<ServicioReedDTO>(trabajo.Servicio);
            return trabajoDto;
        }

        public async Task<bool> CrearTrabajo(TrabajoCrearDTO trabajoDto, int proyectoId, int servicioId)
        {
            var proyecto = await _unidadTrabajo.Proyecto.GetById(proyectoId);
            var servicio = await _unidadTrabajo.Servicio.GetById(servicioId);

            if (proyecto != null && servicio != null)
            {
                var trabajo = _mapper.Map<Trabajo>(trabajoDto);
                trabajo.Proyecto = proyecto;
                trabajo.Servicio = servicio;

                await _unidadTrabajo.Trabajo.Agregar(trabajo);
                await _unidadTrabajo.Guardar();
                return true;
            }
            else
            {
                throw new Exception("El Proyecto o el Servicio no existen en la base de datos.");
            }
        }

        public async Task<bool> ActualizarTrabajo(TrabajoActualizarDTO trabajoDTO)
        {
            var trabajoExiste = await _trabajoRepositorio.ObtenerPrimero(t => t.Id == trabajoDTO.Id);

            if (trabajoExiste !=null)
            {
                // Verificar si trabajoDto.Fecha no es nulo o vacío antes de actualizar
                if (!string.IsNullOrEmpty(trabajoDTO.Fecha))
                {
                    // Puedes realizar la conversión de la cadena a DateTime aquí
                    if (DateTime.TryParse(trabajoDTO.Fecha, out var fecha))
                    {
                        trabajoExiste.Fecha = fecha;
                    }
                    else
                    {
                        throw new Exception("El formato de la fecha no es válido.");
                    }
                }

                // Verificar si trabajoDto.CantHoras es mayor que cero antes de actualizar
                if (trabajoDTO.CantHoras > 0)
                {
                    trabajoExiste.CantHoras = trabajoDTO.CantHoras;
                }

                // Verificar si trabajoDto.ValorHora es mayor que cero antes de actualizar
                if (trabajoDTO.ValorHora > 0)
                {
                    trabajoExiste.ValorHora = trabajoDTO.ValorHora;
                }

                // Verificar si trabajoDto.Costo es mayor o igual a cero antes de actualizar
                if (trabajoDTO.Costo >= 0)
                {
                    trabajoExiste.Costo = trabajoDTO.Costo;
                }

                _trabajoRepositorio.Actualizar(trabajoExiste);
                await _unidadTrabajo.Guardar();
                return true;
            }
            else
            {
                throw new Exception("El Trabajo no existe en la base de datos.");
            }
        }

        public async Task<bool> EliminarTrabajo(int id)
        {
            var trabajo = await _unidadTrabajo.Trabajo.GetById(id);

            if (trabajo != null)
            {
                await _unidadTrabajo.Trabajo.Eliminar(id);
                await _unidadTrabajo.Guardar();
                return true;
            }

            return false;
        }

       
    }
}
