using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Services
{
    /// <summary>
    /// Implementación de <see cref="IArchivoService"/> para gestionar archivos en disco.
    /// </summary>
    public class ArchivoService : IArchivoService
    {
        private readonly string _rutaBase = "Archivos"; 

        /// <inheritdoc />
        public async Task<ApiResponseDto<string>> GuardarArchivoAsync(byte[] contenido, string nombreArchivo, string carpeta)
        {
            try
            {
                var rutaCarpeta = Path.Combine(_rutaBase, carpeta);

                if (!Directory.Exists(rutaCarpeta))
                {
                    Directory.CreateDirectory(rutaCarpeta);
                }

                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);
                await File.WriteAllBytesAsync(rutaCompleta, contenido);

                return new ApiResponseDto<string>
                {
                    Success = true,
                    Data = rutaCompleta,
                    Message = "Archivo guardado exitosamente"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<string>
                {
                    Success = false,
                    Message = "Error al guardar archivo",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<byte[]>> ObtenerArchivoAsync(string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    return new ApiResponseDto<byte[]>
                    {
                        Success = false,
                        Message = "Archivo no encontrado"
                    };
                }

                var contenido = await File.ReadAllBytesAsync(rutaArchivo);

                return new ApiResponseDto<byte[]>
                {
                    Success = true,
                    Data = contenido,
                    Message = "Archivo obtenido exitosamente"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<byte[]>
                {
                    Success = false,
                    Message = "Error al obtener archivo",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<bool>> EliminarArchivoAsync(string rutaArchivo)
        {
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    File.Delete(rutaArchivo);
                }

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Archivo eliminado exitosamente"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al eliminar archivo",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public ApiResponseDto<bool> ValidarTipoArchivo(string nombreArchivo, List<string> extensionesPermitidas)
        {
            var extension = Path.GetExtension(nombreArchivo).ToLower();
            var esValido = extensionesPermitidas.Contains(extension);

            return new ApiResponseDto<bool>
            {
                Success = esValido,
                Data = esValido,
                Message = esValido ? "Tipo de archivo válido" : "Tipo de archivo no permitido",
                Errors = esValido ? new List<string>() : new List<string> { $"Solo se permiten archivos: {string.Join(", ", extensionesPermitidas)}" }
            };
        }

        /// <inheritdoc />
        public ApiResponseDto<bool> ValidarTamañoArchivo(long tamaño, long tamañoMaximoMB = 10)
        {
            var tamañoMaximoBytes = tamañoMaximoMB * 1024 * 1024;
            var esValido = tamaño <= tamañoMaximoBytes;

            return new ApiResponseDto<bool>
            {
                Success = esValido,
                Data = esValido,
                Message = esValido ? "Tamaño de archivo válido" : $"El archivo excede el tamaño máximo de {tamañoMaximoMB}MB",
                Errors = esValido ? new List<string>() : new List<string> { $"Tamaño máximo permitido: {tamañoMaximoMB}MB" }
            };
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<string>> ConvertirBase64AArchivoAsync(string base64, string nombreArchivo, string carpeta)
        {
            try
            {
                var contenido = Convert.FromBase64String(base64);
                return await GuardarArchivoAsync(contenido, nombreArchivo, carpeta);
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<string>
                {
                    Success = false,
                    Message = "Error al convertir archivo Base64",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
