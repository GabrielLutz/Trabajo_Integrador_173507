using Microsoft.EntityFrameworkCore;
using PortalDGC.Domain.Entities;
using System;
using System.Linq;

namespace PortalDGC.DataAccess.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // ======= RESET DURO =======
            const bool HARD_RESET = true;
            if (HARD_RESET)
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
            else
            {
                context.Database.Migrate();
            }

            // =====================
            // Departamentos
            // =====================
            if (!context.Departamentos.Any())
            {
                var departamentos = new[]
                {
                    new Departamento { Nombre = "Montevideo", Codigo = "MVD", Activo = true },
                    new Departamento { Nombre = "Canelones",  Codigo = "CAN", Activo = true },
                    new Departamento { Nombre = "Maldonado",  Codigo = "MAL", Activo = true },
                    new Departamento { Nombre = "Colonia",    Codigo = "COL", Activo = true },
                    new Departamento { Nombre = "Salto",      Codigo = "SAL", Activo = true }
                };
                context.Departamentos.AddRange(departamentos);
                context.SaveChanges();
            }

            var depMvd = context.Departamentos.First(d => d.Nombre == "Montevideo");
            var depCan = context.Departamentos.First(d => d.Nombre == "Canelones");

            // =====================================================
            // LLAMADO 1: FISCAL III (Activo, solo Montevideo)
            // =====================================================
            Llamado? llamadoFiscal = context.Llamados.FirstOrDefault(l => l.Titulo == "Llamado Público DGC - Fiscal III 2025");
            if (llamadoFiscal == null)
            {
                llamadoFiscal = new Llamado
                {
                    Titulo = "Llamado Público DGC - Fiscal III 2025",
                    Descripcion = "Administrativo y fiscalizador en salas de juego (caja, atención al público, fiscalización).",
                    Bases = "Etapas: Prueba (60), Méritos (25), Entrevista (15). Mínimo total 70.",
                    FechaApertura = DateTime.Now.AddDays(-5),
                    FechaCierre = DateTime.Now.AddDays(25),
                    CantidadPuestos = 20,
                    Estado = "Abierto",
                    PorcentajeAfrodescendiente = 8,
                    PorcentajeTrans = 1,
                    PorcentajeDiscapacidad = 4
                };
                context.Llamados.Add(llamadoFiscal);
                context.SaveChanges();
            }

            // Solo Montevideo (20)
            var depsFiscal = context.LlamadoDepartamentos.Where(x => x.LlamadoId == llamadoFiscal.Id).ToList();
            if (depsFiscal.Any())
            {
                context.LlamadoDepartamentos.RemoveRange(depsFiscal);
                context.SaveChanges();
            }
            context.LlamadoDepartamentos.Add(new LlamadoDepartamento
            {
                LlamadoId = llamadoFiscal.Id,
                DepartamentoId = depMvd.Id,
                CantidadPuestos = 20
            });
            context.SaveChanges();

            // Requisitos Excluyentes (asegurar set)
            var reqFiscalExist = context.RequisitosExcluyentes.Where(r => r.LlamadoId == llamadoFiscal.Id).ToList();
            if (reqFiscalExist.Any())
            {
                context.RequisitosExcluyentes.RemoveRange(reqFiscalExist);
                context.SaveChanges();
            }
            context.RequisitosExcluyentes.AddRange(new[]
            {
                new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "Cédula de identidad vigente.", Tipo = "Documentación", Obligatorio = true },
                new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "Ser ciudadano natural o legal.", Tipo = "Condición Legal", Obligatorio = true },
                new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "18 años cumplidos al cierre.", Tipo = "Edad", Obligatorio = true },
                new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "Bachillerato completo.", Tipo = "Formación Académica", Obligatorio = true },
                new RequisitoExcluyente { LlamadoId = llamadoFiscal.Id, Descripcion = "Control de salud vigente.", Tipo = "Documentación", Obligatorio = true }
            });
            context.SaveChanges();

            // Ítems puntuables (25 pts)
            var itemsFiscalOld = context.ItemsPuntuables.Where(i => i.LlamadoId == llamadoFiscal.Id).ToList();
            if (itemsFiscalOld.Any())
            {
                context.ItemsPuntuables.RemoveRange(itemsFiscalOld);
                context.SaveChanges();
            }
            var itemsFiscal = new[]
            {
                new ItemPuntuable { LlamadoId = llamadoFiscal.Id, Nombre = "Cursos de informática",        Descripcion = "Word/Excel.",                          PuntajeMaximo = 8, Categoria = "Formación"   },
                new ItemPuntuable { LlamadoId = llamadoFiscal.Id, Nombre = "Idiomas",                       Descripcion = "Inglés o portugués.",                  PuntajeMaximo = 5, Categoria = "Formación"   },
                new ItemPuntuable { LlamadoId = llamadoFiscal.Id, Nombre = "Cursos atención al cliente",    Descripcion = "Carga horaria acreditada.",            PuntajeMaximo = 6, Categoria = "Formación"   },
                new ItemPuntuable { LlamadoId = llamadoFiscal.Id, Nombre = "Experiencia laboral",           Descripcion = "Atención al público/caja (BPS/cartas).", PuntajeMaximo = 6, Categoria = "Experiencia" }
            };
            context.ItemsPuntuables.AddRange(itemsFiscal);
            context.SaveChanges();

            // Apoyos necesarios
            var apoyosFiscalOld = context.ApoyosNecesarios.Where(a => a.LlamadoId == llamadoFiscal.Id).ToList();
            if (apoyosFiscalOld.Any())
            {
                context.ApoyosNecesarios.RemoveRange(apoyosFiscalOld);
                context.SaveChanges();
            }
            context.ApoyosNecesarios.AddRange(new[]
            {
                new ApoyoNecesario { LlamadoId = llamadoFiscal.Id, Descripcion = "Intérprete de LSU",                Tipo = "Discapacidad Auditiva" },
                new ApoyoNecesario { LlamadoId = llamadoFiscal.Id, Descripcion = "Material en braille / accesible",  Tipo = "Discapacidad Visual"   },
                new ApoyoNecesario { LlamadoId = llamadoFiscal.Id, Descripcion = "Accesibilidad física (rampas/baños)", Tipo = "Discapacidad Motriz" },
                new ApoyoNecesario { LlamadoId = llamadoFiscal.Id, Descripcion = "Tiempo adicional en pruebas",      Tipo = "Adecuación de Evaluación" },
                new ApoyoNecesario { LlamadoId = llamadoFiscal.Id, Descripcion = "Sala individual / baja distracción", Tipo = "Adecuación de Evaluación" }
            });
            context.SaveChanges();

            // Pruebas: Escrita 60 + Entrevista 15 (sin psicolaboral)
            var pruebasFiscal = context.Pruebas.Where(p => p.LlamadoId == llamadoFiscal.Id).ToList();
            if (pruebasFiscal.Any())
            {
                context.Pruebas.RemoveRange(pruebasFiscal);
                context.SaveChanges();
            }
            context.Pruebas.AddRange(new[]
            {
                new Prueba
                {
                    LlamadoId = llamadoFiscal.Id,
                    Tipo = "Escrita",
                    Nombre = "Prueba de Conocimientos",
                    Descripcion = "Normativa de casinos, matemática aplicada, atención al cliente y procedimientos.",
                    PuntajeMaximo = 60,
                    FechaProgramada = DateTime.Now.AddDays(30),
                    Lugar = "Sede Central DGC - Montevideo",
                    Estado = "Programada",
                    EsObligatoria = true,
                    OrdenEjecucion = 1
                },
                new Prueba
                {
                    LlamadoId = llamadoFiscal.Id,
                    Tipo = "Oral",
                    Nombre = "Entrevista Personal con Tribunal",
                    Descripcion = "Entrevista individual.",
                    PuntajeMaximo = 15,
                    FechaProgramada = DateTime.Now.AddDays(50),
                    Lugar = "Sala de Reuniones DGC",
                    Estado = "Programada",
                    EsObligatoria = true,
                    OrdenEjecucion = 2
                }
            });
            context.SaveChanges();

            // =====================================================
            // LLAMADO 2: CRUPIER (Activo, Montevideo + Canelones)
            // =====================================================
            Llamado? llamadoCrupier = context.Llamados.FirstOrDefault(l => l.Titulo == "Llamado Público DGC - Crupier (Especializado III) 2025");
            if (llamadoCrupier == null)
            {
                llamadoCrupier = new Llamado
                {
                    Titulo = "Llamado Público DGC - Crupier (Especializado III) 2025",
                    Descripcion = "Crupier para juegos tradicionales (ruleta, naipes, dados).",
                    Bases = "Etapas: Prueba (50), Méritos (20), Psicolaboral (15), Entrevista (15).",
                    FechaApertura = DateTime.Now.AddDays(-5),
                    FechaCierre = DateTime.Now.AddDays(25),
                    CantidadPuestos = 13,
                    Estado = "Abierto",
                    PorcentajeAfrodescendiente = 8,
                    PorcentajeTrans = 1,
                    PorcentajeDiscapacidad = 0
                };
                context.Llamados.Add(llamadoCrupier);
                context.SaveChanges();
            }

            var depsCrupier = context.LlamadoDepartamentos.Where(x => x.LlamadoId == llamadoCrupier.Id).ToList();
            if (depsCrupier.Any())
            {
                context.LlamadoDepartamentos.RemoveRange(depsCrupier);
                context.SaveChanges();
            }
            context.LlamadoDepartamentos.AddRange(new[]
            {
                new LlamadoDepartamento { LlamadoId = llamadoCrupier.Id, DepartamentoId = depMvd.Id, CantidadPuestos = 9 },
                new LlamadoDepartamento { LlamadoId = llamadoCrupier.Id, DepartamentoId = depCan.Id, CantidadPuestos = 4 }
            });
            context.SaveChanges();

            var reqCrupOld = context.RequisitosExcluyentes.Where(r => r.LlamadoId == llamadoCrupier.Id).ToList();
            if (reqCrupOld.Any())
            {
                context.RequisitosExcluyentes.RemoveRange(reqCrupOld);
                context.SaveChanges();
            }
            context.RequisitosExcluyentes.AddRange(new[]
            {
                new RequisitoExcluyente { LlamadoId = llamadoCrupier.Id, Descripcion = "Cédula vigente.",            Tipo = "Documentación",      Obligatorio = true },
                new RequisitoExcluyente { LlamadoId = llamadoCrupier.Id, Descripcion = "Ciudadanía natural o legal.", Tipo = "Condición Legal",     Obligatorio = true },
                new RequisitoExcluyente { LlamadoId = llamadoCrupier.Id, Descripcion = "Ciclo Básico completo.",      Tipo = "Formación Académica", Obligatorio = true }
            });
            context.SaveChanges();

            var itemsCrupOld = context.ItemsPuntuables.Where(i => i.LlamadoId == llamadoCrupier.Id).ToList();
            if (itemsCrupOld.Any())
            {
                context.ItemsPuntuables.RemoveRange(itemsCrupOld);
                context.SaveChanges();
            }
            context.ItemsPuntuables.AddRange(new[]
            {
                new ItemPuntuable { LlamadoId = llamadoCrupier.Id, Nombre = "Cursos de atención al cliente", Descripcion = "Carga horaria acreditada.", PuntajeMaximo = 5, Categoria = "Formación" },
                new ItemPuntuable { LlamadoId = llamadoCrupier.Id, Nombre = "Idiomas",                      Descripcion = "Certificados inglés/portugués.", PuntajeMaximo = 6, Categoria = "Formación" },
                new ItemPuntuable { LlamadoId = llamadoCrupier.Id, Nombre = "Experiencia en juegos",        Descripcion = "Trabajos afines.",                PuntajeMaximo = 9, Categoria = "Experiencia" }
            });
            context.SaveChanges();

            var apoyosCrupOld = context.ApoyosNecesarios.Where(a => a.LlamadoId == llamadoCrupier.Id).ToList();
            if (apoyosCrupOld.Any())
            {
                context.ApoyosNecesarios.RemoveRange(apoyosCrupOld);
                context.SaveChanges();
            }
            context.ApoyosNecesarios.AddRange(new[]
            {
                new ApoyoNecesario { LlamadoId = llamadoCrupier.Id, Descripcion = "Intérprete de LSU",               Tipo = "Discapacidad Auditiva" },
                new ApoyoNecesario { LlamadoId = llamadoCrupier.Id, Descripcion = "Material en braille / accesible", Tipo = "Discapacidad Visual"   },
                new ApoyoNecesario { LlamadoId = llamadoCrupier.Id, Descripcion = "Accesibilidad física",            Tipo = "Discapacidad Motriz"   },
                new ApoyoNecesario { LlamadoId = llamadoCrupier.Id, Descripcion = "Tiempo adicional en pruebas",     Tipo = "Adecuación de Evaluación" },
                new ApoyoNecesario { LlamadoId = llamadoCrupier.Id, Descripcion = "Sala individual / baja distracción", Tipo = "Adecuación de Evaluación" }
            });
            context.SaveChanges();

            // =====================================================
            // LLAMADOS INACTIVOS
            // =====================================================
            if (!context.Llamados.Any(l => l.Titulo == "Llamado Público DGC - Auxiliar de Limpieza 2024"))
            {
                var limp = new Llamado
                {
                    Titulo = "Llamado Público DGC - Auxiliar de Limpieza 2024",
                    Descripcion = "Tareas de limpieza general en salas de juego.",
                    Bases = "Etapas: Méritos (100).",
                    FechaApertura = DateTime.Now.AddMonths(-8),
                    FechaCierre = DateTime.Now.AddMonths(-6),
                    CantidadPuestos = 10,
                    Estado = "Cerrado"
                };
                context.Llamados.Add(limp);
                context.SaveChanges();
                context.LlamadoDepartamentos.Add(new LlamadoDepartamento { LlamadoId = limp.Id, DepartamentoId = depMvd.Id, CantidadPuestos = 10 });
                context.SaveChanges();

                context.RequisitosExcluyentes.Add(new RequisitoExcluyente
                {
                    LlamadoId = limp.Id,
                    Descripcion = "Primaria completa.",
                    Tipo = "Formación Académica",
                    Obligatorio = true
                });
                context.ItemsPuntuables.Add(new ItemPuntuable
                {
                    LlamadoId = limp.Id,
                    Nombre = "Antigüedad laboral",
                    Descripcion = "Experiencia previa en limpieza.",
                    PuntajeMaximo = 100,
                    Categoria = "Experiencia"
                });
                context.SaveChanges();
            }

            if (!context.Llamados.Any(l => l.Titulo == "Llamado Público DGC - Técnico en Mantenimiento 2024"))
            {
                var tec = new Llamado
                {
                    Titulo = "Llamado Público DGC - Técnico en Mantenimiento 2024",
                    Descripcion = "Electricista y mantenimiento edilicio general.",
                    Bases = "Etapas: Prueba (60), Méritos (40).",
                    FechaApertura = DateTime.Now.AddMonths(-12),
                    FechaCierre = DateTime.Now.AddMonths(-10),
                    CantidadPuestos = 8,
                    Estado = "Finalizado"
                };
                context.Llamados.Add(tec);
                context.SaveChanges();
                context.LlamadoDepartamentos.AddRange(new[]
                {
                    new LlamadoDepartamento { LlamadoId = tec.Id, DepartamentoId = depMvd.Id, CantidadPuestos = 5 },
                    new LlamadoDepartamento { LlamadoId = tec.Id, DepartamentoId = depCan.Id, CantidadPuestos = 3 }
                });
                context.RequisitosExcluyentes.Add(new RequisitoExcluyente
                {
                    LlamadoId = tec.Id,
                    Descripcion = "Título UTU o similar en electricidad/mantenimiento.",
                    Tipo = "Formación Académica",
                    Obligatorio = true
                });
                context.ItemsPuntuables.Add(new ItemPuntuable
                {
                    LlamadoId = tec.Id,
                    Nombre = "Experiencia comprobada",
                    Descripcion = "Años de trabajo en mantenimiento o electricidad.",
                    PuntajeMaximo = 40,
                    Categoria = "Experiencia"
                });
                context.SaveChanges();
            }

            // =====================================================
            // Postulante suelto (María) SIN inscripción
            // =====================================================
            if (!context.Postulantes.Any(p => p.Email == "maria.rodriguez@example.com"))
            {
                context.Postulantes.Add(new Postulante
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
                });
                context.SaveChanges();
            }

            // =====================================================
            // SEMILLA BASE: Ana (inscripta a Fiscal III con méritos y requisitos)
            // =====================================================
            if (!context.Postulantes.Any(p => p.Email == "ana.martinez@example.com"))
            {
                var ana = new Postulante
                {
                    Nombre = "Ana",
                    Apellido = "Martínez",
                    FechaNacimiento = new DateTime(1992, 5, 15),
                    CedulaIdentidad = "3.987.654-2",
                    Genero = "Femenino",
                    Email = "ana.martinez@example.com",
                    Celular = "098123456",
                    Telefono = "24567890",
                    Domicilio = "Av. Italia 2500",
                    FechaCreacion = DateTime.Now,
                    Activo = true
                };
                context.Postulantes.Add(ana);
                context.SaveChanges();

                var insc = new Inscripcion
                {
                    PostulanteId = ana.Id,
                    LlamadoId = llamadoFiscal.Id,
                    DepartamentoId = depMvd.Id,
                    FechaInscripcion = DateTime.Now.AddDays(-10),
                    Estado = "Completada",
                    PuntajeTotal = 0
                };
                context.Inscripciones.Add(insc);
                context.SaveChanges();

                context.AutodefinicionesLey.Add(new AutodefinicionLey
                {
                    InscripcionId = insc.Id,
                    EsAfrodescendiente = true,
                    EsTrans = false,
                    TieneDiscapacidad = false
                });
                context.SaveChanges();

                // Requisitos excluyentes -> Cumple
                var reqs = context.RequisitosExcluyentes.Where(r => r.LlamadoId == llamadoFiscal.Id).ToList();
                foreach (var r in reqs)
                {
                    context.RequisitosPostulante.Add(new RequisitoPostulante
                    {
                        InscripcionId = insc.Id,
                        RequisitoId = r.Id,
                        Cumple = true,
                        Observaciones = "Documentación presentada correctamente"
                    });
                }
                context.SaveChanges();

                // 4 méritos
                var itemsAll = context.ItemsPuntuables.Where(i => i.LlamadoId == llamadoFiscal.Id).ToList();
                foreach (var it in itemsAll)
                {
                    context.MeritosPostulante.Add(new MeritoPostulante
                    {
                        InscripcionId = insc.Id,
                        ItemPuntuableId = it.Id,
                        DocumentoRespaldo = $"certificado_{it.Nombre.Replace(" ", "_").ToLower()}.pdf",
                        PuntajeObtenido = 0,
                        Verificado = false
                    });
                }
                context.SaveChanges();
            }

            // =====================================================
            // 40 INSCRIPTOS (Fiscal III, solo MVD) con 4 méritos y requisitos
            // Distribución objetivo: 9 afro, 4 disca, 5 trans, 22 general
            // Si hay menos, se completan hasta llegar a esos números.
            // =====================================================
            {
                int objetivoAfro = 9, objetivoDisca = 4, objetivoTrans = 5, objetivoGeneral = 22;

                // Contar actuales por universo (en este llamado)
                var inscripcionesFiscal = context.Inscripciones
                    .Where(i => i.LlamadoId == llamadoFiscal.Id)
                    .Select(i => new { i.Id })
                    .ToList();

                int actualesAfro = (from a in context.AutodefinicionesLey
                                    join i in context.Inscripciones on a.InscripcionId equals i.Id
                                    where i.LlamadoId == llamadoFiscal.Id && a.EsAfrodescendiente
                                    select a).Count();

                int actualesDisca = (from a in context.AutodefinicionesLey
                                     join i in context.Inscripciones on a.InscripcionId equals i.Id
                                     where i.LlamadoId == llamadoFiscal.Id && a.TieneDiscapacidad
                                     select a).Count();

                int actualesTrans = (from a in context.AutodefinicionesLey
                                     join i in context.Inscripciones on a.InscripcionId equals i.Id
                                     where i.LlamadoId == llamadoFiscal.Id && a.EsTrans
                                     select a).Count();

                int actualesConAlguno = (from a in context.AutodefinicionesLey
                                         join i in context.Inscripciones on a.InscripcionId equals i.Id
                                         where i.LlamadoId == llamadoFiscal.Id && (a.EsAfrodescendiente || a.TieneDiscapacidad || a.EsTrans)
                                         select a).Count();

                int actualesTotal = context.Inscripciones.Count(i => i.LlamadoId == llamadoFiscal.Id);
                int actualesGeneral = actualesTotal - actualesConAlguno;

                int faltanAfro = Math.Max(0, objetivoAfro - actualesAfro);
                int faltanDisca = Math.Max(0, objetivoDisca - actualesDisca);
                int faltanTrans = Math.Max(0, objetivoTrans - actualesTrans);
                int faltanGeneral = Math.Max(0, objetivoGeneral - actualesGeneral);

                // Generadores
                string[] nombres = { "Agustin", "Bruno", "Carolina", "Daniela", "Emiliano", "Fernanda", "Gabriel", "Helena", "Ignacio", "Julieta", "Kevin", "Lucia", "Matias", "Nadia", "Oscar", "Paula", "Rafael", "Sofia", "Thiago", "Valentina" };
                string[] apellidos = { "Perez", "Rodriguez", "Gomez", "Fernandez", "Lopez", "Gonzalez", "Martinez", "Sosa", "Silva", "Romero", "Alvarez", "Torres", "Suarez", "Ramos", "Castro", "Mendez", "Vazquez", "Acosta", "Pereyra", "Diaz" };
                int nx = nombres.Length, ax = apellidos.Length;

                DateTime hoy = DateTime.Now;
                Random rng = new Random(20251105);

                var itemsAllFiscal = context.ItemsPuntuables.Where(i => i.LlamadoId == llamadoFiscal.Id).ToList();

                void CrearMeritosYRequisitos(int inscripcionId)
                {
                    // 4 méritos
                    foreach (var it in itemsAllFiscal)
                    {
                        if (!context.MeritosPostulante.Any(m => m.InscripcionId == inscripcionId && m.ItemPuntuableId == it.Id))
                        {
                            context.MeritosPostulante.Add(new MeritoPostulante
                            {
                                InscripcionId = inscripcionId,
                                ItemPuntuableId = it.Id,
                                DocumentoRespaldo = $"certificado_{it.Nombre.Replace(" ", "_").ToLower()}.pdf",
                                PuntajeObtenido = 0,
                                Verificado = false
                            });
                        }
                    }

                    // Requisitos excluyentes -> Cumple
                    var reqs = context.RequisitosExcluyentes.Where(r => r.LlamadoId == llamadoFiscal.Id).ToList();
                    foreach (var r in reqs)
                    {
                        if (!context.RequisitosPostulante.Any(rp => rp.InscripcionId == inscripcionId && rp.RequisitoId == r.Id))
                        {
                            context.RequisitosPostulante.Add(new RequisitoPostulante
                            {
                                InscripcionId = inscripcionId,
                                RequisitoId = r.Id,
                                Cumple = true
                            });
                        }
                    }
                    context.SaveChanges();
                }

                int sec = 1;
                void CrearInscripto(string universo)
                {
                    string email = $"fiscal3_{DateTime.Now.Ticks}_{sec:000}@example.com"; // evitar colisiones si se corre más de una vez
                    string nombre = nombres[(sec * 7) % nx];
                    string apellido = apellidos[(sec * 11) % ax];

                    var postulante = new Postulante
                    {
                        Nombre = nombre,
                        Apellido = apellido,
                        FechaNacimiento = new DateTime(rng.Next(1980, 2004), rng.Next(1, 13), rng.Next(1, 28)),
                        CedulaIdentidad = $"{rng.Next(1, 7)}.{rng.Next(100, 999)}.{rng.Next(100, 999)}-{rng.Next(0, 9)}",
                        Genero = (rng.NextDouble() < 0.5) ? "Femenino" : "Masculino",
                        Email = email,
                        Celular = $"09{rng.Next(1, 9)}{rng.Next(100000, 999999)}",
                        Domicilio = "Montevideo",
                        FechaCreacion = hoy.AddDays(-rng.Next(1, 20)),
                        Activo = true
                    };
                    context.Postulantes.Add(postulante);
                    context.SaveChanges();

                    var insc = new Inscripcion
                    {
                        PostulanteId = postulante.Id,
                        LlamadoId = llamadoFiscal.Id,
                        DepartamentoId = depMvd.Id,
                        FechaInscripcion = hoy.AddDays(-rng.Next(1, 15)),
                        Estado = "Completada",
                        PuntajeTotal = 0
                    };
                    context.Inscripciones.Add(insc);
                    context.SaveChanges();

                    context.AutodefinicionesLey.Add(new AutodefinicionLey
                    {
                        InscripcionId = insc.Id,
                        EsAfrodescendiente = (universo == "afro"),
                        TieneDiscapacidad = (universo == "disca"),
                        EsTrans = (universo == "trans")
                    });
                    context.SaveChanges();

                    CrearMeritosYRequisitos(insc.Id);
                    sec++;
                }

                // Completar faltantes para llegar exactamente a 9/4/5/22
                for (int k = 0; k < faltanAfro; k++) CrearInscripto("afro");
                for (int k = 0; k < faltanDisca; k++) CrearInscripto("disca");
                for (int k = 0; k < faltanTrans; k++) CrearInscripto("trans");
                for (int k = 0; k < faltanGeneral; k++) CrearInscripto("general");
            }
        }
    }
}
