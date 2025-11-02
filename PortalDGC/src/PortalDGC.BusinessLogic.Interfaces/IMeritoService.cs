using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Inscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface IMeritoService
    {
        Task<ApiResponseDto<List<MeritoPostulanteResponseDto>>> RegistrarMeritosAsync(int inscripcionId, List<MeritoPostulanteDto> meritosDto);
        Task<ApiResponseDto<List<MeritoPostulanteResponseDto>>> ObtenerMeritosPorInscripcionAsync(int inscripcionId);
        Task<ApiResponseDto<decimal>> CalcularPuntajeTotalMeritosAsync(int inscripcionId);
        Task<ApiResponseDto<MeritoPostulanteResponseDto>> VerificarMeritoAsync(int meritoId, decimal puntajeObtenido);
    }
}
