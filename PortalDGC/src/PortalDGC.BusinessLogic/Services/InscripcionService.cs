using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Domain.Entities;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Inscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidacionService _validacionService;

        public InscripcionService(IUnitOfWork unitOfWork, IValidacionService validacionService)
        {
            _unitOfWork = unitOfWork;
            _validacionService = validacionService;
        }

        public async Task<ApiResponseDto<InscripcionResponseDto>> CrearInscripcionAsync(
            int postulanteId,
            CrearInscripcionDto inscripcionDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var postulante = await _unitOfWork.Postulantes.GetByIdAsync(postulanteId);
                if (postulante == null)
                {
                    return new ApiResponseDto<InscripcionResponseDto>
                    {
                        Success = false,
                        Message = "Postulante no encontrado"
                    };
                }

                var llamado = await _unitOfWork.Llamados.GetByIdWithDetallesAsync(inscripcionDto.LlamadoId);
                if (llamado == null)
                {
                    return new ApiResponseDto<InscripcionResponseDto>
                    {
                        Success = false,
                        Message = "Llamado no encontrado"
                    };
                }

                var llamadoAbierto = await _unitOfWork.Llamados.IsLlamadoAbierto(inscripcionDto.LlamadoId);
                if (!llamadoAbierto)
                {
                    return new ApiResponseDto<InscripcionResponseDto>
                    {
                        Success = false,
                        Message = "El llamado no está disponible para inscripción"
                    };
                }

                var existeInscripcion = await _unitOfWork.Inscripciones.ExistsInscripcionAsync(
                    postulanteId,
                    inscripcionDto.LlamadoId);

                if (existeInscripcion)
                {
                    return new ApiResponseDto<InscripcionResponseDto>
                    {
                        Success = false,
                        Message = "Ya existe una inscripción para este llamado"
                    };
                }

                var departamentoValido = await _unitOfWork.Departamentos.ExistsInLlamado(
                    inscripcionDto.DepartamentoId,
                    inscripcionDto.LlamadoId);

                if (!departamentoValido)
                {
                    return new ApiResponseDto<InscripcionResponseDto>
                    {
                        Success = false,
                        Message = "El departamento seleccionado no está disponible para este llamado"
                    };
                }

                var inscripcion = new Inscripcion
                {
                    PostulanteId = postulanteId,
                    LlamadoId = inscripcionDto.LlamadoId,
                    DepartamentoId = inscripcionDto.DepartamentoId,
                    FechaInscripcion = DateTime.Now,
                    Estado = "Pendiente",
                    PuntajeTotal = 0
                };

                await _unitOfWork.Inscripciones.CreateInscripcionCompleteAsync(inscripcion);
                await _unitOfWork.SaveChangesAsync();

                var autodefinicion = new AutodefinicionLey
                {
                    InscripcionId = inscripcion.Id,
                    EsAfrodescendiente = inscripcionDto.Autodefinicion.EsAfrodescendiente,
                    EsTrans = inscripcionDto.Autodefinicion.EsTrans,
                    TieneDiscapacidad = inscripcionDto.Autodefinicion.TieneDiscapacidad
                };
                await _unitOfWork.AutodefinicionesLey.AddAsync(autodefinicion);

                foreach (var requisito in inscripcionDto.Requisitos)
                {
                    var requisitoPostulante = new RequisitoPostulante
                    {
                        InscripcionId = inscripcion.Id,
                        RequisitoId = requisito.RequisitoId,
                        Cumple = requisito.Cumple,
                        Observaciones = requisito.Observaciones
                    };
                    await _unitOfWork.RequisitosPostulante.AddRequisitoAsync(requisitoPostulante);
                }

                foreach (var merito in inscripcionDto.Meritos)
                {
                    var meritoPostulante = new MeritoPostulante
                    {
                        InscripcionId = inscripcion.Id,
                        ItemPuntuableId = merito.ItemPuntuableId,
                        DocumentoRespaldo = merito.DocumentoRespaldo,
                        PuntajeObtenido = 0,
                        Verificado = false
                    };
                    await _unitOfWork.MeritosPostulante.AddMeritoAsync(meritoPostulante);
                }

                foreach (var apoyoId in inscripcionDto.ApoyosIds)
                {
                    var apoyoSolicitado = new ApoyoSolicitado
                    {
                        InscripcionId = inscripcion.Id,
                        ApoyoId = apoyoId
                    };
                    await _unitOfWork.ApoyosSolicitados.AddApoyoAsync(apoyoSolicitado);
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                var inscripcionCompleta = await _unitOfWork.Inscripciones.GetByIdCompleteAsync(inscripcion.Id);

                var response = MapearInscripcionADto(inscripcionCompleta!);

                return new ApiResponseDto<InscripcionResponseDto>
                {
                    Success = true,
                    Message = "Inscripción creada exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ApiResponseDto<InscripcionResponseDto>
                {
                    Success = false,
                    Message = "Error al crear la inscripción",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponseDto<InscripcionResponseDto>> ObtenerInscripcionPorIdAsync(int inscripcionId)
        {
            try
            {
                var inscripcion = await _unitOfWork.Inscripciones.GetByIdCompleteAsync(inscripcionId);

                if (inscripcion == null)
                {
                    return new ApiResponseDto<InscripcionResponseDto>
                    {
                        Success = false,
                        Message = "Inscripción no encontrada"
                    };
                }

                var response = MapearInscripcionADto(inscripcion);

                return new ApiResponseDto<InscripcionResponseDto>
                {
                    Success = true,
                    Message = "Inscripción obtenida exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<InscripcionResponseDto>
                {
                    Success = false,
                    Message = "Error al obtener la inscripción",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponseDto<List<InscripcionSimpleResponseDto>>> ObtenerInscripcionesPorPostulanteAsync(int postulanteId)
        {
            try
            {
                var inscripciones = await _unitOfWork.Inscripciones.GetByPostulanteIdAsync(postulanteId);

                var response = inscripciones.Select(i => new InscripcionSimpleResponseDto
                {
                    Id = i.Id,
                    TituloLlamado = i.Llamado.Titulo,
                    NombreDepartamento = i.Departamento.Nombre,
                    FechaInscripcion = i.FechaInscripcion,
                    Estado = i.Estado
                }).ToList();

                return new ApiResponseDto<List<InscripcionSimpleResponseDto>>
                {
                    Success = true,
                    Message = "Inscripciones obtenidas exitosamente",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<InscripcionSimpleResponseDto>>
                {
                    Success = false,
                    Message = "Error al obtener las inscripciones",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponseDto<bool>> ValidarInscripcionExistenteAsync(int postulanteId, int llamadoId)
        {
            try
            {
                var existe = await _unitOfWork.Inscripciones.ExistsInscripcionAsync(postulanteId, llamadoId);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = existe,
                    Message = existe ? "Ya existe inscripción" : "No existe inscripción"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al validar inscripción",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponseDto<bool>> ValidarRequisitosObligatoriosAsync(int inscripcionId)
        {
            try
            {
                var cumple = await _unitOfWork.RequisitosPostulante.CumpleTodosRequisitosObligatorios(inscripcionId);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = cumple,
                    Message = cumple ? "Cumple todos los requisitos" : "No cumple requisitos obligatorios"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al validar requisitos",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponseDto<decimal>> CalcularPuntajeTotalAsync(int inscripcionId)
        {
            try
            {
                var puntaje = await _unitOfWork.MeritosPostulante.CalcularPuntajeTotalAsync(inscripcionId);
                await _unitOfWork.Inscripciones.UpdatePuntajeTotalAsync(inscripcionId, puntaje);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponseDto<decimal>
                {
                    Success = true,
                    Data = puntaje,
                    Message = "Puntaje calculado exitosamente"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<decimal>
                {
                    Success = false,
                    Message = "Error al calcular puntaje",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        private InscripcionResponseDto MapearInscripcionADto(Inscripcion inscripcion)
        {
            return new InscripcionResponseDto
            {
                Id = inscripcion.Id,
                PostulanteId = inscripcion.PostulanteId,
                NombrePostulante = $"{inscripcion.Postulante.Nombre} {inscripcion.Postulante.Apellido}",
                LlamadoId = inscripcion.LlamadoId,
                TituloLlamado = inscripcion.Llamado.Titulo,
                DepartamentoId = inscripcion.DepartamentoId,
                NombreDepartamento = inscripcion.Departamento.Nombre,
                FechaInscripcion = inscripcion.FechaInscripcion,
                Estado = inscripcion.Estado,
                PuntajeTotal = inscripcion.PuntajeTotal,
                Autodefinicion = inscripcion.AutodefinicionLey != null ? new AutodefinicionLeyDto
                {
                    EsAfrodescendiente = inscripcion.AutodefinicionLey.EsAfrodescendiente,
                    EsTrans = inscripcion.AutodefinicionLey.EsTrans,
                    TieneDiscapacidad = inscripcion.AutodefinicionLey.TieneDiscapacidad
                } : null,
                Requisitos = inscripcion.RequisitosPostulante.Select(r => new RequisitoPostulanteResponseDto
                {
                    Id = r.Id,
                    RequisitoId = r.RequisitoId,
                    DescripcionRequisito = r.Requisito.Descripcion,
                    TipoRequisito = r.Requisito.Tipo,
                    Obligatorio = r.Requisito.Obligatorio,
                    Cumple = r.Cumple,
                    Observaciones = r.Observaciones
                }).ToList(),
                Meritos = inscripcion.MeritosPostulante.Select(m => new MeritoPostulanteResponseDto
                {
                    Id = m.Id,
                    ItemPuntuableId = m.ItemPuntuableId,
                    NombreItem = m.ItemPuntuable.Nombre,
                    DescripcionItem = m.ItemPuntuable.Descripcion,
                    PuntajeMaximo = m.ItemPuntuable.PuntajeMaximo,
                    Categoria = m.ItemPuntuable.Categoria,
                    DocumentoRespaldo = m.DocumentoRespaldo,
                    PuntajeObtenido = m.PuntajeObtenido,
                    Verificado = m.Verificado
                }).ToList(),
                Apoyos = inscripcion.ApoyosSolicitados.Select(a => new ApoyoSolicitadoResponseDto
                {
                    Id = a.Id,
                    ApoyoId = a.ApoyoId,
                    DescripcionApoyo = a.Apoyo.Descripcion,
                    TipoApoyo = a.Apoyo.Tipo,
                    Justificacion = a.Justificacion
                }).ToList()
            };
        }
    }
}
