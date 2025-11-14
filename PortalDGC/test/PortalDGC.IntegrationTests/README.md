# PortalDGC.IntegrationTests

Proyecto de tests de integración para la API del Portal DGC.

## Estructura

```
PortalDGC.IntegrationTests/
├── Controllers/          # Tests de endpoints de API
├── Infrastructure/       # Configuración de tests (WebApplicationFactory)
├── Fixtures/            # Datos de prueba reutilizables
└── README.md
```

## Características

- **WebApplicationFactory**: Crea una instancia completa de la API para pruebas
- **Base de datos en memoria**: Usa Entity Framework InMemory para aislar tests
- **Tests end-to-end**: Valida toda la stack desde HTTP hasta base de datos
