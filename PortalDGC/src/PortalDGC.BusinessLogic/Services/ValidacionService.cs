using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Services
{
    public class ValidacionService : IValidacionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponseDto<bool> ValidarCedulaIdentidad(string cedulaIdentidad)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(cedulaIdentidad))
            {
                errores.Add("La cédula de identidad es requerida");
            }
            else
            {
                var regex = new Regex(@"^\d{1}\.\d{3}\.\d{3}-\d{1}$|^\d{7,8}$");
                if (!regex.IsMatch(cedulaIdentidad))
                {
                    errores.Add("Formato de cédula inválido");
                }
            }

            return new ApiResponseDto<bool>
            {
                Success = errores.Count == 0,
                Data = errores.Count == 0,
                Message = errores.Count == 0 ? "Cédula válida" : "Cédula inválida",
                Errors = errores
            };
        }

        public ApiResponseDto<bool> ValidarEmail(string email)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(email))
            {
                errores.Add("El email es requerido");
            }
            else
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!regex.IsMatch(email))
                {
                    errores.Add("Formato de email inválido");
                }
            }

            return new ApiResponseDto<bool>
            {
                Success = errores.Count == 0,
                Data = errores.Count == 0,
                Message = errores.Count == 0 ? "Email válido" : "Email inválido",
                Errors = errores
            };
        }

        public ApiResponseDto<bool> ValidarTelefono(string telefono)
        {
            var errores = new List<string>();

            if (!string.IsNullOrWhiteSpace(telefono))
            {
                var regex = new Regex(@"^0\d{8}$");
                if (!regex.IsMatch(telefono))
                {
                    errores.Add("Formato de teléfono inválido");
                }
            }

            return new ApiResponseDto<bool>
            {
                Success = errores.Count == 0,
                Data = errores.Count == 0,
                Message = errores.Count == 0 ? "Teléfono válido" : "Teléfono inválido",
                Errors = errores
            };
        }

        public ApiResponseDto<bool> ValidarEdadMinima(DateTime fechaNacimiento, int edadMinima = 18)
        {
            var errores = new List<string>();
            var edad = DateTime.Now.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > DateTime.Now.AddYears(-edad))
            {
                edad--;
            }

            if (edad < edadMinima)
            {
                errores.Add($"Debe tener al menos {edadMinima} años");
            }

            return new ApiResponseDto<bool>
            {
                Success = errores.Count == 0,
                Data = errores.Count == 0,
                Message = errores.Count == 0 ? "Edad válida" : "Edad inválida",
                Errors = errores
            };
        }

        public ApiResponseDto<bool> ValidarFechaRango(DateTime fecha, DateTime fechaInicio, DateTime fechaFin)
        {
            var errores = new List<string>();

            if (fecha < fechaInicio || fecha > fechaFin)
            {
                errores.Add("La fecha está fuera del rango permitido");
            }

            return new ApiResponseDto<bool>
            {
                Success = errores.Count == 0,
                Data = errores.Count == 0,
                Message = errores.Count == 0 ? "Fecha válida" : "Fecha inválida",
                Errors = errores
            };
        }

        public async Task<ApiResponseDto<bool>> ValidarLlamadoAbierto(int llamadoId)
        {
            try
            {
                var abierto = await _unitOfWork.Llamados.IsLlamadoAbierto(llamadoId);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = abierto,
                    Message = abierto ? "Llamado abierto" : "Llamado cerrado o no disponible"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al validar llamado",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponseDto<bool>> ValidarPostulanteCompletoDatos(int postulanteId)
        {
            try
            {
                var postulante = await _unitOfWork.Postulantes.GetByIdAsync(postulanteId);

                if (postulante == null)
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Postulante no encontrado"
                    };
                }

                var completo = !string.IsNullOrEmpty(postulante.Nombre) &&
                              !string.IsNullOrEmpty(postulante.Apellido) &&
                              !string.IsNullOrEmpty(postulante.CedulaIdentidad) &&
                              !string.IsNullOrEmpty(postulante.Email) &&
                              !string.IsNullOrEmpty(postulante.Celular) &&
                              !string.IsNullOrEmpty(postulante.Domicilio);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = completo,
                    Message = completo ? "Datos completos" : "Faltan datos personales por completar"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al validar postulante",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
