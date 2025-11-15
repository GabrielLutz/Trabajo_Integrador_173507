using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de apoyos solicitados por las inscripciones (RF-05).
    /// </summary>
    public interface IApoyoSolicitadoRepository : IRepository<ApoyoSolicitado>
    {
        /// <summary>
        /// Obtiene los apoyos solicitados vinculados a una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Colección de <see cref="ApoyoSolicitado"/> asociados al registro.
        /// </returns>
        Task<IEnumerable<ApoyoSolicitado>> GetByInscripcionIdAsync(int inscripcionId);

        /// <summary>
        /// Registra un apoyo solicitado individual.
        /// </summary>
        /// <param name="apoyo">Entidad a persistir.</param>
        /// <returns>
        /// Instancia persistida con su identificador.
        /// </returns>
        Task<ApoyoSolicitado> AddApoyoAsync(ApoyoSolicitado apoyo);

        /// <summary>
        /// Registra un conjunto de apoyos en una sola operación.
        /// </summary>
        /// <param name="apoyos">Colección de apoyos solicitados a persistir.</param>
        Task AddRangeAsync(IEnumerable<ApoyoSolicitado> apoyos);
    }
}
