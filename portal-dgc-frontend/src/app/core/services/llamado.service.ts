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

  /**
   * @description RF-03: Recupera la lista de llamados vigentes disponibles para inscripción, respetando filtros definidos en el backend.
   * @returns {Observable<ApiResponse<Llamado[]>>} Observable con la respuesta del API que contiene los llamados activos.
   * @throws {HttpErrorResponse} Cuando se produce un error al consultar los llamados vigentes.
   */
  obtenerLlamadosActivos(): Observable<ApiResponse<Llamado[]>> {
    return this.apiService.get<ApiResponse<Llamado[]>>(`${this.endpoint}/activos`);
  }

  /**
   * @description RF-03: Recupera los llamados que ya no están vigentes para tareas administrativas o de seguimiento.
   * @returns {Observable<ApiResponse<Llamado[]>>} Observable con los llamados inactivos registrados por el sistema.
   * @throws {HttpErrorResponse} Cuando la consulta de llamados inactivos falla.
   */
  obtenerLlamadosInactivos(): Observable<ApiResponse<Llamado[]>> {
    return this.apiService.get<ApiResponse<Llamado[]>>(`${this.endpoint}/inactivos`);
  }

  /**
   * @description RF-04: Obtiene el detalle completo de un llamado incluyendo requisitos excluyentes, ítems puntuables y apoyos disponibles.
   * @param {number} id Identificador del llamado que se desea consultar.
   * @returns {Observable<ApiResponse<LlamadoDetalle>>} Observable con la respuesta del API y el detalle del llamado solicitado.
   * @throws {HttpErrorResponse} Cuando el llamado no existe o la consulta genera un error.
   */
  obtenerLlamadoDetalle(id: number): Observable<ApiResponse<LlamadoDetalle>> {
    return this.apiService.get<ApiResponse<LlamadoDetalle>>(`${this.endpoint}/${id}`);
  }

  /**
   * @description RF-03: Valida si el llamado sigue abierto para nuevas inscripciones de postulantes.
   * @param {number} id Identificador del llamado a validar.
   * @returns {Observable<ApiResponse<boolean>>} Observable con true cuando el llamado está disponible y false en caso contrario.
   * @throws {HttpErrorResponse} Cuando no se puede contactar el servicio de llamados.
   */
  validarLlamadoDisponible(id: number): Observable<ApiResponse<boolean>> {
    return this.apiService.get<ApiResponse<boolean>>(
      `${this.endpoint}/${id}/validar-disponible`
    );
  }
}
