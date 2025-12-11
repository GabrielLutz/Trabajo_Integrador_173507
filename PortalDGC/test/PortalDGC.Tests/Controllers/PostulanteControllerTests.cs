using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Postulante;
using PortalDGC.WebApi.Controllers;
using Xunit;

namespace PortalDGC.Tests.Controllers
{
    public class PostulanteControllerTests
    {
        private readonly Mock<IPostulanteService> _postulanteServiceMock;
        private readonly PostulanteController _sut;

        public PostulanteControllerTests()
        {
            _postulanteServiceMock = new Mock<IPostulanteService>();
            _sut = new PostulanteController(_postulanteServiceMock.Object);
        }

        [Fact]
        public async Task ObtenerPostulante_PostulanteExiste_RetornaOk()
        {
            var postulanteId = 1;
            var response = new ApiResponseDto<PostulanteResponseDto>
            {
                Success = true,
                Data = new PostulanteResponseDto { Id = postulanteId }
            };

            _postulanteServiceMock
                .Setup(s => s.ObtenerPostulantePorIdAsync(postulanteId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerPostulante(postulanteId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<PostulanteResponseDto>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task ObtenerPostulante_PostulanteNoExiste_RetornaNotFound()
        {
            var postulanteId = 999;
            var response = new ApiResponseDto<PostulanteResponseDto>
            {
                Success = false,
                Message = "Postulante no encontrado"
            };

            _postulanteServiceMock
                .Setup(s => s.ObtenerPostulantePorIdAsync(postulanteId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerPostulante(postulanteId);

            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public async Task CompletarDatosPersonales_DatosValidos_RetornaOk()
        {
            var postulanteId = 1;
            var datosPersonales = new PostulanteDatosPersonalesDto
            {
                Nombre = "Juan",
                Apellido = "Pérez"
            };

            var response = new ApiResponseDto<PostulanteResponseDto>
            {
                Success = true,
                Data = new PostulanteResponseDto { Id = postulanteId }
            };

            _postulanteServiceMock
                .Setup(s => s.CompletarDatosPersonalesAsync(postulanteId, datosPersonales))
                .ReturnsAsync(response);

            var resultado = await _sut.CompletarDatosPersonales(postulanteId, datosPersonales);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<PostulanteResponseDto>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task CompletarDatosPersonales_DatosInvalidos_RetornaBadRequest()
        {
            var postulanteId = 1;
            var datosPersonales = new PostulanteDatosPersonalesDto
            {
                Nombre = string.Empty,
                Apellido = string.Empty
            };

            var response = new ApiResponseDto<PostulanteResponseDto>
            {
                Success = false,
                Message = "Datos inválidos",
                Errors = new List<string> { "El nombre es requerido" }
            };

            _postulanteServiceMock
                .Setup(s => s.CompletarDatosPersonalesAsync(postulanteId, datosPersonales))
                .ReturnsAsync(response);

            var resultado = await _sut.CompletarDatosPersonales(postulanteId, datosPersonales);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<PostulanteResponseDto>>(badRequestResult.Value);
            Assert.False(apiResponse.Success);
        }

        [Fact]
        public async Task ValidarCedulaDisponible_CedulaDisponible_RetornaTrue()
        {
            var cedula = "12345678";
            var response = new ApiResponseDto<bool>
            {
                Success = true,
                Data = true,
                Message = "Cédula disponible"
            };

            _postulanteServiceMock
                .Setup(s => s.ValidarCedulaDisponibleAsync(cedula))
                .ReturnsAsync(response);

            var resultado = await _sut.ValidarCedulaDisponible(cedula);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<bool>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.True(apiResponse.Data);
        }

        [Fact]
        public async Task ValidarCedulaDisponible_CedulaNoDisponible_RetornaFalse()
        {
            var cedula = "87654321";
            var response = new ApiResponseDto<bool>
            {
                Success = true,
                Data = false,
                Message = "Cédula ya registrada"
            };

            _postulanteServiceMock
                .Setup(s => s.ValidarCedulaDisponibleAsync(cedula))
                .ReturnsAsync(response);

            var resultado = await _sut.ValidarCedulaDisponible(cedula);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<bool>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.False(apiResponse.Data);
        }

        [Fact]
        public async Task ValidarEmailDisponible_EmailDisponible_RetornaTrue()
        {
            var email = "nuevo@example.com";
            var response = new ApiResponseDto<bool>
            {
                Success = true,
                Data = true,
                Message = "Email disponible"
            };

            _postulanteServiceMock
                .Setup(s => s.ValidarEmailDisponibleAsync(email))
                .ReturnsAsync(response);

            var resultado = await _sut.ValidarEmailDisponible(email);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<bool>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.True(apiResponse.Data);
        }

        [Fact]
        public async Task ValidarEmailDisponible_EmailNoDisponible_RetornaFalse()
        {
            var email = "existente@example.com";
            var response = new ApiResponseDto<bool>
            {
                Success = true,
                Data = false,
                Message = "Email ya registrado"
            };

            _postulanteServiceMock
                .Setup(s => s.ValidarEmailDisponibleAsync(email))
                .ReturnsAsync(response);

            var resultado = await _sut.ValidarEmailDisponible(email);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<bool>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.False(apiResponse.Data);
        }
    }
}
