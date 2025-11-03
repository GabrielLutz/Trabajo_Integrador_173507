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
    public class ConstanciaService : IConstanciaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArchivoService _archivoService;

        public ConstanciaService(IUnitOfWork unitOfWork, IArchivoService archivoService)
        {
            _unitOfWork = unitOfWork;
            _archivoService = archivoService;
        }

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
