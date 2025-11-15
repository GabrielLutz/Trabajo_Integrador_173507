using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de apoyos necesarios declarados en los llamados para políticas de accesibilidad.
    /// </summary>
    public interface IApoyoNecesarioRepository : IRepository<ApoyoNecesario>
    {
        /// <summary>
        /// Obtiene los apoyos necesarios configurados para un llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Colección de instancias de <see cref="ApoyoNecesario"/> asociadas al llamado.
        /// </returns>
        Task<IEnumerable<ApoyoNecesario>> GetByLlamadoIdAsync(int llamadoId);

        /// <summary>
        /// Recupera un apoyo necesario específico.
        /// </summary>
        /// <param name="apoyoId">Identificador del apoyo.</param>
        /// <returns>
        /// Instancia de <see cref="ApoyoNecesario"/> o <c>null</c> si no existe.
        /// </returns>
        Task<ApoyoNecesario?> GetByIdAsync(int apoyoId);
    }
}
