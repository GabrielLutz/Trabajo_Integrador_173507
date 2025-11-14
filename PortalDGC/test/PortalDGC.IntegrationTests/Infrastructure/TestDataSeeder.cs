using PortalDGC.DataAccess.Data;
using PortalDGC.Domain.Entities;

namespace PortalDGC.IntegrationTests.Infrastructure;

public static class TestDataSeeder
{
    public static void SeedTestData(ApplicationDbContext context)
    {
        // Limpiar datos existentes
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // Seed Departamentos
        var departamentos = new[]
        {
            new Departamento { Id = 1, Nombre = "Montevideo", Codigo = "MVD", Activo = true },
            new Departamento { Id = 2, Nombre = "Canelones", Codigo = "CAN", Activo = true },
            new Departamento { Id = 3, Nombre = "Maldonado", Codigo = "MAL", Activo = true }
        };
        context.Departamentos.AddRange(departamentos);

        // Seed Llamados
        var llamado1 = new Llamado
        {
            Id = 1,
            Titulo = "Llamado Ingeniero Software 2025",
            Descripcion = "Llamado para ingenieros de software",
            Bases = "Bases del llamado de software",
            FechaApertura = DateTime.Now.AddDays(-10),
            FechaCierre = DateTime.Now.AddDays(20),
            CantidadPuestos = 10,
            PorcentajeAfrodescendiente = 8.0m,
            PorcentajeTrans = 1.0m,
            PorcentajeDiscapacidad = 4.0m,
            Estado = "Abierto"
        };

        var llamado2 = new Llamado
        {
            Id = 2,
            Titulo = "Llamado Analista QA 2025",
            Descripcion = "Llamado para analistas QA",
            Bases = "Bases del llamado de QA",
            FechaApertura = DateTime.Now.AddDays(-5),
            FechaCierre = DateTime.Now.AddDays(30),
            CantidadPuestos = 5,
            PorcentajeAfrodescendiente = 8.0m,
            PorcentajeTrans = 1.0m,
            PorcentajeDiscapacidad = 4.0m,
            Estado = "Abierto"
        };

        var llamado3 = new Llamado
        {
            Id = 3,
            Titulo = "Llamado Administrador 2024",
            Descripcion = "Llamado cerrado de 2024",
            Bases = "Bases del llamado cerrado",
            FechaApertura = DateTime.Now.AddDays(-90),
            FechaCierre = DateTime.Now.AddDays(-10),
            CantidadPuestos = 3,
            PorcentajeAfrodescendiente = 8.0m,
            PorcentajeTrans = 1.0m,
            PorcentajeDiscapacidad = 4.0m,
            Estado = "Cerrado"
        };

        context.Llamados.AddRange(llamado1, llamado2, llamado3);

        // Seed LlamadoDepartamentos
        var llamadoDepartamentos = new[]
        {
            new LlamadoDepartamento { Id = 1, LlamadoId = 1, DepartamentoId = 1, CantidadPuestos = 5 },
            new LlamadoDepartamento { Id = 2, LlamadoId = 1, DepartamentoId = 2, CantidadPuestos = 3 },
            new LlamadoDepartamento { Id = 3, LlamadoId = 2, DepartamentoId = 1, CantidadPuestos = 5 }
        };
        context.LlamadoDepartamentos.AddRange(llamadoDepartamentos);

        // Seed Requisitos Excluyentes
        var requisitos = new[]
        {
            new RequisitoExcluyente 
            { 
                Id = 1, 
                LlamadoId = 1, 
                Descripcion = "Título universitario en Ingeniería", 
                Tipo = "Documentación", 
                Obligatorio = true 
            },
            new RequisitoExcluyente 
            { 
                Id = 2, 
                LlamadoId = 1, 
                Descripcion = "Cédula de identidad vigente", 
                Tipo = "Documentación", 
                Obligatorio = true 
            },
            new RequisitoExcluyente 
            { 
                Id = 3, 
                LlamadoId = 2, 
                Descripcion = "Experiencia en testing", 
                Tipo = "Experiencia", 
                Obligatorio = true 
            }
        };
        context.RequisitosExcluyentes.AddRange(requisitos);

        // Seed Items Puntuables
        var itemsPuntuables = new[]
        {
            new ItemPuntuable 
            { 
                Id = 1, 
                LlamadoId = 1, 
                Nombre = "Estudios de posgrado", 
                Descripcion = "Maestrías o doctorados relacionados", 
                PuntajeMaximo = 20.0m, 
                Categoria = "Formación" 
            },
            new ItemPuntuable 
            { 
                Id = 2, 
                LlamadoId = 1, 
                Nombre = "Experiencia laboral", 
                Descripcion = "Años de experiencia en desarrollo", 
                PuntajeMaximo = 30.0m, 
                Categoria = "Experiencia" 
            },
            new ItemPuntuable 
            { 
                Id = 3, 
                LlamadoId = 2, 
                Nombre = "Certificaciones", 
                Descripcion = "Certificaciones en testing", 
                PuntajeMaximo = 15.0m, 
                Categoria = "Formación" 
            }
        };
        context.ItemsPuntuables.AddRange(itemsPuntuables);

        // Seed Apoyos Necesarios
        var apoyosNecesarios = new[]
        {
            new ApoyoNecesario 
            { 
                Id = 1, 
                LlamadoId = 1, 
                Descripcion = "Intérprete de lengua de señas", 
                Tipo = "Accesibilidad" 
            },
            new ApoyoNecesario 
            { 
                Id = 2, 
                LlamadoId = 1, 
                Descripcion = "Computadora adaptada", 
                Tipo = "Tecnología" 
            }
        };
        context.ApoyosNecesarios.AddRange(apoyosNecesarios);

        // Seed Pruebas
        var pruebas = new[]
        {
            new Prueba 
            { 
                Id = 1, 
                LlamadoId = 1, 
                Nombre = "Prueba técnica", 
                Descripcion = "Evaluación de conocimientos técnicos", 
                Tipo = "Técnica", 
                PuntajeMaximo = 50.0m, 
                FechaProgramada = DateTime.Now.AddDays(15),
                Lugar = "Sala 101",
                Estado = "Programada",
                EsObligatoria = true,
                OrdenEjecucion = 1
            },
            new Prueba 
            { 
                Id = 2, 
                LlamadoId = 1, 
                Nombre = "Entrevista personal", 
                Descripcion = "Evaluación de habilidades blandas", 
                Tipo = "Entrevista", 
                PuntajeMaximo = 50.0m, 
                FechaProgramada = DateTime.Now.AddDays(20),
                Lugar = "Sala 202",
                Estado = "Programada",
                EsObligatoria = true,
                OrdenEjecucion = 2
            },
            new Prueba 
            { 
                Id = 3, 
                LlamadoId = 2, 
                Nombre = "Prueba de casos de prueba", 
                Descripcion = "Creación de casos de prueba", 
                Tipo = "Práctica", 
                PuntajeMaximo = 40.0m, 
                FechaProgramada = DateTime.Now.AddDays(25),
                Lugar = "Laboratorio",
                Estado = "Programada",
                EsObligatoria = true,
                OrdenEjecucion = 1
            }
        };
        context.Pruebas.AddRange(pruebas);

        // Seed Postulantes
        var postulantes = new[]
        {
            new Postulante 
            { 
                Id = 1,
                CedulaIdentidad = "12345678",
                Nombre = "Carlos",
                Apellido = "González",
                Email = "carlos.gonzalez@email.com",
                Celular = "099123456",
                FechaNacimiento = new DateTime(1990, 5, 15),
                Domicilio = "Av. Italia 1234",
                Genero = "Masculino",
                FechaCreacion = DateTime.Now.AddDays(-30),
                Activo = true
            },
            new Postulante 
            { 
                Id = 2,
                CedulaIdentidad = "87654321",
                Nombre = "María",
                Apellido = "González",
                Email = "maria.gonzalez@test.com",
                Celular = "099654321",
                FechaNacimiento = new DateTime(1992, 8, 20),
                Domicilio = "Bvar. Artigas 5678",
                Genero = "Femenino",
                FechaCreacion = DateTime.Now.AddDays(-25),
                Activo = true
            },
            new Postulante 
            { 
                Id = 3,
                CedulaIdentidad = "11223344",
                Nombre = "Carlos",
                Apellido = "Rodríguez",
                Email = "carlos.rodriguez@test.com",
                Celular = "099112233",
                FechaNacimiento = new DateTime(1988, 3, 10),
                Domicilio = "18 de Julio 9012",
                Genero = "Masculino",
                FechaCreacion = DateTime.Now.AddDays(-20),
                Activo = true
            }
        };
        context.Postulantes.AddRange(postulantes);

        // Seed Inscripciones
        var inscripciones = new[]
        {
            new Inscripcion 
            { 
                Id = 1,
                PostulanteId = 1,
                LlamadoId = 1,
                DepartamentoId = 1,
                FechaInscripcion = DateTime.Now.AddDays(-8),
                Estado = "Pendiente",
                PuntajeTotal = 0
            },
            new Inscripcion 
            { 
                Id = 2,
                PostulanteId = 2,
                LlamadoId = 1,
                DepartamentoId = 1,
                FechaInscripcion = DateTime.Now.AddDays(-7),
                Estado = "Aprobada",
                PuntajeTotal = 0
            },
            new Inscripcion 
            { 
                Id = 3,
                PostulanteId = 3,
                LlamadoId = 2,
                DepartamentoId = 1,
                FechaInscripcion = DateTime.Now.AddDays(-4),
                Estado = "Pendiente",
                PuntajeTotal = 0
            }
        };
        context.Inscripciones.AddRange(inscripciones);

        // Guardar todos los cambios
        context.SaveChanges();
    }
}
