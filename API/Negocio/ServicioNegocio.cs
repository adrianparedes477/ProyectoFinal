using API.Negocio.INegocio;
using AutoMapper;
using Core.DTO;
using Core.Entidades;
using Infraestructura.Data.Repositorio;
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

        public async Task<IEnumerable<ServicioDTO>> GetAllServicios()
        {
            var servicios = await _unidadTrabajo.Servicio.GetAll();
            return _mapper.Map<IEnumerable<ServicioDTO>>(servicios);
        }

        public async Task<ServicioDTO> GetServicioById(int id)
        {
            var servicio = await _unidadTrabajo.Servicio.GetById(id);
            return _mapper.Map<ServicioDTO>(servicio);
        }

        public async Task<bool> CrearServicio(ServicioDTO servicioDTO)
        {
            var servicio = _mapper.Map<Servicio>(servicioDTO);
            await _unidadTrabajo.Servicio.Agregar(servicio);
            await _unidadTrabajo.Guardar();
            return true;
        }

        public async Task<bool> ActualizarServicio(ServicioDTO servicioDTO)
        {
            var servicio = _mapper.Map<Servicio>(servicioDTO);
            var servicioExiste = await _servicioRepositorio.ObtenerPrimero(t => t.Id == servicio.Id);

            if (servicioExiste != null)
            {
                _servicioRepositorio.Actualizar(servicio); 
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
                _unidadTrabajo.Servicio.Remover(servicio);
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
