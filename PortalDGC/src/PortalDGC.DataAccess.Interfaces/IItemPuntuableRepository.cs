using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de ítems puntuables utilizados para evaluar méritos (RF-14).
    /// </summary>
    public interface IItemPuntuableRepository : IRepository<ItemPuntuable>
    {
        /// <summary>
        /// Obtiene los ítems puntuables definidos para un llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Colección de <see cref="ItemPuntuable"/> vigentes para el llamado.
        /// </returns>
        Task<IEnumerable<ItemPuntuable>> GetByLlamadoIdAsync(int llamadoId);

        /// <summary>
        /// Obtiene un ítem puntuable específico.
        /// </summary>
        /// <param name="itemId">Identificador del ítem.</param>
        /// <returns>
        /// Instancia de <see cref="ItemPuntuable"/> o <c>null</c> si no existe.
        /// </returns>
        Task<ItemPuntuable?> GetByIdAsync(int itemId);

        /// <summary>
        /// Filtra los ítems por categoría (formación, experiencia, etc.).
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <param name="categoria">Nombre de la categoría a filtrar.</param>
        /// <returns>
        /// Colección de <see cref="ItemPuntuable"/> que cumplen con la categoría.
        /// </returns>
        Task<IEnumerable<ItemPuntuable>> GetByCategoriaAsync(int llamadoId, string categoria);
    }
}
