using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Inscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface IRequisitoService
    {
        Task<ApiResponseDto<List<RequisitoPostulanteResponseDto>>> RegistrarRequisitosAsync(int inscripcionId, List<RequisitoPostulanteDto> requisitosDto);
        Task<ApiResponseDto<List<RequisitoPostulanteResponseDto>>> ObtenerRequisitosPorInscripcionAsync(int inscripcionId);
        Task<ApiResponseDto<bool>> ValidarRequisitosObligatoriosAsync(int inscripcionId);
    }
}
