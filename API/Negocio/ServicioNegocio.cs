using API.Especificaciones;
using API.Negocio.INegocio;
using AutoMapper;
using Core.DTO;
using Core.Entidades;
using Infraestructura.Data.Repositorio.IRepositorio;

namespace API.Negocio
{
    public class ServicioNegocio : IServicioNegocio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        private readonly IServicioRepositorio _servicioRepositorio;

        public ServicioNegocio(IUnidadTrabajo unidadTrabajo, IMapper mapper, IServicioRepositorio servicioRepositorio)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _servicioRepositorio = servicioRepositorio;
        }

        public async Task<IEnumerable<ServicioReedDTO>> GetAllServicios(int pageNumber, int pageSize)
        {
            var parametros = new Parametros
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginaServicios = await _unidadTrabajo.Servicio.ObtenerTodosPaginado(parametros);
            return _mapper.Map<IEnumerable<ServicioReedDTO>>(paginaServicios);
        }

        public async Task<ServicioReedDTO> GetServicioById(int id)
        {
            var servicio = await _unidadTrabajo.Servicio.GetById(id);

            if (servicio == null)
            {
                return null;
            }

            return _mapper.Map<ServicioReedDTO>(servicio);
        }


        public async Task<bool> CrearServicio(ServicioReedDTO servicioDTO)
        {
            var servicio = _mapper.Map<Servicio>(servicioDTO);
            await _unidadTrabajo.Servicio.Agregar(servicio);
            await _unidadTrabajo.Guardar();
            return true;
        }

        public async Task<bool> ActualizarServicio(ServicioDTO servicioDTO)
        {
            var servicioExiste = await _servicioRepositorio.ObtenerPrimero(t => t.Id == servicioDTO.Id);

            if (servicioExiste != null)
            {
                if (!string.IsNullOrEmpty(servicioDTO.Descr))
                {
                    servicioExiste.Descr = servicioDTO.Descr;
                }

                if (servicioDTO.Estado != default(bool))
                {
                    servicioExiste.Estado = servicioDTO.Estado;
                }

                if (servicioDTO.ValorHora != default(decimal))
                {
                    servicioExiste.ValorHora = servicioDTO.ValorHora;
                }

                _servicioRepositorio.Actualizar(servicioExiste);
                await _unidadTrabajo.Guardar();
                return true;
            }
            else
            {
                throw new Exception("El Servicio no existe en la base de datos.");
            }
        }

        public async Task<bool> EliminarServicio(int id)
        {
            var servicio = await _unidadTrabajo.Servicio.GetById(id);

            if (servicio != null)
            {
                await _unidadTrabajo.Servicio.Eliminar(id);
                await _unidadTrabajo.Guardar();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ServicioDTO>> GetServiciosActivos()
        {
            var serviciosActivos = await _unidadTrabajo.Servicio.GetAll(
                filtro: s => s.Estado,
                isTracking: false
            );

            return _mapper.Map<IEnumerable<ServicioDTO>>(serviciosActivos);
        }
    }
}
