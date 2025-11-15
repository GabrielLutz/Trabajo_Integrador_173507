using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Domain.Entities;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Constancia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Services
{
    /// <summary>
    /// Servicio de negocio para la gestión de constancias y documentación respaldatoria.
    /// Implementa el requerimiento RF-06 (gestión de constancias del postulante).
    /// </summary>
    public class ConstanciaService : IConstanciaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArchivoService _archivoService;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de constancias.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo para operaciones transaccionales</param>
        /// <param name="archivoService">Servicio encargado del almacenamiento de archivos</param>
        public ConstanciaService(IUnitOfWork unitOfWork, IArchivoService archivoService)
        {
            _unitOfWork = unitOfWork;
            _archivoService = archivoService;
        }

        /// <summary>
        /// Sube una constancia digital del postulante validando formato y persistiendo el archivo.
        /// Implementa RF-06: carga de constancias en formato PDF o imagen.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante propietario de la constancia</param>
        /// <param name="constanciaDto">DTO con nombre de archivo, tipo y contenido binario</param>
        /// <returns>
        /// ApiResponseDto con ConstanciaResponseDto recién creada, incluyendo metadata almacenada.
        /// Success = false cuando:
        /// - El postulante no existe
        /// - El archivo no tiene uno de los formatos permitidos (.pdf, .jpg, .jpeg, .png)
        /// </returns>
        /// <exception cref="Exception">Propaga errores generados durante la transacción de guardado</exception>
        /// <remarks>
        /// El archivo se valida mediante IArchivoService.ValidarTipoArchivo y luego se almacena
        /// junto con su metadata. El campo Validado se inicializa en false hasta revisión manual.
        /// </remarks>
        public async Task<ApiResponseDto<ConstanciaResponseDto>> SubirConstanciaAsync(
            int postulanteId,
            SubirConstanciaDto constanciaDto)
        {
            try
            {
                var postulante = await _unitOfWork.Postulantes.GetByIdAsync(postulanteId);
                if (postulante == null)
                {
                    return new ApiResponseDto<ConstanciaResponseDto>
                    {
                        Success = false,
                        Message = "Postulante no encontrado"
                    };
                }

                var validacionTipo = _archivoService.ValidarTipoArchivo(
                    constanciaDto.Nombre,
                    new List<string> { ".pdf", ".jpg", ".jpeg", ".png" });

                if (!validacionTipo.Success)
                {
                    return new ApiResponseDto<ConstanciaResponseDto>
                    {
                        Success = false,
                        Message = "Tipo de archivo no válido",
                        Errors = validacionTipo.Errors
                    };
                }

                var constancia = new Constancia
                {
                    PostulanteId = postulanteId,
                    Nombre = constanciaDto.Nombre,
                    Tipo = constanciaDto.Tipo,
                    Archivo = constanciaDto.Archivo,
                    FechaSubida = DateTime.Now,
                    Validado = false
                };

                await _unitOfWork.Constancias.SubirConstanciaAsync(constancia);
                await _unitOfWork.SaveChangesAsync();

                var response = new ConstanciaResponseDto
                {
                    Id = constancia.Id,
                    Nombre = constancia.Nombre,
                    Tipo = constancia.Tipo,
                    Archivo = constancia.Archivo,
                    FechaSubida = constancia.FechaSubida,
                    Validado = constancia.Validado
                };

                return new ApiResponseDto<ConstanciaResponseDto>
                {
                    Success = true,
                    Message = "Constancia subida exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<ConstanciaResponseDto>
                {
                    Success = false,
                    Message = "Error al subir constancia",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Obtiene el listado de constancias cargadas por un postulante específico.
        /// Implementa RF-06: consulta de constancias existentes del postulante.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante</param>
        /// <returns>
        /// ApiResponseDto con lista de ConstanciaResponseDto (nombre, tipo, fecha, estado de validación).
        /// </returns>
        /// <exception cref="Exception">Cuando ocurre un error al consultar la base de datos</exception>
        public async Task<ApiResponseDto<List<ConstanciaResponseDto>>> ObtenerConstanciasPorPostulanteAsync(int postulanteId)
        {
            try
            {
                var constancias = await _unitOfWork.Constancias.GetByPostulanteIdAsync(postulanteId);

                var response = constancias.Select(c => new ConstanciaResponseDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Tipo = c.Tipo,
                    Archivo = c.Archivo,
                    FechaSubida = c.FechaSubida,
                    Validado = c.Validado
                }).ToList();

                return new ApiResponseDto<List<ConstanciaResponseDto>>
                {
                    Success = true,
                    Message = "Constancias obtenidas exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<ConstanciaResponseDto>>
                {
                    Success = false,
                    Message = "Error al obtener constancias",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Obtiene una constancia específica por su identificador.
        /// </summary>
        /// <param name="constanciaId">Identificador único de la constancia</param>
        /// <returns>
        /// ApiResponseDto con la constancia solicitada o mensaje de no encontrada.
        /// </returns>
        public async Task<ApiResponseDto<ConstanciaResponseDto>> ObtenerConstanciaPorIdAsync(int constanciaId)
        {
            try
            {
                var constancia = await _unitOfWork.Constancias.GetByIdAsync(constanciaId);

                if (constancia == null)
                {
                    return new ApiResponseDto<ConstanciaResponseDto>
                    {
                        Success = false,
                        Message = "Constancia no encontrada"
                    };
                }

                var response = new ConstanciaResponseDto
                {
                    Id = constancia.Id,
                    Nombre = constancia.Nombre,
                    Tipo = constancia.Tipo,
                    Archivo = constancia.Archivo,
                    FechaSubida = constancia.FechaSubida,
                    Validado = constancia.Validado
                };

                return new ApiResponseDto<ConstanciaResponseDto>
                {
                    Success = true,
                    Message = "Constancia obtenida exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<ConstanciaResponseDto>
                {
                    Success = false,
                    Message = "Error al obtener constancia",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Marca una constancia como validada dentro del proceso administrativo.
        /// </summary>
        /// <param name="constanciaId">Identificador de la constancia</param>
        /// <returns>ApiResponseDto indicando si la validación se realizó exitosamente.</returns>
        public async Task<ApiResponseDto<bool>> ValidarConstanciaAsync(int constanciaId)
        {
            try
            {
                var resultado = await _unitOfWork.Constancias.ValidarConstanciaAsync(constanciaId);

                if (!resultado)
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Constancia no encontrada"
                    };
                }

                await _unitOfWork.SaveChangesAsync();

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Constancia validada exitosamente"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al validar constancia",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Descarga el contenido binario de una constancia almacenada.
        /// </summary>
        /// <param name="constanciaId">Identificador de la constancia a descargar</param>
        /// <returns>ApiResponseDto con los bytes del archivo o error si no existe.</returns>
        public async Task<ApiResponseDto<byte[]>> DescargarConstanciaAsync(int constanciaId)
        {
            try
            {
                var constancia = await _unitOfWork.Constancias.GetByIdAsync(constanciaId);

                if (constancia == null)
                {
                    return new ApiResponseDto<byte[]>
                    {
                        Success = false,
                        Message = "Constancia no encontrada"
                    };
                }

                var archivoBytes = await _archivoService.ObtenerArchivoAsync(constancia.Archivo);

                return archivoBytes;
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<byte[]>
                {
                    Success = false,
                    Message = "Error al descargar constancia",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
