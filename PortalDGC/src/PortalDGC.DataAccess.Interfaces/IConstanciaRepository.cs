using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio encargado de los documentos/constancias de respaldo (RF-06).
    /// </summary>
    public interface IConstanciaRepository : IRepository<Constancia>
    {
        /// <summary>
        /// Lista las constancias cargadas por un postulante.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <returns>
        /// Colección de <see cref="Constancia"/> asociadas al postulante.
        /// </returns>
        Task<IEnumerable<Constancia>> GetByPostulanteIdAsync(int postulanteId);

        /// <summary>
        /// Persiste la metadata y contenido de una constancia.
        /// </summary>
        /// <param name="constancia">Entidad a almacenar.</param>
        /// <returns>
        /// Instancia persistida incluyendo su identificador.
        /// </returns>
        Task<Constancia> SubirConstanciaAsync(Constancia constancia);

        /// <summary>
        /// Obtiene una constancia específica por su identificador.
        /// </summary>
        /// <param name="constanciaId">Identificador de la constancia.</param>
        /// <returns>
        /// Instancia de <see cref="Constancia"/> o <c>null</c> si no existe.
        /// </returns>
        Task<Constancia?> GetByIdAsync(int constanciaId);

        /// <summary>
        /// Marca una constancia como validada por el equipo administrativo.
        /// </summary>
        /// <param name="constanciaId">Identificador de la constancia.</param>
        /// <returns>
        /// <c>true</c> si la operación se completó correctamente; de lo contrario <c>false</c>.
        /// </returns>
        Task<bool> ValidarConstanciaAsync(int constanciaId);
    }
}
