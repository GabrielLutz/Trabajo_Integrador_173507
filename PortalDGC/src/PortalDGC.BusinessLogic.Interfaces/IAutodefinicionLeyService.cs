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
    /// Servicio de gestión de autodefinición Ley 19.122 (RF-05 y cupos especiales).
    /// </summary>
    public interface IAutodefinicionLeyService
    {
        /// <summary>
        /// Registra la autodefinición Ley 19.122 para una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <param name="autodefinicionDto">Datos de autodefinición proporcionados por el postulante.</param>
        /// <returns>
        /// Respuesta con la autodefinición almacenada.
        /// </returns>
        Task<ApiResponseDto<AutodefinicionLeyDto>> CrearAutodefinicionAsync(int inscripcionId, AutodefinicionLeyDto autodefinicionDto);

        /// <summary>
        /// Obtiene la autodefinición asociada a una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Respuesta con los datos de autodefinición.
        /// </returns>
        Task<ApiResponseDto<AutodefinicionLeyDto>> ObtenerAutodefinicionPorInscripcionAsync(int inscripcionId);

        /// <summary>
        /// Verifica la disponibilidad de cupos especiales para los colectivos definidos en la ley.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <param name="autodefinicion">Datos de autodefinición del postulante.</param>
        /// <returns>
        /// Respuesta indicando si la inscripción puede utilizar cupo especial.
        /// </returns>
        Task<ApiResponseDto<bool>> VerificarCuposEspecialesAsync(int llamadoId, AutodefinicionLeyDto autodefinicion);
    }
}
