/**
 * Contrato genérico de respuesta utilizado en todas las llamadas al backend.
 * Acompaña los RF expuestos en el portal para describir resultados, mensajes de negocio y errores.
 */
export interface ApiResponse<T> {
  /** Indica si la operación solicitada se resolvió correctamente. */
  success: boolean;
  /** Mensaje principal a mostrar al usuario con el resultado del proceso. */
  message: string;
  /** Carga útil específica del endpoint, en caso de éxito parcial o total. */
  data?: T;
  /** Listado de errores de validación o negocio retornados por el backend. */
  errors: string[];
}
