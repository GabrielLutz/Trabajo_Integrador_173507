using PortalDGC.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    /// <summary>
    /// Servicio de validaciones transversales utilizado por múltiples RF.
    /// </summary>
    public interface IValidacionService
    {
        /// <summary>
        /// Valida una cédula de identidad según reglas locales.
        /// </summary>
        /// <param name="cedulaIdentidad">Número de cédula.</param>
        /// <returns>
        /// Respuesta booleana indicando validez.
        /// </returns>
        ApiResponseDto<bool> ValidarCedulaIdentidad(string cedulaIdentidad);

        /// <summary>
        /// Valida el formato de un correo electrónico.
        /// </summary>
        /// <param name="email">Correo electrónico.</param>
        /// <returns>
        /// Respuesta booleana indicando validez.
        /// </returns>
        ApiResponseDto<bool> ValidarEmail(string email);

        /// <summary>
        /// Valida el formato de un teléfono.
        /// </summary>
        /// <param name="telefono">Número telefónico.</param>
        /// <returns>
        /// Respuesta booleana indicando validez.
        /// </returns>
        ApiResponseDto<bool> ValidarTelefono(string telefono);

        /// <summary>
        /// Valida que la edad calculada desde la fecha de nacimiento alcance un mínimo.
        /// </summary>
        /// <param name="fechaNacimiento">Fecha de nacimiento.</param>
        /// <param name="edadMinima">Edad mínima requerida.</param>
        /// <returns>
        /// Respuesta booleana indicando si cumple la edad.
        /// </returns>
        ApiResponseDto<bool> ValidarEdadMinima(DateTime fechaNacimiento, int edadMinima = 18);

        /// <summary>
        /// Valida que una fecha esté dentro de un rango.
        /// </summary>
        /// <param name="fecha">Fecha a validar.</param>
        /// <param name="fechaInicio">Fecha de inicio del rango.</param>
        /// <param name="fechaFin">Fecha fin del rango.</param>
        /// <returns>
        /// Respuesta booleana indicando si la fecha está en el rango.
        /// </returns>
        ApiResponseDto<bool> ValidarFechaRango(DateTime fecha, DateTime fechaInicio, DateTime fechaFin);

        /// <summary>
        /// Valida si un llamado se encuentra abierto.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta booleana indicando disponibilidad.
        /// </returns>
        Task<ApiResponseDto<bool>> ValidarLlamadoAbierto(int llamadoId);

        /// <summary>
        /// Valida que un postulante tenga sus datos personales completos.
        /// </summary>
        /// <param name="postulanteId">Identificador del postulante.</param>
        /// <returns>
        /// Respuesta booleana indicando si cumple.
        /// </returns>
        Task<ApiResponseDto<bool>> ValidarPostulanteCompletoDatos(int postulanteId);
    }
}
