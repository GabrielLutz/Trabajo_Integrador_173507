using Microsoft.EntityFrameworkCore;
using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            // ==============
            // Departamentos   
            // ==============
            if (!context.Departamentos.Any())
            {
                var departamentosSeed = new[]
                {
                    new Departamento { Nombre = "Montevideo", Codigo = "MVD", Activo = true },
                    new Departamento { Nombre = "Canelones", Codigo = "CAN", Activo = true },
                    new Departamento { Nombre = "Maldonado", Codigo = "MAL", Activo = true },
                    new Departamento { Nombre = "Colonia", Codigo = "COL", Activo = true },
                    new Departamento { Nombre = "Salto", Codigo = "SAL", Activo = true }
                };
                context.Departamentos.AddRange(departamentosSeed);
                context.SaveChanges();
            }

            var departamentos = context.Departamentos.ToList();
            var depMvd = departamentos.First(d => d.Nombre == "Montevideo");
            var depCan = departamentos.First(d => d.Nombre == "Canelones");
            var depMal = departamentos.First(d => d.Nombre == "Maldonado");


            // ======================
            // Llamado 1: Fiscal III 
            // ======================
            if (!context.Llamados.Any(l => l.Titulo == "Llamado Público DGC - Fiscal III 2025"))
            {
                var llamadoFiscal = new Llamado
                {
                    Titulo = "Llamado Público DGC - Fiscal III 2025",
                    Descripcion = "Administrativo y fiscalizador en salas de juego (polifuncionalidad: caja, fiscalización, atención al público).",
                    Bases = "Etapas: Preselección, Ordenamiento aleatorio, Prueba (60), Méritos (25), Entrevista (15). " +
                            "Mínimos: 30/60 en Prueba y 70/100 total. Plazas: 20 (15 Montevideo, 5 Canelones).",
                    FechaApertura = DateTime.Now.AddDays(-5),
                    FechaCierre = DateTime.Now.AddDays(25),
                    CantidadPuestos = 20,
                    PorcentajeAfrodescendiente = 8.0m,
                    PorcentajeTrans = 1.0m,
                    PorcentajeDiscapacidad = 4.0m,
                    Estado = "Abierto"
                };
                context.Llamados.Add(llamadoFiscal);
                context.SaveChanges();

                var depsFiscal = new[]
                {
                    new LlamadoDepartamento { LlamadoId = llamadoFiscal.Id, DepartamentoId = depMvd.Id, CantidadPuestos = 15 },
                    new LlamadoDepartamento { LlamadoId = llamadoFiscal.Id, DepartamentoId = depCan.Id, CantidadPuestos = 5 }
                };
                context.LlamadoDepartamentos.AddRange(depsFiscal);
                context.SaveChanges();

                var requisitosFiscal = new[]
                {
                    new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "Cédula de identidad vigente.", Tipo = "Documentación", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "Ser ciudadano natural o legal.", Tipo = "Condición Legal", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "18 años cumplidos al cierre del llamado.", Tipo = "Edad", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "Acreditar voto en último acto electoral (si corresponde).", Tipo = "Documentación", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "Control de salud vigente (presentación al designarse).", Tipo = "Documentación", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "Educación Media Superior completa (Bachillerato).", Tipo = "Formación Académica", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "Declaración jurada de incompatibilidades/prohibiciones.", Tipo = "Declaración Jurada", Obligatorio = true }
                };
                context.RequisitosExcluyentes.AddRange(requisitosFiscal);
                context.SaveChanges();

                var itemsFiscal = new[]
                {
                    new ItemPuntuable { LlamadoId = llamadoFiscal.Id, Nombre = "Cursos de informática", Descripcion = "Word/Excel con detalle de carga horaria.", PuntajeMaximo = 8.0m, Categoria = "Formación" },
                    new ItemPuntuable { LlamadoId = llamadoFiscal.Id, Nombre = "Idiomas (inglés/portugués)", Descripcion = "Certificados con nivel alcanzado.", PuntajeMaximo = 5.0m, Categoria = "Formación" },
                    new ItemPuntuable { LlamadoId = llamadoFiscal.Id, Nombre = "Cursos de atención al cliente", Descripcion = "Según carga horaria acreditada.", PuntajeMaximo = 6.0m, Categoria = "Formación" },
                    new ItemPuntuable { LlamadoId = llamadoFiscal.Id, Nombre = "Experiencia relacionada", Descripcion = "Atención al cliente (público/privado) con BPS y cartas.", PuntajeMaximo = 6.0m, Categoria = "Experiencia" }
                };
                context.ItemsPuntuables.AddRange(itemsFiscal);
                context.SaveChanges();

                var apoyosFiscal = new[]
                {
                    new ApoyoNecesario { LlamadoId = llamadoFiscal.Id, Descripcion = "Intérprete de LSU", Tipo = "Discapacidad Auditiva" },
                    new ApoyoNecesario { LlamadoId = llamadoFiscal.Id, Descripcion = "Material en braille / accesible", Tipo = "Discapacidad Visual" },
                    new ApoyoNecesario { LlamadoId = llamadoFiscal.Id, Descripcion = "Accesibilidad física (rampas/baños)", Tipo = "Discapacidad Motriz" },
                    new ApoyoNecesario { LlamadoId = llamadoFiscal.Id, Descripcion = "Tiempo adicional en pruebas", Tipo = "Adecuación de Evaluación" },
                    new ApoyoNecesario { LlamadoId = llamadoFiscal.Id, Descripcion = "Sala individual / baja distracción", Tipo = "Adecuación de Evaluación" }
                };
                context.ApoyosNecesarios.AddRange(apoyosFiscal);
                context.SaveChanges();
            }

            // ============================
            // Llamado2: Especializado III 
            // ============================
            if (!context.Llamados.Any(l => l.Titulo == "Llamado Público DGC - Crupier (Especializado III) 2025"))
            {
                var llamadoCrupier = new Llamado
                {
                    Titulo = "Llamado Público DGC - Crupier (Especializado III) 2025",
                    Descripcion = "Crupier para juegos tradicionales (ruleta, naipes, dados): pagar/cobrar apuestas, servir cartas, asistir a clientes.",
                    Bases = "Etapas: Preselección, Ordenamiento aleatorio, Prueba (50), Méritos (20), Evaluación Psicolaboral (15), Entrevista (15). " +
                            "Mínimos: 30/50 en Prueba y 70/100 total. Plazas: 13 (Montevideo).",
                    FechaApertura = DateTime.Now.AddDays(-5),
                    FechaCierre = DateTime.Now.AddDays(25),
                    CantidadPuestos = 13,
                    PorcentajeAfrodescendiente = 8.0m,
                    PorcentajeTrans = 1.0m,
                    PorcentajeDiscapacidad = 0.0m,
                    Estado = "Abierto"
                };
                context.Llamados.Add(llamadoCrupier);
                context.SaveChanges();

                var depsCrupier = new[]
                {
                    new LlamadoDepartamento { LlamadoId = llamadoCrupier.Id, DepartamentoId = depMvd.Id, CantidadPuestos = 13 }
                };
                context.LlamadoDepartamentos.AddRange(depsCrupier);
                context.SaveChanges();

                var requisitosCrupier = new[]
                {
                    new RequisitoExcluyente { LlamadoId = llamadoCrupier.Id, Descripcion = "Cédula de identidad vigente.", Tipo = "Documentación", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoCrupier.Id, Descripcion = "Ser ciudadano natural o legal.", Tipo = "Condición Legal", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoCrupier.Id, Descripcion = "18 años cumplidos al cierre del llamado.", Tipo = "Edad", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoCrupier.Id, Descripcion = "Acreditar voto en último acto electoral (si corresponde).", Tipo = "Documentación", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoCrupier.Id, Descripcion = "Control de salud vigente (presentación al designarse).", Tipo = "Documentación", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoCrupier.Id, Descripcion = "Ciclo Básico completo (Secundaria o DGETP-UTU).", Tipo = "Formación Académica", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoCrupier.Id, Descripcion = "Declaración jurada de incompatibilidades/prohibiciones.", Tipo = "Declaración Jurada", Obligatorio = true }
                };
                context.RequisitosExcluyentes.AddRange(requisitosCrupier);
                context.SaveChanges();

                var itemsCrupier = new[]
                {
                    new ItemPuntuable { LlamadoId = llamadoCrupier.Id, Nombre = "Cursos de informática", Descripcion = "Word/Excel con detalle de carga horaria.", PuntajeMaximo = 4.0m, Categoria = "Formación" },
                    new ItemPuntuable { LlamadoId = llamadoCrupier.Id, Nombre = "Idiomas (inglés/portugués)", Descripcion = "Certificados con nivel alcanzado.", PuntajeMaximo = 6.0m, Categoria = "Formación" },
                    new ItemPuntuable { LlamadoId = llamadoCrupier.Id, Nombre = "Cursos de atención al cliente", Descripcion = "Según carga horaria acreditada.", PuntajeMaximo = 5.0m, Categoria = "Formación" },
                    new ItemPuntuable { LlamadoId = llamadoCrupier.Id, Nombre = "Experiencia relacionada", Descripcion = "Tareas afines en ámbito público/privado con BPS y cartas.", PuntajeMaximo = 5.0m, Categoria = "Experiencia" }
                };
                context.ItemsPuntuables.AddRange(itemsCrupier);
                context.SaveChanges();

                var apoyosCrupier = new[]
                {
                    new ApoyoNecesario { LlamadoId = llamadoCrupier.Id, Descripcion = "Intérprete de LSU", Tipo = "Discapacidad Auditiva" },
                    new ApoyoNecesario { LlamadoId = llamadoCrupier.Id, Descripcion = "Material en braille / accesible", Tipo = "Discapacidad Visual" },
                    new ApoyoNecesario { LlamadoId = llamadoCrupier.Id, Descripcion = "Accesibilidad física (rampas/baños)", Tipo = "Discapacidad Motriz" },
                    new ApoyoNecesario { LlamadoId = llamadoCrupier.Id, Descripcion = "Tiempo adicional en pruebas", Tipo = "Adecuación de Evaluación" },
                    new ApoyoNecesario { LlamadoId = llamadoCrupier.Id, Descripcion = "Sala individual / baja distracción", Tipo = "Adecuación de Evaluación" }
                };
                context.ApoyosNecesarios.AddRange(apoyosCrupier);
                context.SaveChanges();

                if (!context.Postulantes.Any(p => p.Email == "maria.rodriguez@example.com"))
                {
                    var postulante2 = new Postulante
                    {
                        Nombre = "María",
                        Apellido = "Rodríguez",
                        FechaNacimiento = new DateTime(1994, 11, 2),
                        CedulaIdentidad = "4.567.890-1",
                        Genero = "Femenino",
                        Email = "maria.rodriguez@example.com",
                        Celular = "098765432",
                        Telefono = "24000000",
                        Domicilio = "Av. Libertador 999",
                        FechaCreacion = DateTime.Now,
                        Activo = true
                    };
                    context.Postulantes.Add(postulante2);
                    context.SaveChanges();
                }
            }

            // ==================================================
            // LLAMADO INACTIVO: Administrativo II 2023 (Cerrado)
            // ==================================================
            if (!context.Llamados.Any(l => l.Titulo == "Llamado Público DGC - Administrativo II 2023"))
            {
                var llamadoInactivo = new Llamado
                {
                    Titulo = "Llamado Público DGC - Administrativo II 2023",
                    Descripcion = "Llamado cerrado para cubrir cargos administrativos en distintas dependencias de la DGC. " +
                                  "Incluía tareas de registro, atención al público y apoyo a la gestión administrativa.",
                    Bases = "Etapas: Prueba (50), Méritos (30), Entrevista (20). Requiere Bachillerato completo y conocimientos básicos de informática.",
                    FechaApertura = new DateTime(2023, 4, 10),
                    FechaCierre = new DateTime(2023, 5, 10),
                    CantidadPuestos = 10,
                    PorcentajeAfrodescendiente = 8.0m,
                    PorcentajeTrans = 1.0m,
                    PorcentajeDiscapacidad = 4.0m,
                    Estado = "Cerrado"
                };
                context.Llamados.Add(llamadoInactivo);
                context.SaveChanges();

                var depsInactivo = new[]
                {
                    new LlamadoDepartamento { LlamadoId = llamadoInactivo.Id, DepartamentoId = depMvd.Id, CantidadPuestos = 6 },
                    new LlamadoDepartamento { LlamadoId = llamadoInactivo.Id, DepartamentoId = depMal.Id, CantidadPuestos = 4 }
                };
                context.LlamadoDepartamentos.AddRange(depsInactivo);
                context.SaveChanges();

                var requisitosInactivo = new[]
                {
                    new RequisitoExcluyente { LlamadoId = llamadoInactivo.Id, Descripcion = "Cédula de identidad vigente.", Tipo = "Documentación", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoInactivo.Id, Descripcion = "Bachillerato completo (Educación Media Superior).", Tipo = "Formación Académica", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoInactivo.Id, Descripcion = "Carné de salud vigente.", Tipo = "Documentación", Obligatorio = true },
                    new RequisitoExcluyente { LlamadoId = llamadoInactivo.Id, Descripcion = "Declaración jurada de incompatibilidades.", Tipo = "Declaración Jurada", Obligatorio = true }
                };
                context.RequisitosExcluyentes.AddRange(requisitosInactivo);
                context.SaveChanges();

                var itemsPuntuablesInactivo = new[]
                {
                    new ItemPuntuable { LlamadoId = llamadoInactivo.Id, Nombre = "Cursos de informática", Descripcion = "Word/Excel, carga horaria mínima 30h.", PuntajeMaximo = 10.0m, Categoria = "Formación" },
                    new ItemPuntuable { LlamadoId = llamadoInactivo.Id, Nombre = "Cursos de atención al cliente", Descripcion = "Formación acreditada en atención y comunicación.", PuntajeMaximo = 10.0m, Categoria = "Formación" },
                    new ItemPuntuable { LlamadoId = llamadoInactivo.Id, Nombre = "Experiencia laboral", Descripcion = "Tareas administrativas documentadas (público/privado).", PuntajeMaximo = 10.0m, Categoria = "Experiencia" }
                };
                context.ItemsPuntuables.AddRange(itemsPuntuablesInactivo);
                context.SaveChanges();

                var apoyosInactivo = new[]
                {
                    new ApoyoNecesario { LlamadoId = llamadoInactivo.Id, Descripcion = "Intérprete de LSU", Tipo = "Discapacidad Auditiva" },
                    new ApoyoNecesario { LlamadoId = llamadoInactivo.Id, Descripcion = "Materiales accesibles (braille, digital)", Tipo = "Discapacidad Visual" },
                    new ApoyoNecesario { LlamadoId = llamadoInactivo.Id, Descripcion = "Accesibilidad física al sitio de evaluación", Tipo = "Discapacidad Motriz" }
                };
                context.ApoyosNecesarios.AddRange(apoyosInactivo);
                context.SaveChanges();
            }
        }
    }
}
