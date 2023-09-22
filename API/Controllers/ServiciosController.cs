using API.Negocio.INegocio;
using Core.Modelos.DTO;
using Infraestructura.Data.Repositorio.IRepositorio;
using Infraestructura.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Obtiene todos los servicios paginados.
        /// </summary>
        /// <param name="pageNumber">Número de página.</param>
        /// <param name="pageSize">Tamaño de la página.</param>
        /// <returns>Lista de servicios paginados.</returns>
        [HttpGet]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        public async Task<IActionResult> GetAllServicios(int pageNumber = 1, int pageSize = 10)
        {
            var servicioDto = await _servicioNegocio.GetAllServicios(pageNumber, pageSize);
            return ResponseFactory.CreateSuccessResponse(200, servicioDto);
        }


        /// <summary>
        /// Obtiene un servicio por su ID.
        /// </summary>
        /// <param name="id">ID del servicio.</param>
        /// <returns>El servicio correspondiente al ID proporcionado.</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        public async Task<IActionResult> GetServicioById(int id)
        {
            var servicioDto = await _servicioNegocio.GetServicioById(id);

            if (servicioDto == null)
            {
                return ResponseFactory.CreateErrorResponse(404, "El servicio no fue encontrado");
            }

            return ResponseFactory.CreateSuccessResponse(200, servicioDto);
        }


        /// <summary>
        /// Obtiene servicios activos.
        /// </summary>
        /// <returns>Lista de servicios activos.</returns>
        [HttpGet("activos")]
        [Authorize(Policy = "AdminOrConsultor")]
        [ProducesResponseType(typeof(ApiSuccessResponse), 200)]
        public async Task<IActionResult> GetServiciosActivos()
        {
            var serviciosActivos = await _servicioNegocio.GetServiciosActivos();
            return ResponseFactory.CreateSuccessResponse(200, serviciosActivos);
        }

        /// <summary>
        /// Crea un nuevo servicio.
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        public async Task<IActionResult> CrearServicio([FromBody] ServicioReedDTO servicioDTO)
        {
            var creado = await _servicioNegocio.CrearServicio(servicioDTO);

            if (creado)
            {
                return ResponseFactory.CreateSuccessResponse(200, "Servicio creado exitosamente");
            }

            return ResponseFactory.CreateErrorResponse(400, "No se pudo crear el Servicio");
        }

        /// <summary>
        /// Actualiza un servicio existente por su ID.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Policy = "Administrador")]
        public async Task<IActionResult> ActualizarServicio(int id, [FromBody] ServicioDTO servicioDTO)
        {
            if (id != servicioDTO.Id)
            {
                return ResponseFactory.CreateErrorResponse(400, "Id del servicio no coincide");
            }

            if (!ModelState.IsValid)
            {
                return ResponseFactory.CreateErrorResponse(400, "Informacion incorrecta");
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


        /// <summary>
        /// Elimina un servicio por su ID.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Administrador")]
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
