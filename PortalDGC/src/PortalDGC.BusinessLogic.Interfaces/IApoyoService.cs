using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Inscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    /// <summary>
    /// Servicio para gestionar apoyos solicitados por postulantes (RF-05).
    /// </summary>
    public interface IApoyoService
    {
        /// <summary>
        /// Registra los apoyos solicitados para una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <param name="apoyosDto">Colección de apoyos declarados por el postulante.</param>
        /// <returns>
        /// Respuesta con los apoyos persistidos.
        /// </returns>
        Task<ApiResponseDto<List<ApoyoSolicitadoResponseDto>>> SolicitarApoyosAsync(int inscripcionId, List<ApoyoSolicitadoDto> apoyosDto);

        /// <summary>
        /// Obtiene los apoyos solicitados registrados para una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Respuesta con la lista de apoyos.
        /// </returns>
        Task<ApiResponseDto<List<ApoyoSolicitadoResponseDto>>> ObtenerApoyosPorInscripcionAsync(int inscripcionId);
    }
}
