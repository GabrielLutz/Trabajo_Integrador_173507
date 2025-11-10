import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { ApiResponse } from '../models/api-response.model';
import { Postulante, PostulanteDatosPersonales } from '../models/postulante';

@Injectable({
  providedIn: 'root'
})
export class PostulanteService {
  private readonly endpoint = 'postulante';

  constructor(private apiService: ApiService) {}

  obtenerPostulante(id: number): Observable<ApiResponse<Postulante>> {
    return this.apiService.get<ApiResponse<Postulante>>(`${this.endpoint}/${id}`);
  }

  actualizarDatosPersonales(
    id: number,
    datos: PostulanteDatosPersonales
  ): Observable<ApiResponse<Postulante>> {
    return this.apiService.put<ApiResponse<Postulante>>(
      `${this.endpoint}/${id}/datos-personales`,
      datos
    );
  }

  validarCedulaDisponible(cedula: string): Observable<ApiResponse<boolean>> {
    return this.apiService.get<ApiResponse<boolean>>(
      `${this.endpoint}/validar-cedula/${cedula}`
    );
  }

  validarEmailDisponible(email: string): Observable<ApiResponse<boolean>> {
    return this.apiService.get<ApiResponse<boolean>>(
      `${this.endpoint}/validar-email/${email}`
    );
  }
}
