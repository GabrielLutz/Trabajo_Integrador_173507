using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Llamado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Services
{
    /// <summary>
    /// Servicio para consultar departamentos disponibles en los llamados (RF-03).
    /// </summary>
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa el servicio de departamentos.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo para acceso a repositorios</param>
        public DepartamentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtiene los departamentos activos habilitados para inscripciones.
        /// Implementa RF-03 (visualización de llamados con sus departamentos).
        /// </summary>
        /// <returns>
        /// Respuesta con la lista de departamentos activos.
        /// </returns>
        public async Task<ApiResponseDto<List<DepartamentoDto>>> ObtenerDepartamentosActivosAsync()
        {
            try
            {
                var departamentos = await _unitOfWork.Departamentos.GetActivosAsync();

                var response = departamentos.Select(d => new DepartamentoDto
                {
                    Id = d.Id,
                    Nombre = d.Nombre,
                    Codigo = d.Codigo
                }).ToList();

                return new ApiResponseDto<List<DepartamentoDto>>
                {
                    Success = true,
                    Message = "Departamentos obtenidos exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<DepartamentoDto>>
                {
                    Success = false,
                    Message = "Error al obtener departamentos",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Obtiene la información de un departamento por su ID.
        /// </summary>
        /// <param name="departamentoId">Identificador del departamento.</param>
        /// <returns>
        /// Respuesta con los datos del departamento o error si no existe.
        /// </returns>
        public async Task<ApiResponseDto<DepartamentoDto>> ObtenerDepartamentoPorIdAsync(int departamentoId)
        {
            try
            {
                var departamento = await _unitOfWork.Departamentos.GetByIdAsync(departamentoId);

                if (departamento == null)
                {
                    return new ApiResponseDto<DepartamentoDto>
                    {
                        Success = false,
                        Message = "Departamento no encontrado"
                    };
                }

                var response = new DepartamentoDto
                {
                    Id = departamento.Id,
                    Nombre = departamento.Nombre,
                    Codigo = departamento.Codigo
                };

                return new ApiResponseDto<DepartamentoDto>
                {
                    Success = true,
                    Message = "Departamento obtenido exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<DepartamentoDto>
                {
                    Success = false,
                    Message = "Error al obtener departamento",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Obtiene los departamentos asociados a un llamado específico.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta con la lista de departamentos vinculados al llamado.
        /// </returns>
        public async Task<ApiResponseDto<List<DepartamentoLlamadoDto>>> ObtenerDepartamentosPorLlamadoAsync(int llamadoId)
        {
            try
            {
                var llamado = await _unitOfWork.Llamados.GetByIdWithDepartamentosAsync(llamadoId);

                if (llamado == null)
                {
                    return new ApiResponseDto<List<DepartamentoLlamadoDto>>
                    {
                        Success = false,
                        Message = "Llamado no encontrado"
                    };
                }

                var response = llamado.LlamadoDepartamentos.Select(ld => new DepartamentoLlamadoDto
                {
                    DepartamentoId = ld.DepartamentoId,
                    Nombre = ld.Departamento.Nombre,
                    Codigo = ld.Departamento.Codigo,
                    CantidadPuestos = ld.CantidadPuestos
                }).ToList();

                return new ApiResponseDto<List<DepartamentoLlamadoDto>>
                {
                    Success = true,
                    Message = "Departamentos del llamado obtenidos exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<DepartamentoLlamadoDto>>
                {
                    Success = false,
                    Message = "Error al obtener departamentos del llamado",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Valida si un departamento pertenece a un llamado.
        /// </summary>
        /// <param name="departamentoId">Identificador del departamento.</param>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta que indica si la combinación es válida.
        /// </returns>
        public async Task<ApiResponseDto<bool>> ValidarDepartamentoEnLlamadoAsync(int departamentoId, int llamadoId)
        {
            try
            {
                var existe = await _unitOfWork.Departamentos.ExistsInLlamado(departamentoId, llamadoId);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = existe,
                    Message = existe ? "Departamento válido para el llamado" : "Departamento no disponible"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al validar departamento",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
