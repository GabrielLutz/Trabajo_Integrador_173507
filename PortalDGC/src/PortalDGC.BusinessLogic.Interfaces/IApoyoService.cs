using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Inscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface IApoyoService
    {
        Task<ApiResponseDto<List<ApoyoSolicitadoResponseDto>>> SolicitarApoyosAsync(int inscripcionId, List<ApoyoSolicitadoDto> apoyosDto);
        Task<ApiResponseDto<List<ApoyoSolicitadoResponseDto>>> ObtenerApoyosPorInscripcionAsync(int inscripcionId);
    }
}
