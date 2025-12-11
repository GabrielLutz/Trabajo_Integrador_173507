using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de autodefiniciones Ley 19.122 asociadas a las inscripciones.
    /// </summary>
    public interface IAutodefinicionLeyRepository : IRepository<AutodefinicionLey>
    {
        /// <summary>
        /// Obtiene la autodefinición Ley 19.122 ligada a una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Instancia de <see cref="AutodefinicionLey"/> o <c>null</c> si no existe.
        /// </returns>
        Task<AutodefinicionLey?> GetByInscripcionIdAsync(int inscripcionId);

        /// <summary>
        /// Crea o actualiza los datos de autodefinición vinculados a una inscripción.
        /// </summary>
        /// <param name="autodefinicion">Entidad con los datos a persistir.</param>
        /// <returns>
        /// Entidad persistida con los cambios aplicados.
        /// </returns>
        Task<AutodefinicionLey> CreateOrUpdateAsync(AutodefinicionLey autodefinicion);
    }
}
