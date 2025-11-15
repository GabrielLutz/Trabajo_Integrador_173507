using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Llamado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    /// <summary>
    /// Servicio orientado a la consulta de llamados vigentes y sus recursos asociados (RF-03/RF-04).
    /// </summary>
    public interface ILlamadoService
    {
        /// <summary>
        /// Obtiene un llamado con detalle completo.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta con el detalle del llamado.
        /// </returns>
        Task<ApiResponseDto<LlamadoDetalleDto>> ObtenerLlamadoPorIdAsync(int llamadoId);

        /// <summary>
        /// Obtiene los llamados activos.
        /// </summary>
        /// <returns>
        /// Respuesta con la lista de llamados disponibles para inscripción.
        /// </returns>
        Task<ApiResponseDto<List<LlamadoSimpleDto>>> ObtenerLlamadosActivosAsync();

        /// <summary>
        /// Obtiene los llamados inactivos o finalizados.
        /// </summary>
        /// <returns>
        /// Respuesta con la lista de llamados cerrados.
        /// </returns>
        Task<ApiResponseDto<List<LlamadoSimpleDto>>> ObtenerLlamadosInactivosAsync();

        /// <summary>
        /// Valida si un llamado sigue disponible para nuevas inscripciones.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta booleana señalando disponibilidad.
        /// </returns>
        Task<ApiResponseDto<bool>> ValidarLlamadoDisponibleAsync(int llamadoId);

        /// <summary>
        /// Obtiene los requisitos excluyentes asociados a un llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta con la lista de requisitos.
        /// </returns>
        Task<ApiResponseDto<List<RequisitoExcluyenteDto>>> ObtenerRequisitosLlamadoAsync(int llamadoId);

        /// <summary>
        /// Obtiene los ítems puntuables relacionados a un llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta con la lista de ítems puntuables.
        /// </returns>
        Task<ApiResponseDto<List<ItemPuntuableDto>>> ObtenerItemsPuntuablesLlamadoAsync(int llamadoId);

        /// <summary>
        /// Obtiene los apoyos necesarios configurados para un llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta con la lista de apoyos.
        /// </returns>
        Task<ApiResponseDto<List<ApoyoNecesarioDto>>> ObtenerApoyosNecesariosLlamadoAsync(int llamadoId);
    }
}
