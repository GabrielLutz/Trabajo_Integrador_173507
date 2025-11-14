using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using PortalDGC.BusinessLogic.Services;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Domain.Entities;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Llamado;
using Xunit;

namespace PortalDGC.Tests.Services
{
    public class LlamadoServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILlamadoRepository> _llamadoRepositoryMock;
        private readonly Mock<IRequisitoExcluyenteRepository> _requisitoRepositoryMock;
        private readonly Mock<IItemPuntuableRepository> _itemPuntuableRepositoryMock;
        private readonly Mock<IApoyoNecesarioRepository> _apoyoNecesarioRepositoryMock;
        private readonly LlamadoService _sut;

        public LlamadoServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _llamadoRepositoryMock = new Mock<ILlamadoRepository>();
            _requisitoRepositoryMock = new Mock<IRequisitoExcluyenteRepository>();
            _itemPuntuableRepositoryMock = new Mock<IItemPuntuableRepository>();
            _apoyoNecesarioRepositoryMock = new Mock<IApoyoNecesarioRepository>();

            _unitOfWorkMock.SetupGet(u => u.Llamados).Returns(_llamadoRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.RequisitosExcluyentes).Returns(_requisitoRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.ItemsPuntuables).Returns(_itemPuntuableRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.ApoyosNecesarios).Returns(_apoyoNecesarioRepositoryMock.Object);

            _sut = new LlamadoService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task ObtenerLlamadoPorIdAsync_LlamadoExiste_RetornaDetalleMapeado()
        {
            var llamado = CrearLlamadoCompleto();
            _llamadoRepositoryMock
                .Setup(r => r.GetByIdWithDetallesAsync(llamado.Id))
                .ReturnsAsync(llamado);

            var resultado = await _sut.ObtenerLlamadoPorIdAsync(llamado.Id);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Equal(llamado.Id, data.Id);
            Assert.Equal(llamado.LlamadoDepartamentos.Count, data.Departamentos.Count);
            Assert.Equal(llamado.RequisitosExcluyentes.Count, data.RequisitosExcluyentes.Count);
            Assert.Equal(llamado.ItemsPuntuables.Count, data.ItemsPuntuables.Count);
            Assert.Equal(llamado.ApoyosNecesarios.Count, data.ApoyosNecesarios.Count);
            _llamadoRepositoryMock.Verify(r => r.GetByIdWithDetallesAsync(llamado.Id), Times.Once);
        }

