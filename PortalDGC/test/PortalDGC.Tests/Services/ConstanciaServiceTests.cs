using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.BusinessLogic.Services;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Domain.Entities;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Constancia;
using Xunit;

namespace PortalDGC.Tests.Services
{
    public class ConstanciaServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IPostulanteRepository> _postulanteRepositoryMock;
        private readonly Mock<IConstanciaRepository> _constanciaRepositoryMock;
        private readonly Mock<IArchivoService> _archivoServiceMock;
        private readonly ConstanciaService _sut;

        public ConstanciaServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _postulanteRepositoryMock = new Mock<IPostulanteRepository>();
            _constanciaRepositoryMock = new Mock<IConstanciaRepository>();
            _archivoServiceMock = new Mock<IArchivoService>();

            _unitOfWorkMock.SetupGet(u => u.Postulantes).Returns(_postulanteRepositoryMock.Object);
            _unitOfWorkMock.SetupGet(u => u.Constancias).Returns(_constanciaRepositoryMock.Object);

            _sut = new ConstanciaService(_unitOfWorkMock.Object, _archivoServiceMock.Object);
        }

        [Fact]
        public async Task SubirConstanciaAsync_PostulanteNoExiste_RetornaFallo()
        {
            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Postulante?)null);

            var resultado = await _sut.SubirConstanciaAsync(1, CrearDto());

            Assert.False(resultado.Success);
            Assert.Contains("no encontrado", resultado.Message, StringComparison.OrdinalIgnoreCase);
            _constanciaRepositoryMock.Verify(r => r.SubirConstanciaAsync(It.IsAny<Constancia>()), Times.Never);
        }

        [Fact]
        public async Task SubirConstanciaAsync_TipoArchivoInvalido_RetornaErrores()
        {
            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Postulante { Id = 5 });

            _archivoServiceMock
                .Setup(s => s.ValidarTipoArchivo(It.IsAny<string>(), It.IsAny<List<string>>()))
                .Returns(new ApiResponseDto<bool> { Success = false, Errors = new List<string> { "Extensión" } });

            var resultado = await _sut.SubirConstanciaAsync(5, CrearDto());

            Assert.False(resultado.Success);
            Assert.Contains("Tipo de archivo", resultado.Message);
            Assert.NotNull(resultado.Errors);
            _constanciaRepositoryMock.Verify(r => r.SubirConstanciaAsync(It.IsAny<Constancia>()), Times.Never);
        }

        [Fact]
        public async Task SubirConstanciaAsync_AlmacenaYRetornaConstancia()
        {
            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Postulante { Id = 3 });

            _archivoServiceMock
                .Setup(s => s.ValidarTipoArchivo(It.IsAny<string>(), It.IsAny<List<string>>()))
                .Returns(new ApiResponseDto<bool> { Success = true, Data = true });

            _constanciaRepositoryMock
                .Setup(r => r.SubirConstanciaAsync(It.IsAny<Constancia>()))
                .ReturnsAsync(new Constancia { Id = 99 });

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            var dto = CrearDto();
            var resultado = await _sut.SubirConstanciaAsync(3, dto);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Equal(dto.Nombre, data.Nombre);
            _constanciaRepositoryMock.Verify(r => r.SubirConstanciaAsync(It.Is<Constancia>(c => c.Nombre == dto.Nombre && c.PostulanteId == 3)), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task SubirConstanciaAsync_Error_RetornaMensajeError()
        {
            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Postulante { Id = 3 });

            _archivoServiceMock
                .Setup(s => s.ValidarTipoArchivo(It.IsAny<string>(), It.IsAny<List<string>>()))
                .Returns(new ApiResponseDto<bool> { Success = true, Data = true });

            _constanciaRepositoryMock
                .Setup(r => r.SubirConstanciaAsync(It.IsAny<Constancia>()))
                .ThrowsAsync(new InvalidOperationException("db"));

            var resultado = await _sut.SubirConstanciaAsync(3, CrearDto());

            Assert.False(resultado.Success);
            Assert.Contains("Error", resultado.Message);
            Assert.NotNull(resultado.Errors);
        }

        [Fact]
        public async Task ObtenerConstanciasPorPostulanteAsync_MapeaEntidadADto()
        {
            var constancias = new List<Constancia>
            {
                new Constancia { Id = 1, Nombre = "Doc", Tipo = "pdf", Archivo = "path", FechaSubida = DateTime.UtcNow, Validado = true }
            };

            _constanciaRepositoryMock
                .Setup(r => r.GetByPostulanteIdAsync(2))
                .ReturnsAsync(constancias);

            var resultado = await _sut.ObtenerConstanciasPorPostulanteAsync(2);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Single(data);
            Assert.Equal(constancias[0].Nombre, data[0].Nombre);
        }

        [Fact]
        public async Task ObtenerConstanciaPorIdAsync_NoExiste_RetornaFallo()
        {
            _constanciaRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Constancia?)null);

            var resultado = await _sut.ObtenerConstanciaPorIdAsync(10);

            Assert.False(resultado.Success);
            Assert.Null(resultado.Data);
            Assert.Contains("no encontrada", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ObtenerConstanciaPorIdAsync_Existe_RetornaDto()
        {
            var constancia = new Constancia { Id = 5, Nombre = "Doc", Tipo = "pdf", Archivo = "file", FechaSubida = DateTime.UtcNow };
            _constanciaRepositoryMock
                .Setup(r => r.GetByIdAsync(constancia.Id))
                .ReturnsAsync(constancia);

            var resultado = await _sut.ObtenerConstanciaPorIdAsync(constancia.Id);

            Assert.True(resultado.Success);
            var data = AssertNotNull(resultado.Data);
            Assert.Equal(constancia.Id, data.Id);
        }

        [Fact]
        public async Task ValidarConstanciaAsync_NoEncontrada_RetornaFallo()
        {
            _constanciaRepositoryMock
                .Setup(r => r.ValidarConstanciaAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            var resultado = await _sut.ValidarConstanciaAsync(4);

            Assert.False(resultado.Success);
            Assert.False(resultado.Data);
            Assert.Contains("no encontrada", resultado.Message, StringComparison.OrdinalIgnoreCase);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task ValidarConstanciaAsync_Valida_RetornaTrue()
        {
            _constanciaRepositoryMock
                .Setup(r => r.ValidarConstanciaAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            var resultado = await _sut.ValidarConstanciaAsync(4);

            Assert.True(resultado.Success);
            Assert.True(resultado.Data);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DescargarConstanciaAsync_NoExiste_RetornaFallo()
        {
            _constanciaRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Constancia?)null);

            var resultado = await _sut.DescargarConstanciaAsync(7);

            Assert.False(resultado.Success);
            Assert.Contains("no encontrada", resultado.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task DescargarConstanciaAsync_Existe_RetornaArchivo()
        {
            var constancia = new Constancia { Id = 8, Archivo = "ruta" };
            _constanciaRepositoryMock
                .Setup(r => r.GetByIdAsync(constancia.Id))
                .ReturnsAsync(constancia);

            var archivoResponse = new ApiResponseDto<byte[]>
            {
                Success = true,
                Data = new byte[] { 1, 2 }
            };

            _archivoServiceMock
                .Setup(s => s.ObtenerArchivoAsync(constancia.Archivo))
                .ReturnsAsync(archivoResponse);

            var resultado = await _sut.DescargarConstanciaAsync(constancia.Id);

            Assert.Same(archivoResponse, resultado);
            _archivoServiceMock.Verify(s => s.ObtenerArchivoAsync(constancia.Archivo), Times.Once);
        }

        private static SubirConstanciaDto CrearDto()
        {
            return new SubirConstanciaDto
            {
                Nombre = "documento.pdf",
                Tipo = "Identidad",
                Archivo = "base64"
            };
        }

        private static T AssertNotNull<T>(T? value) where T : class
        {
            Assert.NotNull(value);
            return value!;
        }
    }
}
