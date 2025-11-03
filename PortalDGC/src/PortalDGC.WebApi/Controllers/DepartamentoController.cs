using Microsoft.AspNetCore.Mvc;
using PortalDGC.BusinessLogic.Interfaces;

namespace PortalDGC.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDepartamentosActivos()
        {
            var resultado = await _departamentoService.ObtenerDepartamentosActivosAsync();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDepartamento(int id)
        {
            var resultado = await _departamentoService.ObtenerDepartamentoPorIdAsync(id);

            if (!resultado.Success)
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpGet("llamado/{llamadoId}")]
        public async Task<IActionResult> ObtenerDepartamentosPorLlamado(int llamadoId)
        {
            var resultado = await _departamentoService.ObtenerDepartamentosPorLlamadoAsync(llamadoId);

            if (!resultado.Success)
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpGet("validar/{departamentoId}/llamado/{llamadoId}")]
        public async Task<IActionResult> ValidarDepartamentoEnLlamado(int departamentoId, int llamadoId)
        {
            var resultado = await _departamentoService.ValidarDepartamentoEnLlamadoAsync(departamentoId, llamadoId);
            return Ok(resultado);
        }
    }
}
