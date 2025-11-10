import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { ApiResponse } from '../models/api-response.model';
import { Departamento } from '../models/departamento.model';
import { DepartamentoLlamado } from '../models/llamado.model';

@Injectable({
  providedIn: 'root'
})
export class DepartamentoService {
  private readonly endpoint = 'departamento';

  constructor(private apiService: ApiService) {}

  obtenerDepartamentosActivos(): Observable<ApiResponse<Departamento[]>> {
    return this.apiService.get<ApiResponse<Departamento[]>>(this.endpoint);
  }

  obtenerDepartamentosPorLlamado(
    llamadoId: number
  ): Observable<ApiResponse<DepartamentoLlamado[]>> {
    return this.apiService.get<ApiResponse<DepartamentoLlamado[]>>(
      `${this.endpoint}/llamado/${llamadoId}`
    );
  }
}
