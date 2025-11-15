using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de departamentos habilitados para llamados (RF-03).
    /// Permite validar disponibilidad antes de inscribir postulantes.
    /// </summary>
    public interface IDepartamentoRepository : IRepository<Departamento>
    {
        /// <summary>
        /// Obtiene todos los departamentos marcados como activos.
        /// </summary>
        /// <returns>
        /// Colección de <see cref="Departamento"/> habilitados para ser utilizados en llamados.
        /// </returns>
        Task<IEnumerable<Departamento>> GetActivosAsync();

        /// <summary>
        /// Busca un departamento por su código institucional.
        /// </summary>
        /// <param name="codigo">Código único del departamento.</param>
        /// <returns>
        /// Instancia de <see cref="Departamento"/> o <c>null</c> si no existe.
        /// </returns>
        Task<Departamento?> GetByCodigoAsync(string codigo);

        /// <summary>
        /// Verifica si un departamento participa en un llamado dado.
        /// </summary>
        /// <param name="departamentoId">Identificador del departamento.</param>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// <c>true</c> si la relación existe; de lo contrario <c>false</c>.
        /// </returns>
        Task<bool> ExistsInLlamado(int departamentoId, int llamadoId);
    }
}
