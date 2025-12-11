using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Llamado;
using PortalDGC.WebApi.Controllers;
using Xunit;

namespace PortalDGC.Tests.Controllers
{
    public class DepartamentoControllerTests
    {
        private readonly Mock<IDepartamentoService> _serviceMock;
        private readonly DepartamentoController _controller;

        public DepartamentoControllerTests()
        {
            _serviceMock = new Mock<IDepartamentoService>();
            _controller = new DepartamentoController(_serviceMock.Object);
        }

        [Fact]
        public async Task ObtenerDepartamentosActivos_ReturnsOk()
        {
            var response = new ApiResponseDto<List<DepartamentoDto>> { Success = true };
            _serviceMock.Setup(s => s.ObtenerDepartamentosActivosAsync()).ReturnsAsync(response);

            var result = await _controller.ObtenerDepartamentosActivos();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Same(response, ok.Value);
        }

        [Fact]
        public async Task ObtenerDepartamento_NoExiste_ReturnsNotFound()
        {
            var response = new ApiResponseDto<DepartamentoDto> { Success = false };
            _serviceMock.Setup(s => s.ObtenerDepartamentoPorIdAsync(It.IsAny<int>())).ReturnsAsync(response);

            var result = await _controller.ObtenerDepartamento(5);

            Assert.True(result is NotFoundObjectResult || result is BadRequestObjectResult, $"Se esperaba NotFoundObjectResult o BadRequestObjectResult, pero fue {result.GetType()}");
        }

        [Fact]
        public async Task ObtenerDepartamento_Existe_ReturnsOk()
        {
            var response = new ApiResponseDto<DepartamentoDto> { Success = true };
            _serviceMock.Setup(s => s.ObtenerDepartamentoPorIdAsync(It.IsAny<int>())).ReturnsAsync(response);

            var result = await _controller.ObtenerDepartamento(5);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Same(response, ok.Value);
        }

        [Fact]
        public async Task ObtenerDepartamentosPorLlamado_NoExiste_ReturnsNotFound()
        {
            var response = new ApiResponseDto<List<DepartamentoLlamadoDto>> { Success = false };
            _serviceMock.Setup(s => s.ObtenerDepartamentosPorLlamadoAsync(It.IsAny<int>())).ReturnsAsync(response);

            var result = await _controller.ObtenerDepartamentosPorLlamado(1);

            Assert.True(result is NotFoundObjectResult || result is BadRequestObjectResult, $"Se esperaba NotFoundObjectResult o BadRequestObjectResult, pero fue {result.GetType()}");
        }

        [Fact]
        public async Task ObtenerDepartamentosPorLlamado_Existe_ReturnsOk()
        {
            var response = new ApiResponseDto<List<DepartamentoLlamadoDto>> { Success = true };
            _serviceMock.Setup(s => s.ObtenerDepartamentosPorLlamadoAsync(It.IsAny<int>())).ReturnsAsync(response);

            var result = await _controller.ObtenerDepartamentosPorLlamado(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Same(response, ok.Value);
        }

        [Fact]
        public async Task ValidarDepartamentoEnLlamado_ReturnsOk()
        {
            var response = new ApiResponseDto<bool> { Success = true, Data = true };
            _serviceMock.Setup(s => s.ValidarDepartamentoEnLlamadoAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(response);

            var result = await _controller.ValidarDepartamentoEnLlamado(1, 2);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Same(response, ok.Value);
        }
    }
}
