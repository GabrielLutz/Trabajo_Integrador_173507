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
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartamentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
