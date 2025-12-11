using System.Net;
using PortalDGC.IntegrationTests.Infrastructure;
using Xunit;

namespace PortalDGC.IntegrationTests.Controllers;

/// <summary>
/// Tests de integración para LlamadoController.
/// Valida comportamiento end-to-end incluyendo BD, servicios y controladores.
/// </summary>
[Collection("WebApplicationFactory Collection")]
public class LlamadoControllerTests
{
    private readonly HttpClient _client;

    public LlamadoControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ObtenerLlamado_DebeRetornarLlamado_CuandoExiste()
    {
        // Arrange
        var llamadoId = 1;

        // Act
        var response = await _client.GetAsync($"/api/llamado/{llamadoId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerLlamado_DebeRetornarNotFound_CuandoNoExiste()
    {
        // Arrange
        var llamadoIdInexistente = 999;

        // Act
        var response = await _client.GetAsync($"/api/llamado/{llamadoIdInexistente}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerLlamadosActivos_DebeRetornarListaConLlamadosAbiertos()
    {
        // Act
        var response = await _client.GetAsync("/api/llamado/activos");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerLlamadosInactivos_DebeRetornarListaConLlamadosCerrados()
    {
        // Act
        var response = await _client.GetAsync("/api/llamado/inactivos");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ValidarLlamadoDisponible_DebeRetornarOk_CuandoLlamadoEstaAbierto()
    {
        // Arrange
        var llamadoIdAbierto = 1;

        // Act
        var response = await _client.GetAsync($"/api/llamado/{llamadoIdAbierto}/validar-disponible");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ValidarLlamadoDisponible_DebeRetornarOk_CuandoLlamadoEstaCerrado()
    {
        // Arrange
        var llamadoIdCerrado = 3;

        // Act
        var response = await _client.GetAsync($"/api/llamado/{llamadoIdCerrado}/validar-disponible");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerRequisitosLlamado_DebeRetornarListaRequisitos_CuandoLlamadoExiste()
    {
        // Arrange
        var llamadoId = 1;

        // Act
        var response = await _client.GetAsync($"/api/llamado/{llamadoId}/requisitos");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerItemsPuntuablesLlamado_DebeRetornarListaItems_CuandoLlamadoExiste()
    {
        // Arrange
        var llamadoId = 1;

        // Act
        var response = await _client.GetAsync($"/api/llamado/{llamadoId}/items-puntuables");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ObtenerApoyosNecesariosLlamado_DebeRetornarListaApoyos_CuandoLlamadoExiste()
    {
        // Arrange
        var llamadoId = 1;

        // Act
        var response = await _client.GetAsync($"/api/llamado/{llamadoId}/apoyos");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
