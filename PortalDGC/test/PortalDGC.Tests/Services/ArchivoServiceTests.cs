using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

        [Fact]
        public async Task GuardarArchivoAsync_CreaArchivoFisico()
        {
            var carpeta = Guid.NewGuid().ToString("N");
            var nombreArchivo = "prueba.txt";
            var rutaEsperada = Path.Combine("Archivos", carpeta, nombreArchivo);

            try
            {
                var contenido = new byte[] { 1, 2, 3 };
                var resultado = await _sut.GuardarArchivoAsync(contenido, nombreArchivo, carpeta);

                Assert.True(resultado.Success);
                Assert.Equal(rutaEsperada, resultado.Data);
                Assert.True(File.Exists(rutaEsperada));
                var almacenado = await File.ReadAllBytesAsync(rutaEsperada);
                Assert.Equal(contenido, almacenado);
            }
            finally
            {
                var rutaCarpeta = Path.Combine("Archivos", carpeta);
                if (Directory.Exists(rutaCarpeta))
                {
                    Directory.Delete(rutaCarpeta, true);
                }
            }
        }

        [Fact]
        public async Task ObtenerArchivoAsync_ArchivoNoExiste_RetornaFallo()
        {
            var resultado = await _sut.ObtenerArchivoAsync(Path.Combine("Archivos", "no", "existe.bin"));

            Assert.False(resultado.Success);
            Assert.Contains("no encontrado", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ObtenerArchivoAsync_ArchivoExiste_RetornaContenido()
        {
            var carpeta = Guid.NewGuid().ToString("N");
            var archivo = Path.Combine("Archivos", carpeta, "original.bin");
            Directory.CreateDirectory(Path.GetDirectoryName(archivo)!);
            var contenido = new byte[] { 5, 6, 7, 8 };
            await File.WriteAllBytesAsync(archivo, contenido);

            try
            {
                var resultado = await _sut.ObtenerArchivoAsync(archivo);

                Assert.True(resultado.Success);
                Assert.Equal(contenido, resultado.Data);
            }
            finally
            {
                Directory.Delete(Path.Combine("Archivos", carpeta), true);
            }
        }

        [Fact]
        public async Task ConvertirBase64AArchivoAsync_EntradaInvalida_RetornaFallo()
        {
            var resultado = await _sut.ConvertirBase64AArchivoAsync("@@@", "archivo.bin", "base64");

            Assert.False(resultado.Success);
            Assert.Contains("convertir", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task EliminarArchivoAsync_ArchivoPresente_LoBorra()
        {
            var carpeta = Guid.NewGuid().ToString("N");
            var archivo = Path.Combine("Archivos", carpeta, "a_eliminar.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(archivo)!);
            await File.WriteAllTextAsync(archivo, "contenido");

            var resultado = await _sut.EliminarArchivoAsync(archivo);

            Assert.True(resultado.Success);
            Assert.True(resultado.Data);
            Assert.False(File.Exists(archivo));

            Directory.Delete(Path.Combine("Archivos", carpeta), true);
        }
    }
}
