using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de posiciones dentro de un ordenamiento (RF-15).
    /// </summary>
    public interface IPosicionOrdenamientoRepository : IRepository<PosicionOrdenamiento>
    {
        /// <summary>
        /// Obtiene las posiciones asociadas a un ordenamiento.
        /// </summary>
        /// <param name="ordenamientoId">Identificador del ordenamiento.</param>
        /// <returns>
        /// Colección de <see cref="PosicionOrdenamiento"/> ordenadas por puntaje/posición.
        /// </returns>
        Task<IEnumerable<PosicionOrdenamiento>> GetByOrdenamientoIdAsync(int ordenamientoId);

        /// <summary>
        /// Registra múltiples posiciones calculadas por el tribunal.
        /// </summary>
        /// <param name="posiciones">Colección de posiciones a persistir.</param>
        Task AddRangeAsync(IEnumerable<PosicionOrdenamiento> posiciones);
    }
}
