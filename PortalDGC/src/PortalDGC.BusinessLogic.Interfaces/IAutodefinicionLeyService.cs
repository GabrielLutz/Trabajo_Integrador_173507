using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Inscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface IAutodefinicionLeyService
    {
        Task<ApiResponseDto<AutodefinicionLeyDto>> CrearAutodefinicionAsync(int inscripcionId, AutodefinicionLeyDto autodefinicionDto);
        Task<ApiResponseDto<AutodefinicionLeyDto>> ObtenerAutodefinicionPorInscripcionAsync(int inscripcionId);
        Task<ApiResponseDto<bool>> VerificarCuposEspecialesAsync(int llamadoId, AutodefinicionLeyDto autodefinicion);
    }
}
