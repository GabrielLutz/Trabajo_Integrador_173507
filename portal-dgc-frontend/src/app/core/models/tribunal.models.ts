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

export interface RequisitoPostulanteResponse {
  requisitoId: number;
  descripcionRequisito: string;
  obligatorio: boolean;
  cumple: boolean;
  observaciones?: string;
}

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

export interface CalificarPruebaDto {
  inscripcionId: number;
  pruebaId: number;
  puntajeObtenido: number;
  observaciones?: string;
}

export interface ValorarMeritoDto {
  meritoPostulanteId: number;
  puntajeAsignado: number;
  documentacionVerificada: boolean;
  observaciones?: string;
}

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

export interface OrdenamientoDetalle {
  id: number;
  tituloLlamado: string;
  tipo: string;
  estado: string;
  fechaGeneracion: Date;
  posiciones: PosicionOrdenamiento[];
}

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

export interface EstadisticaPrueba {
  nombrePrueba: string;
  evaluados: number;
  aprobados: number;
  promedioNota: number;
}

export interface GenerarOrdenamientoDto {
  llamadoId: number;
  aplicarCuotas: boolean;
  puntajeMinimoAprobacion: number;
  esDefinitivo: boolean;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data?: T;
  errors: string[];
}
