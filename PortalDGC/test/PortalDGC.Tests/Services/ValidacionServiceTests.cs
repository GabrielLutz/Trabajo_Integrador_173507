using System;
using System.Threading.Tasks;
using Moq;
using PortalDGC.BusinessLogic.Services;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Domain.Entities;
using Xunit;

namespace PortalDGC.Tests.Services
{
    public class ValidacionServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILlamadoRepository> _llamadoRepositoryMock;
        private readonly Mock<IPostulanteRepository> _postulanteRepositoryMock;
        private readonly ValidacionService _sut;

        public ValidacionServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _llamadoRepositoryMock = new Mock<ILlamadoRepository>();
            _postulanteRepositoryMock = new Mock<IPostulanteRepository>();

            _unitOfWorkMock.SetupGet(u => u.Llamados).Returns(_llamadoRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.Postulantes).Returns(_postulanteRepositoryMock.Object);

            _sut = new ValidacionService(_unitOfWorkMock.Object);
        }

        [Theory]
        [InlineData("1.234.567-8", true)]
        [InlineData("12345678", true)]
        [InlineData("1234567", true)]
        [InlineData("abc.def.ghi-j", false)]
        [InlineData("", false)]
        [InlineData("123", false)]
        public void ValidarCedulaIdentidad_VariosFormatos_RetornaResultadoEsperado(string cedula, bool esperado)
        {
            var resultado = _sut.ValidarCedulaIdentidad(cedula);

            Assert.Equal(esperado, resultado.Success);
            Assert.Equal(esperado, resultado.Data);
        }

        [Theory]
        [InlineData("test@example.com", true)]
        [InlineData("user.name@domain.co.uk", true)]
        [InlineData("invalid.email", false)]
        [InlineData("@example.com", false)]
        [InlineData("", false)]
        public void ValidarEmail_VariosFormatos_RetornaResultadoEsperado(string email, bool esperado)
        {
            var resultado = _sut.ValidarEmail(email);

            Assert.Equal(esperado, resultado.Success);
            Assert.Equal(esperado, resultado.Data);
        }

        [Theory]
        [InlineData("099123456", true)]
        [InlineData("24012345", false)]
        [InlineData("024012345", true)]
        [InlineData("123456789", false)]
        [InlineData("", true)]
        public void ValidarTelefono_VariosFormatos_RetornaResultadoEsperado(string telefono, bool esperado)
        {
            var resultado = _sut.ValidarTelefono(telefono);

            Assert.Equal(esperado, resultado.Success);
        }

        [Fact]
        public void ValidarEdadMinima_Mayor18_RetornaTrue()
        {
            var fechaNacimiento = DateTime.Now.AddYears(-20);

            var resultado = _sut.ValidarEdadMinima(fechaNacimiento, 18);

            Assert.True(resultado.Success);
            Assert.True(resultado.Data);
        }

        [Fact]
        public void ValidarEdadMinima_Menor18_RetornaFalse()
        {
            var fechaNacimiento = DateTime.Now.AddYears(-15);

            var resultado = _sut.ValidarEdadMinima(fechaNacimiento, 18);

            Assert.False(resultado.Success);
            Assert.False(resultado.Data);
        }

        [Fact]
        public void ValidarFechaRango_DentroDelRango_RetornaTrue()
        {
            var fecha = DateTime.Now;
            var fechaInicio = DateTime.Now.AddDays(-10);
            var fechaFin = DateTime.Now.AddDays(10);

            var resultado = _sut.ValidarFechaRango(fecha, fechaInicio, fechaFin);

            Assert.True(resultado.Success);
            Assert.True(resultado.Data);
        }

        [Fact]
        public void ValidarFechaRango_FechaEnLimiteSuperior_RetornaTrue()
        {
            var fechaInicio = DateTime.Today.AddDays(-5);
            var fechaFin = DateTime.Today;

            var resultado = _sut.ValidarFechaRango(fechaFin, fechaInicio, fechaFin);

            Assert.True(resultado.Success);
            Assert.True(resultado.Data);
        }

        [Fact]
        public void ValidarFechaRango_FueraDelRango_RetornaFalse()
        {
            var fecha = DateTime.Now.AddDays(-20);
            var fechaInicio = DateTime.Now.AddDays(-10);
            var fechaFin = DateTime.Now.AddDays(10);

            var resultado = _sut.ValidarFechaRango(fecha, fechaInicio, fechaFin);

            Assert.False(resultado.Success);
            Assert.False(resultado.Data);
        }

        [Fact]
        public async Task ValidarLlamadoAbierto_Disponible_RetornaTrue()
        {
            _llamadoRepositoryMock
                .Setup(r => r.IsLlamadoAbierto(5))
                .ReturnsAsync(true);

            var resultado = await _sut.ValidarLlamadoAbierto(5);

            Assert.True(resultado.Success);
            Assert.True(resultado.Data);
            Assert.Contains("abierto", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ValidarLlamadoAbierto_Error_RetornaFallo()
        {
            _llamadoRepositoryMock
                .Setup(r => r.IsLlamadoAbierto(It.IsAny<int>()))
                .ThrowsAsync(new InvalidOperationException("db"));

            var resultado = await _sut.ValidarLlamadoAbierto(1);

            Assert.False(resultado.Success);
            Assert.Contains("Error", resultado.Message);
            Assert.NotNull(resultado.Errors);
        }

        [Fact]
        public async Task ValidarPostulanteCompletoDatos_NoExiste_RetornaFallo()
        {
            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Postulante?)null);

            var resultado = await _sut.ValidarPostulanteCompletoDatos(3);

            Assert.False(resultado.Success);
            Assert.Contains("no encontrado", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ValidarPostulanteCompletoDatos_Incompleto_RetornaFalse()
        {
            var postulante = new Postulante { Id = 1, Nombre = "Ana", Apellido = "G", CedulaIdentidad = "1" };
            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(postulante.Id))
                .ReturnsAsync(postulante);

            var resultado = await _sut.ValidarPostulanteCompletoDatos(postulante.Id);

            Assert.True(resultado.Success);
            Assert.False(resultado.Data);
            Assert.Contains("Faltan", resultado.Message);
        }

        [Fact]
        public async Task ValidarPostulanteCompletoDatos_Completo_RetornaTrue()
        {
            var postulante = new Postulante
            {
                Id = 2,
                Nombre = "Luis",
                Apellido = "Perez",
                CedulaIdentidad = "12345678",
                Email = "test@test.com",
                Celular = "099123456",
                Domicilio = "Calle 123"
            };

            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(postulante.Id))
                .ReturnsAsync(postulante);

            var resultado = await _sut.ValidarPostulanteCompletoDatos(postulante.Id);

            Assert.True(resultado.Success);
            Assert.True(resultado.Data);
            Assert.Contains("complet", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }
    }
}
