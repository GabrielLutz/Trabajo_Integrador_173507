using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de evaluaciones de pruebas, base de RF-12.
    /// </summary>
    public interface IEvaluacionPruebaRepository : IRepository<EvaluacionPrueba>
    {
        /// <summary>
        /// Obtiene las evaluaciones de una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Colección de <see cref="EvaluacionPrueba"/> registradas.
        /// </returns>
        Task<IEnumerable<EvaluacionPrueba>> GetByInscripcionIdAsync(int inscripcionId);

        /// <summary>
        /// Obtiene las evaluaciones asociadas a una prueba específica.
        /// </summary>
        /// <param name="pruebaId">Identificador de la prueba.</param>
        /// <returns>
        /// Colección de <see cref="EvaluacionPrueba"/> tomadas sobre la prueba.
        /// </returns>
        Task<IEnumerable<EvaluacionPrueba>> GetByPruebaIdAsync(int pruebaId);

        /// <summary>
        /// Obtiene la evaluación específica de una inscripción en una prueba.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <param name="pruebaId">Identificador de la prueba.</param>
        /// <returns>
        /// Instancia de <see cref="EvaluacionPrueba"/> o <c>null</c> si no existe evaluación.
        /// </returns>
        Task<EvaluacionPrueba?> GetByInscripcionAndPruebaAsync(int inscripcionId, int pruebaId);

        /// <summary>
        /// Calcula el promedio general de una prueba.
        /// </summary>
        /// <param name="pruebaId">Identificador de la prueba.</param>
        /// <returns>
        /// Valor decimal con el promedio de puntajes.
        /// </returns>
        Task<decimal> GetPromedioByPruebaAsync(int pruebaId);

        /// <summary>
        /// Cuenta cuántas inscripciones aprobaron la prueba.
        /// </summary>
        /// <param name="pruebaId">Identificador de la prueba.</param>
        /// <returns>
        /// Cantidad de evaluaciones con resultado aprobatorio.
        /// </returns>
        Task<int> GetCantidadAprobadosAsync(int pruebaId);
    }
}
