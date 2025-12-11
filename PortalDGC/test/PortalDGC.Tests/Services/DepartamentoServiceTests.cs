using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using PortalDGC.BusinessLogic.Services;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Domain.Entities;
using Xunit;

namespace PortalDGC.Tests.Services
{
    public class DepartamentoServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IDepartamentoRepository> _departamentoRepositoryMock;
        private readonly Mock<ILlamadoRepository> _llamadoRepositoryMock;
        private readonly DepartamentoService _sut;

        public DepartamentoServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _departamentoRepositoryMock = new Mock<IDepartamentoRepository>();
            _llamadoRepositoryMock = new Mock<ILlamadoRepository>();

            _unitOfWorkMock.SetupGet(u => u.Departamentos).Returns(_departamentoRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.Llamados).Returns(_llamadoRepositoryMock.Object);

            _sut = new DepartamentoService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task ObtenerDepartamentosActivosAsync_Exito_MapeaEntidad()
        {
            var departamentos = new List<Departamento>
            {
                new Departamento { Id = 1, Nombre = "Montevideo", Codigo = "MO" }
            };

            _departamentoRepositoryMock
                .Setup(r => r.GetActivosAsync())
                .ReturnsAsync(departamentos);

            var resultado = await _sut.ObtenerDepartamentosActivosAsync();

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Single(data);
            Assert.Equal("Montevideo", data[0].Nombre);
        }

        [Fact]
        public async Task ObtenerDepartamentosActivosAsync_Error_RetornaFallo()
        {
            _departamentoRepositoryMock
                .Setup(r => r.GetActivosAsync())
                .ThrowsAsync(new InvalidOperationException("db"));

            var resultado = await _sut.ObtenerDepartamentosActivosAsync();

            Assert.False(resultado.Success);
            Assert.Contains("Error", resultado.Message);
        }

        [Fact]
        public async Task ObtenerDepartamentoPorIdAsync_NoExiste_RetornaFallo()
        {
            _departamentoRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Departamento?)null);

            var resultado = await _sut.ObtenerDepartamentoPorIdAsync(5);

            Assert.False(resultado.Success);
            Assert.Contains("no encontrado", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ObtenerDepartamentoPorIdAsync_Existe_MapeaDto()
        {
            var departamento = new Departamento { Id = 2, Nombre = "Canelones", Codigo = "CA" };
            _departamentoRepositoryMock
                .Setup(r => r.GetByIdAsync(departamento.Id))
                .ReturnsAsync(departamento);

            var resultado = await _sut.ObtenerDepartamentoPorIdAsync(departamento.Id);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Equal(departamento.Codigo, data.Codigo);
        }

        [Fact]
        public async Task ObtenerDepartamentosPorLlamadoAsync_LlamadoNoExiste_Fallo()
        {
            _llamadoRepositoryMock
                .Setup(r => r.GetByIdWithDepartamentosAsync(It.IsAny<int>()))
                .ReturnsAsync((Llamado?)null);

            var resultado = await _sut.ObtenerDepartamentosPorLlamadoAsync(1);

            Assert.False(resultado.Success);
            Assert.Contains("no encontrado", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ObtenerDepartamentosPorLlamadoAsync_DevuelveDepartamentosDelLlamado()
        {
            var llamado = new Llamado
            {
                Id = 3,
                LlamadoDepartamentos =
                {
                    new LlamadoDepartamento
                    {
                        DepartamentoId = 10,
                        CantidadPuestos = 2,
                        Departamento = new Departamento { Id = 10, Nombre = "Rocha", Codigo = "RO" }
                    }
                }
            };

            _llamadoRepositoryMock
                .Setup(r => r.GetByIdWithDepartamentosAsync(llamado.Id))
                .ReturnsAsync(llamado);

            var resultado = await _sut.ObtenerDepartamentosPorLlamadoAsync(llamado.Id);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Single(data);
            Assert.Equal(llamado.LlamadoDepartamentos.First().CantidadPuestos, data[0].CantidadPuestos);
        }

        [Theory]
        [InlineData(true, "llamado")]
        [InlineData(false, "no disponible")]
        public async Task ValidarDepartamentoEnLlamadoAsync_PropagaResultado(bool existe, string texto)
        {
            _departamentoRepositoryMock
                .Setup(r => r.ExistsInLlamado(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(existe);

            var resultado = await _sut.ValidarDepartamentoEnLlamadoAsync(1, 2);

            Assert.True(resultado.Success);
            Assert.Equal(existe, resultado.Data);
            Assert.Contains(texto, resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ValidarDepartamentoEnLlamadoAsync_Error_RetornaFallo()
        {
            _departamentoRepositoryMock
                .Setup(r => r.ExistsInLlamado(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("boom"));

            var resultado = await _sut.ValidarDepartamentoEnLlamadoAsync(1, 2);

            Assert.False(resultado.Success);
            Assert.Contains("Error", resultado.Message);
            Assert.NotNull(resultado.Errors);
        }

        private static T AssertNotNull<T>(T? value) where T : class
        {
            Assert.NotNull(value);
            return value!;
        }
    }
}
