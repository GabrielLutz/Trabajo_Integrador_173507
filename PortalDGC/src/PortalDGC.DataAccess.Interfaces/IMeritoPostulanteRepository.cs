using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de méritos cargados por el postulante, base para las valoraciones del tribunal (RF-14).
    /// </summary>
    public interface IMeritoPostulanteRepository : IRepository<MeritoPostulante>
    {
        /// <summary>
        /// Obtiene los méritos asociados a una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Colección de <see cref="MeritoPostulante"/> cargados por el postulante.
        /// </returns>
        Task<IEnumerable<MeritoPostulante>> GetByInscripcionIdAsync(int inscripcionId);

        /// <summary>
        /// Agrega un mérito individual.
        /// </summary>
        /// <param name="merito">Entidad de mérito a persistir.</param>
        /// <returns>
        /// Mérito persistido con su identificador.
        /// </returns>
        Task<MeritoPostulante> AddMeritoAsync(MeritoPostulante merito);

        /// <summary>
        /// Registra un lote de méritos.
        /// </summary>
        /// <param name="meritos">Colección de méritos a registrar.</param>
        Task AddRangeAsync(IEnumerable<MeritoPostulante> meritos);

        /// <summary>
        /// Calcula el puntaje total de méritos según ítems y valoraciones del tribunal.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <returns>
        /// Valor decimal con el puntaje total ponderado.
        /// </returns>
        Task<decimal> CalcularPuntajeTotalAsync(int inscripcionId);
    }
}
