using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Postulante;

namespace PortalDGC.WebApi.Controllers
{
    /// <summary>
    /// Controlador para operaciones de postulantes (RF-01, RF-02, RF-20).
    /// Expone endpoints para consultar y actualizar datos personales, además de validar cédula/email.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PostulanteController : ControllerBase
    {
        private readonly IPostulanteService _postulanteService;
        public PostulanteController(IPostulanteService postulanteService)
        {
            _postulanteService = postulanteService;
        }
        
        /// <summary>
        /// Obtiene los datos del postulante y el indicador de completitud de perfil (RF-01).
        /// </summary>
        /// <param name="id">Identificador del postulante.</param>
        /// <returns>IActionResult con ApiResponseDto que describe los datos del postulante.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPostulante(int id)
        {
            var resultado = await _postulanteService.ObtenerPostulantePorIdAsync(id);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Actualiza la información personal del postulante aplicando validaciones de negocio (RF-02).
        /// </summary>
        /// <param name="id">Identificador del postulante a actualizar.</param>
        /// <param name="datosPersonales">DTO con datos obligatorios y opcionales del perfil.</param>
        /// <returns>IActionResult con ApiResponseDto que refleja la actualización realizada.</returns>
        [HttpPut("{id}/datos-personales")]
        public async Task<IActionResult> CompletarDatosPersonales(int id, [FromBody] PostulanteDatosPersonalesDto datosPersonales)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _postulanteService.CompletarDatosPersonalesAsync(id, datosPersonales);

            if (!resultado.Success)
                return BuildErrorResponse(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Valida si una cédula está disponible para registrar (RF-20).
        /// </summary>
        /// <param name="cedula">Número de cédula uruguaya sin puntos ni guion.</param>
        /// <returns>IActionResult con ApiResponseDto que indica disponibilidad.</returns>
        [HttpGet("validar-cedula/{cedula}")]
        public async Task<IActionResult> ValidarCedulaDisponible(string cedula)
        {
            var resultado = await _postulanteService.ValidarCedulaDisponibleAsync(cedula);
            return resultado.Success ? Ok(resultado) : BuildErrorResponse(resultado);
        }

        /// <summary>
        /// Verifica si el email ya está registrado por otro postulante.
        /// </summary>
        /// <param name="email">Dirección de correo electrónico a validar.</param>
        /// <returns>IActionResult con ApiResponseDto que indica disponibilidad.</returns>
        [HttpGet("validar-email/{email}")]
        public async Task<IActionResult> ValidarEmailDisponible(string email)
        {
            var resultado = await _postulanteService.ValidarEmailDisponibleAsync(email);
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
