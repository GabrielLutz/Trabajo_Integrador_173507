export interface CrearInscripcion {
	llamadoId: number;
	departamentoId: number;
	autodefinicion: AutodefinicionLey;
	requisitos: RequisitoPostulante[];
	meritos: MeritoPostulante[];
	apoyosIds: number[];
}

export interface AutodefinicionLey {
	esAfrodescendiente: boolean;
	esTrans: boolean;
	tieneDiscapacidad: boolean;
}

export interface RequisitoPostulante {
	requisitoId: number;
	cumple: boolean;
	observaciones?: string;
}

export interface MeritoPostulante {
	itemPuntuableId: number;
	documentoRespaldo?: string;
}

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

export interface RequisitoPostulanteResponse {
	id: number;
	requisitoId: number;
	descripcionRequisito: string;
	tipoRequisito: string;
	obligatorio: boolean;
	cumple: boolean;
	observaciones?: string;
}

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

export interface ApoyoSolicitadoResponse {
	id: number;
	apoyoId: number;
	descripcionApoyo: string;
	tipoApoyo: string;
	justificacion?: string;
}

export interface InscripcionSimple {
	id: number;
	tituloLlamado: string;
	nombreDepartamento: string;
	fechaInscripcion: Date;
	estado: string;
}
