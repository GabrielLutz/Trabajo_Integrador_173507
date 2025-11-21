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

  /**
   * @description RF-05: Crea una inscripción completa que incluye autodefinición, requisitos, méritos y apoyos solicitados.
   * @param {number} postulanteId Identificador del postulante que se está inscribiendo al llamado.
   * @param {CrearInscripcion} inscripcion Payload con la información necesaria para registrar la inscripción.
   * @returns {Observable<ApiResponse<InscripcionResponse>>} Observable con la respuesta del API que incluye los datos de la inscripción creada.
   * @throws {HttpErrorResponse} Cuando la creación falla por validaciones de negocio o errores de comunicación.
   */
  crearInscripcion(
    postulanteId: number,
    inscripcion: CrearInscripcion
  ): Observable<ApiResponse<InscripcionResponse>> {
    return this.apiService.post<ApiResponse<InscripcionResponse>>(
      `${this.endpoint}/postulante/${postulanteId}`,
      inscripcion
    );
  }

  /**
   * @description RF-08: Obtiene el detalle completo de una inscripción, incluyendo requisitos, méritos y apoyos registrados.
   * @param {number} id Identificador de la inscripción que se desea consultar.
   * @returns {Observable<ApiResponse<InscripcionResponse>>} Observable con la respuesta del API y la inscripción detallada.
   * @throws {HttpErrorResponse} Cuando la inscripción no existe o ocurre un error al consultar el backend.
   */
  obtenerInscripcion(id: number): Observable<ApiResponse<InscripcionResponse>> {
    return this.apiService.get<ApiResponse<InscripcionResponse>>(
      `${this.endpoint}/${id}`
    );
  }

  /**
   * @description RF-07: Lista las inscripciones realizadas por un postulante junto con su estado actual.
   * @param {number} postulanteId Identificador del postulante del que se desean recuperar las inscripciones.
   * @returns {Observable<ApiResponse<InscripcionSimple[]>>} Observable con la colección de inscripciones resumidas.
   * @throws {HttpErrorResponse} Cuando ocurre un error durante la consulta al servicio.
   */
  obtenerInscripcionesPorPostulante(
    postulanteId: number
  ): Observable<ApiResponse<InscripcionSimple[]>> {
    return this.apiService.get<ApiResponse<InscripcionSimple[]>>(
      `${this.endpoint}/postulante/${postulanteId}`
    );
  }

  /**
   * @description RF-05: Valida si ya existe una inscripción del postulante para el llamado indicado antes de permitir un nuevo registro.
   * @param {number} postulanteId Identificador del postulante.
   * @param {number} llamadoId Identificador del llamado al que se intenta inscribir.
   * @returns {Observable<ApiResponse<boolean>>} Observable con true si ya existe una inscripción y false si se puede crear una nueva.
   * @throws {HttpErrorResponse} Cuando el servicio devuelve un error.
   */
  validarInscripcionExistente(
    postulanteId: number,
    llamadoId: number
  ): Observable<ApiResponse<boolean>> {
    return this.apiService.get<ApiResponse<boolean>>(
      `${this.endpoint}/validar/${postulanteId}/${llamadoId}`
    );
  }
}
