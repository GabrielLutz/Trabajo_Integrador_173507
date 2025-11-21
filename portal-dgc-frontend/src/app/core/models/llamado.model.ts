/**
 * Resumen de un llamado publicado que se muestra en los listados (RF-03).
 */
export interface Llamado {
  id: number;
  titulo: string;
  descripcion: string;
  fechaApertura: Date;
  fechaCierre: Date;
  estado: string;
  estaHabilitadoInscripcion?: boolean;
}

/**
 * Detalle completo del llamado consultado en RF-04, incluye requisitos y apoyos.
 */
export interface LlamadoDetalle {
  id: number;
  titulo: string;
  descripcion: string;
  bases: string;
  fechaApertura: Date;
  fechaCierre: Date;
  cantidadPuestos: number;
  estado: string;
  estaHabilitadoInscripcion?: boolean;
  departamentos: DepartamentoLlamado[];
  requisitosExcluyentes: RequisitoExcluyente[];
  itemsPuntuables: ItemPuntuable[];
  apoyosNecesarios: ApoyoNecesario[];
}

/**
 * Departamento asociado al llamado con la cantidad de puestos disponibles.
 */
export interface DepartamentoLlamado {
  departamentoId: number;
  nombre: string;
  codigo: string;
  cantidadPuestos: number;
}

/**
 * Requisito excluyente que debe cumplir el postulante para proseguir.
 */
export interface RequisitoExcluyente {
  id: number;
  descripcion: string;
  tipo: string;
  obligatorio: boolean;
}

/**
 * Item puntuable utilizado para evaluar méritos del postulante.
 */
export interface ItemPuntuable {
  id: number;
  nombre: string;
  descripcion: string;
  puntajeMaximo: number;
  categoria: string;
}

/**
 * Apoyo logístico previsto por la organización para los llamados inclusivos.
 */
export interface ApoyoNecesario {
  id: number;
  descripcion: string;
  tipo: string;
}
