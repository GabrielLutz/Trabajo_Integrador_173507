using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Tribunal;

namespace PortalDGC.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TribunalController : ControllerBase
    {
        private readonly ITribunalService _tribunalService;

        public TribunalController(ITribunalService tribunalService)
        {
            _tribunalService = tribunalService;
        }

        [HttpGet("llamado/{llamadoId}/inscripciones")]
        public async Task<IActionResult> ObtenerInscripcionesParaEvaluar(int llamadoId)
        {
            var resultado = await _tribunalService.ObtenerInscripcionesParaEvaluarAsync(llamadoId);
            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpGet("inscripcion/{inscripcionId}/detalle")]
        public async Task<IActionResult> ObtenerDetalleEvaluacion(int inscripcionId)
        {
            var resultado = await _tribunalService.ObtenerDetalleEvaluacionAsync(inscripcionId);
            if (!resultado.Success)
            {
                return NotFound(resultado);
            }

            return Ok(resultado);
        }

        [HttpGet("llamado/{llamadoId}/estadisticas")]
        public async Task<IActionResult> ObtenerEstadisticas(int llamadoId)
        {
            var resultado = await _tribunalService.ObtenerEstadisticasAsync(llamadoId);
            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpGet("llamado/{llamadoId}/pruebas")]
        public async Task<IActionResult> ObtenerPruebasDelLlamado(int llamadoId)
        {
            var resultado = await _tribunalService.ObtenerPruebasDelLlamadoAsync(llamadoId);
            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpPost("calificar-prueba")]
        public async Task<IActionResult> CalificarPrueba([FromBody] CalificarPruebaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _tribunalService.CalificarPruebaAsync(dto);
            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpPost("valorar-merito")]
        public async Task<IActionResult> ValorarMerito([FromBody] ValorarMeritoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _tribunalService.ValorarMeritoAsync(dto);
            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpPost("inscripcion/{inscripcionId}/valorar-meritos")]
        public async Task<IActionResult> ValorarMeritos(int inscripcionId, [FromBody] List<ValorarMeritoDto> meritos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _tribunalService.ValorarMeritosAsync(inscripcionId, meritos);
            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpPost("generar-ordenamiento")]
        public async Task<IActionResult> GenerarOrdenamiento([FromBody] GenerarOrdenamientoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _tribunalService.GenerarOrdenamientoAsync(dto);
            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpGet("llamado/{llamadoId}/ordenamientos")]
        public async Task<IActionResult> ObtenerOrdenamientos(int llamadoId)
        {
            var resultado = await _tribunalService.ObtenerOrdenamientosAsync(llamadoId);
            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpGet("ordenamiento/{ordenamientoId}")]
        public async Task<IActionResult> ObtenerDetalleOrdenamiento(int ordenamientoId)
        {
            var resultado = await _tribunalService.ObtenerDetalleOrdenamientoAsync(ordenamientoId);
            if (!resultado.Success)
            {
                return NotFound(resultado);
            }

            return Ok(resultado);
        }

        [HttpPost("ordenamiento/{ordenamientoId}/publicar")]
        public async Task<IActionResult> PublicarOrdenamiento(int ordenamientoId)
        {
            var resultado = await _tribunalService.PublicarOrdenamientoAsync(ordenamientoId);
            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }   
}
