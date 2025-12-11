import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  /**
   * @description Ejecuta una petición HTTP GET contra la API del backend.
   * @param {string} endpoint Ruta relativa del recurso (sin la URL base).
   * @returns {Observable<T>} Observable con el cuerpo de la respuesta tipado.
   * @throws {HttpErrorResponse} Cuando la petición falla por problemas de red o estados HTTP distintos de 2xx.
   */
  get<T>(endpoint: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}/${endpoint}`);
  }

  /**
   * @description Ejecuta una petición HTTP POST contra la API del backend.
   * @param {string} endpoint Ruta relativa del recurso.
   * @param {unknown} data Payload a enviar en el cuerpo de la petición.
  * @returns {Observable<T>} Observable con la respuesta devuelta por el backend.
   * @throws {HttpErrorResponse} Cuando la operación POST falla.
   */
  post<T>(endpoint: string, data: unknown): Observable<T> {
    return this.http.post<T>(`${this.apiUrl}/${endpoint}`, data);
  }

  /**
   * @description Ejecuta una petición HTTP PUT contra la API del backend.
   * @param {string} endpoint Ruta relativa del recurso.
   * @param {unknown} data Datos a persistir en el recurso.
   * @returns {Observable<T>} Observable con la respuesta tipada.
   * @throws {HttpErrorResponse} Cuando la operación PUT devuelve un error.
   */
  put<T>(endpoint: string, data: unknown): Observable<T> {
    return this.http.put<T>(`${this.apiUrl}/${endpoint}`, data);
  }

  /**
   * @description Ejecuta una petición HTTP DELETE contra la API del backend.
   * @param {string} endpoint Ruta relativa del recurso a eliminar.
   * @returns {Observable<T>} Observable con la respuesta del servidor tras eliminar el recurso.
   * @throws {HttpErrorResponse} Cuando el borrado falla en el backend.
   */
  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(`${this.apiUrl}/${endpoint}`);
  }
}
