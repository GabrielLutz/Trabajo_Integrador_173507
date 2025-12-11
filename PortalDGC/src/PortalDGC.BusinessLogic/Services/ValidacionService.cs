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
    /// <summary>
    /// Servicio centralizado de validaciones de negocio (RF-20).
    /// Incluye reglas para cédula uruguaya, edad mínima y requisitos excluyentes.
    /// </summary>
    public class ValidacionService : IValidacionService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa el servicio de validaciones.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo para validaciones que dependen de datos persistidos</param>
        public ValidacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Valida el formato de la cédula uruguaya (con puntos y guion o solo números).
        /// Implementa RF-20: ValidarCedulaUruguayaAsync.
        /// </summary>
        /// <param name="cedulaIdentidad">Cédula de identidad a validar, con o sin separadores.</param>
        /// <returns>
        /// ApiResponseDto que indica si la cédula es válida e incluye mensajes de error cuando corresponde.
        /// </returns>
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

        /// <summary>
        /// Valida el formato de un correo electrónico siguiendo las reglas básicas de RFC 5322.
        /// </summary>
        /// <param name="email">Correo electrónico a evaluar.</param>
        /// <returns>
        /// ApiResponseDto que indica si el correo es válido e incluye mensajes de error cuando el formato es incorrecto.
        /// </returns>
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

        /// <summary>
        /// Valida un número de teléfono uruguayo en formato nacional.
        /// </summary>
        /// <param name="telefono">Número telefónico a validar.</param>
        /// <returns>
        /// ApiResponseDto que indica si el teléfono cumple el patrón esperado y detalla errores encontrados.
        /// </returns>
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

        /// <summary>
        /// Valida que la edad sea igual o superior al mínimo requerido (18 por defecto).
        /// Implementa RF-20: ValidarEdadAsync.
        /// </summary>
        /// <param name="fechaNacimiento">Fecha de nacimiento del postulante.</param>
        /// <param name="edadMinima">Edad mínima permitida para el proceso.</param>
        /// <returns>
        /// ApiResponseDto que indica si se cumple la edad mínima e incluye mensajes descriptivos.
        /// </returns>
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

        /// <summary>
        /// Valida que una fecha dada se encuentre dentro de un rango permitido.
        /// </summary>
        /// <param name="fecha">Fecha a evaluar.</param>
        /// <param name="fechaInicio">Inicio del rango admitido.</param>
        /// <param name="fechaFin">Fin del rango admitido.</param>
        /// <returns>
        /// ApiResponseDto que indica si la fecha está dentro del rango y detalla los errores encontrados.
        /// </returns>
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

        /// <summary>
        /// Verifica si un llamado está dentro del rango de fechas habilitado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado a validar.</param>
        /// <returns>
        /// ApiResponseDto que indica si el llamado se encuentra abierto e incluye mensajes descriptivos.
        /// </returns>
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

        /// <summary>
        /// Valida que un postulante tenga todos los datos obligatorios completos.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante a validar.</param>
        /// <returns>
        /// ApiResponseDto que indica si los datos del postulante están completos e incluye mensajes descriptivos.
        /// </returns>
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
