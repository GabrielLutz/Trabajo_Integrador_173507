using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Constancia;

namespace PortalDGC.WebApi.Controllers
{
    /// <summary>
    /// Endpoints para gestión de constancias/documentos del postulante (RF-06).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ConstanciaController : ControllerBase
    {
        private readonly IConstanciaService _constanciaService;

        public ConstanciaController(IConstanciaService constanciaService)
        {
            _constanciaService = constanciaService;
        }

        /// <summary>
        /// Sube y registra una constancia en PDF o imagen para un postulante.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <param name="constanciaDto">Archivo y metadatos a almacenar.</param>
        /// <returns>IActionResult con ApiResponseDto que describe la constancia creada.</returns>
        [HttpPost("postulante/{postulanteId}")]
        public async Task<IActionResult> SubirConstancia(int postulanteId, [FromBody] SubirConstanciaDto constanciaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _constanciaService.SubirConstanciaAsync(postulanteId, constanciaDto);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return CreatedAtAction(nameof(ObtenerConstancia), new { id = resultado.Data!.Id }, resultado);
        }

        /// <summary>
        /// Obtiene el listado de constancias cargadas por el postulante.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <returns>IActionResult con ApiResponseDto que lista constancias del postulante.</returns>
        [HttpGet("postulante/{postulanteId}")]
        public async Task<IActionResult> ObtenerConstanciasPorPostulante(int postulanteId)
        {
            var resultado = await _constanciaService.ObtenerConstanciasPorPostulanteAsync(postulanteId);
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
        }

        /// <summary>
        /// Recupera los metadatos de una constancia específica.
        /// </summary>
        /// <param name="id">Identificador de la constancia.</param>
        /// <returns>IActionResult con ApiResponseDto que describe la constancia.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerConstancia(int id)
        {
            var resultado = await _constanciaService.ObtenerConstanciaPorIdAsync(id);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Marca una constancia como validada por el equipo administrativo.
        /// </summary>
        /// <param name="id">Identificador de la constancia.</param>
        /// <returns>IActionResult con ApiResponseDto que confirma la validación.</returns>
        [HttpPut("{id}/validar")]
        public async Task<IActionResult> ValidarConstancia(int id)
        {
            var resultado = await _constanciaService.ValidarConstanciaAsync(id);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Descarga el archivo asociado a una constancia existente.
        /// </summary>
        /// <param name="id">Identificador de la constancia.</param>
        /// <returns>FileStreamResult con el contenido binario o IActionResult con error.</returns>
        [HttpGet("{id}/descargar")]
        public async Task<IActionResult> DescargarConstancia(int id)
        {
            var resultado = await _constanciaService.DescargarConstanciaAsync(id);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return File(resultado.Data!, "application/octet-stream");
        }

        private IActionResult BuildErrorResponse<T>(ApiResponseDto<T> resultado)
        {
            var message = (resultado.Message ?? string.Empty).ToLowerInvariant();

            if (message.Contains("no encontrado"))
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
