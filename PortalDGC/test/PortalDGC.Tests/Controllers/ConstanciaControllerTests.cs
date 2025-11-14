using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Constancia;
using PortalDGC.WebApi.Controllers;
using Xunit;

namespace PortalDGC.Tests.Controllers
{
    public class ConstanciaControllerTests
    {
        private readonly Mock<IConstanciaService> _serviceMock;
        private readonly ConstanciaController _controller;

        public ConstanciaControllerTests()
        {
            _serviceMock = new Mock<IConstanciaService>();
            _controller = new ConstanciaController(_serviceMock.Object);
        }

        [Fact]
        public async Task SubirConstancia_Success_ReturnsCreated()
        {
            var response = new ApiResponseDto<ConstanciaResponseDto>
            {
                Success = true,
                Data = new ConstanciaResponseDto { Id = 9 }
            };

            _serviceMock
                .Setup(s => s.SubirConstanciaAsync(It.IsAny<int>(), It.IsAny<SubirConstanciaDto>()))
                .ReturnsAsync(response);

            var result = await _controller.SubirConstancia(3, new SubirConstanciaDto());

            var created = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(ConstanciaController.ObtenerConstancia), created.ActionName);
            Assert.Same(response, created.Value);
        }

        [Fact]
        public async Task SubirConstancia_Failure_ReturnsBadRequest()
        {
            var response = new ApiResponseDto<ConstanciaResponseDto> { Success = false, Message = "error" };
            _serviceMock
                .Setup(s => s.SubirConstanciaAsync(It.IsAny<int>(), It.IsAny<SubirConstanciaDto>()))
                .ReturnsAsync(response);

            var result = await _controller.SubirConstancia(3, new SubirConstanciaDto());

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Same(response, badRequest.Value);
        }

        [Fact]
        public async Task ObtenerConstanciasPorPostulante_ReturnsOk()
        {
            var response = new ApiResponseDto<List<ConstanciaResponseDto>> { Success = true };
            _serviceMock
                .Setup(s => s.ObtenerConstanciasPorPostulanteAsync(It.IsAny<int>()))
                .ReturnsAsync(response);

            var result = await _controller.ObtenerConstanciasPorPostulante(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Same(response, ok.Value);
        }

        [Fact]
        public async Task ObtenerConstancia_NoExiste_ReturnsNotFound()
        {
            var response = new ApiResponseDto<ConstanciaResponseDto> { Success = false };
            _serviceMock
                .Setup(s => s.ObtenerConstanciaPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(response);

            var result = await _controller.ObtenerConstancia(7);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Same(response, notFound.Value);
        }

        [Fact]
        public async Task ObtenerConstancia_Existe_ReturnsOk()
        {
            var response = new ApiResponseDto<ConstanciaResponseDto> { Success = true };
            _serviceMock
                .Setup(s => s.ObtenerConstanciaPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(response);

            var result = await _controller.ObtenerConstancia(7);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Same(response, ok.Value);
        }

        [Fact]
        public async Task ValidarConstancia_Failure_ReturnsBadRequest()
        {
            var response = new ApiResponseDto<bool> { Success = false };
            _serviceMock
                .Setup(s => s.ValidarConstanciaAsync(It.IsAny<int>()))
                .ReturnsAsync(response);

            var result = await _controller.ValidarConstancia(4);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Same(response, badRequest.Value);
        }

        [Fact]
        public async Task ValidarConstancia_Success_ReturnsOk()
        {
            var response = new ApiResponseDto<bool> { Success = true, Data = true };
            _serviceMock
                .Setup(s => s.ValidarConstanciaAsync(It.IsAny<int>()))
                .ReturnsAsync(response);

            var result = await _controller.ValidarConstancia(4);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Same(response, ok.Value);
        }

        [Fact]
        public async Task DescargarConstancia_NoExiste_ReturnsNotFound()
        {
            var response = new ApiResponseDto<byte[]> { Success = false };
            _serviceMock
                .Setup(s => s.DescargarConstanciaAsync(It.IsAny<int>()))
                .ReturnsAsync(response);

            var result = await _controller.DescargarConstancia(8);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Same(response, notFound.Value);
        }

        [Fact]
        public async Task DescargarConstancia_Success_ReturnsFile()
        {
            var bytes = new byte[] { 1, 2, 3 };
            var response = new ApiResponseDto<byte[]>
            {
                Success = true,
                Data = bytes
            };

            _serviceMock
                .Setup(s => s.DescargarConstanciaAsync(It.IsAny<int>()))
                .ReturnsAsync(response);

            var result = await _controller.DescargarConstancia(8);

            var file = Assert.IsType<FileContentResult>(result);
            Assert.Equal(bytes, file.FileContents);
            Assert.Equal("application/octet-stream", file.ContentType);
        }
    }
}
