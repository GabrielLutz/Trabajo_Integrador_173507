using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;

namespace PortalDGC.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LlamadoController : ControllerBase
    {
        private readonly ILlamadoService _llamadoService;

        public LlamadoController(ILlamadoService llamadoService)
        {
            _llamadoService = llamadoService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerLlamado(int id)
        {
            var resultado = await _llamadoService.ObtenerLlamadoPorIdAsync(id);

            if (!resultado.Success)
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpGet("activos")]
        public async Task<IActionResult> ObtenerLlamadosActivos()
        {
            var resultado = await _llamadoService.ObtenerLlamadosActivosAsync();
            return Ok(resultado);
        }

        [HttpGet("inactivos")]
        public async Task<IActionResult> ObtenerLlamadosInactivos()
        {
            var resultado = await _llamadoService.ObtenerLlamadosInactivosAsync();
            return Ok(resultado);
        }

        [HttpGet("{id}/validar-disponible")]
        public async Task<IActionResult> ValidarLlamadoDisponible(int id)
        {
            var resultado = await _llamadoService.ValidarLlamadoDisponibleAsync(id);
            return Ok(resultado);
        }

        [HttpGet("{id}/requisitos")]
        public async Task<IActionResult> ObtenerRequisitosLlamado(int id)
        {
            var resultado = await _llamadoService.ObtenerRequisitosLlamadoAsync(id);
            return Ok(resultado);
        }

        [HttpGet("{id}/items-puntuables")]
        public async Task<IActionResult> ObtenerItemsPuntuablesLlamado(int id)
        {
            var resultado = await _llamadoService.ObtenerItemsPuntuablesLlamadoAsync(id);
            return Ok(resultado);
        }
        
        [HttpGet("{id}/apoyos")]
        public async Task<IActionResult> ObtenerApoyosNecesariosLlamado(int id)
        {
            var resultado = await _llamadoService.ObtenerApoyosNecesariosLlamadoAsync(id);
            return Ok(resultado);
        }
    }
}
