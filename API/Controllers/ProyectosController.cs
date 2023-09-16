using API.Especificaciones;
using API.Negocio.INegocio;
using AutoMapper;
using Core.DTO;
using Core.Entidades;
using Core.Negocio;
using Core.Negocio.INegocio;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetAllProyectos(int pageNumber = 1, int pageSize = 10)
        {
            var parametros = new Parametros
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginaProyectos = await _unidadTrabajo.Proyecto.ObtenerTodosPaginado(parametros);
            return ResponseFactory.CreateSuccessResponse(200, paginaProyectos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProyectoById(int id)
        {
            var proyecto = await _proyectoNegocio.GetProyectoById(id);
            return ResponseFactory.CreateSuccessResponse(200, proyecto);
        }

        [HttpGet("proyectosPorEstado/{estado}")]
        public async Task<IActionResult> GetProyectosPorEstado(int estado)
        {
            var proyecto = await _proyectoNegocio.GetProyectosPorEstado(estado);
            return ResponseFactory.CreateSuccessResponse(200, proyecto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProyecto([FromBody] ProyectoDto proyectoDto)
        {
            var creado = await _proyectoNegocio.CrearProyecto(proyectoDto);

            if (creado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Proyecto creado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(400, "No se pudo crear el Proyecto");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProyecto(int id, [FromBody] ProyectoDto proyectoDto)
        {
            if (id != proyectoDto.Id)
            {
                return ResponseFactory.CreateErrorResponse(400, "Id del proyecto no Coincide");
            }

            if (!ModelState.IsValid)
            {
                return ResponseFactory.CreateErrorResponse(400, "Informacion Incorrecta");
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