using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de llamados públicos utilizado en RF-03 y RF-04 para consultar detalles,
    /// estado y componentes (requisitos, ítems puntuables, apoyos, departamentos).
    /// </summary>
    public interface ILlamadoRepository : IRepository<Llamado>
    {
        /// <summary>
        /// Obtiene un llamado con información general y relaciones principales.
        /// </summary>
        /// <param name="id">Identificador del llamado.</param>
        /// <returns>
        /// Instancia de <see cref="Llamado"/> o <c>null</c> si no existe.
        /// </returns>
        Task<Llamado?> GetByIdWithDetallesAsync(int id);

        /// <summary>
        /// Lista los llamados con vigencia activa para inscripción.
        /// </summary>
        /// <returns>
        /// Colección de <see cref="Llamado"/> que aceptan nuevas inscripciones.
        /// </returns>
        Task<IEnumerable<Llamado>> GetLlamadosActivosAsync();

        /// <summary>
        /// Lista los llamados finalizados o cerrados.
        /// </summary>
        /// <returns>
        /// Colección de <see cref="Llamado"/> que ya no se encuentran vigentes.
        /// </returns>
        Task<IEnumerable<Llamado>> GetLlamadosInactivosAsync();

        /// <summary>
        /// Obtiene un llamado junto a sus requisitos excluyentes.
        /// </summary>
        /// <param name="id">Identificador del llamado.</param>
        /// <returns>
        /// Instancia de <see cref="Llamado"/> con requisitos o <c>null</c> si no existe.
        /// </returns>
        Task<Llamado?> GetByIdWithRequisitosAsync(int id);

        /// <summary>
        /// Obtiene un llamado con los ítems puntuables configurados.
        /// </summary>
        /// <param name="id">Identificador del llamado.</param>
        /// <returns>
        /// Instancia de <see cref="Llamado"/> con ítems puntuables o <c>null</c> si no existe.
        /// </returns>
        Task<Llamado?> GetByIdWithItemsPuntuablesAsync(int id);

        /// <summary>
        /// Obtiene un llamado con los apoyos necesarios declarados.
        /// </summary>
        /// <param name="id">Identificador del llamado.</param>
        /// <returns>
        /// Instancia de <see cref="Llamado"/> con apoyos o <c>null</c> si no existe.
        /// </returns>
        Task<Llamado?> GetByIdWithApoyosAsync(int id);

        /// <summary>
        /// Obtiene un llamado junto a los departamentos habilitados.
        /// </summary>
        /// <param name="id">Identificador del llamado.</param>
        /// <returns>
        /// Instancia de <see cref="Llamado"/> con departamentos o <c>null</c> si no existe.
        /// </returns>
        Task<Llamado?> GetByIdWithDepartamentosAsync(int id);

        /// <summary>
        /// Determina si el llamado continúa abierto para nuevas inscripciones.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// <c>true</c> si el llamado se encuentra abierto; de lo contrario <c>false</c>.
        /// </returns>
        Task<bool> IsLlamadoAbierto(int llamadoId);
    }
}
