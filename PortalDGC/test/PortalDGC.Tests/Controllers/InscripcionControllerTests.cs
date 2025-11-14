using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Inscripcion;
using PortalDGC.WebApi.Controllers;
using Xunit;

namespace PortalDGC.Tests.Controllers
{
    public class InscripcionControllerTests
    {
        private readonly Mock<IInscripcionService> _inscripcionServiceMock;
        private readonly InscripcionController _sut;

        public InscripcionControllerTests()
        {
            _inscripcionServiceMock = new Mock<IInscripcionService>();
            _sut = new InscripcionController(_inscripcionServiceMock.Object);
        }

        [Fact]
        public async Task CrearInscripcion_DatosValidos_RetornaCreated()
        {
            var postulanteId = 1;
            var inscripcionDto = new CrearInscripcionDto
            {
                LlamadoId = 1,
                DepartamentoId = 1
            };

            var response = new ApiResponseDto<InscripcionResponseDto>
            {
                Success = true,
                Data = new InscripcionResponseDto { Id = 1 }
            };

            _inscripcionServiceMock
                .Setup(s => s.CrearInscripcionAsync(postulanteId, inscripcionDto))
                .ReturnsAsync(response);

            var resultado = await _sut.CrearInscripcion(postulanteId, inscripcionDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<InscripcionResponseDto>>(createdResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Equal(1, apiResponse.Data?.Id);
        }

        [Fact]
        public async Task CrearInscripcion_DatosInvalidos_RetornaBadRequest()
        {
            var postulanteId = 1;
            var inscripcionDto = new CrearInscripcionDto();

            var response = new ApiResponseDto<InscripcionResponseDto>
            {
                Success = false,
                Message = "Datos inválidos"
            };

            _inscripcionServiceMock
                .Setup(s => s.CrearInscripcionAsync(postulanteId, inscripcionDto))
                .ReturnsAsync(response);

            var resultado = await _sut.CrearInscripcion(postulanteId, inscripcionDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<InscripcionResponseDto>>(badRequestResult.Value);
            Assert.False(apiResponse.Success);
        }

        [Fact]
        public async Task ObtenerInscripcion_InscripcionExiste_RetornaOk()
        {
            var inscripcionId = 1;
            var response = new ApiResponseDto<InscripcionResponseDto>
            {
                Success = true,
                Data = new InscripcionResponseDto { Id = inscripcionId }
            };

            _inscripcionServiceMock
                .Setup(s => s.ObtenerInscripcionPorIdAsync(inscripcionId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerInscripcion(inscripcionId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<InscripcionResponseDto>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task ObtenerInscripcion_InscripcionNoExiste_RetornaNotFound()
        {
            var inscripcionId = 999;
            var response = new ApiResponseDto<InscripcionResponseDto>
            {
                Success = false,
                Message = "Inscripción no encontrada"
            };

            _inscripcionServiceMock
                .Setup(s => s.ObtenerInscripcionPorIdAsync(inscripcionId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerInscripcion(inscripcionId);

            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public async Task ObtenerInscripcionesPorPostulante_RetornaLista()
        {
            var postulanteId = 1;
            var response = new ApiResponseDto<List<InscripcionSimpleResponseDto>>
            {
                Success = true,
                Data = new List<InscripcionSimpleResponseDto>
                {
                    new InscripcionSimpleResponseDto { Id = 1 }
                }
            };

            _inscripcionServiceMock
                .Setup(s => s.ObtenerInscripcionesPorPostulanteAsync(postulanteId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerInscripcionesPorPostulante(postulanteId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<List<InscripcionSimpleResponseDto>>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Single(apiResponse.Data!);
        }

        [Fact]
        public async Task ValidarInscripcionExistente_Existe_RetornaTrue()
        {
            var postulanteId = 1;
            var llamadoId = 1;
            var response = new ApiResponseDto<bool>
            {
                Success = true,
                Data = true
            };

            _inscripcionServiceMock
                .Setup(s => s.ValidarInscripcionExistenteAsync(postulanteId, llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ValidarInscripcionExistente(postulanteId, llamadoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<bool>>(okResult.Value);
            Assert.True(apiResponse.Data);
        }

        [Fact]
        public async Task ValidarRequisitosObligatorios_Cumple_RetornaTrue()
        {
            var inscripcionId = 1;
            var response = new ApiResponseDto<bool>
            {
                Success = true,
                Data = true
            };

            _inscripcionServiceMock
                .Setup(s => s.ValidarRequisitosObligatoriosAsync(inscripcionId))
                .ReturnsAsync(response);

            var resultado = await _sut.ValidarRequisitosObligatorios(inscripcionId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<bool>>(okResult.Value);
            Assert.True(apiResponse.Data);
        }

        [Fact]
        public async Task CalcularPuntajeTotal_Exitoso_RetornaOk()
        {
            var inscripcionId = 1;
            var response = new ApiResponseDto<decimal>
            {
                Success = true,
                Data = 85.5m
            };

            _inscripcionServiceMock
                .Setup(s => s.CalcularPuntajeTotalAsync(inscripcionId))
                .ReturnsAsync(response);

            var resultado = await _sut.CalcularPuntajeTotal(inscripcionId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<decimal>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Equal(85.5m, apiResponse.Data);
        }

        [Fact]
        public async Task CalcularPuntajeTotal_Error_RetornaBadRequest()
        {
            var inscripcionId = 1;
            var response = new ApiResponseDto<decimal>
            {
                Success = false,
                Message = "Error al calcular puntaje"
            };

            _inscripcionServiceMock
                .Setup(s => s.CalcularPuntajeTotalAsync(inscripcionId))
                .ReturnsAsync(response);

            var resultado = await _sut.CalcularPuntajeTotal(inscripcionId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<decimal>>(badRequestResult.Value);
            Assert.False(apiResponse.Success);
        }
    }
}
