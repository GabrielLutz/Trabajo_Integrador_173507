using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Inscripcion;

namespace PortalDGC.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionController : ControllerBase
    {
        private readonly IInscripcionService _inscripcionService;

        public InscripcionController(IInscripcionService inscripcionService)
        {
            _inscripcionService = inscripcionService;
        }

        [HttpPost("postulante/{postulanteId}")]
        public async Task<IActionResult> CrearInscripcion(int postulanteId, [FromBody] CrearInscripcionDto inscripcionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _inscripcionService.CrearInscripcionAsync(postulanteId, inscripcionDto);

            if (!resultado.Success)
                return BadRequest(resultado);

            return CreatedAtAction(nameof(ObtenerInscripcion), new { id = resultado.Data!.Id }, resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerInscripcion(int id)
        {
            var resultado = await _inscripcionService.ObtenerInscripcionPorIdAsync(id);

            if (!resultado.Success)
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpGet("postulante/{postulanteId}")]
        public async Task<IActionResult> ObtenerInscripcionesPorPostulante(int postulanteId)
        {
            var resultado = await _inscripcionService.ObtenerInscripcionesPorPostulanteAsync(postulanteId);
            return Ok(resultado);
        }

        [HttpGet("validar/{postulanteId}/{llamadoId}")]
        public async Task<IActionResult> ValidarInscripcionExistente(int postulanteId, int llamadoId)
        {
            var resultado = await _inscripcionService.ValidarInscripcionExistenteAsync(postulanteId, llamadoId);
            return Ok(resultado);
        }

        [HttpGet("{id}/validar-requisitos")]
        public async Task<IActionResult> ValidarRequisitosObligatorios(int id)
        {
            var resultado = await _inscripcionService.ValidarRequisitosObligatoriosAsync(id);
            return Ok(resultado);
        }

        [HttpPost("{id}/calcular-puntaje")]
        public async Task<IActionResult> CalcularPuntajeTotal(int id)
        {
            var resultado = await _inscripcionService.CalcularPuntajeTotalAsync(id);

            if (!resultado.Success)
                return BadRequest(resultado);

            return Ok(resultado);
        }
    }
}
