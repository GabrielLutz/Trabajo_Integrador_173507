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
    /// Servicio para gestionar méritos y su evaluación (RF-14).
    /// </summary>
    public interface IMeritoService
    {
        /// <summary>
        /// Registra los méritos presentados para una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <param name="meritosDto">Colección de méritos declarados.</param>
        /// <returns>
        /// Respuesta con los méritos persistidos.
        /// </returns>
        Task<ApiResponseDto<List<MeritoPostulanteResponseDto>>> RegistrarMeritosAsync(int inscripcionId, List<MeritoPostulanteDto> meritosDto);

        /// <summary>
        /// Obtiene los méritos registrados para una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Respuesta con la lista de méritos.
        /// </returns>
        Task<ApiResponseDto<List<MeritoPostulanteResponseDto>>> ObtenerMeritosPorInscripcionAsync(int inscripcionId);

        /// <summary>
        /// Calcula el puntaje total de méritos.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Respuesta con el puntaje total.
        /// </returns>
        Task<ApiResponseDto<decimal>> CalcularPuntajeTotalMeritosAsync(int inscripcionId);

        /// <summary>
        /// Verifica y actualiza el puntaje obtenido por un mérito específico.
        /// </summary>
        /// <param name="meritoId">Identificador del mérito.</param>
        /// <param name="puntajeObtenido">Puntaje asignado por el tribunal.</param>
        /// <returns>
        /// Respuesta con el mérito actualizado.
        /// </returns>
        Task<ApiResponseDto<MeritoPostulanteResponseDto>> VerificarMeritoAsync(int meritoId, decimal puntajeObtenido);
    }
}
