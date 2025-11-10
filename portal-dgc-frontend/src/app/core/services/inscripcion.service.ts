import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { ApiResponse } from '../models/api-response.model';
import {
  CrearInscripcion,
  InscripcionResponse,
  InscripcionSimple
} from '../models/inscripcion.model';

@Injectable({
  providedIn: 'root'
})
export class InscripcionService {
  private readonly endpoint = 'inscripcion';

  constructor(private apiService: ApiService) {}

  crearInscripcion(
    postulanteId: number,
    inscripcion: CrearInscripcion
  ): Observable<ApiResponse<InscripcionResponse>> {
    return this.apiService.post<ApiResponse<InscripcionResponse>>(
      `${this.endpoint}/postulante/${postulanteId}`,
      inscripcion
    );
  }

  obtenerInscripcion(id: number): Observable<ApiResponse<InscripcionResponse>> {
    return this.apiService.get<ApiResponse<InscripcionResponse>>(
      `${this.endpoint}/${id}`
    );
  }

  obtenerInscripcionesPorPostulante(
    postulanteId: number
  ): Observable<ApiResponse<InscripcionSimple[]>> {
    return this.apiService.get<ApiResponse<InscripcionSimple[]>>(
      `${this.endpoint}/postulante/${postulanteId}`
    );
  }

  validarInscripcionExistente(
    postulanteId: number,
    llamadoId: number
  ): Observable<ApiResponse<boolean>> {
    return this.apiService.get<ApiResponse<boolean>>(
      `${this.endpoint}/validar/${postulanteId}/${llamadoId}`
    );
  }
}
