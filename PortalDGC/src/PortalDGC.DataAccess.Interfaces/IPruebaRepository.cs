using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de pruebas evaluadas por el tribunal (RF-11 y RF-12).
    /// </summary>
    public interface IPruebaRepository : IRepository<Prueba>
    {
        /// <summary>
        /// Obtiene las pruebas configuradas para un llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Colección de <see cref="Prueba"/> asociadas al llamado.
        /// </returns>
        Task<IEnumerable<Prueba>> GetByLlamadoIdAsync(int llamadoId);

        /// <summary>
        /// Obtiene únicamente las pruebas habilitadas para calificar.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Colección de <see cref="Prueba"/> activas.
        /// </returns>
        Task<IEnumerable<Prueba>> GetPruebasActivasAsync(int llamadoId);

        /// <summary>
        /// Obtiene una prueba con sus evaluaciones asociadas.
        /// </summary>
        /// <param name="pruebaId">Identificador de la prueba.</param>
        /// <returns>
        /// Instancia de <see cref="Prueba"/> con evaluaciones o <c>null</c> si no existe.
        /// </returns>
        Task<Prueba?> GetByIdWithEvaluacionesAsync(int pruebaId);
    }
}
