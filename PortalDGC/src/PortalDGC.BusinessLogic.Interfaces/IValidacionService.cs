using PortalDGC.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface IValidacionService
    {
        ApiResponseDto<bool> ValidarCedulaIdentidad(string cedulaIdentidad);
        ApiResponseDto<bool> ValidarEmail(string email);
        ApiResponseDto<bool> ValidarTelefono(string telefono);
        ApiResponseDto<bool> ValidarEdadMinima(DateTime fechaNacimiento, int edadMinima = 18);
        ApiResponseDto<bool> ValidarFechaRango(DateTime fecha, DateTime fechaInicio, DateTime fechaFin);
        Task<ApiResponseDto<bool>> ValidarLlamadoAbierto(int llamadoId);
        Task<ApiResponseDto<bool>> ValidarPostulanteCompletoDatos(int postulanteId);
    }
}
