# Trabajo_Integrador_173507

### 173507 - Gabriel Lutz

Backend .NET 8 organizado por capas (`Domain`, `DataAccess`, `BusinessLogic`, `WebApi`) dentro de `PortalDGC/`. Incluye pruebas unitarias e integrales bajo `test/`.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (solo para entornos que no usen la base InMemory)

## Compilar la solución

```cmd
cd "c:\Users\Usuario\Documents\Trabajo Integrador\Trabajo_Integrador_173507\PortalDGC"
dotnet build PortalDGC.sln
```

## Ejecutar la Web API

```cmd
cd "c:\Users\Usuario\Documents\Trabajo Integrador\Trabajo_Integrador_173507\PortalDGC"
dotnet run --project src\PortalDGC.WebApi\PortalDGC.WebApi.csproj
```

Durante el desarrollo, el proyecto usa base InMemory cuando `ASPNETCORE_ENVIRONMENT=Testing`. En otros entornos se toma la cadena `DefaultConnection` de `appsettings*.json`.

## Documentación automática

- `PortalDGC.BusinessLogic` y `PortalDGC.WebApi` generan archivos XML de comentarios en `bin/Debug/net8.0/`.
- `Program.cs` agrega automáticamente esos archivos mediante `IncludeXmlComments`, por lo que Swagger muestra los resúmenes de clases y endpoints.
- Con la API levantada, la documentación interactiva queda disponible en `https://localhost:5001/swagger` (o `http://localhost:5000/swagger`).

## Pruebas

```cmd
cd "c:\Users\Usuario\Documents\Trabajo Integrador\Trabajo_Integrador_173507\PortalDGC"
dotnet test PortalDGC.sln
```



