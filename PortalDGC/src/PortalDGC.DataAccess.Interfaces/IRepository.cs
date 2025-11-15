using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.DataAccess.Interfaces
{
    /// <summary>
    /// Contrato base para repositorios del Portal DGC. Define operaciones CRUD asíncronas
    /// reutilizadas por los requisitos RF-01 al RF-20 a través de los servicios de negocio.
    /// </summary>
    /// <typeparam name="T">Entidad de dominio gestionada por el repositorio.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <returns>
        /// Instancia de <typeparamref name="T"/> o <c>null</c> si no existe.
        /// </returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Recupera todas las entidades del tipo <typeparamref name="T"/>.
        /// </summary>
        /// <returns>
        /// Colección de <typeparamref name="T"/>.
        /// </returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Registra una entidad en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad a persistir.</param>
        /// <returns>
        /// Entidad persistida con su identificador.
        /// </returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Actualiza el estado de una entidad previamente persistida.
        /// </summary>
        /// <param name="entity">Entidad con cambios a sincronizar.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Elimina una entidad por su identificador lógico.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Verifica si existe una entidad con el identificador proporcionado.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <returns>
        /// <c>true</c> si la entidad existe; de lo contrario <c>false</c>.
        /// </returns>
        Task<bool> ExistsAsync(int id);
    }
}
