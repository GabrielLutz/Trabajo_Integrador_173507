/**
 * Representación mínima de un departamento habilitado para un llamado (RF-03).
 */
export interface Departamento {
  /** Identificador interno del departamento dentro del portal. */
  id: number;
  /** Nombre legible que se muestra en los listados y combos. */
  nombre: string;
  /** Código oficial del departamento, usado en integraciones. */
  codigo: string;
}
