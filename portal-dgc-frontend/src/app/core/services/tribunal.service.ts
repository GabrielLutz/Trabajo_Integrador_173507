import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ApiResponse,
  CalificarPruebaDto,
  DetalleEvaluacion,
  EvaluacionPrueba,
  GenerarOrdenamientoDto,
  InscripcionParaEvaluar,
  Ordenamiento,
  OrdenamientoDetalle,
  PruebaDto,
  ValorarMeritoDto,
  EstadisticasTribunal
} from '../models/tribunal.models';

@Injectable({
  providedIn: 'root'
})
export class TribunalService {
  private readonly apiUrl = `${environment.apiUrl}/tribunal`;

  constructor(private readonly http: HttpClient) {}

  obtenerInscripcionesParaEvaluar(llamadoId: number): Observable<ApiResponse<InscripcionParaEvaluar[]>> {
    return this.http.get<ApiResponse<InscripcionParaEvaluar[]>>(
      `${this.apiUrl}/llamado/${llamadoId}/inscripciones`
    );
  }

  obtenerDetalleEvaluacion(inscripcionId: number): Observable<ApiResponse<DetalleEvaluacion>> {
    return this.http.get<ApiResponse<DetalleEvaluacion>>(
      `${this.apiUrl}/inscripcion/${inscripcionId}/detalle`
    );
  }

  obtenerEstadisticas(llamadoId: number): Observable<ApiResponse<EstadisticasTribunal>> {
    return this.http.get<ApiResponse<EstadisticasTribunal>>(
      `${this.apiUrl}/llamado/${llamadoId}/estadisticas`
    );
  }

  obtenerPruebasDelLlamado(llamadoId: number): Observable<ApiResponse<PruebaDto[]>> {
    return this.http.get<ApiResponse<PruebaDto[]>>(
      `${this.apiUrl}/llamado/${llamadoId}/pruebas`
    );
  }

  calificarPrueba(dto: CalificarPruebaDto): Observable<ApiResponse<EvaluacionPrueba>> {
    return this.http.post<ApiResponse<EvaluacionPrueba>>(`${this.apiUrl}/calificar-prueba`, dto);
  }

  valorarMerito(dto: ValorarMeritoDto): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${this.apiUrl}/valorar-merito`, dto);
  }

  valorarMeritos(inscripcionId: number, meritos: ValorarMeritoDto[]): Observable<ApiResponse<any[]>> {
    return this.http.post<ApiResponse<any[]>>(
      `${this.apiUrl}/inscripcion/${inscripcionId}/valorar-meritos`,
      meritos
    );
  }

  generarOrdenamiento(dto: GenerarOrdenamientoDto): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${this.apiUrl}/generar-ordenamiento`, dto);
  }

  obtenerOrdenamientos(llamadoId: number): Observable<ApiResponse<Ordenamiento[]>> {
    return this.http.get<ApiResponse<Ordenamiento[]>>(
      `${this.apiUrl}/llamado/${llamadoId}/ordenamientos`
    );
  }

  obtenerDetalleOrdenamiento(ordenamientoId: number): Observable<ApiResponse<OrdenamientoDetalle>> {
    return this.http.get<ApiResponse<OrdenamientoDetalle>>(
      `${this.apiUrl}/ordenamiento/${ordenamientoId}`
    );
  }

  publicarOrdenamiento(ordenamientoId: number): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(
      `${this.apiUrl}/ordenamiento/${ordenamientoId}/publicar`,
      {}
    );
  }
}
