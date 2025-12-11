using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Tribunal;

namespace PortalDGC.WebApi.Controllers
{
    /// <summary>
    /// Controlador del tribunal evaluador (RF-11, RF-12, RF-14, RF-15).
    /// Gestiona inscripciones para evaluar, calificaciones de pruebas, méritos y ordenamientos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TribunalController : ControllerBase
    {
        private readonly ITribunalService _tribunalService;

        public TribunalController(ITribunalService tribunalService)
        {
            _tribunalService = tribunalService;
        }

        /// <summary>
        /// Obtiene las inscripciones de un llamado con su estado de evaluación (RF-11).
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado a analizar.</param>
        /// <returns>IActionResult con ApiResponseDto detallando inscripciones y estado de evaluación.</returns>
        [HttpGet("llamado/{llamadoId}/inscripciones")]
        public async Task<IActionResult> ObtenerInscripcionesParaEvaluar(int llamadoId)
        {
            var resultado = await _tribunalService.ObtenerInscripcionesParaEvaluarAsync(llamadoId);
            if (!resultado.Success)
            {
                return BuildErrorResponse(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Devuelve el detalle completo de evaluaciones, requisitos y méritos de una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción a consultar.</param>
        /// <returns>IActionResult con ApiResponseDto que incluye la información consolidada de la inscripción.</returns>
        [HttpGet("inscripcion/{inscripcionId}/detalle")]
        public async Task<IActionResult> ObtenerDetalleEvaluacion(int inscripcionId)
        {
            var resultado = await _tribunalService.ObtenerDetalleEvaluacionAsync(inscripcionId);
            if (!resultado.Success)
            {
                return BuildErrorResponse(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Obtiene estadísticas agregadas del llamado para seguimiento del tribunal.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado del que se requieren estadísticas.</param>
        /// <returns>IActionResult con ApiResponseDto que resume indicadores del tribunal.</returns>
        [HttpGet("llamado/{llamadoId}/estadisticas")]
        public async Task<IActionResult> ObtenerEstadisticas(int llamadoId)
        {
            var resultado = await _tribunalService.ObtenerEstadisticasAsync(llamadoId);
            if (!resultado.Success)
            {
                return BuildErrorResponse(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Lista las pruebas del llamado con información de evaluaciones registradas.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado a consultar.</param>
        /// <returns>IActionResult con ApiResponseDto que describe las pruebas del llamado.</returns>
        [HttpGet("llamado/{llamadoId}/pruebas")]
        public async Task<IActionResult> ObtenerPruebasDelLlamado(int llamadoId)
        {
            var resultado = await _tribunalService.ObtenerPruebasDelLlamadoAsync(llamadoId);
            if (!resultado.Success)
            {
                return BuildErrorResponse(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Registra la calificación de una prueba rendida por una inscripción (RF-12).
        /// </summary>
        /// <param name="dto">DTO con datos de inscripción, prueba y puntaje obtenido.</param>
        /// <returns>IActionResult con ApiResponseDto representando la evaluación almacenada.</returns>
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
                return BuildErrorResponse(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Valoración individual de un mérito incluyendo verificación de documentación (RF-14).
        /// </summary>
        /// <param name="dto">DTO con información del mérito evaluado.</param>
        /// <returns>IActionResult con ApiResponseDto que refleja el resultado de la valoración.</returns>
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
                return BuildErrorResponse(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Valoración masiva de los méritos asociados a una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción a valorar.</param>
        /// <param name="meritos">Listado de méritos y puntajes a registrar.</param>
        /// <returns>IActionResult con ApiResponseDto que contiene las valoraciones procesadas.</returns>
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
                return BuildErrorResponse(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Genera ordenamientos preliminares o definitivos, aplicando cuotas y desempates (RF-15).
        /// </summary>
        /// <param name="dto">Parámetros necesarios para generar el ordenamiento.</param>
        /// <returns>IActionResult con ApiResponseDto indicando listas y estadísticas generadas.</returns>
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
                return BuildErrorResponse(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Obtiene los ordenamientos generados para el llamado especificado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>IActionResult con ApiResponseDto que lista los ordenamientos disponibles.</returns>
        [HttpGet("llamado/{llamadoId}/ordenamientos")]
        public async Task<IActionResult> ObtenerOrdenamientos(int llamadoId)
        {
            var resultado = await _tribunalService.ObtenerOrdenamientosAsync(llamadoId);
            if (!resultado.Success)
            {
                return BuildErrorResponse(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Devuelve el detalle (posiciones) de un ordenamiento específico.
        /// </summary>
        /// <param name="ordenamientoId">Identificador del ordenamiento.</param>
        /// <returns>IActionResult con ApiResponseDto que incluye las posiciones del ordenamiento.</returns>
        [HttpGet("ordenamiento/{ordenamientoId}")]
        public async Task<IActionResult> ObtenerDetalleOrdenamiento(int ordenamientoId)
        {
            var resultado = await _tribunalService.ObtenerDetalleOrdenamientoAsync(ordenamientoId);
            if (!resultado.Success)
            {
                return BuildErrorResponse(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Publica un ordenamiento para dejarlo disponible externamente.
        /// </summary>
        /// <param name="ordenamientoId">Identificador del ordenamiento a publicar.</param>
        /// <returns>IActionResult con ApiResponseDto que indica el resultado de la publicación.</returns>
        [HttpPost("ordenamiento/{ordenamientoId}/publicar")]
        public async Task<IActionResult> PublicarOrdenamiento(int ordenamientoId)
        {
            var resultado = await _tribunalService.PublicarOrdenamientoAsync(ordenamientoId);
            if (!resultado.Success)
            {
                return BuildErrorResponse(resultado);
            }

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
