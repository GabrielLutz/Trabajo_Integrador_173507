import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { ApiResponse } from '../models/api-response.model';
import { Constancia, SubirConstancia } from '../models/constancia.model';

@Injectable({
  providedIn: 'root'
})
export class ConstanciaService {
  private readonly endpoint = 'constancia';

  constructor(private apiService: ApiService) {}

  subirConstancia(
    postulanteId: number,
    constancia: SubirConstancia
  ): Observable<ApiResponse<Constancia>> {
    return this.apiService.post<ApiResponse<Constancia>>(
      `${this.endpoint}/postulante/${postulanteId}`,
      constancia
    );
  }

  obtenerConstanciasPorPostulante(
    postulanteId: number
  ): Observable<ApiResponse<Constancia[]>> {
    return this.apiService.get<ApiResponse<Constancia[]>>(
      `${this.endpoint}/postulante/${postulanteId}`
    );
  }

  descargarConstancia(id: number): Observable<Blob> {
    return this.apiService.get<Blob>(`${this.endpoint}/${id}/descargar`);
  }
}
