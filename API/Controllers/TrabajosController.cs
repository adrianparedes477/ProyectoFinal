using API.Negocio.INegocio;
using Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Trabajos")]
    public class TrabajosController : ControllerBase
    {
        private readonly ITrabajoNegocio _trabajoNegocio;

        public TrabajosController(ITrabajoNegocio trabajoNegocio)
        {
            _trabajoNegocio = trabajoNegocio;
        }

        [HttpGet]
        public async Task<IEnumerable<TrabajoDTO>> GetAllTrabajos()
        {
            return await _trabajoNegocio.GetAllTrabajos();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrabajoDTO>> GetTrabajoById(int id)
        {
            var trabajo = await _trabajoNegocio.GetTrabajoById(id);

            if (trabajo == null)
            {
                return NotFound();
            }

            return trabajo;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CrearTrabajo(TrabajoDTO trabajoDTO)
        {
            try
            {
                await _trabajoNegocio.CrearTrabajo(trabajoDTO);
                return true;
            }
            catch (Exception ex)
            {
                // Maneja la excepción según tus necesidades
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> ActualizarTrabajo(int id, TrabajoDTO trabajoDTO)
        {
            try
            {
                if (id != trabajoDTO.Id)
                {
                    return BadRequest("ID no coincide con el trabajo");
                }

                var actualizado = await _trabajoNegocio.ActualizarTrabajo(trabajoDTO);

                if (!actualizado)
                {
                    return NotFound("Trabajo no encontrado");
                }

                return true;
            }
            catch (Exception ex)
            {
                // Maneja la excepción según tus necesidades
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> EliminarTrabajo(int id)
        {
            try
            {
                var eliminado = await _trabajoNegocio.EliminarTrabajo(id);

                if (!eliminado)
                {
                    return NotFound("Trabajo no encontrado");
                }

                return true;
            }
            catch (Exception ex)
            {
                // Maneja la excepción según tus necesidades
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }
    }
}
