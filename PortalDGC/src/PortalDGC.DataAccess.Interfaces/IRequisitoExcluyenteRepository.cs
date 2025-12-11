using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de requisitos excluyentes definidos por llamado (RF-04 y validaciones de RF-05).
    /// </summary>
    public interface IRequisitoExcluyenteRepository : IRepository<RequisitoExcluyente>
    {
        /// <summary>
        /// Obtiene los requisitos excluyentes asociados al llamado indicado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Colección de <see cref="RequisitoExcluyente"/>.
        /// </returns>
        Task<IEnumerable<RequisitoExcluyente>> GetByLlamadoIdAsync(int llamadoId);

        /// <summary>
        /// Recupera un requisito puntual.
        /// </summary>
        /// <param name="requisitoId">Identificador del requisito.</param>
        /// <returns>
        /// Instancia de <see cref="RequisitoExcluyente"/> o <c>null</c> si no existe.
        /// </returns>
        Task<RequisitoExcluyente?> GetByIdAsync(int requisitoId);
    }
}
