using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.BusinessLogic.Services;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Domain.Entities;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Postulante;
using Xunit;

namespace PortalDGC.Tests.Services
{
    public class PostulanteServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidacionService> _validacionServiceMock;
        private readonly Mock<IPostulanteRepository> _postulanteRepositoryMock;
        private readonly PostulanteService _sut;

        public PostulanteServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validacionServiceMock = new Mock<IValidacionService>();
            _postulanteRepositoryMock = new Mock<IPostulanteRepository>();

            _unitOfWorkMock.SetupGet(u => u.Postulantes).Returns(_postulanteRepositoryMock.Object);
            _sut = new PostulanteService(_unitOfWorkMock.Object, _validacionServiceMock.Object);
        }

        [Fact]
        public async Task ObtenerPostulantePorId_PostulanteExiste_RetornaSuccess()
        {
            var postulanteId = 1;
            var postulante = new Postulante
            {
                Id = postulanteId,
                Nombre = "Juan",
                Apellido = "Pérez",
                CedulaIdentidad = "1.234.567-8",
                Email = "juan@example.com"
            };

            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(postulanteId))
                .ReturnsAsync(postulante);

            var resultado = await _sut.ObtenerPostulantePorIdAsync(postulanteId);

            Assert.True(resultado.Success);
            Assert.NotNull(resultado.Data);
            Assert.Equal("Juan", resultado.Data!.Nombre);
            Assert.Equal("Pérez", resultado.Data.Apellido);
        }

        [Fact]
        public async Task ObtenerPostulantePorId_PostulanteNoExiste_RetornaError()
        {
            var postulanteId = 999;
            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(postulanteId))
                .ReturnsAsync((Postulante?)null);

            var resultado = await _sut.ObtenerPostulantePorIdAsync(postulanteId);

            Assert.False(resultado.Success);
            Assert.Null(resultado.Data);
            Assert.Contains("no encontrado", resultado.Message.ToLower());
        }

        [Fact]
        public async Task CompletarDatosPersonales_DatosValidos_RetornaSuccess()
        {
            var postulanteId = 1;
            var postulante = new Postulante
            {
                Id = postulanteId,
                CedulaIdentidad = "1.234.567-8",
                Email = "juan@example.com"
            };

            var datosPersonales = new PostulanteDatosPersonalesDto
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                FechaNacimiento = new DateTime(1990, 1, 1),
                CedulaIdentidad = "1.234.567-8",
                Genero = "Masculino",
                Email = "juan@example.com",
                Celular = "099123456",
                Domicilio = "Dirección 123"
            };

            _postulanteRepositoryMock
                .Setup(r => r.GetByIdAsync(postulanteId))
                .ReturnsAsync(postulante);

            _postulanteRepositoryMock
                .Setup(r => r.UpdateDatosPersonalesAsync(It.IsAny<Postulante>()))
                .Returns(Task.CompletedTask);

            _validacionServiceMock
                .Setup(v => v.ValidarCedulaIdentidad(It.IsAny<string>()))
                .Returns(new ApiResponseDto<bool> { Success = true, Data = true });

            _validacionServiceMock
                .Setup(v => v.ValidarEmail(It.IsAny<string>()))
                .Returns(new ApiResponseDto<bool> { Success = true, Data = true });

            _validacionServiceMock
                .Setup(v => v.ValidarEdadMinima(It.IsAny<DateTime>(), It.IsAny<int>()))
                .Returns(new ApiResponseDto<bool> { Success = true, Data = true });

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            var resultado = await _sut.CompletarDatosPersonalesAsync(postulanteId, datosPersonales);

            Assert.True(resultado.Success);
            Assert.NotNull(resultado.Data);
            Assert.True(resultado.Data!.DatosCompletados);

            _postulanteRepositoryMock.Verify(r => r.UpdateDatosPersonalesAsync(It.Is<Postulante>(p => p.Nombre == "Juan" && p.Apellido == "Pérez")), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ValidarCedulaDisponible_CedulaDisponible_RetornaTrue()
        {
            var cedula = "1.234.567-8";
            _postulanteRepositoryMock
                .Setup(r => r.ExistsByCedulaAsync(cedula))
                .ReturnsAsync(false);

            var resultado = await _sut.ValidarCedulaDisponibleAsync(cedula);

            Assert.True(resultado.Success);
            Assert.True(resultado.Data);
        }

        [Fact]
        public async Task ValidarCedulaDisponible_CedulaYaExiste_RetornaFalse()
        {
            var cedula = "1.234.567-8";
            _postulanteRepositoryMock
                .Setup(r => r.ExistsByCedulaAsync(cedula))
                .ReturnsAsync(true);

            var resultado = await _sut.ValidarCedulaDisponibleAsync(cedula);

            Assert.True(resultado.Success);
            Assert.False(resultado.Data);
        }
    }
}
