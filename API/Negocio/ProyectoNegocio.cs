using API.Especificaciones;
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
using static Core.Entidades.Proyecto;

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

        public async Task<IEnumerable<ProyectoReedDto>> GetAllProyectos(int pageNumber, int pageSize)
        {
            var parametros = new Parametros
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginaProyectos = await _unidadTrabajo.Proyecto.ObtenerTodosPaginado(parametros);
            return _mapper.Map<IEnumerable<ProyectoReedDto>>(paginaProyectos);
        }

        public async Task<ProyectoReedDto> GetProyectoById(int id)
        {
            var proyecto = await _unidadTrabajo.Proyecto.GetById(id);

            if (proyecto == null)
            {
                return null; 
            }

            return _mapper.Map<ProyectoReedDto>(proyecto);
        }

        public async Task<bool> CrearProyecto(ProyectoReedDto proyectoDto)
        {
            var existeProyecto = await _unidadTrabajo.Proyecto.Existe(p => p.Nombre == proyectoDto.Nombre);

            if (existeProyecto)
            {
                throw new Exception("Ya existe un proyecto con ese nombre.");
            }

            var proyecto = _mapper.Map<Proyecto>(proyectoDto);
            await _unidadTrabajo.Proyecto.Agregar(proyecto);
            await _unidadTrabajo.Guardar();
            return true;
        }

        public async Task<bool> ActualizarProyecto(ProyectoDto proyectoDto)
        {
            var proyectoExiste = await _proyectoRepositorio.ObtenerPrimero(t => t.Id == proyectoDto.Id);

            if (proyectoExiste != null)
            {
                // Verificar si proyectoDto.Nombre no es nulo o vacío antes de actualizar
                if (!string.IsNullOrEmpty(proyectoDto.Nombre))
                {
                    proyectoExiste.Nombre = proyectoDto.Nombre;
                }

                // Verificar si proyectoDto.Direccion no es nulo o vacío antes de actualizar
                if (!string.IsNullOrEmpty(proyectoDto.Direccion))
                {
                    proyectoExiste.Direccion = proyectoDto.Direccion;
                }

                // Verificar si proyectoDto.Estado no es nulo o vacío antes de actualizar
                if (!string.IsNullOrEmpty(proyectoDto.Estado))
                {
                    if (Enum.TryParse(typeof(EstadoProyecto), proyectoDto.Estado, out var estado))
                    {
                        proyectoExiste.Estado = (EstadoProyecto)estado;
                    }
                    else
                    {
                        throw new Exception("El valor del estado no es válido.");
                    }
                }
                _proyectoRepositorio.Actualizar(proyectoExiste);
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

        public async Task<IEnumerable<ProyectoReedDto>> GetProyectosPorEstado(int estado)
        {
            var estadoProyecto = (Proyecto.EstadoProyecto)estado;
            var proyectosPorEstado = await _unidadTrabajo.Proyecto.GetAll(
                filtro: p => p.Estado == estadoProyecto,
                isTracking: false
            );

            var proyectosDto = _mapper.Map<IEnumerable<ProyectoReedDto>>(proyectosPorEstado);

            return proyectosDto;
        }
    }


}
