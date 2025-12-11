using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;

namespace PortalDGC.WebApi.Controllers
{
    /// <summary>
    /// Endpoints para consultar departamentos disponibles en llamados (RF-03).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        /// <summary>
        /// Obtiene todos los departamentos activos del sistema.
        /// </summary>
        /// <returns>IActionResult con ApiResponseDto que lista departamentos activos.</returns>
        [HttpGet]
        public async Task<IActionResult> ObtenerDepartamentosActivos()
        {
            var resultado = await _departamentoService.ObtenerDepartamentosActivosAsync();
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
        }

        /// <summary>
        /// Recupera un departamento específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del departamento consultado.</param>
        /// <returns>IActionResult con ApiResponseDto que describe el departamento.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDepartamento(int id)
        {
            var resultado = await _departamentoService.ObtenerDepartamentoPorIdAsync(id);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Lista los departamentos habilitados para un llamado determinado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>IActionResult con ApiResponseDto que detalla departamentos asociados.</returns>
        [HttpGet("llamado/{llamadoId}")]
        public async Task<IActionResult> ObtenerDepartamentosPorLlamado(int llamadoId)
        {
            var resultado = await _departamentoService.ObtenerDepartamentosPorLlamadoAsync(llamadoId);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Valida si el departamento forma parte del llamado antes de permitir la inscripción.
        /// </summary>
        /// <param name="departamentoId">Identificador del departamento.</param>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>IActionResult con ApiResponseDto indicando si la relación es válida.</returns>
        [HttpGet("validar/{departamentoId}/llamado/{llamadoId}")]
        public async Task<IActionResult> ValidarDepartamentoEnLlamado(int departamentoId, int llamadoId)
        {
            var resultado = await _departamentoService.ValidarDepartamentoEnLlamadoAsync(departamentoId, llamadoId);
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
