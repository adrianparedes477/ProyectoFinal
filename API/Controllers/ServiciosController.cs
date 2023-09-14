using API.Especificaciones;
using API.Negocio.INegocio;
using AutoMapper;
using Core.DTO;
using Core.Entidades;
using Core.Negocio.INegocio;
using Infraestructura.Data.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(paginaServicios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServicioById(int id)
        {
            var servicio = await _servicioNegocio.GetServicioById(id);
            return Ok(servicio);
        }

        [HttpGet("activos")]
        public async Task<IActionResult> GetServiciosActivos()
        {
            var serviciosActivos = await _servicioNegocio.GetServiciosActivos();
            return Ok(serviciosActivos);
        }


        [HttpPost]
        public async Task<IActionResult> CrearProyecto([FromBody] ServicioDTO servicioDTO)
        {
            var creado = await _servicioNegocio.CrearServicio(servicioDTO);

            if (creado)
            {
                return Ok("Servicio creado exitosamente");
            }

            return BadRequest("No se pudo crear el Servicio");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarServicio(int id, [FromBody] ServicioDTO servicioDTO)
        {
            // Llama al método de negocio para actualizar un usuario
            var actualizado = await _servicioNegocio.ActualizarServicio(servicioDTO);

            if (actualizado)
            {
                return Ok("Servicio Actulizado exitosamente"); // Indica que la operación fue exitosa
            }

            return NotFound("Servicio No Encontrado"); // Indica que no se encontró el usuario o no se pudo actualizar
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarServicio(int id)
        {
            var eliminado = await _servicioNegocio.EliminarServicio(id);

            if (eliminado)
            {
                return Ok("Servicio eliminado exitosamente");
            }

            return NotFound("Servicio no encontrado");
        }
    }

}
