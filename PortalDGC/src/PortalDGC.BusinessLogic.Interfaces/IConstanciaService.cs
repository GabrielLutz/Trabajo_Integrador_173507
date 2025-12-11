using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Constancia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    /// <summary>
    /// Servicio para gestionar constancias/documentos de respaldo (RF-06).
    /// </summary>
    public interface IConstanciaService
    {
        /// <summary>
        /// Sube y registra una constancia para un postulante.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <param name="constanciaDto">Datos y contenido de la constancia.</param>
        /// <returns>
        /// Respuesta con la constancia persistida.
        /// </returns>
        Task<ApiResponseDto<ConstanciaResponseDto>> SubirConstanciaAsync(int postulanteId, SubirConstanciaDto constanciaDto);

        /// <summary>
        /// Obtiene todas las constancias registradas por un postulante.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <returns>
        /// Respuesta con la lista de constancias.
        /// </returns>
        Task<ApiResponseDto<List<ConstanciaResponseDto>>> ObtenerConstanciasPorPostulanteAsync(int postulanteId);

        /// <summary>
        /// Obtiene una constancia puntual por su identificador.
        /// </summary>
        /// <param name="constanciaId">Identificador de la constancia.</param>
        /// <returns>
        /// Respuesta con la constancia encontrada o error si no existe.
        /// </returns>
        Task<ApiResponseDto<ConstanciaResponseDto>> ObtenerConstanciaPorIdAsync(int constanciaId);

        /// <summary>
        /// Valida formalmente una constancia.
        /// </summary>
        /// <param name="constanciaId">Identificador de la constancia.</param>
        /// <returns>
        /// Respuesta indicando si la validación fue exitosa.
        /// </returns>
        Task<ApiResponseDto<bool>> ValidarConstanciaAsync(int constanciaId);

        /// <summary>
        /// Descarga el archivo de una constancia.
        /// </summary>
        /// <param name="constanciaId">Identificador de la constancia.</param>
        /// <returns>
        /// Respuesta con el contenido binario a entregar.
        /// </returns>
        Task<ApiResponseDto<byte[]>> DescargarConstanciaAsync(int constanciaId);
    }
}
