using System.Collections.Generic;
using PortalDGC.BusinessLogic.Services;
using Xunit;

namespace PortalDGC.Tests.Services
{
    public class ArchivoServiceTests
    {
        private readonly ArchivoService _sut;

        public ArchivoServiceTests()
        {
            _sut = new ArchivoService();
        }

        [Theory]
        [InlineData("documento.pdf", new[] { ".pdf", ".jpg" }, true)]
        [InlineData("imagen.jpg", new[] { ".pdf", ".jpg" }, true)]
        [InlineData("archivo.docx", new[] { ".pdf", ".jpg" }, false)]
        [InlineData("archivo.PDF", new[] { ".pdf", ".jpg" }, true)]
        public void ValidarTipoArchivo_VariasExtensiones_RetornaResultadoEsperado(string nombreArchivo, string[] extensiones, bool esperado)
        {
            var resultado = _sut.ValidarTipoArchivo(nombreArchivo, new List<string>(extensiones));

            Assert.Equal(esperado, resultado.Success);
            Assert.Equal(esperado, resultado.Data);
        }

        [Theory]
        [InlineData(1024 * 1024, 10, true)]
        [InlineData(5 * 1024 * 1024, 10, true)]
        [InlineData(10 * 1024 * 1024, 10, true)]
        [InlineData(11 * 1024 * 1024, 10, false)]
        [InlineData(20 * 1024 * 1024, 10, false)]
        public void ValidarTamanioArchivo_VariosTamanios_RetornaResultadoEsperado(long tamanio, long tamanioMaximoMb, bool esperado)
        {
            var resultado = _sut.ValidarTamañoArchivo(tamanio, tamanioMaximoMb);

            Assert.Equal(esperado, resultado.Success);
            Assert.Equal(esperado, resultado.Data);
        }
    }
}
