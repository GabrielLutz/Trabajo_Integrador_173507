import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { ApiResponse } from '../models/api-response.model';
import { Postulante, PostulanteDatosPersonales } from '../models/postulante.model';

@Injectable({
  providedIn: 'root'
})
export class PostulanteService {
  private readonly endpoint = 'postulante';

  constructor(private apiService: ApiService) {}

  /**
   * @description RF-01: Obtiene los datos del postulante y el indicador de completitud del perfil.
   * @param {number} id Identificador único del postulante cuyos datos se desean visualizar.
   * @returns {Observable<ApiResponse<Postulante>>} Observable con la respuesta del API que incluye la información del postulante y mensajes de negocio.
   * @throws {HttpErrorResponse} Cuando el backend responde con error de red u otro estado diferente de 2xx.
   */
  obtenerPostulante(id: number): Observable<ApiResponse<Postulante>> {
    return this.apiService.get<ApiResponse<Postulante>>(`${this.endpoint}/${id}`);
  }

  /**
   * @description RF-02: Actualiza la información personal del postulante aplicando las validaciones de negocio.
   * @param {number} id Identificador del postulante a actualizar.
   * @param {PostulanteDatosPersonales} datos Datos personales completos que se desean persistir.
   * @returns {Observable<ApiResponse<Postulante>>} Observable con la respuesta del API y el postulante actualizado cuando la operación es exitosa.
   * @throws {HttpErrorResponse} Cuando el backend rechaza la actualización o se produce un error de comunicación.
   */
  actualizarDatosPersonales(
    id: number,
    datos: PostulanteDatosPersonales
  ): Observable<ApiResponse<Postulante>> {
    return this.apiService.put<ApiResponse<Postulante>>(
      `${this.endpoint}/${id}/datos-personales`,
      datos
    );
  }

  /**
   * @description RF-02: Verifica en tiempo real la disponibilidad de una cédula de identidad.
   * @param {string} cedula Número de cédula uruguaya sin puntos ni guiones que se desea validar.
   * @returns {Observable<ApiResponse<boolean>>} Observable con true cuando la cédula está disponible y false cuando ya existe.
   * @throws {HttpErrorResponse} Cuando ocurre un error al consultar el servicio de validación.
   */
  validarCedulaDisponible(cedula: string): Observable<ApiResponse<boolean>> {
    return this.apiService.get<ApiResponse<boolean>>(
      `${this.endpoint}/validar-cedula/${cedula}`
    );
  }

  /**
   * @description RF-02: Comprueba si un email está libre para ser utilizado por un postulante.
   * @param {string} email Dirección de correo electrónico a validar.
   * @returns {Observable<ApiResponse<boolean>>} Observable con true cuando el email está disponible, false en caso contrario.
   * @throws {HttpErrorResponse} Cuando se produce un error al contactar el servicio de validación.
   */
  validarEmailDisponible(email: string): Observable<ApiResponse<boolean>> {
    return this.apiService.get<ApiResponse<boolean>>(
      `${this.endpoint}/validar-email/${email}`
    );
  }
}
