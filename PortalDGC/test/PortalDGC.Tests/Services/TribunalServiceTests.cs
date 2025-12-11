using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using PortalDGC.BusinessLogic.Services;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Domain.Entities;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Tribunal;
using Xunit;

namespace PortalDGC.Tests.Services
{
    public class TribunalServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IInscripcionRepository> _inscripcionesMock;
        private readonly Mock<IPruebaRepository> _pruebasMock;
        private readonly Mock<IEvaluacionPruebaRepository> _evaluacionesPruebasMock;
        private readonly Mock<IEvaluacionMeritoRepository> _evaluacionesMeritosMock;
        private readonly Mock<IMeritoPostulanteRepository> _meritosMock;
        private readonly Mock<IItemPuntuableRepository> _itemsMock;
        private readonly Mock<IOrdenamientoRepository> _ordenamientosMock;
        private readonly Mock<IPosicionOrdenamientoRepository> _posicionesMock;
        private readonly Mock<ILlamadoRepository> _llamadosMock;
        private readonly TribunalService _sut;

        public TribunalServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _inscripcionesMock = new Mock<IInscripcionRepository>();
            _pruebasMock = new Mock<IPruebaRepository>();
            _evaluacionesPruebasMock = new Mock<IEvaluacionPruebaRepository>();
            _evaluacionesMeritosMock = new Mock<IEvaluacionMeritoRepository>();
            _meritosMock = new Mock<IMeritoPostulanteRepository>();
            _itemsMock = new Mock<IItemPuntuableRepository>();
            _ordenamientosMock = new Mock<IOrdenamientoRepository>();
            _posicionesMock = new Mock<IPosicionOrdenamientoRepository>();
            _llamadosMock = new Mock<ILlamadoRepository>();

            _unitOfWorkMock.SetupGet(u => u.Inscripciones).Returns(_inscripcionesMock.Object);
            _unitOfWorkMock.SetupGet(u => u.Pruebas).Returns(_pruebasMock.Object);
            _unitOfWorkMock.SetupGet(u => u.EvaluacionesPruebas).Returns(_evaluacionesPruebasMock.Object);
            _unitOfWorkMock.SetupGet(u => u.EvaluacionesMeritos).Returns(_evaluacionesMeritosMock.Object);
            _unitOfWorkMock.SetupGet(u => u.MeritosPostulante).Returns(_meritosMock.Object);
            _unitOfWorkMock.SetupGet(u => u.ItemsPuntuables).Returns(_itemsMock.Object);
            _unitOfWorkMock.SetupGet(u => u.Ordenamientos).Returns(_ordenamientosMock.Object);
            _unitOfWorkMock.SetupGet(u => u.PosicionesOrdenamiento).Returns(_posicionesMock.Object);
            _unitOfWorkMock.SetupGet(u => u.Llamados).Returns(_llamadosMock.Object);

