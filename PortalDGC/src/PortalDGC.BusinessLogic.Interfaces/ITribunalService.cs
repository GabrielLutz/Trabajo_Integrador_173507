using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Tribunal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface ITribunalService
    {
        Task<ApiResponseDto<List<InscripcionParaEvaluarDto>>> ObtenerInscripcionesParaEvaluarAsync(int llamadoId);
        Task<ApiResponseDto<DetalleEvaluacionDto>> ObtenerDetalleEvaluacionAsync(int inscripcionId);
        Task<ApiResponseDto<EstadisticasTribunalDto>> ObtenerEstadisticasAsync(int llamadoId);
        Task<ApiResponseDto<List<PruebaDto>>> ObtenerPruebasDelLlamadoAsync(int llamadoId);
        Task<ApiResponseDto<EvaluacionPruebaDto>> CalificarPruebaAsync(CalificarPruebaDto dto);
        Task<ApiResponseDto<EvaluacionMeritoDto>> ValorarMeritoAsync(ValorarMeritoDto dto);
        Task<ApiResponseDto<List<EvaluacionMeritoDto>>> ValorarMeritosAsync(int inscripcionId, List<ValorarMeritoDto> meritos);
        Task<ApiResponseDto<ResultadoGeneracionOrdenamientoDto>> GenerarOrdenamientoAsync(GenerarOrdenamientoDto dto);
        Task<ApiResponseDto<List<OrdenamientoDto>>> ObtenerOrdenamientosAsync(int llamadoId);
        Task<ApiResponseDto<OrdenamientoDetalleDto>> ObtenerDetalleOrdenamientoAsync(int ordenamientoId);
        Task<ApiResponseDto<bool>> PublicarOrdenamientoAsync(int ordenamientoId);
    }
}
