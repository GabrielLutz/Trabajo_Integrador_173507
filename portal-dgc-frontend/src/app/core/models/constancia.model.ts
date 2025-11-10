export interface SubirConstancia {
  nombre: string;
  tipo: string;
  archivo: string; // Base64
}

export interface Constancia {
  id: number;
  nombre: string;
  tipo: string;
  archivo: string;
  fechaSubida: Date;
  validado: boolean;
}
