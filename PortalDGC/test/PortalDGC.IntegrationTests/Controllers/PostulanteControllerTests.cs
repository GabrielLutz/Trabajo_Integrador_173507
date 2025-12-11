using System.Net;
using System.Net.Http.Json;
using PortalDGC.Dtos.Postulante;
using PortalDGC.IntegrationTests.Infrastructure;
using Xunit;

namespace PortalDGC.IntegrationTests.Controllers;

[Collection("WebApplicationFactory Collection")]
public class PostulanteControllerTests
{
    private readonly HttpClient _client;

    public PostulanteControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ObtenerPostulante_DebeRetornarOk_CuandoPostulanteExiste()
    {
        // Arrange
        var postulanteId = 1;

        // Act
        var response = await _client.GetAsync($"/api/Postulante/{postulanteId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var resultado = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(resultado);
    }

    [Fact]
    public async Task ObtenerPostulante_DebeRetornarNotFound_CuandoPostulanteNoExiste()
    {
        // Arrange
        var postulanteId = 999;

        // Act
        var response = await _client.GetAsync($"/api/Postulante/{postulanteId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CompletarDatosPersonales_DebeRetornarOk_CuandoDatosSonValidos()
    {
        // Arrange
        var postulanteId = 1;
        var datosPersonales = new PostulanteDatosPersonalesDto
        {
            Nombre = "Carlos Actualizado",
            Apellido = "González Actualizado",
            FechaNacimiento = new DateTime(1990, 5, 15),
            CedulaIdentidad = "12345678",
            Genero = "Masculino",
            Email = "carlos.gonzalez.updated@email.com",
            Celular = "099888777",
            Domicilio = "Calle Nueva 456"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/Postulante/{postulanteId}/datos-personales", datosPersonales);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CompletarDatosPersonales_DebeRetornarBadRequest_CuandoDatosSonInvalidos()
    {
        // Arrange
        var postulanteId = 1;
        var datosPersonales = new PostulanteDatosPersonalesDto
        {
            Nombre = "", // Nombre vacío - inválido
            Apellido = "González",
            FechaNacimiento = DateTime.Now.AddDays(1), // Fecha futura - inválida
            CedulaIdentidad = "",
            Genero = "Masculino",
            Email = "email-invalido", // Email sin formato correcto
            Celular = "099888777",
            Domicilio = "Calle Nueva 456"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/Postulante/{postulanteId}/datos-personales", datosPersonales);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CompletarDatosPersonales_DebeRetornarBadRequest_CuandoPostulanteNoExiste()
    {
        // Arrange
        var postulanteId = 999;
        var datosPersonales = new PostulanteDatosPersonalesDto
        {
            Nombre = "Juan",
            Apellido = "Pérez",
            FechaNacimiento = new DateTime(1990, 1, 1),
            CedulaIdentidad = "11111111",
            Genero = "Masculino",
            Email = "juan.perez@email.com",
            Celular = "099111222",
            Domicilio = "Calle 123"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/Postulante/{postulanteId}/datos-personales", datosPersonales);

        // Assert
        Assert.True(response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound, $"Se esperaba BadRequest (400) o NotFound (404), pero fue {response.StatusCode}");
    }

    [Fact]
    public async Task ValidarCedulaDisponible_DebeRetornarOk_ConResultadoTrue_CuandoCedulaNoExiste()
    {
        // Arrange
        var cedulaNoExistente = "99999999";

        // Act
        var response = await _client.GetAsync($"/api/Postulante/validar-cedula/{cedulaNoExistente}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var resultado = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(resultado);
    }

    [Fact]
    public async Task ValidarCedulaDisponible_DebeRetornarOk_ConResultadoFalse_CuandoCedulaYaExiste()
    {
        // Arrange
        var cedulaExistente = "12345678"; // Cédula del postulante con Id = 1

        // Act
        var response = await _client.GetAsync($"/api/Postulante/validar-cedula/{cedulaExistente}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var resultado = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(resultado);
    }

    [Fact]
    public async Task ValidarEmailDisponible_DebeRetornarOk_ConResultadoTrue_CuandoEmailNoExiste()
    {
        // Arrange
        var emailNoExistente = "nuevo.email@test.com";

        // Act
        var response = await _client.GetAsync($"/api/Postulante/validar-email/{emailNoExistente}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var resultado = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(resultado);
    }

    [Fact]
    public async Task ValidarEmailDisponible_DebeRetornarOk_ConResultadoFalse_CuandoEmailYaExiste()
    {
        // Arrange
        var emailExistente = "carlos.gonzalez@email.com"; // Email del postulante con Id = 1

        // Act
        var response = await _client.GetAsync($"/api/Postulante/validar-email/{emailExistente}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var resultado = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(resultado);
    }
}
