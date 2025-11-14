using System.Net;
using System.Net.Http.Json;
using PortalDGC.IntegrationTests.Infrastructure;
using PortalDGC.Dtos.Inscripcion;
using Xunit;

namespace PortalDGC.IntegrationTests.Controllers;

/// <summary>
/// Tests de integración para InscripcionController.
/// Valida comportamiento end-to-end incluyendo BD, servicios y controladores.
/// </summary>
[Collection("WebApplicationFactory Collection")]
public class InscripcionControllerTests
{
    private readonly HttpClient _client;

    public InscripcionControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ObtenerInscripcion_DebeRetornarInscripcion_CuandoExiste()
    {
        // Arrange
        var inscripcionId = 1;

        // Act
        var response = await _client.GetAsync($"/api/inscripcion/{inscripcionId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerInscripcion_DebeRetornarNotFound_CuandoNoExiste()
    {
        // Arrange
        var inscripcionIdInexistente = 999;

        // Act
        var response = await _client.GetAsync($"/api/inscripcion/{inscripcionIdInexistente}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerInscripcionesPorPostulante_DebeRetornarLista_CuandoPostulanteExiste()
    {
        // Arrange
        var postulanteId = 1;

        // Act
        var response = await _client.GetAsync($"/api/inscripcion/postulante/{postulanteId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ValidarInscripcionExistente_DebeRetornarOk_CuandoInscripcionExiste()
    {
        // Arrange
        var postulanteId = 1;
        var llamadoId = 1;

        // Act
        var response = await _client.GetAsync($"/api/inscripcion/validar/{postulanteId}/{llamadoId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ValidarInscripcionExistente_DebeRetornarOk_CuandoInscripcionNoExiste()
    {
        // Arrange
        var postulanteId = 1;
        var llamadoIdNoInscripto = 2;

        // Act
        var response = await _client.GetAsync($"/api/inscripcion/validar/{postulanteId}/{llamadoIdNoInscripto}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ValidarRequisitosObligatorios_DebeRetornarResultado_CuandoInscripcionExiste()
    {
        // Arrange
        var inscripcionId = 1;

        // Act
        var response = await _client.GetAsync($"/api/inscripcion/{inscripcionId}/validar-requisitos");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CrearInscripcion_DebeRetornarBadRequest_CuandoLlamadoNoExiste()
    {
        // Arrange
        var postulanteId = 1;
        var nuevaInscripcion = new CrearInscripcionDto
        {
            LlamadoId = 999, // Llamado que no existe
            DepartamentoId = 1
        };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/inscripcion/postulante/{postulanteId}", nuevaInscripcion);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CrearInscripcion_DebeRetornarBadRequest_CuandoInscripcionYaExiste()
    {
        // Arrange
        var postulanteId = 1;
        var inscripcionDuplicada = new CrearInscripcionDto
        {
            LlamadoId = 1, // Este postulante ya está inscrito en este llamado
            DepartamentoId = 1
        };

        // Act
        var response = await _client.PostAsJsonAsync($"/api/inscripcion/postulante/{postulanteId}", inscripcionDuplicada);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CalcularPuntajeTotal_DebeRetornarOk_CuandoInscripcionExiste()
    {
        // Arrange
        var inscripcionId = 1;

        // Act
        var response = await _client.PostAsync($"/api/inscripcion/{inscripcionId}/calcular-puntaje", null);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
