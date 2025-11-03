using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Postulante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Services
{
    public class PostulanteService : IPostulanteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidacionService _validacionService;

        public PostulanteService(IUnitOfWork unitOfWork, IValidacionService validacionService)
        {
            _unitOfWork = unitOfWork;
            _validacionService = validacionService;
        }

        public async Task<ApiResponseDto<PostulanteResponseDto>> ObtenerPostulantePorIdAsync(int postulanteId)
        {
            try
            {
                var postulante = await _unitOfWork.Postulantes.GetByIdAsync(postulanteId);

                if (postulante == null)
                {
                    return new ApiResponseDto<PostulanteResponseDto>
                    {
                        Success = false,
                        Message = "Postulante no encontrado",
                        Errors = new List<string> { "No existe un postulante con el ID especificado" }
                    };
                }

                var datosCompletados = !string.IsNullOrEmpty(postulante.Nombre) &&
                                      !string.IsNullOrEmpty(postulante.Apellido) &&
                                      !string.IsNullOrEmpty(postulante.CedulaIdentidad);

                var response = new PostulanteResponseDto
                {
                    Id = postulante.Id,
                    Nombre = postulante.Nombre,
                    Apellido = postulante.Apellido,
                    Email = postulante.Email,
                    CedulaIdentidad = postulante.CedulaIdentidad,
                    FechaNacimiento = postulante.FechaNacimiento,
                    Genero = postulante.Genero,
                    GeneroOtro = postulante.GeneroOtro,
                    Celular = postulante.Celular,
                    Telefono = postulante.Telefono,
                    Domicilio = postulante.Domicilio,
                    DatosCompletados = datosCompletados,
                };

                return new ApiResponseDto<PostulanteResponseDto>
                {
                    Success = true,
                    Message = "Postulante obtenido exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<PostulanteResponseDto>
                {
                    Success = false,
                    Message = "Error al obtener el postulante",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponseDto<PostulanteResponseDto>> CompletarDatosPersonalesAsync(
            int postulanteId,
            PostulanteDatosPersonalesDto datosPersonales)
        {
            try
            {
                var validacionCedula = _validacionService.ValidarCedulaIdentidad(datosPersonales.CedulaIdentidad);
                if (!validacionCedula.Success)
                    return new ApiResponseDto<PostulanteResponseDto> { Success = false, Errors = validacionCedula.Errors };

                var validacionEmail = _validacionService.ValidarEmail(datosPersonales.Email);
                if (!validacionEmail.Success)
                    return new ApiResponseDto<PostulanteResponseDto> { Success = false, Errors = validacionEmail.Errors };

                var validacionEdad = _validacionService.ValidarEdadMinima(datosPersonales.FechaNacimiento);
                if (!validacionEdad.Success)
                    return new ApiResponseDto<PostulanteResponseDto> { Success = false, Errors = validacionEdad.Errors };

                var postulante = await _unitOfWork.Postulantes.GetByIdAsync(postulanteId);
                if (postulante == null)
                {
                    return new ApiResponseDto<PostulanteResponseDto>
                    {
                        Success = false,
                        Message = "Postulante no encontrado"
                    };
                }

                if (postulante.CedulaIdentidad != datosPersonales.CedulaIdentidad)
                {
                    var existeCedula = await _unitOfWork.Postulantes.ExistsByCedulaAsync(datosPersonales.CedulaIdentidad);
                    if (existeCedula)
                    {
                        return new ApiResponseDto<PostulanteResponseDto>
                        {
                            Success = false,
                            Message = "La cédula ya está registrada",
                            Errors = new List<string> { "Ya existe un postulante con esta cédula" }
                        };
                    }
                }

                postulante.Nombre = datosPersonales.Nombre;
                postulante.Apellido = datosPersonales.Apellido;
                postulante.FechaNacimiento = datosPersonales.FechaNacimiento;
                postulante.CedulaIdentidad = datosPersonales.CedulaIdentidad;
                postulante.Genero = datosPersonales.Genero;
                postulante.GeneroOtro = datosPersonales.GeneroOtro;
                postulante.Email = datosPersonales.Email;
                postulante.Celular = datosPersonales.Celular;
                postulante.Telefono = datosPersonales.Telefono;
                postulante.Domicilio = datosPersonales.Domicilio;

                await _unitOfWork.Postulantes.UpdateDatosPersonalesAsync(postulante);
                await _unitOfWork.SaveChangesAsync();

                var response = new PostulanteResponseDto
                {
                    Id = postulante.Id,
                    Nombre = postulante.Nombre,
                    Apellido = postulante.Apellido,
                    Email = postulante.Email,
                    CedulaIdentidad = postulante.CedulaIdentidad,
                    DatosCompletados = true
                };

                return new ApiResponseDto<PostulanteResponseDto>
                {
                    Success = true,
                    Message = "Datos personales actualizados exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<PostulanteResponseDto>
                {
                    Success = false,
                    Message = "Error al actualizar datos personales",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponseDto<bool>> ValidarCedulaDisponibleAsync(string cedulaIdentidad)
        {
            try
            {
                var existe = await _unitOfWork.Postulantes.ExistsByCedulaAsync(cedulaIdentidad);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = !existe,
                    Message = existe ? "Cédula ya registrada" : "Cédula disponible"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al validar cédula",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponseDto<bool>> ValidarEmailDisponibleAsync(string email)
        {
            try
            {
                var existe = await _unitOfWork.Postulantes.ExistsByEmailAsync(email);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = !existe,
                    Message = existe ? "Email ya registrado" : "Email disponible"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al validar email",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
