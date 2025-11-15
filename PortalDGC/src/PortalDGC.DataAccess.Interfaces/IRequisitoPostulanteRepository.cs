using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio que almacena los requisitos que cada postulante certifica en su inscripción.
    /// </summary>
    public interface IRequisitoPostulanteRepository : IRepository<RequisitoPostulante>
    {
        /// <summary>
        /// Obtiene los requisitos asociados a una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Colección de <see cref="RequisitoPostulante"/> registrados.
        /// </returns>
        Task<IEnumerable<RequisitoPostulante>> GetByInscripcionIdAsync(int inscripcionId);

        /// <summary>
        /// Agrega un requisito individual.
        /// </summary>
        /// <param name="requisito">Entidad a persistir.</param>
        /// <returns>
        /// Instancia persistida con su identificador.
        /// </returns>
        Task<RequisitoPostulante> AddRequisitoAsync(RequisitoPostulante requisito);

        /// <summary>
        /// Agrega múltiples requisitos en bloque.
        /// </summary>
        /// <param name="requisitos">Colección de requisitos a registrar.</param>
        Task AddRangeAsync(IEnumerable<RequisitoPostulante> requisitos);

        /// <summary>
        /// Valida si todos los requisitos obligatorios se cargaron correctamente.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// <c>true</c> si todos los requisitos obligatorios están presentes; de lo contrario <c>false</c>.
        /// </returns>
        Task<bool> CumpleTodosRequisitosObligatorios(int inscripcionId);
    }
}
