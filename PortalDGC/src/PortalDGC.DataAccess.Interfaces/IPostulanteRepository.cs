using PortalDGC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Repositorio especializado en postulantes que respalda RF-01 y RF-02
    /// (consulta y actualización de datos personales, validación de cédula/email).
    /// </summary>
    public interface IPostulanteRepository : IRepository<Postulante>
    {
        /// <summary>
        /// Busca un postulante por su cédula de identidad.
        /// </summary>
        /// <param name="cedulaIdentidad">Número de cédula sin formato.</param>
        /// <returns>
        /// Instancia de <see cref="Postulante"/> o <c>null</c> si no existe.
        /// </returns>
        Task<Postulante?> GetByCedulaAsync(string cedulaIdentidad);

        /// <summary>
        /// Busca un postulante por su dirección de email.
        /// </summary>
        /// <param name="email">Correo electrónico registrado.</param>
        /// <returns>
        /// Instancia de <see cref="Postulante"/> o <c>null</c> si no existe.
        /// </returns>
        Task<Postulante?> GetByEmailAsync(string email);

        /// <summary>
        /// Obtiene un postulante junto con sus inscripciones y dependencias.
        /// </summary>
        /// <param name="id">Identificador del postulante.</param>
        /// <returns>
        /// Instancia de <see cref="Postulante"/> con relaciones o <c>null</c> si no existe.
        /// </returns>
        Task<Postulante?> GetByIdWithInscripcionesAsync(int id);

        /// <summary>
        /// Indica si existe un postulante con la cédula dada.
        /// </summary>
        /// <param name="cedulaIdentidad">Número de cédula.</param>
        /// <returns>
        /// <c>true</c> si existe un registro con la cédula; de lo contrario <c>false</c>.
        /// </returns>
        Task<bool> ExistsByCedulaAsync(string cedulaIdentidad);

        /// <summary>
        /// Indica si existe un postulante con el email dado.
        /// </summary>
        /// <param name="email">Correo electrónico.</param>
        /// <returns>
        /// <c>true</c> si el email ya está registrado; de lo contrario <c>false</c>.
        /// </returns>
        Task<bool> ExistsByEmailAsync(string email);

        /// <summary>
        /// Sincroniza los datos personales con el contexto.
        /// </summary>
        /// <param name="postulante">Entidad con la información a actualizar.</param>
        Task UpdateDatosPersonalesAsync(Postulante postulante);
    }
}
