using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de ordenamientos preliminares/definitivos generados por el tribunal (RF-15).
    /// </summary>
    public interface IOrdenamientoRepository : IRepository<Ordenamiento>
    {
        /// <summary>
        /// Obtiene un ordenamiento con sus posiciones.
        /// </summary>
        /// <param name="id">Identificador del ordenamiento.</param>
        /// <returns>
        /// Instancia de <see cref="Ordenamiento"/> con posiciones o <c>null</c> si no existe.
        /// </returns>
        Task<Ordenamiento?> GetByIdWithPosicionesAsync(int id);

        /// <summary>
        /// Lista los ordenamientos creados para un llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Colección de <see cref="Ordenamiento"/> asociados al llamado.
        /// </returns>
        Task<IEnumerable<Ordenamiento>> GetByLlamadoIdAsync(int llamadoId);

        /// <summary>
        /// Obtiene el ordenamiento definitivo por tipo (general, discapacidad, etc.).
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <param name="tipo">Tipo de ordenamiento (general, discapacidad, afrodescendiente, etc.).</param>
        /// <returns>
        /// Instancia de <see cref="Ordenamiento"/> definitiva o <c>null</c> si no existe.
        /// </returns>
        Task<Ordenamiento?> GetDefinitivoByLlamadoAndTipoAsync(int llamadoId, string tipo);
    }
}
