using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Tribunal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    /// <summary>
    /// Servicio utilizado por el tribunal para evaluar inscripciones, calificar pruebas y gestionar ordenamientos (RF-11 a RF-15).
    /// </summary>
    public interface ITribunalService
    {
        /// <summary>
        /// Obtiene la lista de inscripciones pendientes de evaluación para un llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta con las inscripciones a evaluar.
        /// </returns>
        Task<ApiResponseDto<List<InscripcionParaEvaluarDto>>> ObtenerInscripcionesParaEvaluarAsync(int llamadoId);

        /// <summary>
        /// Obtiene el detalle de evaluación de una inscripción específica.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Respuesta con el detalle de evaluaciones y méritos.
        /// </returns>
        Task<ApiResponseDto<DetalleEvaluacionDto>> ObtenerDetalleEvaluacionAsync(int inscripcionId);

        /// <summary>
        /// Obtiene estadísticas generales del tribunal para un llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta con estadísticas agregadas.
        /// </returns>
        Task<ApiResponseDto<EstadisticasTribunalDto>> ObtenerEstadisticasAsync(int llamadoId);

        /// <summary>
        /// Obtiene las pruebas configuradas para el llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta con las pruebas previstas.
        /// </returns>
        Task<ApiResponseDto<List<PruebaDto>>> ObtenerPruebasDelLlamadoAsync(int llamadoId);

        /// <summary>
        /// Registra la calificación de una prueba.
        /// </summary>
        /// <param name="dto">Datos de calificación.</param>
        /// <returns>
        /// Respuesta con la evaluación registrada.
        /// </returns>
        Task<ApiResponseDto<EvaluacionPruebaDto>> CalificarPruebaAsync(CalificarPruebaDto dto);

        /// <summary>
        /// Valora un mérito individual.
        /// </summary>
        /// <param name="dto">Datos de valoración.</param>
        /// <returns>
        /// Respuesta con la evaluación del mérito.
        /// </returns>
        Task<ApiResponseDto<EvaluacionMeritoDto>> ValorarMeritoAsync(ValorarMeritoDto dto);

        /// <summary>
        /// Valora múltiples méritos de una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <param name="meritos">Colección de méritos a valorar.</param>
        /// <returns>
        /// Respuesta con las evaluaciones resultantes.
        /// </returns>
        Task<ApiResponseDto<List<EvaluacionMeritoDto>>> ValorarMeritosAsync(int inscripcionId, List<ValorarMeritoDto> meritos);

        /// <summary>
        /// Genera un ordenamiento a partir de las evaluaciones registradas.
        /// </summary>
        /// <param name="dto">Parámetros para la generación del ordenamiento.</param>
        /// <returns>
        /// Respuesta con el resultado de la generación.
        /// </returns>
        Task<ApiResponseDto<ResultadoGeneracionOrdenamientoDto>> GenerarOrdenamientoAsync(GenerarOrdenamientoDto dto);

        /// <summary>
        /// Obtiene los ordenamientos generados para un llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta con la lista de ordenamientos.
        /// </returns>
        Task<ApiResponseDto<List<OrdenamientoDto>>> ObtenerOrdenamientosAsync(int llamadoId);

        /// <summary>
        /// Obtiene el detalle de un ordenamiento específico.
        /// </summary>
        /// <param name="ordenamientoId">Identificador del ordenamiento.</param>
        /// <returns>
        /// Respuesta con las posiciones y datos relevantes.
        /// </returns>
        Task<ApiResponseDto<OrdenamientoDetalleDto>> ObtenerDetalleOrdenamientoAsync(int ordenamientoId);

        /// <summary>
        /// Publica un ordenamiento para dejarlo disponible externamente.
        /// </summary>
        /// <param name="ordenamientoId">Identificador del ordenamiento.</param>
        /// <returns>
        /// Respuesta indicando el resultado de la publicación.
        /// </returns>
        Task<ApiResponseDto<bool>> PublicarOrdenamientoAsync(int ordenamientoId);
    }
}
