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
    /// Servicio para consultar departamentos disponibles en los llamados (RF-03).
    /// </summary>
    public interface IDepartamentoService
    {
        /// <summary>
        /// Obtiene los departamentos activos.
        /// </summary>
        /// <returns>
        /// Respuesta con la lista de departamentos habilitados.
        /// </returns>
        Task<ApiResponseDto<List<DepartamentoDto>>> ObtenerDepartamentosActivosAsync();

        /// <summary>
        /// Obtiene un departamento por su identificador.
        /// </summary>
        /// <param name="departamentoId">Identificador del departamento.</param>
        /// <returns>
        /// Respuesta con la información del departamento o error si no existe.
        /// </returns>
        Task<ApiResponseDto<DepartamentoDto>> ObtenerDepartamentoPorIdAsync(int departamentoId);

        /// <summary>
        /// Obtiene los departamentos asociados a un llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta con la lista de departamentos vinculados.
        /// </returns>
        Task<ApiResponseDto<List<DepartamentoLlamadoDto>>> ObtenerDepartamentosPorLlamadoAsync(int llamadoId);

        /// <summary>
        /// Valida si un departamento participa en un llamado específico.
        /// </summary>
        /// <param name="departamentoId">Identificador del departamento.</param>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// Respuesta indicando si la asociación es válida.
        /// </returns>
        Task<ApiResponseDto<bool>> ValidarDepartamentoEnLlamadoAsync(int departamentoId, int llamadoId);
    }
}
