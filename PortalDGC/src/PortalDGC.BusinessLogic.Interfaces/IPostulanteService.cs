using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Postulante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface IPostulanteService
    {
        Task<ApiResponseDto<PostulanteResponseDto>> ObtenerPostulantePorIdAsync(int postulanteId);
        Task<ApiResponseDto<PostulanteResponseDto>> CompletarDatosPersonalesAsync(int postulanteId, PostulanteDatosPersonalesDto datosPersonales);
        Task<ApiResponseDto<bool>> ValidarCedulaDisponibleAsync(string cedulaIdentidad);
        Task<ApiResponseDto<bool>> ValidarEmailDisponibleAsync(string email);
    }
}
