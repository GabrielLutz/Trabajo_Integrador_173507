using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Inscripcion;

namespace PortalDGC.WebApi.Controllers
{
    /// <summary>
    /// Controlador para la gestión de inscripciones de postulantes (RF-05, RF-07, RF-08).
    /// Permite crear, consultar y validar inscripciones junto con sus requisitos y puntajes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionController : ControllerBase
    {
        private readonly IInscripcionService _inscripcionService;

        public InscripcionController(IInscripcionService inscripcionService)
        {
            _inscripcionService = inscripcionService;
        }

        /// <summary>
        /// Crea una nueva inscripción completa para un postulante dado (RF-05).
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <param name="inscripcionDto">Datos completos de la inscripción: autodefinición, requisitos, méritos, apoyos.</param>
        /// <returns>IActionResult con ApiResponseDto describiendo la inscripción recién creada.</returns>
        [HttpPost("postulante/{postulanteId}")]
        public async Task<IActionResult> CrearInscripcion(int postulanteId, [FromBody] CrearInscripcionDto inscripcionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _inscripcionService.CrearInscripcionAsync(postulanteId, inscripcionDto);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return CreatedAtAction(nameof(ObtenerInscripcion), new { id = resultado.Data!.Id }, resultado);
        }

        /// <summary>
        /// Obtiene el detalle completo de una inscripción (RF-08).
        /// </summary>
        /// <param name="id">Identificador de la inscripción.</param>
        /// <returns>IActionResult con ApiResponseDto que incluye el detalle de la inscripción.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerInscripcion(int id)
        {
            var resultado = await _inscripcionService.ObtenerInscripcionPorIdAsync(id);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Lista las inscripciones realizadas por un postulante (RF-07).
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <returns>IActionResult con ApiResponseDto que resume inscripciones del postulante.</returns>
        [HttpGet("postulante/{postulanteId}")]
        public async Task<IActionResult> ObtenerInscripcionesPorPostulante(int postulanteId)
        {
            var resultado = await _inscripcionService.ObtenerInscripcionesPorPostulanteAsync(postulanteId);
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
        }

        /// <summary>
        /// Valida si ya existe una inscripción del postulante en el llamado indicado.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>IActionResult con ApiResponseDto que indica duplicidad o disponibilidad.</returns>
        [HttpGet("validar/{postulanteId}/{llamadoId}")]
        public async Task<IActionResult> ValidarInscripcionExistente(int postulanteId, int llamadoId)
        {
            var resultado = await _inscripcionService.ValidarInscripcionExistenteAsync(postulanteId, llamadoId);
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
        }

        /// <summary>
        /// Valida el cumplimiento de requisitos obligatorios de una inscripción.
        /// </summary>
        /// <param name="id">Identificador de la inscripción.</param>
        /// <returns>IActionResult con ApiResponseDto que detalla el resultado de la validación.</returns>
        [HttpGet("{id}/validar-requisitos")]
        public async Task<IActionResult> ValidarRequisitosObligatorios(int id)
        {
            var resultado = await _inscripcionService.ValidarRequisitosObligatoriosAsync(id);
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
        }

        /// <summary>
        /// Calcula y actualiza el puntaje total de la inscripción combinando pruebas y méritos.
        /// </summary>
        /// <param name="id">Identificador de la inscripción.</param>
        /// <returns>IActionResult con ApiResponseDto que expone el puntaje calculado.</returns>
        [HttpPost("{id}/calcular-puntaje")]
        public async Task<IActionResult> CalcularPuntajeTotal(int id)
        {
            var resultado = await _inscripcionService.CalcularPuntajeTotalAsync(id);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return Ok(resultado);
        }

        private IActionResult BuildErrorResponse<T>(ApiResponseDto<T> resultado)
        {
            var message = (resultado.Message ?? string.Empty).ToLowerInvariant();

            if (message.Contains("no encontrad"))
            {
                return NotFound(resultado);
            }

            if (message.StartsWith("error"))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, resultado);
            }

            return BadRequest(resultado);
        }
    }
}
