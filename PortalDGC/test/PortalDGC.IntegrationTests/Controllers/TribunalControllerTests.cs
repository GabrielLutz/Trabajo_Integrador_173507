using System.Net;
using System.Net.Http.Json;
using PortalDGC.Dtos.Tribunal;
using PortalDGC.IntegrationTests.Infrastructure;
using Xunit;

namespace PortalDGC.IntegrationTests.Controllers;

[Collection("WebApplicationFactory Collection")]
public class TribunalControllerTests
{
    private readonly HttpClient _client;

    public TribunalControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ObtenerInscripcionesParaEvaluar_DebeRetornarOk_CuandoLlamadoExiste()
    {
        // Arrange
        var llamadoId = 1;

        // Act
        var response = await _client.GetAsync($"/api/Tribunal/llamado/{llamadoId}/inscripciones");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var resultado = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(resultado);
    }

    [Fact]
    public async Task ObtenerInscripcionesParaEvaluar_DebeRetornarOk_CuandoLlamadoNoExiste()
    {
        // Arrange
        var llamadoId = 999;

        // Act
        var response = await _client.GetAsync($"/api/Tribunal/llamado/{llamadoId}/inscripciones");

        // Assert - El servicio retorna OK con lista vacía
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerDetalleEvaluacion_DebeRetornarOk_CuandoInscripcionExiste()
    {
        // Arrange
        var inscripcionId = 1;

        // Act
        var response = await _client.GetAsync($"/api/Tribunal/inscripcion/{inscripcionId}/detalle");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var resultado = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(resultado);
    }

    [Fact]
    public async Task ObtenerDetalleEvaluacion_DebeRetornarNotFound_CuandoInscripcionNoExiste()
    {
        // Arrange
        var inscripcionId = 999;

        // Act
        var response = await _client.GetAsync($"/api/Tribunal/inscripcion/{inscripcionId}/detalle");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerEstadisticas_DebeRetornarOk_CuandoLlamadoExiste()
    {
        // Arrange
        var llamadoId = 1;

        // Act
        var response = await _client.GetAsync($"/api/Tribunal/llamado/{llamadoId}/estadisticas");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var resultado = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(resultado);
    }

    [Fact]
    public async Task ObtenerEstadisticas_DebeRetornarBadRequest_CuandoLlamadoNoExiste()
    {
        // Arrange
        var llamadoId = 999;

        // Act
        var response = await _client.GetAsync($"/api/Tribunal/llamado/{llamadoId}/estadisticas");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerPruebasDelLlamado_DebeRetornarOk_CuandoLlamadoExiste()
    {
        // Arrange
        var llamadoId = 1;

        // Act
        var response = await _client.GetAsync($"/api/Tribunal/llamado/{llamadoId}/pruebas");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var resultado = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(resultado);
    }

    [Fact]
    public async Task ObtenerPruebasDelLlamado_DebeRetornarOk_CuandoLlamadoNoExiste()
    {
        // Arrange
        var llamadoId = 999;

        // Act
        var response = await _client.GetAsync($"/api/Tribunal/llamado/{llamadoId}/pruebas");

        // Assert - El servicio retorna OK con lista vacía
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CalificarPrueba_DebeRetornarBadRequest_CuandoDatosInvalidos()
    {
        // Arrange - Datos inválidos (puntaje negativo)
        var dto = new CalificarPruebaDto
        {
            InscripcionId = 1,
            PruebaId = 1,
            PuntajeObtenido = -10, // Puntaje inválido
            Observaciones = "Test"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Tribunal/calificar-prueba", dto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerOrdenamientos_DebeRetornarOk_CuandoLlamadoExiste()
    {
        // Arrange
        var llamadoId = 1;

        // Act
        var response = await _client.GetAsync($"/api/Tribunal/llamado/{llamadoId}/ordenamientos");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var resultado = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(resultado);
    }

    [Fact]
    public async Task ObtenerOrdenamientos_DebeRetornarOk_CuandoLlamadoNoExiste()
    {
        // Arrange
        var llamadoId = 999;

        // Act
        var response = await _client.GetAsync($"/api/Tribunal/llamado/{llamadoId}/ordenamientos");

        // Assert - El servicio retorna OK con lista vacía
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerDetalleOrdenamiento_DebeRetornarNotFound_CuandoOrdenamientoNoExiste()
    {
        // Arrange
        var ordenamientoId = 999;

        // Act
        var response = await _client.GetAsync($"/api/Tribunal/ordenamiento/{ordenamientoId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PublicarOrdenamiento_DebeRetornarBadRequest_CuandoOrdenamientoNoExiste()
    {
        // Arrange
        var ordenamientoId = 999;

        // Act
        var response = await _client.PostAsync($"/api/Tribunal/ordenamiento/{ordenamientoId}/publicar", null);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
