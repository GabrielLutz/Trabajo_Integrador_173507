using PortalDGC.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Interfaces
{
    /// <summary>
    /// Servicio de utilidades para persistencia y validaciones de archivos adjuntos (RF-06 y RF-07).
    /// </summary>
    public interface IArchivoService
    {
        /// <summary>
        /// Guarda un archivo en el almacenamiento configurado.
        /// </summary>
        /// <param name="contenido">Contenido binario del archivo.</param>
        /// <param name="nombreArchivo">Nombre que se asignará al archivo.</param>
        /// <param name="carpeta">Carpeta o prefijo dentro del almacenamiento.</param>
        /// <returns>
        /// Respuesta con la ruta relativa generada.
        /// </returns>
        Task<ApiResponseDto<string>> GuardarArchivoAsync(byte[] contenido, string nombreArchivo, string carpeta);

        /// <summary>
        /// Obtiene un archivo previamente almacenado.
        /// </summary>
        /// <param name="rutaArchivo">Ruta física o relativa del archivo.</param>
        /// <returns>
        /// Respuesta con el contenido binario.
        /// </returns>
        Task<ApiResponseDto<byte[]>> ObtenerArchivoAsync(string rutaArchivo);

        /// <summary>
        /// Elimina un archivo del almacenamiento.
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo a eliminar.</param>
        /// <returns>
        /// Respuesta indicando si la eliminación fue exitosa.
        /// </returns>
        Task<ApiResponseDto<bool>> EliminarArchivoAsync(string rutaArchivo);

        /// <summary>
        /// Valida que un nombre de archivo pertenezca a una lista de extensiones permitidas.
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo a validar.</param>
        /// <param name="extensionesPermitidas">Extensiones válidas (sin punto).</param>
        /// <returns>
        /// Resultado booleano dentro de la respuesta estándar.
        /// </returns>
        ApiResponseDto<bool> ValidarTipoArchivo(string nombreArchivo, List<string> extensionesPermitidas);

        /// <summary>
        /// Valida que un archivo no exceda el tamaño permitido.
        /// </summary>
        /// <param name="tamaño">Tamaño del archivo en bytes.</param>
        /// <param name="tamañoMaximoMB">Tamaño máximo permitido en MB.</param>
        /// <returns>
        /// Resultado booleano indicando si está dentro del límite.
        /// </returns>
        ApiResponseDto<bool> ValidarTamañoArchivo(long tamaño, long tamañoMaximoMB = 10);

        /// <summary>
        /// Convierte un contenido Base64 a archivo físico persistido.
        /// </summary>
        /// <param name="base64">Cadena en Base64 que representa el archivo.</param>
        /// <param name="nombreArchivo">Nombre destino del archivo.</param>
        /// <param name="carpeta">Carpeta donde se almacenará.</param>
        /// <returns>
        /// Ruta relativa generada para el archivo persistido.
        /// </returns>
        Task<ApiResponseDto<string>> ConvertirBase64AArchivoAsync(string base64, string nombreArchivo, string carpeta);
    }
}
