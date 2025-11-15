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
    /// Servicio core de inscripciones que implementa RF-05, RF-07 y RF-08.
    /// </summary>
    public interface IInscripcionService
    {
        /// <summary>
        /// Crea una inscripción para un postulante.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <param name="inscripcionDto">Datos de la inscripción a generar.</param>
        /// <returns>
        /// Respuesta con la inscripción creada.
        /// </returns>
        Task<ApiResponseDto<InscripcionResponseDto>> CrearInscripcionAsync(int postulanteId, CrearInscripcionDto inscripcionDto);

        /// <summary>
        /// Obtiene una inscripción por su identificador.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Respuesta con el detalle de la inscripción.
        /// </returns>
        Task<ApiResponseDto<InscripcionResponseDto>> ObtenerInscripcionPorIdAsync(int inscripcionId);

        /// <summary>
        /// Obtiene las inscripciones realizadas por un postulante.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <returns>
        /// Respuesta con la lista de inscripciones.
        /// </returns>
        Task<ApiResponseDto<List<InscripcionSimpleResponseDto>>> ObtenerInscripcionesPorPostulanteAsync(int postulanteId);

        /// <summary>
        /// Valida si un postulante ya cuenta con una inscripción en un llamado.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta booleana indicando si existe la inscripción.
        /// </returns>
        Task<ApiResponseDto<bool>> ValidarInscripcionExistenteAsync(int postulanteId, int llamadoId);

        /// <summary>
        /// Valida que una inscripción cumpla con los requisitos obligatorios.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Respuesta indicando si se satisfacen los requisitos.
        /// </returns>
        Task<ApiResponseDto<bool>> ValidarRequisitosObligatoriosAsync(int inscripcionId);

        /// <summary>
        /// Calcula el puntaje total consolidado para una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Respuesta con el puntaje total.
        /// </returns>
        Task<ApiResponseDto<decimal>> CalcularPuntajeTotalAsync(int inscripcionId);
    }
}
