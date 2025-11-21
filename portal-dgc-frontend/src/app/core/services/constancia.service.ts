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

  /**
   * @description RF-06: Valida formato y registra una constancia en PDF o imagen para el postulante.
   * @param {number} postulanteId Identificador del postulante propietario de la constancia.
   * @param {SubirConstancia} constancia Datos y contenido del archivo a subir.
   * @returns {Observable<ApiResponse<Constancia>>} Observable con la respuesta del API y la constancia persistida.
   * @throws {HttpErrorResponse} Cuando el backend rechaza el archivo o ocurre un error de comunicación.
   */
  subirConstancia(
    postulanteId: number,
    constancia: SubirConstancia
  ): Observable<ApiResponse<Constancia>> {
    return this.apiService.post<ApiResponse<Constancia>>(
      `${this.endpoint}/postulante/${postulanteId}`,
      constancia
    );
  }

  /**
   * @description RF-06: Obtiene el listado de constancias cargadas por el postulante.
   * @param {number} postulanteId Identificador del postulante.
   * @returns {Observable<ApiResponse<Constancia[]>>} Observable con la colección de constancias asociadas.
   * @throws {HttpErrorResponse} Cuando el servicio de constancias devuelve un error.
   */
  obtenerConstanciasPorPostulante(
    postulanteId: number
  ): Observable<ApiResponse<Constancia[]>> {
    return this.apiService.get<ApiResponse<Constancia[]>>(
      `${this.endpoint}/postulante/${postulanteId}`
    );
  }

  /**
   * @description RF-06: Descarga el archivo binario de una constancia previamente almacenada.
   * @param {number} id Identificador de la constancia que se desea descargar.
   * @returns {Observable<Blob>} Observable con el contenido del archivo.
   * @throws {HttpErrorResponse} Cuando el archivo no existe o no puede obtenerse.
   */
  descargarConstancia(id: number): Observable<Blob> {
    return this.apiService.get<Blob>(`${this.endpoint}/${id}/descargar`);
  }
}