            _unitOfWorkMock.Setup(u => u.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CommitTransactionAsync()).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.RollbackTransactionAsync()).Returns(Task.CompletedTask);

            _sut = new TribunalService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task ObtenerInscripcionesParaEvaluarAsync_MapeaPuntajesYTotales()
        {
            var llamadoId = 1;
            var inscripcion = CrearInscripcion();
            _inscripcionesMock
                .Setup(r => r.GetByLlamadoIdAsync(llamadoId))
                .ReturnsAsync(new List<Inscripcion> { inscripcion });

            _pruebasMock
                .Setup(r => r.GetByLlamadoIdAsync(llamadoId))
                .ReturnsAsync(new List<Prueba> { new Prueba { Id = 10 } });

            _evaluacionesPruebasMock
                .Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id))
                .ReturnsAsync(new List<EvaluacionPrueba>
                {
                    new EvaluacionPrueba { Verificado = true, PuntajeObtenido = 30, Aprobado = true },
                    new EvaluacionPrueba { Verificado = false, PuntajeObtenido = 20, Aprobado = true }
                });

            _evaluacionesMeritosMock
                .Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id))
                .ReturnsAsync(new List<EvaluacionMerito>
                {
                    new EvaluacionMerito { Estado = "Aprobado", PuntajeAsignado = 15 },
                    new EvaluacionMerito { Estado = "Rechazado", PuntajeAsignado = 40 }
                });

            _meritosMock
                .Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id))
                .ReturnsAsync(new List<MeritoPostulante> { new MeritoPostulante(), new MeritoPostulante() });

            var resultado = await _sut.ObtenerInscripcionesParaEvaluarAsync(llamadoId);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Single(data);
            Assert.Equal(30, data[0].PuntajePruebas);
            Assert.Equal(15, data[0].PuntajeMeritos);
            Assert.Equal(45, data[0].PuntajeTotal);
            Assert.Equal(1, data[0].PruebasTotales);
            _inscripcionesMock.Verify(r => r.GetByLlamadoIdAsync(llamadoId), Times.Once);
        }

        [Fact]
        public async Task ObtenerDetalleEvaluacionAsync_NoExiste_RetornaFallo()
        {
            _inscripcionesMock
                .Setup(r => r.GetByIdCompleteAsync(It.IsAny<int>()))
                .ReturnsAsync((Inscripcion?)null);

            var resultado = await _sut.ObtenerDetalleEvaluacionAsync(50);

            Assert.False(resultado.Success);
            Assert.Null(resultado.Data);
            Assert.Contains("no encontrada", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task CalificarPruebaAsync_PuntajeFueraDeRango_CancelaTransaccion()
        {
            _pruebasMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Prueba { Id = 1, PuntajeMaximo = 20 });

            var dto = new CalificarPruebaDto
            {
                InscripcionId = 1,
                PruebaId = 1,
                PuntajeObtenido = 40,
                Observaciones = "n/a"
            };

            var resultado = await _sut.CalificarPruebaAsync(dto);

            Assert.False(resultado.Success);
            Assert.Contains("puntaje", resultado.Message, StringComparison.OrdinalIgnoreCase);
            _unitOfWorkMock.Verify(u => u.RollbackTransactionAsync(), Times.Once);
            _evaluacionesPruebasMock.Verify(r => r.AddAsync(It.IsAny<EvaluacionPrueba>()), Times.Never);
        }

        [Fact]
        public async Task CalificarPruebaAsync_SinEvaluacionPrevio_CreaYConfirma()
        {
            _pruebasMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Prueba { Id = 2, PuntajeMaximo = 100, Nombre = "Prueba", Tipo = "Escrito" });

            _evaluacionesPruebasMock
                .Setup(r => r.GetByInscripcionAndPruebaAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((EvaluacionPrueba?)null);

            _evaluacionesPruebasMock
                .Setup(r => r.AddAsync(It.IsAny<EvaluacionPrueba>()))
                .ReturnsAsync((EvaluacionPrueba eval) =>
                {
                    eval.Id = 77;
                    return eval;
                });

            var dto = new CalificarPruebaDto
            {
                InscripcionId = 10,
                PruebaId = 2,
                PuntajeObtenido = 80,
                Observaciones = "Bien"
            };

            var resultado = await _sut.CalificarPruebaAsync(dto);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Equal(77, data.Id);
            Assert.True(data.Aprobado);
            _unitOfWorkMock.Verify(u => u.CommitTransactionAsync(), Times.Once);
            _evaluacionesPruebasMock.Verify(r => r.AddAsync(It.Is<EvaluacionPrueba>(e => e.PruebaId == dto.PruebaId)), Times.Once);
        }

        [Fact]
        public async Task ValorarMeritoAsync_ActualizaEvaluacionExistente()
        {
            var merito = new MeritoPostulante { Id = 5, ItemPuntuableId = 9, DocumentoRespaldo = "doc" };
            var item = new ItemPuntuable { Id = 9, PuntajeMaximo = 50, Nombre = "Curso", Categoria = "Formacion" };
            var evaluacion = new EvaluacionMerito { Id = 1, MeritoPostulanteId = 5 };

            _meritosMock.Setup(r => r.GetByIdAsync(merito.Id)).ReturnsAsync(merito);
            _itemsMock.Setup(r => r.GetByIdAsync(item.Id)).ReturnsAsync(item);
            _evaluacionesMeritosMock.Setup(r => r.GetByMeritoIdAsync(merito.Id)).ReturnsAsync(evaluacion);

            var dto = new ValorarMeritoDto
            {
                MeritoPostulanteId = merito.Id,
                PuntajeAsignado = 40,
                DocumentacionVerificada = true,
                Observaciones = "ok"
            };

            var resultado = await _sut.ValorarMeritoAsync(dto);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Equal(dto.PuntajeAsignado, data.PuntajeAsignado);
            Assert.Equal("Aprobado", data.Estado);
            _evaluacionesMeritosMock.Verify(r => r.UpdateAsync(It.Is<EvaluacionMerito>(e => e.PuntajeAsignado == dto.PuntajeAsignado)), Times.Once);
        }

        [Fact]
        public async Task ValorarMeritoAsync_PuntajeInvalido_RetornaFallo()
        {
            var merito = new MeritoPostulante { Id = 5, ItemPuntuableId = 9 };
            var item = new ItemPuntuable { Id = 9, PuntajeMaximo = 20 };

            _meritosMock.Setup(r => r.GetByIdAsync(merito.Id)).ReturnsAsync(merito);
            _itemsMock.Setup(r => r.GetByIdAsync(item.Id)).ReturnsAsync(item);

            var dto = new ValorarMeritoDto
            {
                MeritoPostulanteId = merito.Id,
                PuntajeAsignado = 25,
                DocumentacionVerificada = false
            };

            var resultado = await _sut.ValorarMeritoAsync(dto);

            Assert.False(resultado.Success);
            Assert.Contains("puntaje", resultado.Message, StringComparison.OrdinalIgnoreCase);
            _unitOfWorkMock.Verify(u => u.RollbackTransactionAsync(), Times.Once);
        }

        [Fact]
        public async Task ValorarMeritosAsync_MezclaValida_SoloProcesaPermitidos()
        {
            var meritoValido = new MeritoPostulante { Id = 1, ItemPuntuableId = 5, DocumentoRespaldo = "doc" };
            var itemValido = new ItemPuntuable { Id = 5, PuntajeMaximo = 50, Nombre = "Curso", Categoria = "Formacion" };

            _meritosMock.Setup(r => r.GetByIdAsync(meritoValido.Id)).ReturnsAsync(meritoValido);
            _meritosMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((MeritoPostulante?)null);
            _itemsMock.Setup(r => r.GetByIdAsync(itemValido.Id)).ReturnsAsync(itemValido);
            _evaluacionesMeritosMock.Setup(r => r.GetByMeritoIdAsync(meritoValido.Id)).ReturnsAsync((EvaluacionMerito?)null);
            _evaluacionesMeritosMock
                .Setup(r => r.AddAsync(It.IsAny<EvaluacionMerito>()))
                .ReturnsAsync((EvaluacionMerito e) =>
                {
                    e.Id = 22;
                    return e;
                });

            var dtos = new List<ValorarMeritoDto>
            {
                new ValorarMeritoDto { MeritoPostulanteId = meritoValido.Id, PuntajeAsignado = 45, DocumentacionVerificada = true },
                new ValorarMeritoDto { MeritoPostulanteId = 999, PuntajeAsignado = 10, DocumentacionVerificada = true }
            };

            var resultado = await _sut.ValorarMeritosAsync(12, dtos);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Single(data);
            Assert.Equal(22, data[0].Id);
            Assert.Equal(45, data[0].PuntajeAsignado);
            _unitOfWorkMock.Verify(u => u.CommitTransactionAsync(), Times.Once);
        }

        [Fact]
        public async Task ObtenerPruebasDelLlamadoAsync_MapeaEstadisticas()
        {
            var llamadoId = 7;
            var prueba = new Prueba
            {
                Id = 15,
                LlamadoId = llamadoId,
                Nombre = "Escrito",
                Tipo = "Escrito",
                PuntajeMaximo = 100,
                EsObligatoria = true
            };

            _pruebasMock.Setup(r => r.GetByLlamadoIdAsync(llamadoId)).ReturnsAsync(new List<Prueba> { prueba });
            _evaluacionesPruebasMock
                .Setup(r => r.GetByPruebaIdAsync(prueba.Id))
                .ReturnsAsync(new List<EvaluacionPrueba>
                {
                    new EvaluacionPrueba { Aprobado = true, PuntajeObtenido = 80 },
                    new EvaluacionPrueba { Aprobado = false, PuntajeObtenido = 60 }
                });

            var resultado = await _sut.ObtenerPruebasDelLlamadoAsync(llamadoId);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Single(data);
            Assert.Equal(2, data[0].CantidadEvaluados);
            Assert.Equal(1, data[0].CantidadAprobados);
            Assert.Equal(70, data[0].PromedioGeneral);
        }

        [Fact]
        public async Task ObtenerOrdenamientosAsync_MapeaCantidadPosiciones()
        {
            var ordenamiento = new Ordenamiento
            {
                Id = 5,
                LlamadoId = 2,
                Tipo = "General",
                Estado = "Preliminar",
                FechaGeneracion = DateTime.UtcNow,
                Llamado = new Llamado { Id = 2, Titulo = "Docente" },
                DepartamentoId = 9,
                Departamento = new Departamento { Id = 9, Nombre = "Canelones" }
            };

            _ordenamientosMock.Setup(r => r.GetByLlamadoIdAsync(2)).ReturnsAsync(new List<Ordenamiento> { ordenamiento });
            _posicionesMock
                .Setup(r => r.GetByOrdenamientoIdAsync(ordenamiento.Id))
                .ReturnsAsync(new List<PosicionOrdenamiento>
                {
                    new PosicionOrdenamiento { Id = 1, OrdenamientoId = ordenamiento.Id, Posicion = 1, InscripcionId = 10 },
                    new PosicionOrdenamiento { Id = 2, OrdenamientoId = ordenamiento.Id, Posicion = 2, InscripcionId = 11 }
                });

            var resultado = await _sut.ObtenerOrdenamientosAsync(2);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Single(data);
            Assert.Equal(2, data[0].CantidadPosiciones);
            Assert.Equal("Docente", data[0].TituloLlamado);
            Assert.Equal("Canelones", data[0].NombreDepartamento);
        }

        [Fact]
        public async Task ObtenerDetalleOrdenamientoAsync_NoExiste_RetornaFallo()
        {
            _ordenamientosMock
                .Setup(r => r.GetByIdWithPosicionesAsync(It.IsAny<int>()))
                .ReturnsAsync((Ordenamiento?)null);

            var resultado = await _sut.ObtenerDetalleOrdenamientoAsync(8);

            Assert.False(resultado.Success);
            Assert.Null(resultado.Data);
            Assert.Contains("no encontrado", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ObtenerDetalleOrdenamientoAsync_MapeaPosiciones()
        {
            var inscripcion = CrearInscripcion();
            var ordenamiento = new Ordenamiento
            {
                Id = 6,
                Tipo = "General",
                Estado = "Definitivo",
                FechaGeneracion = DateTime.UtcNow,
                Llamado = new Llamado { Id = inscripcion.LlamadoId, Titulo = "Docente" },
                Posiciones = new List<PosicionOrdenamiento>
                {
                    new PosicionOrdenamiento
                    {
                        OrdenamientoId = 6,
                        InscripcionId = inscripcion.Id,
                        Posicion = 1,
                        PuntajeTotal = 95,
                        Inscripcion = inscripcion
                    }
                }
            };

            _ordenamientosMock
                .Setup(r => r.GetByIdWithPosicionesAsync(ordenamiento.Id))
                .ReturnsAsync(ordenamiento);

            _evaluacionesPruebasMock
                .Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id))
                .ReturnsAsync(new List<EvaluacionPrueba>
                {
                    new EvaluacionPrueba { PuntajeObtenido = 40 },
                    new EvaluacionPrueba { PuntajeObtenido = 35 }
                });

            _evaluacionesMeritosMock
                .Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id))
                .ReturnsAsync(new List<EvaluacionMerito>
                {
                    new EvaluacionMerito { Estado = "Aprobado", PuntajeAsignado = 20 },
                    new EvaluacionMerito { Estado = "Rechazado", PuntajeAsignado = 50 }
                });

            var resultado = await _sut.ObtenerDetalleOrdenamientoAsync(ordenamiento.Id);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Single(data.Posiciones);
            var posicion = data.Posiciones[0];
            Assert.Equal(75, posicion.PuntajePruebas);
            Assert.Equal(20, posicion.PuntajeMeritos);
            Assert.Equal("Docente", data.TituloLlamado);
        }

        [Fact]
        public async Task ObtenerEstadisticasAsync_LlamadoNoExiste_RetornaFallo()
        {
            _llamadosMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Llamado?)null);

            var resultado = await _sut.ObtenerEstadisticasAsync(2);

            Assert.False(resultado.Success);
            Assert.Contains("no encontrado", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ObtenerEstadisticasAsync_CalculaIndicadores()
        {
            var llamadoId = 4;
            var llamado = new Llamado { Id = llamadoId, Titulo = "Docente" };
            var inscripcion = CrearInscripcion();
            inscripcion.LlamadoId = llamadoId;
            inscripcion.AutodefinicionLey = new AutodefinicionLey { EsAfrodescendiente = true };

            _llamadosMock.Setup(r => r.GetByIdAsync(llamadoId)).ReturnsAsync(llamado);
            _inscripcionesMock.Setup(r => r.GetByLlamadoIdAsync(llamadoId)).ReturnsAsync(new List<Inscripcion> { inscripcion });

            var pruebas = new List<Prueba> { new Prueba { Id = 9, Nombre = "Escrito" } };
            _pruebasMock.Setup(r => r.GetByLlamadoIdAsync(llamadoId)).ReturnsAsync(pruebas);

            _evaluacionesPruebasMock
                .Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id))
                .ReturnsAsync(new List<EvaluacionPrueba>
                {
                    new EvaluacionPrueba { PruebaId = 9, PuntajeObtenido = 80, Aprobado = true },
                    new EvaluacionPrueba { PruebaId = 10, PuntajeObtenido = 20, Aprobado = true }
                });

            _evaluacionesMeritosMock
                .Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id))
                .ReturnsAsync(new List<EvaluacionMerito>
                {
                    new EvaluacionMerito { Estado = "Aprobado", PuntajeAsignado = 15 }
                });

            _meritosMock
                .Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id))
                .ReturnsAsync(new List<MeritoPostulante> { new MeritoPostulante { Id = 1 } });

            _evaluacionesPruebasMock
                .Setup(r => r.GetByPruebaIdAsync(9))
                .ReturnsAsync(new List<EvaluacionPrueba>
                {
                    new EvaluacionPrueba { PruebaId = 9, Aprobado = true, PuntajeObtenido = 80 }
                });

            _ordenamientosMock
                .Setup(r => r.GetByLlamadoIdAsync(llamadoId))
                .ReturnsAsync(new List<Ordenamiento>
                {
                    new Ordenamiento { Id = 1, Estado = "Definitivo", FechaGeneracion = DateTime.UtcNow }
                });

            var resultado = await _sut.ObtenerEstadisticasAsync(llamadoId);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Equal(1, data.TotalInscripciones);
            Assert.True(data.OrdenamientoGenerado);
            Assert.Equal(1, data.TotalAfrodescendientes);
            Assert.Equal(115, data.PromedioGeneral);
            Assert.Single(data.DetallesPruebas);
        }

        [Fact]
        public async Task GenerarOrdenamientoAsync_SinInscripcionesValidas_RetornaFallo()
        {
            var dto = new GenerarOrdenamientoDto
            {
                LlamadoId = 3,
                PuntajeMinimoAprobacion = 70,
                EsDefinitivo = true,
                AplicarCuotas = false
            };

            var llamado = new Llamado { Id = 3, CantidadPuestos = 2, PorcentajeAfrodescendiente = 0, PorcentajeTrans = 0, PorcentajeDiscapacidad = 0 };

            _llamadosMock.Setup(r => r.GetByIdWithDetallesAsync(dto.LlamadoId)).ReturnsAsync(llamado);

            var inscripcion = CrearInscripcion();
            _inscripcionesMock.Setup(r => r.GetByLlamadoIdAsync(dto.LlamadoId)).ReturnsAsync(new List<Inscripcion> { inscripcion });
            _evaluacionesPruebasMock.Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id)).ReturnsAsync(new List<EvaluacionPrueba>
            {
                new EvaluacionPrueba { Aprobado = false, Verificado = true, PuntajeObtenido = 10 }
            });
            _evaluacionesMeritosMock.Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id)).ReturnsAsync(new List<EvaluacionMerito>());

            var resultado = await _sut.GenerarOrdenamientoAsync(dto);

            Assert.False(resultado.Success);
            Assert.Contains("puntaje mínimo", resultado.Message, StringComparison.OrdinalIgnoreCase);
            _unitOfWorkMock.Verify(u => u.RollbackTransactionAsync(), Times.Once);
            _ordenamientosMock.Verify(r => r.AddAsync(It.IsAny<Ordenamiento>()), Times.Never);
        }

        [Fact]
        public async Task GenerarOrdenamientoAsync_InscripcionesValidas_GeneraOrdenamiento()
        {
            var dto = new GenerarOrdenamientoDto
            {
                LlamadoId = 4,
                PuntajeMinimoAprobacion = 60,
                EsDefinitivo = false,
                AplicarCuotas = false
            };

            var llamado = new Llamado { Id = dto.LlamadoId, CantidadPuestos = 2 };
            _llamadosMock.Setup(r => r.GetByIdWithDetallesAsync(dto.LlamadoId)).ReturnsAsync(llamado);

            var inscripcion = CrearInscripcion();
            inscripcion.LlamadoId = dto.LlamadoId;
            _inscripcionesMock.Setup(r => r.GetByLlamadoIdAsync(dto.LlamadoId)).ReturnsAsync(new List<Inscripcion> { inscripcion });

            _evaluacionesPruebasMock
                .Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id))
                .ReturnsAsync(new List<EvaluacionPrueba>
                {
                    new EvaluacionPrueba { Verificado = true, Aprobado = true, PuntajeObtenido = 50 }
                });

            _evaluacionesMeritosMock
                .Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id))
                .ReturnsAsync(new List<EvaluacionMerito>
                {
                    new EvaluacionMerito { Estado = "Aprobado", PuntajeAsignado = 20 }
                });

            _meritosMock.Setup(r => r.GetByInscripcionIdAsync(inscripcion.Id)).ReturnsAsync(new List<MeritoPostulante>());

            _ordenamientosMock
                .Setup(r => r.AddAsync(It.IsAny<Ordenamiento>()))
                .ReturnsAsync((Ordenamiento o) =>
                {
                    o.Id = 88;
                    return o;
                });

            _posicionesMock
                .Setup(r => r.AddRangeAsync(It.IsAny<IEnumerable<PosicionOrdenamiento>>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _ordenamientosMock.Setup(r => r.GetByLlamadoIdAsync(dto.LlamadoId)).ReturnsAsync(new List<Ordenamiento>());

            var resultado = await _sut.GenerarOrdenamientoAsync(dto);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Single(data.OrdenamientosGenerados);
            Assert.Equal("General", data.OrdenamientosGenerados[0].Tipo);
            _unitOfWorkMock.Verify(u => u.CommitTransactionAsync(), Times.Once);
            _ordenamientosMock.Verify(r => r.AddAsync(It.IsAny<Ordenamiento>()), Times.Once);
            _posicionesMock.Verify(r => r.AddRangeAsync(It.Is<IEnumerable<PosicionOrdenamiento>>(p => p.Count() == 1)), Times.Once);
        }

        [Fact]
        public async Task PublicarOrdenamientoAsync_NoExiste_RetornaFallo()
        {
            _ordenamientosMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Ordenamiento?)null);

            var resultado = await _sut.PublicarOrdenamientoAsync(9);

            Assert.False(resultado.Success);
            Assert.Contains("no encontrado", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task PublicarOrdenamientoAsync_ActualizaEstado()
        {
            var ordenamiento = new Ordenamiento { Id = 2, Estado = "Preliminar" };
            _ordenamientosMock.Setup(r => r.GetByIdAsync(ordenamiento.Id)).ReturnsAsync(ordenamiento);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var resultado = await _sut.PublicarOrdenamientoAsync(ordenamiento.Id);

            Assert.True(resultado.Success);
            Assert.True(resultado.Data);
            _ordenamientosMock.Verify(r => r.UpdateAsync(It.Is<Ordenamiento>(o => o.Estado == "Publicado")), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        private static Inscripcion CrearInscripcion()
        {
            return new Inscripcion
            {
                Id = 11,
                PostulanteId = 5,
                DepartamentoId = 3,
                FechaInscripcion = DateTime.UtcNow,
                Estado = "Pendiente",
                Postulante = new Postulante
                {
                    Id = 5,
                    Nombre = "Ana",
                    Apellido = "Gomez",
                    CedulaIdentidad = "4.123.456-7",
                    Email = "ana@test.com"
                },
                Departamento = new Departamento { Id = 3, Nombre = "Montevideo", Codigo = "MO" },
                AutodefinicionLey = new AutodefinicionLey { EsAfrodescendiente = true, EsTrans = false, TieneDiscapacidad = false },
                MeritosPostulante =
                {
                    new MeritoPostulante
                    {
                        Id = 20,
                        ItemPuntuable = new ItemPuntuable { Id = 30, Nombre = "Curso", Categoria = "Formacion", PuntajeMaximo = 50 }
                    }
                },
                RequisitosPostulante =
                {
                    new RequisitoPostulante
                    {
                        Requisito = new RequisitoExcluyente { Id = 1, Descripcion = "Titulo", Obligatorio = true }
                    }
                }
            };
        }

        private static T AssertNotNull<T>(T? value) where T : class
        {
            Assert.NotNull(value);
            return value!;
        }
    }
}
