using Xunit;

namespace PortalDGC.IntegrationTests.Infrastructure;

[CollectionDefinition("WebApplicationFactory Collection")]
public class WebApplicationFactoryCollection : ICollectionFixture<CustomWebApplicationFactory>
{
    // Esta clase no tiene código. Solo sirve como marcador para la colección.
    // Todos los tests que usen [Collection("WebApplicationFactory Collection")]
    // compartirán la misma instancia de CustomWebApplicationFactory.
}
