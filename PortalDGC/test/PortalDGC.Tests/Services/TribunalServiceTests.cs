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
