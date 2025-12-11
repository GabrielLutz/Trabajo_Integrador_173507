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
    /// <summary>
    /// Servicio de negocio para gestión de postulantes.
    /// Implementa requerimientos funcionales RF-01 y RF-02.
    /// </summary>
    public class PostulanteService : IPostulanteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidacionService _validacionService;

        /// <summary>
        /// Constructor del servicio de postulantes.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo para acceso a datos</param>
        /// <param name="validacionService">Servicio de validaciones de negocio</param>
        public PostulanteService(IUnitOfWork unitOfWork, IValidacionService validacionService)
        {
            _unitOfWork = unitOfWork;
            _validacionService = validacionService;
        }

        /// <summary>
        /// Obtiene los datos completos de un postulante por su ID y verifica completitud de datos.
        /// Implementa RF-01: Visualización de datos del postulante.
        /// </summary>
        /// <param name="postulanteId">Identificador único del postulante</param>
        /// <returns>
        /// ApiResponseDto con PostulanteResponseDto que incluye:
        /// - Datos personales completos del postulante
        /// - DatosCompletados: indica si el postulante completó todos los datos obligatorios (Nombre, Apellido, Cédula)
        /// </returns>
        /// <exception cref="Exception">Cuando ocurre un error en la consulta a la base de datos</exception>
        /// <remarks>
        /// Este método verifica que los campos obligatorios estén completos:
        /// - Nombre
        /// - Apellido
        /// - Cédula de Identidad
        /// El flag DatosCompletados determina si el postulante puede realizar inscripciones.
        /// </remarks>
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

        /// <summary>
        /// Actualiza los datos personales del postulante con validaciones de negocio.
        /// Implementa RF-02: Actualización de datos personales del postulante.
        /// </summary>
        /// <param name="postulanteId">Identificador único del postulante</param>
        /// <param name="datosPersonales">DTO con los nuevos datos personales a actualizar</param>
        /// <returns>
        /// ApiResponseDto con PostulanteResponseDto actualizado.
        /// Success = true si la actualización fue exitosa.
        /// Success = false si:
        /// - El formato de cédula es inválido
        /// - El email no tiene formato válido
        /// - La edad es menor a 18 años
        /// - La cédula ya está registrada por otro postulante
        /// - El postulante no existe
        /// </returns>
        /// <exception cref="Exception">Cuando ocurre un error en la transacción de actualización</exception>
        /// <remarks>
        /// Validaciones aplicadas (RF-20):
        /// - ValidarCedulaIdentidad: formato de cédula uruguaya válido
        /// - ValidarEmail: formato de email correcto
        /// - ValidarEdadMinima: edad >= 18 años
        /// - Unicidad de cédula si se está modificando
        /// 
        /// Campos actualizables:
        /// - Nombre, Apellido
        /// - FechaNacimiento
        /// - CedulaIdentidad (verificando unicidad)
        /// - Género y GeneroOtro
        /// - Email, Celular, Teléfono
        /// - Domicilio
        /// </remarks>
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

        /// <summary>
        /// Verifica si una cédula de identidad está disponible para registro.
        /// Implementa RF-20: Validación de unicidad de cédula.
        /// </summary>
        /// <param name="cedulaIdentidad">Cédula de identidad uruguaya sin puntos ni guiones</param>
        /// <returns>
        /// ApiResponseDto con bool indicando:
        /// - true: la cédula está disponible (no existe en el sistema)
        /// - false: la cédula ya está registrada por otro postulante
        /// </returns>
        /// <exception cref="Exception">Cuando ocurre un error en la consulta a la base de datos</exception>
        /// <remarks>
        /// Este método es utilizado para validación en tiempo real durante el registro
        /// y evita duplicación de postulantes en el sistema.
        /// </remarks>
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

        /// <summary>
        /// Verifica si un email está disponible para registro.
        /// </summary>
        /// <param name="email">Dirección de correo electrónico a validar</param>
        /// <returns>
        /// ApiResponseDto con bool indicando:
        /// - true: el email está disponible (no existe en el sistema)
        /// - false: el email ya está registrado por otro postulante
        /// </returns>
        /// <exception cref="Exception">Cuando ocurre un error en la consulta a la base de datos</exception>
        /// <remarks>
        /// Este método es utilizado para validación en tiempo real durante el registro
        /// y evita duplicación de emails en el sistema.
        /// </remarks>
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
