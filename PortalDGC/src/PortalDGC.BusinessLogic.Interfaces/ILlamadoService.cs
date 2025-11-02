using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Llamado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface ILlamadoService
    {
        Task<ApiResponseDto<LlamadoDetalleDto>> ObtenerLlamadoPorIdAsync(int llamadoId);
        Task<ApiResponseDto<List<LlamadoSimpleDto>>> ObtenerLlamadosActivosAsync();
        Task<ApiResponseDto<List<LlamadoSimpleDto>>> ObtenerLlamadosInactivosAsync();
        Task<ApiResponseDto<bool>> ValidarLlamadoDisponibleAsync(int llamadoId);
        Task<ApiResponseDto<List<RequisitoExcluyenteDto>>> ObtenerRequisitosLlamadoAsync(int llamadoId);
        Task<ApiResponseDto<List<ItemPuntuableDto>>> ObtenerItemsPuntuablesLlamadoAsync(int llamadoId);
        Task<ApiResponseDto<List<ApoyoNecesarioDto>>> ObtenerApoyosNecesariosLlamadoAsync(int llamadoId);
    }
}
