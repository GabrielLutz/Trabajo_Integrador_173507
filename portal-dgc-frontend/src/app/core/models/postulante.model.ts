/**
 * Datos completos del postulante recuperados en RF-01 para componer el perfil.
 */
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

/**
 * Payload enviado al backend para actualizar los datos personales (RF-02).
 */
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
