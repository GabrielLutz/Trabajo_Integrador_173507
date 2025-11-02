using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Inscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface IInscripcionService
    {
        Task<ApiResponseDto<InscripcionResponseDto>> CrearInscripcionAsync(int postulanteId, CrearInscripcionDto inscripcionDto);
        Task<ApiResponseDto<InscripcionResponseDto>> ObtenerInscripcionPorIdAsync(int inscripcionId);
        Task<ApiResponseDto<List<InscripcionSimpleResponseDto>>> ObtenerInscripcionesPorPostulanteAsync(int postulanteId);
        Task<ApiResponseDto<bool>> ValidarInscripcionExistenteAsync(int postulanteId, int llamadoId);
        Task<ApiResponseDto<bool>> ValidarRequisitosObligatoriosAsync(int inscripcionId);
        Task<ApiResponseDto<decimal>> CalcularPuntajeTotalAsync(int inscripcionId);
    }
}