        [Fact]
        public async Task ObtenerLlamadoPorIdAsync_LlamadoNoExiste_RetornaNotFound()
        {
            _llamadoRepositoryMock
                .Setup(r => r.GetByIdWithDetallesAsync(It.IsAny<int>()))
                .ReturnsAsync((Llamado?)null);

            var resultado = await _sut.ObtenerLlamadoPorIdAsync(5);

            Assert.False(resultado.Success);
            Assert.Null(resultado.Data);
            Assert.Contains("no encontrado", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ObtenerLlamadoPorIdAsync_Error_RetornaMensajeDeError()
        {
            _llamadoRepositoryMock
                .Setup(r => r.GetByIdWithDetallesAsync(It.IsAny<int>()))
                .ThrowsAsync(new InvalidOperationException("boom"));

            var resultado = await _sut.ObtenerLlamadoPorIdAsync(1);

            Assert.False(resultado.Success);
            Assert.Contains("Error", resultado.Message);
            var errors = AssertNotNull(resultado.Errors);
            Assert.Single(errors);
        }

        [Fact]
        public async Task ObtenerLlamadosActivosAsync_LlamadosDisponibles_RetornaLista()
        {
            var llamados = new List<Llamado>
            {
                new Llamado { Id = 1, Titulo = "A", Descripcion = "desc", FechaApertura = DateTime.Today, FechaCierre = DateTime.Today.AddDays(5), Estado = "Abierto" }
            };
            _llamadoRepositoryMock
                .Setup(r => r.GetLlamadosActivosAsync())
                .ReturnsAsync(llamados);

            var resultado = await _sut.ObtenerLlamadosActivosAsync();

            Assert.True(resultado.Success);
            var dataActivos = AssertNotNull(resultado.Data);
            Assert.Single(dataActivos);
            Assert.Equal("A", dataActivos[0].Titulo);
        }

        [Fact]
        public async Task ObtenerLlamadosActivosAsync_Error_RetornaFallo()
        {
            _llamadoRepositoryMock
                .Setup(r => r.GetLlamadosActivosAsync())
                .ThrowsAsync(new Exception("sql"));

            var resultado = await _sut.ObtenerLlamadosActivosAsync();

            Assert.False(resultado.Success);
            Assert.Contains("Error", resultado.Message);
        }

        [Theory]
        [InlineData(true, true, "disponible")]
        [InlineData(false, true, "no disponible")]
        public async Task ValidarLlamadoDisponibleAsync_RetornaSegunRepositorio(bool repoValue, bool successEsperado, string textoEsperado)
        {
            _llamadoRepositoryMock
                .Setup(r => r.IsLlamadoAbierto(It.IsAny<int>()))
                .ReturnsAsync(repoValue);

            var resultado = await _sut.ValidarLlamadoDisponibleAsync(10);

            Assert.Equal(successEsperado, resultado.Success);
            Assert.Equal(repoValue, resultado.Data);
            Assert.Contains(textoEsperado, resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ValidarLlamadoDisponibleAsync_Error_RetornaFallo()
        {
            _llamadoRepositoryMock
                .Setup(r => r.IsLlamadoAbierto(It.IsAny<int>()))
                .ThrowsAsync(new Exception("fail"));

            var resultado = await _sut.ValidarLlamadoDisponibleAsync(10);

            Assert.False(resultado.Success);
            Assert.Contains("Error", resultado.Message);
            Assert.NotNull(resultado.Errors);
        }

        [Fact]
        public async Task ObtenerRequisitosLlamadoAsync_MapeaCorrectamente()
        {
            var requisitos = new List<RequisitoExcluyente>
            {
                new RequisitoExcluyente { Id = 1, Descripcion = "titulo", Tipo = "Tipo", Obligatorio = true }
            };
            _requisitoRepositoryMock
                .Setup(r => r.GetByLlamadoIdAsync(1))
                .ReturnsAsync(requisitos);

            var resultado = await _sut.ObtenerRequisitosLlamadoAsync(1);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Single(data);
            Assert.Equal(requisitos[0].Descripcion, data[0].Descripcion);
        }

        [Fact]
        public async Task ObtenerItemsPuntuablesLlamadoAsync_MapeaCorrectamente()
        {
            var items = new List<ItemPuntuable>
            {
                new ItemPuntuable { Id = 5, Nombre = "Item", Descripcion = "Desc", PuntajeMaximo = 10, Categoria = "Cat" }
            };
            _itemPuntuableRepositoryMock
                .Setup(r => r.GetByLlamadoIdAsync(1))
                .ReturnsAsync(items);

            var resultado = await _sut.ObtenerItemsPuntuablesLlamadoAsync(1);

            Assert.True(resultado.Success);
            var itemsData = AssertNotNull(resultado.Data);
            Assert.Single(itemsData);
            Assert.Equal(items[0].PuntajeMaximo, itemsData[0].PuntajeMaximo);
        }

        [Fact]
        public async Task ObtenerApoyosNecesariosLlamadoAsync_MapeaCorrectamente()
        {
            var apoyos = new List<ApoyoNecesario>
            {
                new ApoyoNecesario { Id = 7, Descripcion = "Apoyo", Tipo = "Tecnico" }
            };
            _apoyoNecesarioRepositoryMock
                .Setup(r => r.GetByLlamadoIdAsync(1))
                .ReturnsAsync(apoyos);

            var resultado = await _sut.ObtenerApoyosNecesariosLlamadoAsync(1);

            Assert.True(resultado.Success);
            var apoyosData = AssertNotNull(resultado.Data);
            Assert.Single(apoyosData);
            Assert.Equal(apoyos[0].Tipo, apoyosData[0].Tipo);
        }

        private static T AssertNotNull<T>(T? value) where T : class
        {
            Assert.NotNull(value);
            return value!;
        }

        private static Llamado CrearLlamadoCompleto()
        {
            return new Llamado
            {
                Id = 1,
                Titulo = "Llamado",
                Descripcion = "Desc",
                Bases = "Bases",
                FechaApertura = DateTime.Today,
                FechaCierre = DateTime.Today.AddDays(10),
                CantidadPuestos = 3,
                Estado = "Abierto",
                LlamadoDepartamentos =
                {
                    new LlamadoDepartamento
                    {
                        DepartamentoId = 10,
                        CantidadPuestos = 2,
                        Departamento = new Departamento { Id = 10, Nombre = "Montevideo", Codigo = "MO" }
                    }
                },
                RequisitosExcluyentes =
                {
                    new RequisitoExcluyente { Id = 2, Descripcion = "Titulo", Tipo = "Academico", Obligatorio = true }
                },
                ItemsPuntuables =
                {
                    new ItemPuntuable { Id = 3, Nombre = "Curso", Descripcion = "desc", PuntajeMaximo = 5, Categoria = "Formacion" }
                },
                ApoyosNecesarios =
                {
                    new ApoyoNecesario { Id = 4, Descripcion = "Apoyo", Tipo = "Tecnico" }
                }
            };
        }
    }
}
