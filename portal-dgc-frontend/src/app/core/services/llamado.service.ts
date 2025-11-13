import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { ApiResponse } from '../models/api-response.model';
import { Llamado, LlamadoDetalle } from '../models/llamado.model';

@Injectable({
  providedIn: 'root'
})
export class LlamadoService {
  private readonly endpoint = 'llamado';

  constructor(private apiService: ApiService) {}

  obtenerLlamadosActivos(): Observable<ApiResponse<Llamado[]>> {
    return this.apiService.get<ApiResponse<Llamado[]>>(`${this.endpoint}/activos`);
  }

  obtenerLlamadosInactivos(): Observable<ApiResponse<Llamado[]>> {
    return this.apiService.get<ApiResponse<Llamado[]>>(`${this.endpoint}/inactivos`);
  }

  obtenerLlamadoDetalle(id: number): Observable<ApiResponse<LlamadoDetalle>> {
    return this.apiService.get<ApiResponse<LlamadoDetalle>>(`${this.endpoint}/${id}`);
  }

  validarLlamadoDisponible(id: number): Observable<ApiResponse<boolean>> {
    return this.apiService.get<ApiResponse<boolean>>(
      `${this.endpoint}/${id}/validar-disponible`
    );
  }
}
