/**
 * Payload utilizado en RF-06 para adjuntar una constancia en formato Base64.
 */
export interface SubirConstancia {
  /** Nombre original proporcionado por el postulante. */
  nombre: string;
  /** Tipo de documento seleccionado en la UI (ej. PDF, JPG). */
  tipo: string;
  /** Contenido codificado en Base64 del archivo adjunto. */
  archivo: string;
}

/**
 * Constancia asociada al postulante, tal como la devuelve el backend en RF-06.
 */
export interface Constancia {
  /** Identificador único asignado por el backend. */
  id: number;
  /** Nombre descriptivo del archivo adjunto. */
  nombre: string;
  /** Tipo o categoría de la constancia. */
  tipo: string;
  /** Contenido codificado de la constancia cuando corresponde al flujo de descarga. */
  archivo: string;
  /** Fecha y hora del registro en la plataforma. */
  fechaSubida: Date;
  /** Marca si la constancia fue revisada y aceptada. */
  validado: boolean;
}
