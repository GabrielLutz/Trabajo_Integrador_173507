export interface Postulante {
	id: number;
	nombre: string;
	apellido: string;
	fechaNacimiento: Date;
	cedulaIdentidad: string;
	genero: string;
	generoOtro?: string;
	email: string;
	celular: string;
	telefono?: string;
	domicilio: string;
	documentoCedula?: string;
	datosCompletados: boolean;
}

export interface PostulanteDatosPersonales {
	nombre: string;
	apellido: string;
	fechaNacimiento: Date;
	cedulaIdentidad: string;
	genero: string;
	generoOtro?: string;
	email: string;
	celular: string;
	telefono?: string;
	domicilio: string;
}
