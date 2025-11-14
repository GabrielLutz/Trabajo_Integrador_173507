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
    public class LlamadoControllerTests
    {
        private readonly Mock<ILlamadoService> _llamadoServiceMock;
        private readonly LlamadoController _sut;

        public LlamadoControllerTests()
        {
            _llamadoServiceMock = new Mock<ILlamadoService>();
            _sut = new LlamadoController(_llamadoServiceMock.Object);
        }

        [Fact]
        public async Task ObtenerLlamado_LlamadoExiste_RetornaOk()
        {
            var llamadoId = 1;
            var response = new ApiResponseDto<LlamadoDetalleDto>
            {
                Success = true,
                Data = new LlamadoDetalleDto { Id = llamadoId }
            };

            _llamadoServiceMock
                .Setup(s => s.ObtenerLlamadoPorIdAsync(llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerLlamado(llamadoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<LlamadoDetalleDto>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task ObtenerLlamado_LlamadoNoExiste_RetornaNotFound()
        {
            var llamadoId = 999;
            var response = new ApiResponseDto<LlamadoDetalleDto>
            {
                Success = false,
                Message = "Llamado no encontrado"
            };

            _llamadoServiceMock
                .Setup(s => s.ObtenerLlamadoPorIdAsync(llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerLlamado(llamadoId);

            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public async Task ObtenerLlamadosActivos_RetornaLista()
        {
            var response = new ApiResponseDto<List<LlamadoSimpleDto>>
            {
                Success = true,
                Data = new List<LlamadoSimpleDto>
                {
                    new LlamadoSimpleDto { Id = 1 },
                    new LlamadoSimpleDto { Id = 2 }
                }
            };

            _llamadoServiceMock
                .Setup(s => s.ObtenerLlamadosActivosAsync())
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerLlamadosActivos();

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<List<LlamadoSimpleDto>>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Equal(2, apiResponse.Data!.Count);
        }

        [Fact]
        public async Task ObtenerLlamadosInactivos_RetornaLista()
        {
            var response = new ApiResponseDto<List<LlamadoSimpleDto>>
            {
                Success = true,
                Data = new List<LlamadoSimpleDto>
                {
                    new LlamadoSimpleDto { Id = 3 }
                }
            };

            _llamadoServiceMock
                .Setup(s => s.ObtenerLlamadosInactivosAsync())
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerLlamadosInactivos();

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<List<LlamadoSimpleDto>>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Single(apiResponse.Data!);
        }

        [Fact]
        public async Task ValidarLlamadoDisponible_Disponible_RetornaTrue()
        {
            var llamadoId = 1;
            var response = new ApiResponseDto<bool>
            {
                Success = true,
                Data = true
            };

            _llamadoServiceMock
                .Setup(s => s.ValidarLlamadoDisponibleAsync(llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ValidarLlamadoDisponible(llamadoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<bool>>(okResult.Value);
            Assert.True(apiResponse.Data);
        }

        [Fact]
        public async Task ObtenerRequisitosLlamado_RetornaLista()
        {
            var llamadoId = 1;
            var response = new ApiResponseDto<List<RequisitoExcluyenteDto>>
            {
                Success = true,
                Data = new List<RequisitoExcluyenteDto>
                {
                    new RequisitoExcluyenteDto { Id = 1 }
                }
            };

            _llamadoServiceMock
                .Setup(s => s.ObtenerRequisitosLlamadoAsync(llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerRequisitosLlamado(llamadoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<List<RequisitoExcluyenteDto>>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Single(apiResponse.Data!);
        }

        [Fact]
        public async Task ObtenerItemsPuntuablesLlamado_RetornaLista()
        {
            var llamadoId = 1;
            var response = new ApiResponseDto<List<ItemPuntuableDto>>
            {
                Success = true,
                Data = new List<ItemPuntuableDto>
                {
                    new ItemPuntuableDto { Id = 1 },
                    new ItemPuntuableDto { Id = 2 }
                }
            };

            _llamadoServiceMock
                .Setup(s => s.ObtenerItemsPuntuablesLlamadoAsync(llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerItemsPuntuablesLlamado(llamadoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<List<ItemPuntuableDto>>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Equal(2, apiResponse.Data!.Count);
        }

        [Fact]
        public async Task ObtenerApoyosNecesariosLlamado_RetornaLista()
        {
            var llamadoId = 1;
            var response = new ApiResponseDto<List<ApoyoNecesarioDto>>
            {
                Success = true,
                Data = new List<ApoyoNecesarioDto>
                {
                    new ApoyoNecesarioDto { Id = 1 }
                }
            };

            _llamadoServiceMock
                .Setup(s => s.ObtenerApoyosNecesariosLlamadoAsync(llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerApoyosNecesariosLlamado(llamadoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<List<ApoyoNecesarioDto>>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Single(apiResponse.Data!);
        }
    }
}
