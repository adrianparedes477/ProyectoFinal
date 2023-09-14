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

        public async Task<IEnumerable<TrabajoDTO>> GetAllTrabajos()
        {
            var trabajos = await _unidadTrabajo.Trabajo.GetAll(incluirPropiedades: "Proyecto,Servicio");
            return _mapper.Map<IEnumerable<TrabajoDTO>>(trabajos);
        }

        public async Task<TrabajoDTO> GetTrabajoById(int id)
        {
            var trabajo = await _unidadTrabajo.Trabajo.GetByIdWithPropertiesAsync(id);

            return _mapper.Map<TrabajoDTO>(trabajo);
        }

        public async Task<bool> CrearTrabajo(TrabajoDTO trabajoDTO)
        {
            var trabajo = _mapper.Map<Trabajo>(trabajoDTO);
            await _unidadTrabajo.Trabajo.Agregar(trabajo);
            await _unidadTrabajo.Guardar();
            return true;
        }

        public async Task<bool> ActualizarTrabajo(TrabajoDTO trabajoDTO)
        {
            var trabajo = _mapper.Map<Trabajo>(trabajoDTO);
            var trabajoExiste = await _trabajoRepositorio.ObtenerPrimero(t => t.Id == trabajo.Id);

            if (trabajoExiste != null)
            {
                _trabajoRepositorio.Actualizar(trabajo);
                await _unidadTrabajo.Guardar();
                return true;
            }
            else
            {
                throw new Exception("El trabajo no existe en la base de datos.");
            }
        }




        public async Task<bool> EliminarTrabajo(int id)
        {
            var trabajo = await _unidadTrabajo.Trabajo.GetById(id);

            if (trabajo != null)
            {
                _unidadTrabajo.Trabajo.Remover(trabajo);
                await _unidadTrabajo.Guardar();
                return true;
            }

            return false;
        }

       
    }
}
