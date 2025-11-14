using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.BusinessLogic.Services;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Domain.Entities;
using PortalDGC.Dtos.Inscripcion;
using Xunit;

namespace PortalDGC.Tests.Services
{
    public class InscripcionServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidacionService> _validacionServiceMock;
        private readonly Mock<IPostulanteRepository> _postulanteRepositoryMock;
        private readonly Mock<ILlamadoRepository> _llamadoRepositoryMock;
        private readonly Mock<IInscripcionRepository> _inscripcionRepositoryMock;
        private readonly Mock<IDepartamentoRepository> _departamentoRepositoryMock;
        private readonly Mock<IAutodefinicionLeyRepository> _autodefinicionRepositoryMock;
        private readonly Mock<IRequisitoPostulanteRepository> _requisitoPostulanteRepositoryMock;
        private readonly Mock<IMeritoPostulanteRepository> _meritoPostulanteRepositoryMock;
        private readonly Mock<IApoyoSolicitadoRepository> _apoyoSolicitadoRepositoryMock;
        private readonly InscripcionService _sut;

        public InscripcionServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validacionServiceMock = new Mock<IValidacionService>();
            _postulanteRepositoryMock = new Mock<IPostulanteRepository>();
            _llamadoRepositoryMock = new Mock<ILlamadoRepository>();
            _inscripcionRepositoryMock = new Mock<IInscripcionRepository>();
            _departamentoRepositoryMock = new Mock<IDepartamentoRepository>();
            _autodefinicionRepositoryMock = new Mock<IAutodefinicionLeyRepository>();
            _requisitoPostulanteRepositoryMock = new Mock<IRequisitoPostulanteRepository>();
            _meritoPostulanteRepositoryMock = new Mock<IMeritoPostulanteRepository>();
            _apoyoSolicitadoRepositoryMock = new Mock<IApoyoSolicitadoRepository>();

            _unitOfWorkMock.SetupGet(u => u.Postulantes).Returns(_postulanteRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.Llamados).Returns(_llamadoRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.Inscripciones).Returns(_inscripcionRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.Departamentos).Returns(_departamentoRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.AutodefinicionesLey).Returns(_autodefinicionRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.RequisitosPostulante).Returns(_requisitoPostulanteRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.MeritosPostulante).Returns(_meritoPostulanteRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.ApoyosSolicitados).Returns(_apoyoSolicitadoRepositoryMock.Object);

