using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de evaluaciones de méritos realizadas por el tribunal (RF-14).
    /// </summary>
    public interface IEvaluacionMeritoRepository : IRepository<EvaluacionMerito>
    {
        /// <summary>
        /// Obtiene las evaluaciones de méritos asociadas a una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Colección de <see cref="EvaluacionMerito"/> evaluadas por el tribunal.
        /// </returns>
        Task<IEnumerable<EvaluacionMerito>> GetByInscripcionIdAsync(int inscripcionId);

        /// <summary>
        /// Obtiene la evaluación vinculada a un mérito específico.
        /// </summary>
        /// <param name="meritoPostulanteId">Identificador del mérito presentado por el postulante.</param>
        /// <returns>
        /// Instancia de <see cref="EvaluacionMerito"/> o <c>null</c> si no fue evaluado.
        /// </returns>
        Task<EvaluacionMerito?> GetByMeritoIdAsync(int meritoPostulanteId);

        /// <summary>
        /// Calcula el puntaje total de méritos evaluados para una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Valor decimal con el puntaje consolidado.
        /// </returns>
        Task<decimal> GetPuntajeTotalMeritosAsync(int inscripcionId);
    }
}
