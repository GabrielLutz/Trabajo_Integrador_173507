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

  /**
   * @description RF-03: Obtiene los departamentos activos que están disponibles para ser seleccionados durante la inscripción.
   * @returns {Observable<ApiResponse<Departamento[]>>} Observable con la respuesta del API que lista los departamentos habilitados.
   * @throws {HttpErrorResponse} Cuando el servicio de departamentos devuelve un error.
   */
  obtenerDepartamentosActivos(): Observable<ApiResponse<Departamento[]>> {
    return this.apiService.get<ApiResponse<Departamento[]>>(this.endpoint);
  }

  /**
   * @description RF-03: Devuelve los departamentos disponibles para un llamado específico.
   * @param {number} llamadoId Identificador del llamado.
   * @returns {Observable<ApiResponse<DepartamentoLlamado[]>>} Observable con la lista de departamentos asociados al llamado.
   * @throws {HttpErrorResponse} Cuando la consulta no puede realizarse correctamente.
   */
  obtenerDepartamentosPorLlamado(
    llamadoId: number
  ): Observable<ApiResponse<DepartamentoLlamado[]>> {
    return this.apiService.get<ApiResponse<DepartamentoLlamado[]>>(
      `${this.endpoint}/llamado/${llamadoId}`
    );
  }
}
