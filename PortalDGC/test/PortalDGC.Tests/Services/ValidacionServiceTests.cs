using System;
using Moq;
using PortalDGC.BusinessLogic.Services;
using PortalDGC.DataAccess.Interfaces;
using Xunit;

namespace PortalDGC.Tests.Services
{
    public class ValidacionServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ValidacionService _sut;

        public ValidacionServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
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
        public void ValidarFechaRango_FueraDelRango_RetornaFalse()
        {
            var fecha = DateTime.Now.AddDays(-20);
            var fechaInicio = DateTime.Now.AddDays(-10);
            var fechaFin = DateTime.Now.AddDays(10);

            var resultado = _sut.ValidarFechaRango(fecha, fechaInicio, fechaFin);

            Assert.False(resultado.Success);
            Assert.False(resultado.Data);
        }
    }
}
