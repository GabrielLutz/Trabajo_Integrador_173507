using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;

namespace PortalDGC.WebApi.Controllers
{
    /// <summary>
    /// Controlador de llamados públicos (RF-03 y RF-04).
    /// Permite consultar listados, detalle y componentes del llamado.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LlamadoController : ControllerBase
    {
        private readonly ILlamadoService _llamadoService;

        public LlamadoController(ILlamadoService llamadoService)
        {
            _llamadoService = llamadoService;
        }

        /// <summary>
        /// Obtiene el detalle completo de un llamado (requisitos, ítems puntuables, apoyos).
        /// </summary>
        /// <param name="id">Identificador del llamado.</param>
        /// <returns>IActionResult con ApiResponseDto que contiene el detalle del llamado.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerLlamado(int id)
        {
            var resultado = await _llamadoService.ObtenerLlamadoPorIdAsync(id);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Lista los llamados vigentes disponibles para inscripción (RF-03).
        /// </summary>
        /// <returns>IActionResult con ApiResponseDto que lista llamados activos.</returns>
        [HttpGet("activos")]
        public async Task<IActionResult> ObtenerLlamadosActivos()
        {
            var resultado = await _llamadoService.ObtenerLlamadosActivosAsync();
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
        }

        /// <summary>
        /// Devuelve los llamados finalizados o cerrados para tareas administrativas.
        /// </summary>
        /// <returns>IActionResult con ApiResponseDto que detalla llamados inactivos.</returns>
        [HttpGet("inactivos")]
        public async Task<IActionResult> ObtenerLlamadosInactivos()
        {
            var resultado = await _llamadoService.ObtenerLlamadosInactivosAsync();
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
        }

        /// <summary>
        /// Valida si el llamado está abierto para nuevas inscripciones.
        /// </summary>
        /// <param name="id">Identificador del llamado.</param>
        /// <returns>IActionResult con ApiResponseDto que indica disponibilidad.</returns>
        [HttpGet("{id}/validar-disponible")]
        public async Task<IActionResult> ValidarLlamadoDisponible(int id)
        {
            var resultado = await _llamadoService.ValidarLlamadoDisponibleAsync(id);
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
        }

        /// <summary>
        /// Recupera la lista de requisitos excluyentes del llamado.
        /// </summary>
        /// <param name="id">Identificador del llamado.</param>
        /// <returns>IActionResult con ApiResponseDto que detalla los requisitos.</returns>
        [HttpGet("{id}/requisitos")]
        public async Task<IActionResult> ObtenerRequisitosLlamado(int id)
        {
            var resultado = await _llamadoService.ObtenerRequisitosLlamadoAsync(id);
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
        }

        /// <summary>
        /// Obtiene los ítems puntuables que utilizará el tribunal para evaluar méritos.
        /// </summary>
        /// <param name="id">Identificador del llamado.</param>
        /// <returns>IActionResult con ApiResponseDto que lista los ítems puntuables.</returns>
        [HttpGet("{id}/items-puntuables")]
        public async Task<IActionResult> ObtenerItemsPuntuablesLlamado(int id)
        {
            var resultado = await _llamadoService.ObtenerItemsPuntuablesLlamadoAsync(id);
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
        }
        
        /// <summary>
        /// Lista los apoyos necesarios ofrecidos en el llamado (accesibilidad).
        /// </summary>
        /// <param name="id">Identificador del llamado.</param>
        /// <returns>IActionResult con ApiResponseDto que expone los apoyos disponibles.</returns>
        [HttpGet("{id}/apoyos")]
        public async Task<IActionResult> ObtenerApoyosNecesariosLlamado(int id)
        {
            var resultado = await _llamadoService.ObtenerApoyosNecesariosLlamadoAsync(id);
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
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
