using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Postulante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    /// <summary>
    /// Servicio para gestionar datos de postulantes (RF-01 y RF-02).
    /// </summary>
    public interface IPostulanteService
    {
        /// <summary>
        /// Obtiene un postulante por su identificador.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <returns>
        /// Respuesta con los datos del postulante.
        /// </returns>
        Task<ApiResponseDto<PostulanteResponseDto>> ObtenerPostulantePorIdAsync(int postulanteId);

        /// <summary>
        /// Actualiza los datos personales de un postulante.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <param name="datosPersonales">Datos a registrar.</param>
        /// <returns>
        /// Respuesta con el postulante actualizado.
        /// </returns>
        Task<ApiResponseDto<PostulanteResponseDto>> CompletarDatosPersonalesAsync(int postulanteId, PostulanteDatosPersonalesDto datosPersonales);

        /// <summary>
        /// Valida si una cédula de identidad está disponible para registro.
        /// </summary>
        /// <param name="cedulaIdentidad">Número de cédula.</param>
        /// <returns>
        /// Respuesta booleana indicando disponibilidad.
        /// </returns>
        Task<ApiResponseDto<bool>> ValidarCedulaDisponibleAsync(string cedulaIdentidad);

        /// <summary>
        /// Valida si un correo electrónico está disponible.
        /// </summary>
        /// <param name="email">Correo electrónico.</param>
        /// <returns>
        /// Respuesta booleana indicando disponibilidad.
        /// </returns>
        Task<ApiResponseDto<bool>> ValidarEmailDisponibleAsync(string email);
    }
}
