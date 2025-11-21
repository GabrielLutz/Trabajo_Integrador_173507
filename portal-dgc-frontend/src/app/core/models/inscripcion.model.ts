/**
 * Payload utilizado en RF-05 para iniciar una nueva inscripción.
 */
export interface CrearInscripcion {
  /** Identificador del llamado seleccionado en el paso 1. */
  llamadoId: number;
  /** Departamento donde se realiza la inscripción. */
  departamentoId: number;
  /** Autodefinición proporcionada en cumplimiento de la Ley 19.122 (RF-08). */
  autodefinicion: AutodefinicionLey;
  /** Requisitos declarados por el postulante durante la carga (RF-07). */
  requisitos: RequisitoPostulante[];
  /** Méritos y antecedentes adjuntados para evaluación. */
  meritos: MeritoPostulante[];
  /** Apoyos solicitados por el postulante para el proceso de evaluación. */
  apoyosIds: number[];
}

/**
 * Datos sensibles de autodefinición requeridos en el proceso inclusivo.
 */
export interface AutodefinicionLey {
  esAfrodescendiente: boolean;
  esTrans: boolean;
  tieneDiscapacidad: boolean;
}

/**
 * Declaración de cumplimiento de un requisito excluyente u opcional.
 */
export interface RequisitoPostulante {
  requisitoId: number;
  cumple: boolean;
  observaciones?: string;
}

/**
 * Mérito aportado por el postulante con su respaldo documental.
 */
export interface MeritoPostulante {
  itemPuntuableId: number;
  documentoRespaldo?: string;
}

/**
 * Respuesta completa de la API al consultar una inscripción (RF-05).
 */
export interface InscripcionResponse {
  id: number;
  postulanteId: number;
  nombrePostulante: string;
  llamadoId: number;
  tituloLlamado: string;
  departamentoId: number;
  nombreDepartamento: string;
  fechaInscripcion: Date;
  estado: string;
  puntajeTotal: number;
  autodefinicion?: AutodefinicionLey;
  requisitos: RequisitoPostulanteResponse[];
  meritos: MeritoPostulanteResponse[];
  apoyos: ApoyoSolicitadoResponse[];
}

/**
 * Requisito evaluado tal como lo devuelve el backend en la consulta detallada.
 */
export interface RequisitoPostulanteResponse {
  id: number;
  requisitoId: number;
  descripcionRequisito: string;
  tipoRequisito: string;
  obligatorio: boolean;
  cumple: boolean;
  observaciones?: string;
}

/**
 * Mérito evaluado con el puntaje obtenido por el postulante.
 */
export interface MeritoPostulanteResponse {
  id: number;
  itemPuntuableId: number;
  nombreItem: string;
  descripcionItem: string;
  puntajeMaximo: number;
  categoria: string;
  documentoRespaldo?: string;
  puntajeObtenido: number;
  verificado: boolean;
}

/**
 * Apoyo solicitado por el postulante para garantizar accesibilidad en las evaluaciones.
 */
export interface ApoyoSolicitadoResponse {
  id: number;
  apoyoId: number;
  descripcionApoyo: string;
  tipoApoyo: string;
  justificacion?: string;
}

/**
 * Resumen utilizado en el listado "Mis inscripciones" (RF-05).
 */
export interface InscripcionSimple {
  id: number;
  tituloLlamado: string;
  nombreDepartamento: string;
  fechaInscripcion: Date;
  estado: string;
}
