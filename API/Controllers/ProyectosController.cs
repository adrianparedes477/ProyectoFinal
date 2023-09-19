using API.Especificaciones;
using API.Negocio.INegocio;
using AutoMapper;
using Core.DTO;
using Core.Entidades;
using Core.Negocio;
using Core.Negocio.INegocio;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Proyectos")]
    public class ProyectosController : ControllerBase
    {
        private readonly IProyectoNegocio _proyectoNegocio;
        private readonly IUnidadTrabajo _unidadTrabajo;
        public ProyectosController(IProyectoNegocio proyectoNegocio, IUnidadTrabajo unidadTrabajo)
        {
            _proyectoNegocio = proyectoNegocio;
            _unidadTrabajo = unidadTrabajo;
        }


        /// <summary>
        /// Obtiene todos los proyectos paginados.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProyectos(int pageNumber = 1, int pageSize = 10)
        {
            var proyectosDto = await _proyectoNegocio.GetAllProyectos(pageNumber, pageSize);
            return ResponseFactory.CreateSuccessResponse(200, proyectosDto);
        }

        /// <summary>
        /// Obtiene un proyecto por su ID.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProyectoById(int id)
        {
            var proyecto = await _proyectoNegocio.GetProyectoById(id);

            if (proyecto == null)
            {
                return ResponseFactory.CreateSuccessResponse(404,"El proyecto no fue encontrado");
            }

            return ResponseFactory.CreateSuccessResponse(200, proyecto);
        }

        /// <summary>
        /// Obtiene proyectos por su estado.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("proyectosPorEstado/{estado}")]
        public async Task<IActionResult> GetProyectosPorEstado(int estado)
        {
            var proyecto = await _proyectoNegocio.GetProyectosPorEstado(estado);
            return ResponseFactory.CreateSuccessResponse(200, proyecto);
        }

        /// <summary>
        /// Crea un nuevo proyecto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearProyecto([FromBody] ProyectoReedDto proyectoDto)
        {
            var existeProyecto = await _unidadTrabajo.Proyecto.Existe(p => p.Nombre == proyectoDto.Nombre);

            if (existeProyecto)
            {
                return ResponseFactory.CreateErrorResponse(400, "Ya existe un proyecto con ese nombre.");
            }

            var creado = await _proyectoNegocio.CrearProyecto(proyectoDto);

            if (creado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Proyecto creado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(400, "No se pudo crear el Proyecto");
        }

        /// <summary>
        /// Actualiza un proyecto existente por su ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProyecto(int id, [FromBody] ProyectoDto proyectoDto)
        {
            if (id != proyectoDto.Id)
            {
                return ResponseFactory.CreateErrorResponse(400, "Id del proyecto no coincide");
            }

            if (!ModelState.IsValid)
            {
                return ResponseFactory.CreateErrorResponse(400, "Informacion incorrecta");
            }

            try
            {
                var actualizado = await _proyectoNegocio.ActualizarProyecto(proyectoDto);

                if (actualizado)
                {
                    return ResponseFactory.CreateSuccessResponse(200, "Proyecto actualizado con éxito");
                }
                else
                {
                    return ResponseFactory.CreateErrorResponse(400, "El Proyecto no pudo ser actualizado");
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateErrorResponse(500, ex.Message);
            }
        }

        /// <summary>
        /// Elimina un proyecto por su ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProyecto(int id)
        {
            var eliminado = await _proyectoNegocio.EliminarProyecto(id);

            if (eliminado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Proyecto eliminado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(404, "Proyecto no encontrado");
        }
    }
}