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
    /// Servicio para gestionar requisitos cargados por postulantes (RF-05).
    /// </summary>
    public interface IRequisitoService
    {
        /// <summary>
        /// Registra los requisitos presentados para una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <param name="requisitosDto">Colección de requisitos declarados.</param>
        /// <returns>
        /// Respuesta con los requisitos persistidos.
        /// </returns>
        Task<ApiResponseDto<List<RequisitoPostulanteResponseDto>>> RegistrarRequisitosAsync(int inscripcionId, List<RequisitoPostulanteDto> requisitosDto);

        /// <summary>
        /// Obtiene los requisitos asociados a una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Respuesta con la lista de requisitos.
        /// </returns>
        Task<ApiResponseDto<List<RequisitoPostulanteResponseDto>>> ObtenerRequisitosPorInscripcionAsync(int inscripcionId);

        /// <summary>
        /// Valida que una inscripción cumpla con los requisitos obligatorios.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Respuesta booleana indicando si se cumplen todos los requisitos.
        /// </returns>
        Task<ApiResponseDto<bool>> ValidarRequisitosObligatoriosAsync(int inscripcionId);
    }
}
