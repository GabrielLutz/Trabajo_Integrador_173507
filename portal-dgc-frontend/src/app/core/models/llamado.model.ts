export interface Llamado {
  id: number;
  titulo: string;
  descripcion: string;
  fechaApertura: Date;
  fechaCierre: Date;
  estado: string;
}

export interface LlamadoDetalle {
  id: number;
  titulo: string;
  descripcion: string;
  bases: string;
  fechaApertura: Date;
  fechaCierre: Date;
  cantidadPuestos: number;
  estado: string;
  departamentos: DepartamentoLlamado[];
  requisitosExcluyentes: RequisitoExcluyente[];
  itemsPuntuables: ItemPuntuable[];
  apoyosNecesarios: ApoyoNecesario[];
}

export interface DepartamentoLlamado {
  departamentoId: number;
  nombre: string;
  codigo: string;
  cantidadPuestos: number;
}

export interface RequisitoExcluyente {
  id: number;
  descripcion: string;
  tipo: string;
  obligatorio: boolean;
}

export interface ItemPuntuable {
  id: number;
  nombre: string;
  descripcion: string;
  puntajeMaximo: number;
  categoria: string;
}

export interface ApoyoNecesario {
  id: number;
  descripcion: string;
  tipo: string;
}
