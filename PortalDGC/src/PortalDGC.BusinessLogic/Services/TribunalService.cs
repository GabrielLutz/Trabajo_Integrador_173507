using PortalDGC.BusinessLogic.Interfaces;
using PortalDGC.DataAccess.Interfaces;
using PortalDGC.Domain.Entities;
using PortalDGC.Dtos.Common;
using PortalDGC.Dtos.Inscripcion;
using PortalDGC.Dtos.Tribunal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalDGC.BusinessLogic.Services
{
    /// <summary>
    /// Servicio de negocio para operaciones del tribunal evaluador.
    /// Implementa los requerimientos RF-11 (gestión de evaluaciones),
    /// RF-12 (registro de pruebas), RF-14 (valoración de méritos) y RF-15 (ordenamientos).
    /// </summary>
    public class TribunalService : ITribunalService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa el servicio de tribunal con la unidad de trabajo provista.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo para repositorios transaccionales</param>
        public TribunalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtiene las inscripciones de un llamado junto con su estado de evaluación.
        /// Implementa RF-11: gestionar las inscripciones que deben ser evaluadas por el tribunal.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado a evaluar</param>
        /// <returns>
        /// ApiResponseDto con lista de InscripcionParaEvaluarDto que detalla puntajes parciales,
        /// cantidad de pruebas/meritos evaluados, indicadores de cupos de autodefinición, etc.
        /// </returns>
        /// <exception cref="Exception">Cuando ocurre un error al consultar datos relacionados</exception>
        public async Task<ApiResponseDto<List<InscripcionParaEvaluarDto>>> ObtenerInscripcionesParaEvaluarAsync(int llamadoId)
        {
            try
            {
                var inscripciones = await _unitOfWork.Inscripciones.GetByLlamadoIdAsync(llamadoId);
                var pruebas = await _unitOfWork.Pruebas.GetByLlamadoIdAsync(llamadoId);
                var totalPruebas = pruebas.Count();

                var resultado = new List<InscripcionParaEvaluarDto>();

                foreach (var inscripcion in inscripciones)
                {
                    var evaluacionesPruebas = await _unitOfWork.EvaluacionesPruebas.GetByInscripcionIdAsync(inscripcion.Id);
                    var evaluacionesMeritos = await _unitOfWork.EvaluacionesMeritos.GetByInscripcionIdAsync(inscripcion.Id);
                    var meritos = await _unitOfWork.MeritosPostulante.GetByInscripcionIdAsync(inscripcion.Id);

                    var puntajePruebas = evaluacionesPruebas.Where(e => e.Verificado).Sum(e => e.PuntajeObtenido);
                    var puntajeMeritos = evaluacionesMeritos.Where(e => e.Estado == "Aprobado").Sum(e => e.PuntajeAsignado);
                    var aproboPruebas = evaluacionesPruebas.All(e => e.Aprobado);

                    resultado.Add(new InscripcionParaEvaluarDto
                    {
                        InscripcionId = inscripcion.Id,
                        PostulanteId = inscripcion.PostulanteId,
                        NombreCompleto = $"{inscripcion.Postulante.Nombre} {inscripcion.Postulante.Apellido}",
                        CedulaIdentidad = inscripcion.Postulante.CedulaIdentidad,
                        Email = inscripcion.Postulante.Email,
                        Departamento = inscripcion.Departamento.Nombre,
                        FechaInscripcion = inscripcion.FechaInscripcion,
                        EstadoInscripcion = inscripcion.Estado,
                        EsAfrodescendiente = inscripcion.AutodefinicionLey?.EsAfrodescendiente ?? false,
                        EsTrans = inscripcion.AutodefinicionLey?.EsTrans ?? false,
                        TieneDiscapacidad = inscripcion.AutodefinicionLey?.TieneDiscapacidad ?? false,
                        PruebasEvaluadas = evaluacionesPruebas.Count(),
                        PruebasTotales = totalPruebas,
                        MeritosEvaluados = evaluacionesMeritos.Count(),
                        MeritosTotales = meritos.Count(),
                        PuntajePruebas = puntajePruebas,
                        PuntajeMeritos = puntajeMeritos,
                        PuntajeTotal = puntajePruebas + puntajeMeritos,
                        AproboPruebas = evaluacionesPruebas.Any() ? aproboPruebas : null
                    });
                }

                return new ApiResponseDto<List<InscripcionParaEvaluarDto>>
                {
                    Success = true,
                    Message = "Inscripciones obtenidas exitosamente",
                    Data = resultado
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<InscripcionParaEvaluarDto>>
                {
                    Success = false,
                    Message = "Error al obtener inscripciones",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<DetalleEvaluacionDto>> ObtenerDetalleEvaluacionAsync(int inscripcionId)
        {
            try
            {
                var inscripcion = await _unitOfWork.Inscripciones.GetByIdCompleteAsync(inscripcionId);
                if (inscripcion == null)
                {
                    return new ApiResponseDto<DetalleEvaluacionDto>
                    {
                        Success = false,
                        Message = "Inscripción no encontrada"
                    };
                }

                var evaluacionesPruebas = await _unitOfWork.EvaluacionesPruebas.GetByInscripcionIdAsync(inscripcionId);
                var evaluacionesMeritos = await _unitOfWork.EvaluacionesMeritos.GetByInscripcionIdAsync(inscripcionId);

                var resultado = new DetalleEvaluacionDto
                {
                    InscripcionId = inscripcion.Id,
                    NombreCompleto = $"{inscripcion.Postulante.Nombre} {inscripcion.Postulante.Apellido}",
                    CedulaIdentidad = inscripcion.Postulante.CedulaIdentidad,
                    Email = inscripcion.Postulante.Email,
                    Departamento = inscripcion.Departamento.Nombre,
                    EsAfrodescendiente = inscripcion.AutodefinicionLey?.EsAfrodescendiente ?? false,
                    EsTrans = inscripcion.AutodefinicionLey?.EsTrans ?? false,
                    TieneDiscapacidad = inscripcion.AutodefinicionLey?.TieneDiscapacidad ?? false,
                    Requisitos = inscripcion.RequisitosPostulante.Select(r => new RequisitoPostulanteResponseDto
                    {
                        RequisitoId = r.RequisitoId,
                        DescripcionRequisito = r.Requisito.Descripcion,
                        Obligatorio = r.Requisito.Obligatorio,
                        Cumple = r.Cumple,
                        Observaciones = r.Observaciones
                    }).ToList(),
                    Pruebas = evaluacionesPruebas.Select(e => new EvaluacionPruebaDto
                    {
                        Id = e.Id,
                        InscripcionId = e.InscripcionId,
                        PruebaId = e.PruebaId,
                        NombrePrueba = e.Prueba.Nombre,
                        TipoPrueba = e.Prueba.Tipo,
                        PuntajeMaximo = e.Prueba.PuntajeMaximo,
                        PuntajeObtenido = e.PuntajeObtenido,
                        Aprobado = e.Aprobado,
                        Observaciones = e.Observaciones,
                        FechaEvaluacion = e.FechaEvaluacion,
                        Verificado = e.Verificado
                    }).ToList(),
                    Meritos = inscripcion.MeritosPostulante.Select(m =>
                    {
                        var evaluacion = evaluacionesMeritos.FirstOrDefault(e => e.MeritoPostulanteId == m.Id);
                        return new MeritoParaEvaluarDto
                        {
                            MeritoPostulanteId = m.Id,
                            NombreItem = m.ItemPuntuable.Nombre,
                            Categoria = m.ItemPuntuable.Categoria,
                            PuntajeMaximo = m.ItemPuntuable.PuntajeMaximo,
                            DocumentoRespaldo = m.DocumentoRespaldo,
                            FueEvaluado = evaluacion != null,
                            PuntajeAsignado = evaluacion?.PuntajeAsignado,
                            DocumentacionVerificada = evaluacion?.DocumentacionVerificada,
                            Observaciones = evaluacion?.Observaciones,
                            Estado = evaluacion?.Estado
                        };
                    }).ToList()
                };

                resultado.PuntajePruebas = resultado.Pruebas.Where(p => p.Verificado).Sum(p => p.PuntajeObtenido);
                resultado.PuntajeMeritos = evaluacionesMeritos.Where(e => e.Estado == "Aprobado").Sum(e => e.PuntajeAsignado);
                resultado.PuntajeTotal = resultado.PuntajePruebas + resultado.PuntajeMeritos;

                return new ApiResponseDto<DetalleEvaluacionDto>
                {
                    Success = true,
                    Message = "Detalle obtenido exitosamente",
                    Data = resultado
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<DetalleEvaluacionDto>
                {
                    Success = false,
                    Message = "Error al obtener detalle",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<List<PruebaDto>>> ObtenerPruebasDelLlamadoAsync(int llamadoId)
        {
            try
            {
                var pruebas = await _unitOfWork.Pruebas.GetByLlamadoIdAsync(llamadoId);

                var resultado = new List<PruebaDto>();
                foreach (var prueba in pruebas)
                {
                    var evaluaciones = await _unitOfWork.EvaluacionesPruebas.GetByPruebaIdAsync(prueba.Id);

                    resultado.Add(new PruebaDto
                    {
                        Id = prueba.Id,
                        LlamadoId = prueba.LlamadoId,
                        Tipo = prueba.Tipo,
                        Nombre = prueba.Nombre,
                        Descripcion = prueba.Descripcion,
                        PuntajeMaximo = prueba.PuntajeMaximo,
                        FechaProgramada = prueba.FechaProgramada,
                        Lugar = prueba.Lugar,
                        Estado = prueba.Estado,
                        EsObligatoria = prueba.EsObligatoria,
                        OrdenEjecucion = prueba.OrdenEjecucion,
                        CantidadEvaluados = evaluaciones.Count(),
                        CantidadAprobados = evaluaciones.Count(e => e.Aprobado),
                        PromedioGeneral = evaluaciones.Any() ? evaluaciones.Average(e => e.PuntajeObtenido) : 0
                    });
                }

                return new ApiResponseDto<List<PruebaDto>>
                {
                    Success = true,
                    Message = "Pruebas obtenidas exitosamente",
                    Data = resultado
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<PruebaDto>>
                {
                    Success = false,
                    Message = "Error al obtener pruebas",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Registra o actualiza la calificación de una prueba rendida por una inscripción.
        /// Implementa RF-12: evaluación de pruebas con puntuación entre 0 y el máximo definido.
        /// </summary>
        /// <param name="dto">DTO con identificadores de inscripción/prueba y puntaje obtenido</param>
        /// <returns>
        /// ApiResponseDto con EvaluacionPruebaDto actualizado, incluyendo datos de la prueba, puntaje y estado.
        /// Success = false si la prueba no existe o el puntaje está fuera del rango permitido.
        /// </returns>
        /// <exception cref="Exception">Cuando ocurre un error el registro se revierte (rollback)</exception>
        public async Task<ApiResponseDto<EvaluacionPruebaDto>> CalificarPruebaAsync(CalificarPruebaDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var evaluacionExistente = await _unitOfWork.EvaluacionesPruebas.GetByInscripcionAndPruebaAsync(dto.InscripcionId, dto.PruebaId);

                var prueba = await _unitOfWork.Pruebas.GetByIdAsync(dto.PruebaId);
                if (prueba == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ApiResponseDto<EvaluacionPruebaDto>
                    {
                        Success = false,
                        Message = "Prueba no encontrada"
                    };
                }

                if (dto.PuntajeObtenido < 0 || dto.PuntajeObtenido > prueba.PuntajeMaximo)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ApiResponseDto<EvaluacionPruebaDto>
                    {
                        Success = false,
                        Message = $"El puntaje debe estar entre 0 y {prueba.PuntajeMaximo}"
                    };
                }

                var puntajeMinimoAprobacion = prueba.PuntajeMaximo * 0.5m;
                var aprobado = dto.PuntajeObtenido >= puntajeMinimoAprobacion;

                if (evaluacionExistente != null)
                {
                    evaluacionExistente.PuntajeObtenido = dto.PuntajeObtenido;
                    evaluacionExistente.Aprobado = aprobado;
                    evaluacionExistente.Observaciones = dto.Observaciones;
                    evaluacionExistente.FechaEvaluacion = DateTime.Now;
                    evaluacionExistente.Verificado = true;

                    await _unitOfWork.EvaluacionesPruebas.UpdateAsync(evaluacionExistente);
                }
                else
                {
                    var nuevaEvaluacion = new EvaluacionPrueba
                    {
                        InscripcionId = dto.InscripcionId,
                        PruebaId = dto.PruebaId,
                        PuntajeObtenido = dto.PuntajeObtenido,
                        Aprobado = aprobado,
                        Observaciones = dto.Observaciones,
                        FechaEvaluacion = DateTime.Now,
                        Verificado = true
                    };

                    evaluacionExistente = await _unitOfWork.EvaluacionesPruebas.AddAsync(nuevaEvaluacion);
                }

                await _unitOfWork.CommitTransactionAsync();

                return new ApiResponseDto<EvaluacionPruebaDto>
                {
                    Success = true,
                    Message = "Prueba calificada exitosamente",
                    Data = new EvaluacionPruebaDto
                    {
                        Id = evaluacionExistente.Id,
                        InscripcionId = evaluacionExistente.InscripcionId,
                        PruebaId = evaluacionExistente.PruebaId,
                        NombrePrueba = prueba.Nombre,
                        TipoPrueba = prueba.Tipo,
                        PuntajeMaximo = prueba.PuntajeMaximo,
                        PuntajeObtenido = evaluacionExistente.PuntajeObtenido,
                        Aprobado = evaluacionExistente.Aprobado,
                        Observaciones = evaluacionExistente.Observaciones,
                        FechaEvaluacion = evaluacionExistente.FechaEvaluacion,
                        Verificado = evaluacionExistente.Verificado
                    }
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ApiResponseDto<EvaluacionPruebaDto>
                {
                    Success = false,
                    Message = "Error al calificar prueba",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Valora un mérito individual asignando puntaje y estado según la documentación.
        /// Implementa RF-14: valoración de méritos de postulantes.
        /// </summary>
        /// <param name="dto">DTO con identificador del mérito y datos de valoración</param>
        /// <returns>
        /// ApiResponseDto con EvaluacionMeritoDto resultante, incluyendo puntaje asignado y observaciones.
        /// Success = false cuando el mérito o item puntuable no existen o el puntaje excede el máximo.
        /// </returns>
        /// <exception cref="Exception">Se revierte la transacción ante cualquier error</exception>
        public async Task<ApiResponseDto<EvaluacionMeritoDto>> ValorarMeritoAsync(ValorarMeritoDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var merito = await _unitOfWork.MeritosPostulante.GetByIdAsync(dto.MeritoPostulanteId);
                if (merito == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ApiResponseDto<EvaluacionMeritoDto>
                    {
                        Success = false,
                        Message = "Mérito no encontrado"
                    };
                }

                var item = await _unitOfWork.ItemsPuntuables.GetByIdAsync(merito.ItemPuntuableId);
                if (item == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ApiResponseDto<EvaluacionMeritoDto>
                    {
                        Success = false,
                        Message = "Item puntuable no encontrado"
                    };
                }

                if (dto.PuntajeAsignado < 0 || dto.PuntajeAsignado > item.PuntajeMaximo)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ApiResponseDto<EvaluacionMeritoDto>
                    {
                        Success = false,
                        Message = $"El puntaje debe estar entre 0 y {item.PuntajeMaximo}"
                    };
                }

                var evaluacionExistente = await _unitOfWork.EvaluacionesMeritos.GetByMeritoIdAsync(dto.MeritoPostulanteId);

                if (evaluacionExistente != null)
                {
                    evaluacionExistente.PuntajeAsignado = dto.PuntajeAsignado;
                    evaluacionExistente.DocumentacionVerificada = dto.DocumentacionVerificada;
                    evaluacionExistente.Observaciones = dto.Observaciones;
                    evaluacionExistente.Estado = dto.DocumentacionVerificada ? "Aprobado" : "Rechazado";
                    evaluacionExistente.FechaEvaluacion = DateTime.Now;

                    await _unitOfWork.EvaluacionesMeritos.UpdateAsync(evaluacionExistente);
                }
                else
                {
                    var nuevaEvaluacion = new EvaluacionMerito
                    {
                        MeritoPostulanteId = dto.MeritoPostulanteId,
                        PuntajeAsignado = dto.PuntajeAsignado,
                        DocumentacionVerificada = dto.DocumentacionVerificada,
                        Observaciones = dto.Observaciones,
                        Estado = dto.DocumentacionVerificada ? "Aprobado" : "Rechazado",
                        FechaEvaluacion = DateTime.Now
                    };

                    evaluacionExistente = await _unitOfWork.EvaluacionesMeritos.AddAsync(nuevaEvaluacion);
                }

                await _unitOfWork.CommitTransactionAsync();

                return new ApiResponseDto<EvaluacionMeritoDto>
                {
                    Success = true,
                    Message = "Mérito valorado exitosamente",
                    Data = new EvaluacionMeritoDto
                    {
                        Id = evaluacionExistente.Id,
                        MeritoPostulanteId = evaluacionExistente.MeritoPostulanteId,
                        NombreItem = item.Nombre,
                        CategoriaItem = item.Categoria,
                        PuntajeMaximo = item.PuntajeMaximo,
                        PuntajeAsignado = evaluacionExistente.PuntajeAsignado,
                        DocumentoRespaldo = merito.DocumentoRespaldo,
                        Estado = evaluacionExistente.Estado,
                        Observaciones = evaluacionExistente.Observaciones,
                        DocumentacionVerificada = evaluacionExistente.DocumentacionVerificada,
                        FechaEvaluacion = evaluacionExistente.FechaEvaluacion
                    }
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ApiResponseDto<EvaluacionMeritoDto>
                {
                    Success = false,
                    Message = "Error al valorar mérito",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Valora en lote múltiples méritos asociados a una inscripción.
        /// </summary>
        /// <param name="inscripcionId">Identificador de la inscripción evaluada.</param>
        /// <param name="meritos">Listado de méritos a valorar con sus puntajes.</param>
        /// <returns>
        /// ApiResponseDto con la colección de evaluaciones registradas exitosamente.
        /// </returns>
        public async Task<ApiResponseDto<List<EvaluacionMeritoDto>>> ValorarMeritosAsync(int inscripcionId, List<ValorarMeritoDto> meritos)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var resultados = new List<EvaluacionMeritoDto>();

                foreach (var meritoDto in meritos)
                {
                    var merito = await _unitOfWork.MeritosPostulante.GetByIdAsync(meritoDto.MeritoPostulanteId);
                    if (merito == null)
                    {
                        continue;
                    }

                    var item = await _unitOfWork.ItemsPuntuables.GetByIdAsync(merito.ItemPuntuableId);
                    if (item == null)
                    {
                        continue;
                    }

                    if (meritoDto.PuntajeAsignado < 0 || meritoDto.PuntajeAsignado > item.PuntajeMaximo)
                    {
                        continue;
                    }

                    var evaluacionExistente = await _unitOfWork.EvaluacionesMeritos.GetByMeritoIdAsync(meritoDto.MeritoPostulanteId);

                    if (evaluacionExistente != null)
                    {
                        evaluacionExistente.PuntajeAsignado = meritoDto.PuntajeAsignado;
                        evaluacionExistente.DocumentacionVerificada = meritoDto.DocumentacionVerificada;
                        evaluacionExistente.Observaciones = meritoDto.Observaciones;
                        evaluacionExistente.Estado = meritoDto.DocumentacionVerificada ? "Aprobado" : "Rechazado";
                        evaluacionExistente.FechaEvaluacion = DateTime.Now;

                        await _unitOfWork.EvaluacionesMeritos.UpdateAsync(evaluacionExistente);
                    }
                    else
                    {
                        var nuevaEvaluacion = new EvaluacionMerito
                        {
                            MeritoPostulanteId = meritoDto.MeritoPostulanteId,
                            PuntajeAsignado = meritoDto.PuntajeAsignado,
                            DocumentacionVerificada = meritoDto.DocumentacionVerificada,
                            Observaciones = meritoDto.Observaciones,
                            Estado = meritoDto.DocumentacionVerificada ? "Aprobado" : "Rechazado",
                            FechaEvaluacion = DateTime.Now
                        };

                        evaluacionExistente = await _unitOfWork.EvaluacionesMeritos.AddAsync(nuevaEvaluacion);
                    }

                    resultados.Add(new EvaluacionMeritoDto
                    {
                        Id = evaluacionExistente.Id,
                        MeritoPostulanteId = evaluacionExistente.MeritoPostulanteId,
                        NombreItem = item.Nombre,
                        CategoriaItem = item.Categoria,
                        PuntajeMaximo = item.PuntajeMaximo,
                        PuntajeAsignado = evaluacionExistente.PuntajeAsignado,
                        DocumentoRespaldo = merito.DocumentoRespaldo,
                        Estado = evaluacionExistente.Estado,
                        Observaciones = evaluacionExistente.Observaciones,
                        DocumentacionVerificada = evaluacionExistente.DocumentacionVerificada,
                        FechaEvaluacion = evaluacionExistente.FechaEvaluacion
                    });
                }

                await _unitOfWork.CommitTransactionAsync();

                return new ApiResponseDto<List<EvaluacionMeritoDto>>
                {
                    Success = true,
                    Message = $"{resultados.Count} méritos valorados exitosamente",
                    Data = resultados
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ApiResponseDto<List<EvaluacionMeritoDto>>
                {
                    Success = false,
                    Message = "Error al valorar méritos",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Genera los ordenamientos (listas de prelación) de un llamado aplicando reglas de desempate y cuotas.
        /// Implementa RF-15: generación de ordenamientos preliminares/definitivos.
        /// </summary>
        /// <param name="dto">DTO con parámetros de generación (llamado, puntaje mínimo, cuotas, etc.)</param>
        /// <returns>
        /// ApiResponseDto con ResultadoGeneracionOrdenamientoDto que incluye las listas generadas y estadísticas.
        /// Success = false si no existen inscripciones que cumplan requisitos o el llamado no existe.
        /// </returns>
        /// <exception cref="Exception">Rollback de la transacción ante fallos</exception>
        public async Task<ApiResponseDto<ResultadoGeneracionOrdenamientoDto>> GenerarOrdenamientoAsync(GenerarOrdenamientoDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var llamado = await _unitOfWork.Llamados.GetByIdWithDetallesAsync(dto.LlamadoId);
                if (llamado == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ApiResponseDto<ResultadoGeneracionOrdenamientoDto>
                    {
                        Success = false,
                        Message = "Llamado no encontrado"
                    };
                }

                var inscripciones = await _unitOfWork.Inscripciones.GetByLlamadoIdAsync(dto.LlamadoId);
                var inscripcionesConPuntaje = new List<InscripcionConPuntaje>();

                foreach (var inscripcion in inscripciones)
                {
                    var evaluacionesPruebas = await _unitOfWork.EvaluacionesPruebas.GetByInscripcionIdAsync(inscripcion.Id);
                    var evaluacionesMeritos = await _unitOfWork.EvaluacionesMeritos.GetByInscripcionIdAsync(inscripcion.Id);

                    var puntajePruebas = evaluacionesPruebas.Where(e => e.Verificado).Sum(e => e.PuntajeObtenido);
                    var puntajeMeritos = evaluacionesMeritos.Where(e => e.Estado == "Aprobado").Sum(e => e.PuntajeAsignado);
                    var puntajeTotal = puntajePruebas + puntajeMeritos;
                    var aproboTodasPruebas = evaluacionesPruebas.All(e => e.Aprobado);

                    if (puntajeTotal >= dto.PuntajeMinimoAprobacion && aproboTodasPruebas)
                    {
                        inscripcionesConPuntaje.Add(new InscripcionConPuntaje
                        {
                            Inscripcion = inscripcion,
                            PuntajePruebas = puntajePruebas,
                            PuntajeMeritos = puntajeMeritos,
                            PuntajeTotal = puntajeTotal
                        });
                    }
                }

                if (!inscripcionesConPuntaje.Any())
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new ApiResponseDto<ResultadoGeneracionOrdenamientoDto>
                    {
                        Success = false,
                        Message = "No hay inscripciones que cumplan el puntaje mínimo"
                    };
                }

                var ordenamientosGenerados = new List<OrdenamientoDto>();
                var estadoOrdenamiento = dto.EsDefinitivo ? "Definitivo" : "Preliminar";

                var ordenamientoGeneral = await CrearOrdenamientoAsync(
                    dto.LlamadoId,
                    "General",
                    estadoOrdenamiento,
                    inscripcionesConPuntaje,
                    null,
                    llamado);
                ordenamientosGenerados.Add(MapearOrdenamientoADto(ordenamientoGeneral));

                if (dto.AplicarCuotas)
                {
                    var afrodescendientes = inscripcionesConPuntaje
                        .Where(i => i.Inscripcion.AutodefinicionLey?.EsAfrodescendiente == true)
                        .ToList();

                    if (afrodescendientes.Any())
                    {
                        var ordenamientoAfro = await CrearOrdenamientoAsync(
                            dto.LlamadoId,
                            "Afrodescendiente",
                            estadoOrdenamiento,
                            afrodescendientes,
                            null,
                            llamado);
                        ordenamientosGenerados.Add(MapearOrdenamientoADto(ordenamientoAfro));
                    }

                    var trans = inscripcionesConPuntaje
                        .Where(i => i.Inscripcion.AutodefinicionLey?.EsTrans == true)
                        .ToList();

                    if (trans.Any())
                    {
                        var ordenamientoTrans = await CrearOrdenamientoAsync(
                            dto.LlamadoId,
                            "Trans",
                            estadoOrdenamiento,
                            trans,
                            null,
                            llamado);
                        ordenamientosGenerados.Add(MapearOrdenamientoADto(ordenamientoTrans));
                    }

                    var discapacidad = inscripcionesConPuntaje
                        .Where(i => i.Inscripcion.AutodefinicionLey?.TieneDiscapacidad == true)
                        .ToList();

                    if (discapacidad.Any())
                    {
                        var ordenamientoDisc = await CrearOrdenamientoAsync(
                            dto.LlamadoId,
                            "Discapacidad",
                            estadoOrdenamiento,
                            discapacidad,
                            null,
                            llamado);
                        ordenamientosGenerados.Add(MapearOrdenamientoADto(ordenamientoDisc));
                    }

                    await AplicarCuotasLeyAsync(ordenamientoGeneral, llamado);
                }

                await _unitOfWork.CommitTransactionAsync();

                var estadisticas = new EstadisticasOrdenamientoDto
                {
                    TotalEvaluados = inscripciones.Count(),
                    TotalAprobados = inscripcionesConPuntaje.Count,
                    TotalReprobados = inscripciones.Count() - inscripcionesConPuntaje.Count,
                    PuntajePromedio = inscripcionesConPuntaje.Average(i => i.PuntajeTotal),
                    PuntajeMaximo = inscripcionesConPuntaje.Max(i => i.PuntajeTotal),
                    PuntajeMinimo = inscripcionesConPuntaje.Min(i => i.PuntajeTotal)
                };

                if (dto.AplicarCuotas)
                {
                    var posicionesGeneral = await _unitOfWork.PosicionesOrdenamiento
                        .GetByOrdenamientoIdAsync(ordenamientoGeneral.Id);

                    estadisticas.CuotaAfroAplicada = posicionesGeneral
                        .Count(p => p.AplicaCuota && p.TipoCuota == "Afrodescendiente");
                    estadisticas.CuotaTransAplicada = posicionesGeneral
                        .Count(p => p.AplicaCuota && p.TipoCuota == "Trans");
                    estadisticas.CuotaDiscapacidadAplicada = posicionesGeneral
                        .Count(p => p.AplicaCuota && p.TipoCuota == "Discapacidad");
                }

                return new ApiResponseDto<ResultadoGeneracionOrdenamientoDto>
                {
                    Success = true,
                    Message = $"{ordenamientosGenerados.Count} ordenamiento(s) generado(s) exitosamente",
                    Data = new ResultadoGeneracionOrdenamientoDto
                    {
                        Success = true,
                        Message = "Ordenamientos generados correctamente",
                        OrdenamientosGenerados = ordenamientosGenerados,
                        Estadisticas = estadisticas
                    }
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ApiResponseDto<ResultadoGeneracionOrdenamientoDto>
                {
                    Success = false,
                    Message = "Error al generar ordenamiento",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// Obtiene todas las listas de ordenamiento generadas para un llamado determinado.
        /// </summary>
        /// <param name="llamadoId">Identificador del llamado.</param>
        /// <returns>
        /// ApiResponseDto con el resumen de ordenamientos (tipo, estado y cantidad de posiciones).
        /// </returns>
        public async Task<ApiResponseDto<List<OrdenamientoDto>>> ObtenerOrdenamientosAsync(int llamadoId)
        {
            try
            {
                var ordenamientos = await _unitOfWork.Ordenamientos.GetByLlamadoIdAsync(llamadoId);

                var resultado = new List<OrdenamientoDto>();
                foreach (var ord in ordenamientos)
                {
                    var posiciones = await _unitOfWork.PosicionesOrdenamiento.GetByOrdenamientoIdAsync(ord.Id);

                    resultado.Add(new OrdenamientoDto
                    {
                        Id = ord.Id,
                        LlamadoId = ord.LlamadoId,
                        TituloLlamado = ord.Llamado.Titulo,
                        DepartamentoId = ord.DepartamentoId,
                        NombreDepartamento = ord.Departamento?.Nombre,
                        Tipo = ord.Tipo,
                        FechaGeneracion = ord.FechaGeneracion,
                        Estado = ord.Estado,
                        CantidadPosiciones = posiciones.Count()
                    });
                }

                return new ApiResponseDto<List<OrdenamientoDto>>
                {
                    Success = true,
                    Message = "Ordenamientos obtenidos exitosamente",
                    Data = resultado
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<OrdenamientoDto>>
                {
                    Success = false,
                    Message = "Error al obtener ordenamientos",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<OrdenamientoDetalleDto>> ObtenerDetalleOrdenamientoAsync(int ordenamientoId)
        {
            try
            {
                var ordenamiento = await _unitOfWork.Ordenamientos.GetByIdWithPosicionesAsync(ordenamientoId);
                if (ordenamiento == null)
                {
                    return new ApiResponseDto<OrdenamientoDetalleDto>
                    {
                        Success = false,
                        Message = "Ordenamiento no encontrado"
                    };
                }

                var posiciones = new List<PosicionOrdenamientoDto>();
                foreach (var posicion in ordenamiento.Posiciones.OrderBy(p => p.Posicion))
                {
                    var evaluacionesPruebas = await _unitOfWork.EvaluacionesPruebas
                        .GetByInscripcionIdAsync(posicion.InscripcionId);
                    var evaluacionesMeritos = await _unitOfWork.EvaluacionesMeritos
                        .GetByInscripcionIdAsync(posicion.InscripcionId);

                    posiciones.Add(new PosicionOrdenamientoDto
                    {
                        Posicion = posicion.Posicion,
                        NombreCompleto = $"{posicion.Inscripcion.Postulante.Nombre} {posicion.Inscripcion.Postulante.Apellido}",
                        CedulaIdentidad = posicion.Inscripcion.Postulante.CedulaIdentidad,
                        Departamento = posicion.Inscripcion.Departamento.Nombre,
                        PuntajeTotal = posicion.PuntajeTotal,
                        AplicaCuota = posicion.AplicaCuota,
                        TipoCuota = posicion.TipoCuota,
                        PuntajePruebas = evaluacionesPruebas.Sum(e => e.PuntajeObtenido),
                        PuntajeMeritos = evaluacionesMeritos.Where(e => e.Estado == "Aprobado").Sum(e => e.PuntajeAsignado)
                    });
                }

                var resultado = new OrdenamientoDetalleDto
                {
                    Id = ordenamiento.Id,
                    TituloLlamado = ordenamiento.Llamado.Titulo,
                    Tipo = ordenamiento.Tipo,
                    Estado = ordenamiento.Estado,
                    FechaGeneracion = ordenamiento.FechaGeneracion,
                    Posiciones = posiciones
                };

                return new ApiResponseDto<OrdenamientoDetalleDto>
                {
                    Success = true,
                    Message = "Detalle obtenido exitosamente",
                    Data = resultado
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<OrdenamientoDetalleDto>
                {
                    Success = false,
                    Message = "Error al obtener detalle del ordenamiento",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<bool>> PublicarOrdenamientoAsync(int ordenamientoId)
        {
            try
            {
                var ordenamiento = await _unitOfWork.Ordenamientos.GetByIdAsync(ordenamientoId);
                if (ordenamiento == null)
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Ordenamiento no encontrado"
                    };
                }

                ordenamiento.Estado = "Publicado";
                await _unitOfWork.Ordenamientos.UpdateAsync(ordenamiento);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Ordenamiento publicado exitosamente"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Error al publicar ordenamiento",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDto<EstadisticasTribunalDto>> ObtenerEstadisticasAsync(int llamadoId)
        {
            try
            {
                var llamado = await _unitOfWork.Llamados.GetByIdAsync(llamadoId);
                if (llamado == null)
                {
                    return new ApiResponseDto<EstadisticasTribunalDto>
                    {
                        Success = false,
                        Message = "Llamado no encontrado"
                    };
                }

                var inscripciones = await _unitOfWork.Inscripciones.GetByLlamadoIdAsync(llamadoId);
                var pruebas = await _unitOfWork.Pruebas.GetByLlamadoIdAsync(llamadoId);

                int conPruebasCompletas = 0;
                int conMeritosCompletos = 0;
                int aprobadosPruebas = 0;
                int aprobadosFinal = 0;
                decimal sumaPromedios = 0;

                foreach (var inscripcion in inscripciones)
                {
                    var evaluacionesPruebas = await _unitOfWork.EvaluacionesPruebas.GetByInscripcionIdAsync(inscripcion.Id);
                    var evaluacionesMeritos = await _unitOfWork.EvaluacionesMeritos.GetByInscripcionIdAsync(inscripcion.Id);
                    var meritos = await _unitOfWork.MeritosPostulante.GetByInscripcionIdAsync(inscripcion.Id);

                    if (evaluacionesPruebas.Count() == pruebas.Count())
                    {
                        conPruebasCompletas++;
                    }

                    if (evaluacionesMeritos.Count() == meritos.Count())
                    {
                        conMeritosCompletos++;
                    }

                    if (evaluacionesPruebas.Any() && evaluacionesPruebas.All(e => e.Aprobado))
                    {
                        aprobadosPruebas++;
                    }

                    var puntajeTotal = evaluacionesPruebas.Sum(e => e.PuntajeObtenido) +
                                      evaluacionesMeritos.Where(e => e.Estado == "Aprobado").Sum(e => e.PuntajeAsignado);

                    if (puntajeTotal >= 70 && evaluacionesPruebas.Any() && evaluacionesPruebas.All(e => e.Aprobado))
                    {
                        aprobadosFinal++;
                    }

                    sumaPromedios += puntajeTotal;
                }

                var detallesPruebas = new List<EstadisticaPruebaDto>();
                foreach (var prueba in pruebas)
                {
                    var evaluaciones = await _unitOfWork.EvaluacionesPruebas.GetByPruebaIdAsync(prueba.Id);

                    detallesPruebas.Add(new EstadisticaPruebaDto
                    {
                        NombrePrueba = prueba.Nombre,
                        Evaluados = evaluaciones.Count(),
                        Aprobados = evaluaciones.Count(e => e.Aprobado),
                        PromedioNota = evaluaciones.Any() ? evaluaciones.Average(e => e.PuntajeObtenido) : 0
                    });
                }

                var ordenamientos = await _unitOfWork.Ordenamientos.GetByLlamadoIdAsync(llamadoId);
                var ordenamientoDefinitivo = ordenamientos.FirstOrDefault(o => o.Estado == "Definitivo");

                var resultado = new EstadisticasTribunalDto
                {
                    LlamadoId = llamadoId,
                    TituloLlamado = llamado.Titulo,
                    TotalInscripciones = inscripciones.Count(),
                    InscripcionesConPruebasCompletas = conPruebasCompletas,
                    InscripcionesConMeritosCompletos = conMeritosCompletos,
                    TotalPruebas = pruebas.Count(),
                    DetallesPruebas = detallesPruebas,
                    AprobadosPruebas = aprobadosPruebas,
                    AprobadosFinal = aprobadosFinal,
                    PromedioGeneral = inscripciones.Any() ? sumaPromedios / inscripciones.Count() : 0,
                    TotalAfrodescendientes = inscripciones.Count(i => i.AutodefinicionLey?.EsAfrodescendiente == true),
                    TotalTrans = inscripciones.Count(i => i.AutodefinicionLey?.EsTrans == true),
                    TotalDiscapacidad = inscripciones.Count(i => i.AutodefinicionLey?.TieneDiscapacidad == true),
                    OrdenamientoGenerado = ordenamientoDefinitivo != null,
                    FechaOrdenamiento = ordenamientoDefinitivo?.FechaGeneracion
                };

                return new ApiResponseDto<EstadisticasTribunalDto>
                {
                    Success = true,
                    Message = "Estadísticas obtenidas exitosamente",
                    Data = resultado
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<EstadisticasTribunalDto>
                {
                    Success = false,
                    Message = "Error al obtener estadísticas",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        private async Task<Ordenamiento> CrearOrdenamientoAsync(
            int llamadoId,
            string tipo,
            string estado,
            List<InscripcionConPuntaje> inscripciones,
            int? departamentoId,
            Llamado llamado)
        {
            var inscripcionesOrdenadas = inscripciones
                .OrderByDescending(i => i.PuntajeTotal)
                .ThenByDescending(i => i.PuntajePruebas)
                .ToList();

            var ordenamiento = new Ordenamiento
            {
                LlamadoId = llamadoId,
                Tipo = tipo,
                Estado = estado,
                FechaGeneracion = DateTime.Now,
                DepartamentoId = departamentoId,
                Llamado = llamado
            };

            await _unitOfWork.Ordenamientos.AddAsync(ordenamiento);
            await _unitOfWork.SaveChangesAsync();

            var posiciones = new List<PosicionOrdenamiento>();
            int posicion = 1;

            foreach (var inscripcion in inscripcionesOrdenadas)
            {
                posiciones.Add(new PosicionOrdenamiento
                {
                    OrdenamientoId = ordenamiento.Id,
                    InscripcionId = inscripcion.Inscripcion.Id,
                    Posicion = posicion++,
                    PuntajeTotal = inscripcion.PuntajeTotal,
                    AplicaCuota = false,
                    TipoCuota = null
                });
            }

            await _unitOfWork.PosicionesOrdenamiento.AddRangeAsync(posiciones);
            await _unitOfWork.SaveChangesAsync();

            ordenamiento.Posiciones = posiciones;

            return ordenamiento;
        }

        private async Task AplicarCuotasLeyAsync(Ordenamiento ordenamientoGeneral, Llamado llamado)
        {
            var posiciones = await _unitOfWork.PosicionesOrdenamiento
                .GetByOrdenamientoIdAsync(ordenamientoGeneral.Id);

            var totalPuestos = llamado.CantidadPuestos;

            var cupoAfro = (int)Math.Ceiling(totalPuestos * (llamado.PorcentajeAfrodescendiente / 100));
            var cupoTrans = (int)Math.Ceiling(totalPuestos * (llamado.PorcentajeTrans / 100));
            var cupoDisc = (int)Math.Ceiling(totalPuestos * (llamado.PorcentajeDiscapacidad / 100));

            var afrodescendientes = posiciones
                .Where(p => p.Inscripcion.AutodefinicionLey?.EsAfrodescendiente == true)
                .OrderBy(p => p.Posicion)
                .Take(cupoAfro)
                .ToList();

            foreach (var pos in afrodescendientes)
            {
                pos.AplicaCuota = true;
                pos.TipoCuota = "Afrodescendiente";
                await _unitOfWork.PosicionesOrdenamiento.UpdateAsync(pos);
            }

            var trans = posiciones
                .Where(p => p.Inscripcion.AutodefinicionLey?.EsTrans == true && !p.AplicaCuota)
                .OrderBy(p => p.Posicion)
                .Take(cupoTrans)
                .ToList();

            foreach (var pos in trans)
            {
                pos.AplicaCuota = true;
                pos.TipoCuota = "Trans";
                await _unitOfWork.PosicionesOrdenamiento.UpdateAsync(pos);
            }

            var discapacidad = posiciones
                .Where(p => p.Inscripcion.AutodefinicionLey?.TieneDiscapacidad == true && !p.AplicaCuota)
                .OrderBy(p => p.Posicion)
                .Take(cupoDisc)
                .ToList();

            foreach (var pos in discapacidad)
            {
                pos.AplicaCuota = true;
                pos.TipoCuota = "Discapacidad";
                await _unitOfWork.PosicionesOrdenamiento.UpdateAsync(pos);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        private OrdenamientoDto MapearOrdenamientoADto(Ordenamiento ordenamiento)
        {
            return new OrdenamientoDto
            {
                Id = ordenamiento.Id,
                LlamadoId = ordenamiento.LlamadoId,
                TituloLlamado = ordenamiento.Llamado?.Titulo ?? string.Empty,
                DepartamentoId = ordenamiento.DepartamentoId,
                NombreDepartamento = ordenamiento.Departamento?.Nombre,
                Tipo = ordenamiento.Tipo,
                FechaGeneracion = ordenamiento.FechaGeneracion,
                Estado = ordenamiento.Estado,
                CantidadPosiciones = ordenamiento.Posiciones?.Count ?? 0
            };
        }

        private class InscripcionConPuntaje
        {
            public Inscripcion Inscripcion { get; set; } = null!;
            public decimal PuntajePruebas { get; set; }
            public decimal PuntajeMeritos { get; set; }
            public decimal PuntajeTotal { get; set; }
        }
    }
}
