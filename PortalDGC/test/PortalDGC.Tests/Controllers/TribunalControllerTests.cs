using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Tribunal;
using PortalDGC.WebApi.Controllers;
using Xunit;

namespace PortalDGC.Tests.Controllers
{
    public class TribunalControllerTests
    {
        private readonly Mock<ITribunalService> _tribunalServiceMock;
        private readonly TribunalController _sut;

        public TribunalControllerTests()
        {
            _tribunalServiceMock = new Mock<ITribunalService>();
            _sut = new TribunalController(_tribunalServiceMock.Object);
        }

        [Fact]
        public async Task ObtenerInscripcionesParaEvaluar_Exitoso_RetornaOk()
        {
            var llamadoId = 1;
            var response = new ApiResponseDto<List<InscripcionParaEvaluarDto>>
            {
                Success = true,
                Data = new List<InscripcionParaEvaluarDto>
                {
                    new InscripcionParaEvaluarDto { InscripcionId = 1 }
                }
            };

            _tribunalServiceMock
                .Setup(s => s.ObtenerInscripcionesParaEvaluarAsync(llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerInscripcionesParaEvaluar(llamadoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<List<InscripcionParaEvaluarDto>>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Single(apiResponse.Data!);
        }

        [Fact]
        public async Task ObtenerInscripcionesParaEvaluar_Error_RetornaBadRequest()
        {
            var llamadoId = 1;
            var response = new ApiResponseDto<List<InscripcionParaEvaluarDto>>
            {
                Success = false,
                Message = "Error al obtener inscripciones"
            };

            _tribunalServiceMock
                .Setup(s => s.ObtenerInscripcionesParaEvaluarAsync(llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerInscripcionesParaEvaluar(llamadoId);

            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact]
        public async Task ObtenerDetalleEvaluacion_Exitoso_RetornaOk()
        {
            var inscripcionId = 1;
            var response = new ApiResponseDto<DetalleEvaluacionDto>
            {
                Success = true,
                Data = new DetalleEvaluacionDto { InscripcionId = inscripcionId }
            };

            _tribunalServiceMock
                .Setup(s => s.ObtenerDetalleEvaluacionAsync(inscripcionId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerDetalleEvaluacion(inscripcionId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<DetalleEvaluacionDto>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task ObtenerDetalleEvaluacion_NoEncontrado_RetornaNotFound()
        {
            var inscripcionId = 999;
            var response = new ApiResponseDto<DetalleEvaluacionDto>
            {
                Success = false,
                Message = "Evaluación no encontrada"
            };

            _tribunalServiceMock
                .Setup(s => s.ObtenerDetalleEvaluacionAsync(inscripcionId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerDetalleEvaluacion(inscripcionId);

            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public async Task ObtenerEstadisticas_Exitoso_RetornaOk()
        {
            var llamadoId = 1;
            var response = new ApiResponseDto<EstadisticasTribunalDto>
            {
                Success = true,
                Data = new EstadisticasTribunalDto { LlamadoId = llamadoId }
            };

            _tribunalServiceMock
                .Setup(s => s.ObtenerEstadisticasAsync(llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerEstadisticas(llamadoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<EstadisticasTribunalDto>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task ObtenerPruebasDelLlamado_Exitoso_RetornaOk()
        {
            var llamadoId = 1;
            var response = new ApiResponseDto<List<PruebaDto>>
            {
                Success = true,
                Data = new List<PruebaDto>
                {
                    new PruebaDto { Id = 1 }
                }
            };

            _tribunalServiceMock
                .Setup(s => s.ObtenerPruebasDelLlamadoAsync(llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerPruebasDelLlamado(llamadoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<List<PruebaDto>>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task CalificarPrueba_DatosValidos_RetornaOk()
        {
            var calificarDto = new CalificarPruebaDto
            {
                InscripcionId = 1,
                PruebaId = 1,
                PuntajeObtenido = 85
            };

            var response = new ApiResponseDto<EvaluacionPruebaDto>
            {
                Success = true,
                Data = new EvaluacionPruebaDto { Id = 1 }
            };

            _tribunalServiceMock
                .Setup(s => s.CalificarPruebaAsync(calificarDto))
                .ReturnsAsync(response);

            var resultado = await _sut.CalificarPrueba(calificarDto);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<EvaluacionPruebaDto>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task CalificarPrueba_Error_RetornaBadRequest()
        {
            var calificarDto = new CalificarPruebaDto();
            var response = new ApiResponseDto<EvaluacionPruebaDto>
            {
                Success = false,
                Message = "Error al calificar"
            };

            _tribunalServiceMock
                .Setup(s => s.CalificarPruebaAsync(calificarDto))
                .ReturnsAsync(response);

            var resultado = await _sut.CalificarPrueba(calificarDto);

            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact]
        public async Task ValorarMerito_DatosValidos_RetornaOk()
        {
            var valorarDto = new ValorarMeritoDto
            {
                MeritoPostulanteId = 1,
                PuntajeAsignado = 10
            };

            var response = new ApiResponseDto<EvaluacionMeritoDto>
            {
                Success = true,
                Data = new EvaluacionMeritoDto { Id = 1 }
            };

            _tribunalServiceMock
                .Setup(s => s.ValorarMeritoAsync(valorarDto))
                .ReturnsAsync(response);

            var resultado = await _sut.ValorarMerito(valorarDto);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<EvaluacionMeritoDto>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task ValorarMeritos_DatosValidos_RetornaOk()
        {
            var inscripcionId = 1;
            var meritos = new List<ValorarMeritoDto>
            {
                new ValorarMeritoDto { MeritoPostulanteId = 1, PuntajeAsignado = 10 }
            };

            var response = new ApiResponseDto<List<EvaluacionMeritoDto>>
            {
                Success = true,
                Data = new List<EvaluacionMeritoDto>
                {
                    new EvaluacionMeritoDto { Id = 1 }
                }
            };

            _tribunalServiceMock
                .Setup(s => s.ValorarMeritosAsync(inscripcionId, meritos))
                .ReturnsAsync(response);

            var resultado = await _sut.ValorarMeritos(inscripcionId, meritos);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<List<EvaluacionMeritoDto>>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task GenerarOrdenamiento_DatosValidos_RetornaOk()
        {
            var generarDto = new GenerarOrdenamientoDto
            {
                LlamadoId = 1
            };

            var response = new ApiResponseDto<ResultadoGeneracionOrdenamientoDto>
            {
                Success = true,
                Data = new ResultadoGeneracionOrdenamientoDto { Success = true }
            };

            _tribunalServiceMock
                .Setup(s => s.GenerarOrdenamientoAsync(generarDto))
                .ReturnsAsync(response);

            var resultado = await _sut.GenerarOrdenamiento(generarDto);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<ResultadoGeneracionOrdenamientoDto>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task ObtenerOrdenamientos_Exitoso_RetornaOk()
        {
            var llamadoId = 1;
            var response = new ApiResponseDto<List<OrdenamientoDto>>
            {
                Success = true,
                Data = new List<OrdenamientoDto>
                {
                    new OrdenamientoDto { Id = 1 }
                }
            };

            _tribunalServiceMock
                .Setup(s => s.ObtenerOrdenamientosAsync(llamadoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerOrdenamientos(llamadoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<List<OrdenamientoDto>>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task ObtenerDetalleOrdenamiento_Exitoso_RetornaOk()
        {
            var ordenamientoId = 1;
            var response = new ApiResponseDto<OrdenamientoDetalleDto>
            {
                Success = true,
                Data = new OrdenamientoDetalleDto { Id = ordenamientoId }
            };

            _tribunalServiceMock
                .Setup(s => s.ObtenerDetalleOrdenamientoAsync(ordenamientoId))
                .ReturnsAsync(response);

            var resultado = await _sut.ObtenerDetalleOrdenamiento(ordenamientoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<OrdenamientoDetalleDto>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }

        [Fact]
        public async Task PublicarOrdenamiento_Exitoso_RetornaOk()
        {
            var ordenamientoId = 1;
            var response = new ApiResponseDto<bool>
            {
                Success = true,
                Data = true
            };

            _tribunalServiceMock
                .Setup(s => s.PublicarOrdenamientoAsync(ordenamientoId))
                .ReturnsAsync(response);

            var resultado = await _sut.PublicarOrdenamiento(ordenamientoId);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var apiResponse = Assert.IsType<ApiResponseDto<bool>>(okResult.Value);
            Assert.True(apiResponse.Success);
        }
    }
}
