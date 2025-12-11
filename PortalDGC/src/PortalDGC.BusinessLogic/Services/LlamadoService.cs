using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Llamado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Services
{
    /// <summary>
    /// Servicio de negocio para gestión de llamados públicos.
    /// Implementa requerimientos funcionales RF-03 y RF-04.
    /// </summary>
    public class LlamadoService : ILlamadoService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor del servicio de llamados.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo para acceso a datos</param>
        public LlamadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtiene el detalle completo de un llamado específico con toda su información.
        /// Implementa RF-04: Consulta de detalle de llamado.
        /// </summary>
        /// <param name="llamadoId">Identificador único del llamado</param>
        /// <returns>
        /// ApiResponseDto con LlamadoDetalleDto que incluye:
        /// - Información básica del llamado (título, descripción, bases)
        /// - Fechas de apertura y cierre
        /// - Cantidad de puestos y estado
        /// - Lista de departamentos con cantidad de puestos por departamento
        /// - Lista completa de requisitos excluyentes (obligatorios y opcionales)
        /// - Lista de ítems puntuables con puntajes máximos y categorías
        /// - Lista de apoyos necesarios disponibles
        /// Success = false si el llamado no existe
        /// </returns>
        /// <exception cref="Exception">Cuando ocurre un error en la consulta</exception>
        /// <remarks>
        /// Este método carga todos los datos relacionados necesarios para:
        /// - Visualizar el llamado completo para postulantes
        /// - Permitir al postulante conocer requisitos y evaluaciones
        /// - Facilitar el proceso de inscripción informada
        /// </remarks>
        public async Task<ApiResponseDto<LlamadoDetalleDto>> ObtenerLlamadoPorIdAsync(int llamadoId)
        {
            try
            {
                var llamado = await _unitOfWork.Llamados.GetByIdWithDetallesAsync(llamadoId);

                if (llamado == null)
                {
                    return new ApiResponseDto<LlamadoDetalleDto>
                    {
                        Success = false,
                        Message = "Llamado no encontrado"
                    };
                }

                var response = new LlamadoDetalleDto
                {
                    Id = llamado.Id,
                    Titulo = llamado.Titulo,
                    Descripcion = llamado.Descripcion,
                    Bases = llamado.Bases,
                    FechaApertura = llamado.FechaApertura,
                    FechaCierre = llamado.FechaCierre,
                    CantidadPuestos = llamado.CantidadPuestos,
                    Estado = llamado.Estado,
                    Departamentos = llamado.LlamadoDepartamentos.Select(ld => new DepartamentoLlamadoDto
                    {
                        DepartamentoId = ld.DepartamentoId,
                        Nombre = ld.Departamento.Nombre,
                        Codigo = ld.Departamento.Codigo,
                        CantidadPuestos = ld.CantidadPuestos
                    }).ToList(),
                    RequisitosExcluyentes = llamado.RequisitosExcluyentes.Select(r => new RequisitoExcluyenteDto
                    {
                        Id = r.Id,
                        Descripcion = r.Descripcion,
                        Tipo = r.Tipo,
                        Obligatorio = r.Obligatorio
                    }).ToList(),
                    ItemsPuntuables = llamado.ItemsPuntuables.Select(i => new ItemPuntuableDto
                    {
                        Id = i.Id,
                        Nombre = i.Nombre,
                        Descripcion = i.Descripcion,
                        PuntajeMaximo = i.PuntajeMaximo,
                        Categoria = i.Categoria
                    }).ToList(),
                    ApoyosNecesarios = llamado.ApoyosNecesarios.Select(a => new ApoyoNecesarioDto
                    {
                        Id = a.Id,
                        Descripcion = a.Descripcion,
                        Tipo = a.Tipo
                    }).ToList()
                };

                return new ApiResponseDto<LlamadoDetalleDto>
                {
                    Success = true,
                    Message = "Llamado obtenido exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<LlamadoDetalleDto>
                {
                    Success = false,
                    Message = "Error al obtener el llamado",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Obtiene la lista de llamados vigentes y disponibles para inscripción.
        /// Implementa RF-03: Visualización de llamados vigentes.
        /// </summary>
        /// <returns>
        /// ApiResponseDto con lista de LlamadoSimpleDto que incluye:
        /// - ID, título y descripción del llamado
        /// - Fechas de apertura y cierre
        /// - Estado actual (debería ser "Abierto")
        /// </returns>
        /// <exception cref="Exception">Cuando ocurre un error en la consulta</exception>
        /// <remarks>
        /// Los llamados activos son aquellos que:
        /// - Están en estado "Abierto"
        /// - La fecha actual está entre la fecha de apertura y cierre
        /// Este método retorna una vista simplificada.
        /// Para ver detalles completos usar ObtenerLlamadoPorIdAsync.
        /// </remarks>
        public async Task<ApiResponseDto<List<LlamadoSimpleDto>>> ObtenerLlamadosActivosAsync()
        {
            try
            {
                var llamados = await _unitOfWork.Llamados.GetLlamadosActivosAsync();

                var response = llamados.Select(l => new LlamadoSimpleDto
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Descripcion = l.Descripcion,
                    FechaApertura = l.FechaApertura,
                    FechaCierre = l.FechaCierre,
                    Estado = l.Estado
                }).ToList();

                return new ApiResponseDto<List<LlamadoSimpleDto>>
                {
                    Success = true,
                    Message = "Llamados obtenidos exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<LlamadoSimpleDto>>
                {
                    Success = false,
                    Message = "Error al obtener llamados",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<List<LlamadoSimpleDto>>> ObtenerLlamadosInactivosAsync()
        {
            try
            {
                var llamados = await _unitOfWork.Llamados.GetLlamadosInactivosAsync();

                var response = llamados.Select(l => new LlamadoSimpleDto
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Descripcion = l.Descripcion,
                    FechaApertura = l.FechaApertura,
                    FechaCierre = l.FechaCierre,
                    Estado = l.Estado
                }).ToList();

                return new ApiResponseDto<List<LlamadoSimpleDto>>
                {
                    Success = true,
                    Message = "Llamados inactivos obtenidos exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<LlamadoSimpleDto>>
                {
                    Success = false,
                    Message = "Error al obtener llamados inactivos",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<bool>> ValidarLlamadoDisponibleAsync(int llamadoId)
        {
            try
            {
                var disponible = await _unitOfWork.Llamados.IsLlamadoAbierto(llamadoId);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = disponible,
                    Message = disponible ? "Llamado disponible" : "Llamado no disponible"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al validar llamado",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<List<RequisitoExcluyenteDto>>> ObtenerRequisitosLlamadoAsync(int llamadoId)
        {
            try
            {
                var requisitos = await _unitOfWork.RequisitosExcluyentes.GetByLlamadoIdAsync(llamadoId);

                var response = requisitos.Select(r => new RequisitoExcluyenteDto
                {
                    Id = r.Id,
                    Descripcion = r.Descripcion,
                    Tipo = r.Tipo,
                    Obligatorio = r.Obligatorio
                }).ToList();

                return new ApiResponseDto<List<RequisitoExcluyenteDto>>
                {
                    Success = true,
                    Message = "Requisitos obtenidos exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<RequisitoExcluyenteDto>>
                {
                    Success = false,
                    Message = "Error al obtener requisitos",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<List<ItemPuntuableDto>>> ObtenerItemsPuntuablesLlamadoAsync(int llamadoId)
        {
            try
            {
                var items = await _unitOfWork.ItemsPuntuables.GetByLlamadoIdAsync(llamadoId);

                var response = items.Select(i => new ItemPuntuableDto
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Descripcion = i.Descripcion,
                    PuntajeMaximo = i.PuntajeMaximo,
                    Categoria = i.Categoria
                }).ToList();

                return new ApiResponseDto<List<ItemPuntuableDto>>
                {
                    Success = true,
                    Message = "Items puntuables obtenidos exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<ItemPuntuableDto>>
                {
                    Success = false,
                    Message = "Error al obtener items puntuables",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<List<ApoyoNecesarioDto>>> ObtenerApoyosNecesariosLlamadoAsync(int llamadoId)
        {
            try
            {
                var apoyos = await _unitOfWork.ApoyosNecesarios.GetByLlamadoIdAsync(llamadoId);

                var response = apoyos.Select(a => new ApoyoNecesarioDto
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    Tipo = a.Tipo
                }).ToList();

                return new ApiResponseDto<List<ApoyoNecesarioDto>>
                {
                    Success = true,
                    Message = "Apoyos necesarios obtenidos exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<ApoyoNecesarioDto>>
                {
                    Success = false,
                    Message = "Error al obtener apoyos necesarios",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
