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
    [Route("api/Servicios")]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioNegocio _servicioNegocio;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ServiciosController(IServicioNegocio servicioNegocio, IUnidadTrabajo unidadTrabajo)
        {
            _servicioNegocio = servicioNegocio;
            _unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServicios(int pageNumber = 1, int pageSize = 10)
        {
            var parametros = new Parametros
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginaServicios = await _unidadTrabajo.Servicio.ObtenerTodosPaginado(parametros);
            return ResponseFactory.CreateSuccessResponse(200, paginaServicios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServicioById(int id)
        {
            var servicio = await _servicioNegocio.GetServicioById(id);
            if (servicio == null)
            {
                return ResponseFactory.CreateErrorResponse(404, "Servicio no encontrado");
            }

            return ResponseFactory.CreateSuccessResponse(200, servicio);
        }

        [HttpGet("activos")]
        public async Task<IActionResult> GetServiciosActivos()
        {
            var serviciosActivos = await _servicioNegocio.GetServiciosActivos();
            return ResponseFactory.CreateSuccessResponse(200, serviciosActivos);
        }

        [HttpPost]
        public async Task<IActionResult> CrearServicio([FromBody] ServicioDTO servicioDTO)
        {
            var creado = await _servicioNegocio.CrearServicio(servicioDTO);

            if (creado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Servicio creado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(400, "No se pudo crear el Servicio");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarServicio(int id, [FromBody] ServicioDTO servicioDTO)
        {
            if (id != servicioDTO.Id)
            {
                return ResponseFactory.CreateErrorResponse(400, "Id del servicio no Coincide");
            }

            if (!ModelState.IsValid)
            {
                return ResponseFactory.CreateErrorResponse(400, "Informacion Incorrecta");
            }

            try
            {
                var actualizado = await _servicioNegocio.ActualizarServicio(servicioDTO);

                if (actualizado)
                {
                    return ResponseFactory.CreateSuccessResponse(200, "Servicio actualizado con éxito");
                }
                else
                {
                    return ResponseFactory.CreateErrorResponse(400, "El Servicio no pudo ser actualizado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarServicio(int id)
        {
            var eliminado = await _servicioNegocio.EliminarServicio(id);

            if (eliminado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Servicio eliminado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(404, "Servicio no encontrado");
        }

    }

}
