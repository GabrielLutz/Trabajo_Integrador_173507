using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.BusinessLogic.Services;
using PortalDGC.DataAccess.Data;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.DataAccess.UnitOfWork;
using WebApplication = Microsoft.AspNetCore.Builder.WebApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Portal DGC API",
        Version = "v1",
        Description = "API para gestión de inscripciones de postulantes"
    });

    // Incluir documentación XML tanto del WebApi como de BusinessLogic
    var xmlDocuments = new[]
    {
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml",
        "PortalDGC.BusinessLogic.xml"
    };

    foreach (var xml in xmlDocuments)
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
        if (File.Exists(xmlPath))
        {
            c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        }
    }
});

builder.Services.AddControllers();

// Database Context In Memory (for testing or development purposes)
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseInMemoryDatabase("PortalDGCDev"));

// Database Context - Usar InMemory en Testing, SqlServer en otros ambientes
if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseInMemoryDatabase("PortalDGCTestDb");
        // Ignorar advertencias de transacciones para InMemory
        options.ConfigureWarnings(warnings => 
            warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    });
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Business Services
builder.Services.AddScoped<IPostulanteService, PostulanteService>();
builder.Services.AddScoped<IInscripcionService, InscripcionService>();
builder.Services.AddScoped<ILlamadoService, LlamadoService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IConstanciaService, ConstanciaService>();
builder.Services.AddScoped<IValidacionService, ValidacionService>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();
builder.Services.AddScoped<ITribunalService, TribunalService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portal DGC API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Solo inicializar la BD en Development/Production, NO en Testing
if (!app.Environment.IsEnvironment("Testing"))
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Error al inicializar BD: {Message}", ex.Message);
        }
    }
}

app.Run();

// Hacer la clase Program pública para que sea accesible en los tests de integración
public partial class Program { }
