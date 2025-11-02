using PortalDGC.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    public interface IArchivoService
    {
        Task<ApiResponseDto<string>> GuardarArchivoAsync(byte[] contenido, string nombreArchivo, string carpeta);
        Task<ApiResponseDto<byte[]>> ObtenerArchivoAsync(string rutaArchivo);
        Task<ApiResponseDto<bool>> EliminarArchivoAsync(string rutaArchivo);
        ApiResponseDto<bool> ValidarTipoArchivo(string nombreArchivo, List<string> extensionesPermitidas);
        ApiResponseDto<bool> ValidarTamañoArchivo(long tamaño, long tamañoMaximoMB = 10);
        Task<ApiResponseDto<string>> ConvertirBase64AArchivoAsync(string base64, string nombreArchivo, string carpeta);
    }
}
