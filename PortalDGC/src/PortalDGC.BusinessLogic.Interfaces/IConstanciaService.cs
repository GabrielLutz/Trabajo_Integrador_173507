using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Constancia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface IConstanciaService
    {
        Task<ApiResponseDto<ConstanciaResponseDto>> SubirConstanciaAsync(int postulanteId, SubirConstanciaDto constanciaDto);
        Task<ApiResponseDto<List<ConstanciaResponseDto>>> ObtenerConstanciasPorPostulanteAsync(int postulanteId);
        Task<ApiResponseDto<ConstanciaResponseDto>> ObtenerConstanciaPorIdAsync(int constanciaId);
        Task<ApiResponseDto<bool>> ValidarConstanciaAsync(int constanciaId);
        Task<ApiResponseDto<byte[]>> DescargarConstanciaAsync(int constanciaId);
    }
}
