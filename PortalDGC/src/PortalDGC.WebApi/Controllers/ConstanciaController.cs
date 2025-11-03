using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Constancia;

namespace PortalDGC.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConstanciaController : ControllerBase
    {
        private readonly IConstanciaService _constanciaService;

        public ConstanciaController(IConstanciaService constanciaService)
        {
            _constanciaService = constanciaService;
        }

        [HttpPost("postulante/{postulanteId}")]
        public async Task<IActionResult> SubirConstancia(int postulanteId, [FromBody] SubirConstanciaDto constanciaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _constanciaService.SubirConstanciaAsync(postulanteId, constanciaDto);

            if (!resultado.Success)
                return BadRequest(resultado);

            return CreatedAtAction(nameof(ObtenerConstancia), new { id = resultado.Data!.Id }, resultado);
        }

        [HttpGet("postulante/{postulanteId}")]
        public async Task<IActionResult> ObtenerConstanciasPorPostulante(int postulanteId)
        {
            var resultado = await _constanciaService.ObtenerConstanciasPorPostulanteAsync(postulanteId);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerConstancia(int id)
        {
            var resultado = await _constanciaService.ObtenerConstanciaPorIdAsync(id);

            if (!resultado.Success)
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpPut("{id}/validar")]
        public async Task<IActionResult> ValidarConstancia(int id)
        {
            var resultado = await _constanciaService.ValidarConstanciaAsync(id);

            if (!resultado.Success)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpGet("{id}/descargar")]
        public async Task<IActionResult> DescargarConstancia(int id)
        {
            var resultado = await _constanciaService.DescargarConstanciaAsync(id);

            if (!resultado.Success)
                return NotFound(resultado);

            return File(resultado.Data!, "application/octet-stream");
        }
    }
}
