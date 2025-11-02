using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Llamado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface IDepartamentoService
    {
        Task<ApiResponseDto<List<DepartamentoDto>>> ObtenerDepartamentosActivosAsync();
        Task<ApiResponseDto<DepartamentoDto>> ObtenerDepartamentoPorIdAsync(int departamentoId);
        Task<ApiResponseDto<List<DepartamentoLlamadoDto>>> ObtenerDepartamentosPorLlamadoAsync(int llamadoId);
        Task<ApiResponseDto<bool>> ValidarDepartamentoEnLlamadoAsync(int departamentoId, int llamadoId);
    }
}