            _unitOfWorkMock.Setup(u => u.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.CommitTransactionAsync()).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.RollbackTransactionAsync()).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            _sut = new InscripcionService(_unitOfWorkMock.Object, _validacionServiceMock.Object);
        }

        [Fact]
        public async Task CrearInscripcion_DatosValidos_RetornaSuccess()
        {
            var postulanteId = 1;
            var inscripcionDto = new CrearInscripcionDto
            {
                LlamadoId = 1,
                DepartamentoId = 1,
                Autodefinicion = new AutodefinicionLeyDto(),
                Requisitos = new List<RequisitoPostulanteDto>(),
                Meritos = new List<MeritoPostulanteDto>(),
                ApoyosIds = new List<int>()
            };

            var postulante = new Postulante { Id = postulanteId, Nombre = "Juan", Apellido = "Pérez" };
            var llamado = new Llamado
            {
                Id = 1,
                Estado = "Abierto",
                Titulo = "Llamado 1",
                FechaCierre = DateTime.Now.AddDays(10),
                RequisitosExcluyentes = new List<RequisitoExcluyente>(),
                ItemsPuntuables = new List<ItemPuntuable>(),
                ApoyosNecesarios = new List<ApoyoNecesario>(),
                LlamadoDepartamentos = new List<LlamadoDepartamento>()
            };

            var inscripcionCreada = new Inscripcion
            {
                Id = 1,
                PostulanteId = postulanteId,
                LlamadoId = inscripcionDto.LlamadoId,
                DepartamentoId = inscripcionDto.DepartamentoId,
                Postulante = postulante,
                Llamado = llamado,
                Departamento = new Departamento { Id = 1, Nombre = "Montevideo" },
                RequisitosPostulante = new List<RequisitoPostulante>(),
                MeritosPostulante = new List<MeritoPostulante>(),
                ApoyosSolicitados = new List<ApoyoSolicitado>()
            };

            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(postulanteId))
                .ReturnsAsync(postulante);

            _llamadoRepositoryMock
                .Setup(r => r.GetByIdWithDetallesAsync(inscripcionDto.LlamadoId))
                .ReturnsAsync(llamado);

            _llamadoRepositoryMock
                .Setup(r => r.IsLlamadoAbierto(inscripcionDto.LlamadoId))
                .ReturnsAsync(true);

            _inscripcionRepositoryMock
                .Setup(r => r.ExistsInscripcionAsync(postulanteId, inscripcionDto.LlamadoId))
                .ReturnsAsync(false);

            _departamentoRepositoryMock
                .Setup(r => r.ExistsInLlamado(inscripcionDto.DepartamentoId, inscripcionDto.LlamadoId))
                .ReturnsAsync(true);

            _autodefinicionRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<AutodefinicionLey>()))
                .ReturnsAsync(new AutodefinicionLey());

            _inscripcionRepositoryMock
                .Setup(r => r.CreateInscripcionCompleteAsync(It.IsAny<Inscripcion>()))
                .ReturnsAsync((Inscripcion i) =>
                {
                    i.Id = inscripcionCreada.Id;
                    return inscripcionCreada;
                });

            _inscripcionRepositoryMock
                .Setup(r => r.GetByIdCompleteAsync(It.IsAny<int>()))
                .ReturnsAsync(inscripcionCreada);

            var resultado = await _sut.CrearInscripcionAsync(postulanteId, inscripcionDto);

            Assert.True(resultado.Success);
            Assert.NotNull(resultado.Data);
            _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitTransactionAsync(), Times.Once);
        }

        [Fact]
        public async Task CrearInscripcion_PostulanteNoExiste_RetornaError()
        {
            var postulanteId = 999;
            var inscripcionDto = new CrearInscripcionDto
            {
                LlamadoId = 1,
                DepartamentoId = 1
            };

            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(postulanteId))
                .ReturnsAsync((Postulante?)null);

            var resultado = await _sut.CrearInscripcionAsync(postulanteId, inscripcionDto);

            Assert.False(resultado.Success);
            Assert.Contains("no encontrado", resultado.Message.ToLower());
        }

        [Fact]
        public async Task CrearInscripcion_LlamadoCerrado_RetornaError()
        {
            var postulanteId = 1;
            var inscripcionDto = new CrearInscripcionDto
            {
                LlamadoId = 1,
                DepartamentoId = 1
            };

            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(postulanteId))
                .ReturnsAsync(new Postulante { Id = postulanteId });

            _llamadoRepositoryMock
                .Setup(r => r.GetByIdWithDetallesAsync(inscripcionDto.LlamadoId))
                .ReturnsAsync(new Llamado { Id = 1 });

            _llamadoRepositoryMock
                .Setup(r => r.IsLlamadoAbierto(inscripcionDto.LlamadoId))
                .ReturnsAsync(false);

            var resultado = await _sut.CrearInscripcionAsync(postulanteId, inscripcionDto);

            Assert.False(resultado.Success);
            Assert.Contains("no está disponible", resultado.Message.ToLower());
        }

        [Fact]
        public async Task ValidarInscripcionExistente_YaExiste_RetornaTrue()
        {
            var postulanteId = 1;
            var llamadoId = 1;

            _inscripcionRepositoryMock
                .Setup(r => r.ExistsInscripcionAsync(postulanteId, llamadoId))
                .ReturnsAsync(true);

            var resultado = await _sut.ValidarInscripcionExistenteAsync(postulanteId, llamadoId);

            Assert.True(resultado.Success);
            Assert.True(resultado.Data);
        }
    }
}
