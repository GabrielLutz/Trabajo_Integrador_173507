/**
 * Resumen de la inscripción que el tribunal debe evaluar (RF-11 y RF-12).
 */
export interface InscripcionParaEvaluar {
  inscripcionId: number;
  postulanteId: number;
  nombreCompleto: string;
  cedulaIdentidad: string;
  email: string;
  departamento: string;
  fechaInscripcion: Date;
  estadoInscripcion: string;
  esAfrodescendiente: boolean;
  esTrans: boolean;
  tieneDiscapacidad: boolean;
  pruebasEvaluadas: number;
  pruebasTotales: number;
  meritosEvaluados: number;
  meritosTotales: number;
  puntajePruebas?: number;
  puntajeMeritos?: number;
  puntajeTotal?: number;
  aproboPruebas?: boolean;
}

/**
 * Detalle completo para mostrar al tribunal antes de calificar (RF-11).
 */
export interface DetalleEvaluacion {
  inscripcionId: number;
  nombreCompleto: string;
  cedulaIdentidad: string;
  email: string;
  departamento: string;
  esAfrodescendiente: boolean;
  esTrans: boolean;
  tieneDiscapacidad: boolean;
  requisitos: RequisitoPostulanteResponse[];
  pruebas: EvaluacionPrueba[];
  meritos: MeritoParaEvaluar[];
  puntajePruebas: number;
  puntajeMeritos: number;
  puntajeTotal: number;
}

/**
 * Requisito evaluado dentro del flujo de revisión del tribunal.
 */
export interface RequisitoPostulanteResponse {
  requisitoId: number;
  descripcionRequisito: string;
  obligatorio: boolean;
  cumple: boolean;
  observaciones?: string;
}

/**
 * Resultado de la evaluación de una prueba por parte del tribunal (RF-11).
 */
export interface EvaluacionPrueba {
  id: number;
  inscripcionId: number;
  pruebaId: number;
  nombrePrueba: string;
  tipoPrueba: string;
  puntajeMaximo: number;
  puntajeObtenido: number;
  aprobado: boolean;
  observaciones?: string;
  fechaEvaluacion: Date;
  verificado: boolean;
}

/**
 * Mérito pendiente de valoración según RF-12.
 */
export interface MeritoParaEvaluar {
  meritoPostulanteId: number;
  nombreItem: string;
  categoria: string;
  puntajeMaximo: number;
  documentoRespaldo?: string;
  fueEvaluado: boolean;
  puntajeAsignado?: number;
  documentacionVerificada?: boolean;
  observaciones?: string;
  estado?: string;
}

/**
 * Payload que envía el tribunal para calificar una prueba (RF-11).
 */
export interface CalificarPruebaDto {
  inscripcionId: number;
  pruebaId: number;
  puntajeObtenido: number;
  observaciones?: string;
}

/**
 * Payload que registra la valoración de un mérito (RF-12).
 */
export interface ValorarMeritoDto {
  meritoPostulanteId: number;
  puntajeAsignado: number;
  documentacionVerificada: boolean;
  observaciones?: string;
}

/**
 * Metadatos de una prueba planificada dentro del llamado (RF-11).
 */
export interface PruebaDto {
  id: number;
  llamadoId: number;
  tipo: string;
  nombre: string;
  descripcion: string;
  puntajeMaximo: number;
  fechaProgramada: Date;
  lugar: string;
  estado: string;
  esObligatoria: boolean;
  ordenEjecucion: number;
  cantidadEvaluados?: number;
  cantidadAprobados?: number;
  promedioGeneral?: number;
}

/**
 * Ordenamiento generado luego de la evaluación, utilizado en RF-14.
 */
export interface Ordenamiento {
  id: number;
  llamadoId: number;
  tituloLlamado: string;
  departamentoId?: number;
  nombreDepartamento?: string;
  tipo: string;
  fechaGeneracion: Date;
  estado: string;
  cantidadPosiciones: number;
}

/**
 * Detalle del ordenamiento para visualizar posiciones y puntajes (RF-14).
 */
export interface OrdenamientoDetalle {
  id: number;
  tituloLlamado: string;
  tipo: string;
  estado: string;
  fechaGeneracion: Date;
  posiciones: PosicionOrdenamiento[];
}

/**
 * Posición individual dentro del ordenamiento resultante.
 */
export interface PosicionOrdenamiento {
  posicion: number;
  nombreCompleto: string;
  cedulaIdentidad: string;
  departamento: string;
  puntajeTotal: number;
  aplicaCuota: boolean;
  tipoCuota?: string;
  puntajePruebas?: number;
  puntajeMeritos?: number;
}

/**
 * Estadísticas generales del tribunal que apoyan el seguimiento de RF-11.
 */
export interface EstadisticasTribunal {
  llamadoId: number;
  tituloLlamado: string;
  totalInscripciones: number;
  inscripcionesConPruebasCompletas: number;
  inscripcionesConMeritosCompletos: number;
  totalPruebas: number;
  detallesPruebas: EstadisticaPrueba[];
  aprobadosPruebas: number;
  aprobadosFinal: number;
  promedioGeneral: number;
  totalAfrodescendientes: number;
  totalTrans: number;
  totalDiscapacidad: number;
  ordenamientoGenerado: boolean;
  fechaOrdenamiento?: Date;
}

/**
 * Estadísticas por prueba incluidas en el tablero del tribunal.
 */
export interface EstadisticaPrueba {
  nombrePrueba: string;
  evaluados: number;
  aprobados: number;
  promedioNota: number;
}

/**
 * Solicitud proveniente del tribunal para generar o regenerar el ordenamiento (RF-14).
 */
export interface GenerarOrdenamientoDto {
  llamadoId: number;
  aplicarCuotas: boolean;
  puntajeMinimoAprobacion: number;
  esDefinitivo: boolean;
}

/**
 * Respuesta genérica local cuando el módulo opera sin depender del contrato compartido.
 */
export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data?: T;
  errors: string[];
}
