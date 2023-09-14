using API.Negocio.INegocio;
using AutoMapper;
using Core.DTO;
using Core.Entidades;
using Core.Negocio.INegocio;
using Infraestructura.Data.Repositorio;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Negocio
{
    public class ProyectoNegocio : IProyectoNegocio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        private readonly IProyectoRepositorio _proyectoRepositorio;

        public ProyectoNegocio(IUnidadTrabajo unidadTrabajo, IMapper mapper, IProyectoRepositorio proyectoRepositorio)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _proyectoRepositorio = proyectoRepositorio;
        }

        public async Task<IEnumerable<ProyectoDto>> GetAllProyectos()
        {
            var proyecto = await _unidadTrabajo.Proyecto.GetAll();
            return _mapper.Map<IEnumerable<ProyectoDto>>(proyecto);
        }

        public async Task<ProyectoDto> GetProyectoById(int id)
        {
            var proyecto = await _unidadTrabajo.Proyecto.GetById(id);
            return _mapper.Map<ProyectoDto>(proyecto);
        }

        public async Task<bool> CrearProyecto(ProyectoDto proyectoDto)
        {
            var proyecto = _mapper.Map<Proyecto>(proyectoDto);
            await _unidadTrabajo.Proyecto.Agregar(proyecto);
            await _unidadTrabajo.Guardar();
            return true;
        }

        public async Task<bool> ActualizarProyecto(ProyectoDto proyectoDto)
        {
            var proyecto = _mapper.Map<Proyecto>(proyectoDto);
            var proyectoExiste = await _proyectoRepositorio.ObtenerPrimero(t => t.Id == proyecto.Id);

            if (proyectoExiste != null)
            {
                _proyectoRepositorio.Actualizar(proyecto); 
                await _unidadTrabajo.Guardar();
                return true;
            }
            else
            {
                throw new Exception("El Proyecto no existe en la base de datos.");
            }

        }

        public async Task<bool> EliminarProyecto(int id)
        {
            var proyecto = await _unidadTrabajo.Proyecto.GetById(id);

            if (proyecto != null)
            {
                _unidadTrabajo.Proyecto.Remover(proyecto);
                await _unidadTrabajo.Guardar();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ProyectoDto>> GetProyectosPorEstado(int estado)
        {
            var estadoProyecto = (Proyecto.EstadoProyecto)estado;
            var proyectosPorEstado = await _unidadTrabajo.Proyecto.GetAll(
                filtro: p => p.Estado == estadoProyecto,
                isTracking: false
            );

            var proyectosDto = _mapper.Map<IEnumerable<ProyectoDto>>(proyectosPorEstado);

            return proyectosDto;
        }
    }


}
