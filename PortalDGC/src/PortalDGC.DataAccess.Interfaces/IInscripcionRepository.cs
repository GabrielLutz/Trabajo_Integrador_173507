using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio de inscripciones que soporta RF-05, RF-07 y RF-08 (creación, consulta y validaciones).
    /// </summary>
    public interface IInscripcionRepository : IRepository<Inscripcion>
    {
        /// <summary>
        /// Obtiene una inscripción con todas sus relaciones (requisitos, méritos, apoyos y datos de postulante).
        /// </summary>
        /// <param name="id">Identificador de la inscripción a recuperar.</param>
        /// <returns>
        /// Instancia completa de <see cref="Inscripcion"/> o <c>null</c> si no existe.
        /// </returns>
        Task<Inscripcion?> GetByIdCompleteAsync(int id);

        /// <summary>
        /// Lista todas las inscripciones realizadas por un postulante determinado.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <returns>
        /// Colección de <see cref="Inscripcion"/> asociadas al postulante.
        /// </returns>
        Task<IEnumerable<Inscripcion>> GetByPostulanteIdAsync(int postulanteId);

        /// <summary>
        /// Lista las inscripciones asociadas a un llamado específico.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Colección de <see cref="Inscripcion"/> vinculadas al llamado.
        /// </returns>
        Task<IEnumerable<Inscripcion>> GetByLlamadoIdAsync(int llamadoId);

        /// <summary>
        /// Verifica si ya existe una inscripción para un postulante en un llamado.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// <c>true</c> si ya existe una inscripción activa; de lo contrario <c>false</c>.
        /// </returns>
        Task<bool> ExistsInscripcionAsync(int postulanteId, int llamadoId);


        /// <summary>
        /// Crea una inscripción junto con sus entidades hijas (requisitos, méritos, apoyos) en una única transacción.
        /// </summary>
        /// <param name="inscripcion">Entidad completa a persistir.</param>
        /// <returns>
        /// Instancia persistida con su identificador definitivo.
        /// </returns>
        Task<Inscripcion> CreateInscripcionCompleteAsync(Inscripcion inscripcion);

        /// <summary>
        /// Actualiza el estado del flujo (Pendiente, Validada, etc.) para una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción.</param>
        /// <param name="estado">Nuevo estado a registrar.</param>
        Task UpdateEstadoAsync(int inscripcionId, string estado);

        /// <summary>
        /// Persiste el puntaje total calculado por el tribunal.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción a actualizar.</param>
        /// <param name="puntaje">Valor decimal correspondiente al puntaje total.</param>
        Task UpdatePuntajeTotalAsync(int inscripcionId, decimal puntaje);
    }
}
