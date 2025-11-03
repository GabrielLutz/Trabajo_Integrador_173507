using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Postulante;

namespace PortalDGC.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostulanteController : ControllerBase
    {
        private readonly IPostulanteService _postulanteService;
        public PostulanteController(IPostulanteService postulanteService)
        {
            _postulanteService = postulanteService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPostulante(int id)
        {
            var resultado = await _postulanteService.ObtenerPostulantePorIdAsync(id);

            if (!resultado.Success)
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpPut("{id}/datos-personales")]
        public async Task<IActionResult> CompletarDatosPersonales(int id, [FromBody] PostulanteDatosPersonalesDto datosPersonales)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _postulanteService.CompletarDatosPersonalesAsync(id, datosPersonales);

            if (!resultado.Success)
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpGet("validar-cedula/{cedula}")]
        public async Task<IActionResult> ValidarCedulaDisponible(string cedula)
        {
            var resultado = await _postulanteService.ValidarCedulaDisponibleAsync(cedula);
            return Ok(resultado);
        }

        [HttpGet("validar-email/{email}")]
        public async Task<IActionResult> ValidarEmailDisponible(string email)
        {
            var resultado = await _postulanteService.ValidarEmailDisponibleAsync(email);
            return Ok(resultado);
        }
    }
}
