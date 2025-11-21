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

  /**
   * @description RF-11: Recupera las inscripciones de un llamado junto con su estado de evaluación para que el tribunal gestione el proceso.
   * @param {number} llamadoId Identificador del llamado que se está evaluando.
   * @returns {Observable<ApiResponse<InscripcionParaEvaluar[]>>} Observable con la colección de inscripciones y sus indicadores de evaluación.
   * @throws {HttpErrorResponse} Cuando ocurre un fallo en la consulta al servicio de tribunal.
   */
  obtenerInscripcionesParaEvaluar(llamadoId: number): Observable<ApiResponse<InscripcionParaEvaluar[]>> {
    return this.http.get<ApiResponse<InscripcionParaEvaluar[]>>(
      `${this.apiUrl}/llamado/${llamadoId}/inscripciones`
    );
  }

  /**
   * @description RF-11: Devuelve el detalle completo de una inscripción, incluyendo evaluaciones, requisitos y méritos.
   * @param {number} inscripcionId Identificador de la inscripción a consultar.
   * @returns {Observable<ApiResponse<DetalleEvaluacion>>} Observable con la información consolidada para el tribunal.
   * @throws {HttpErrorResponse} Cuando el detalle no puede obtenerse.
   */
  obtenerDetalleEvaluacion(inscripcionId: number): Observable<ApiResponse<DetalleEvaluacion>> {
    return this.http.get<ApiResponse<DetalleEvaluacion>>(
      `${this.apiUrl}/inscripcion/${inscripcionId}/detalle`
    );
  }

  /**
   * @description RF-11: Obtiene estadísticas agregadas del tribunal para un llamado determinado.
   * @param {number} llamadoId Identificador del llamado.
   * @returns {Observable<ApiResponse<EstadisticasTribunal>>} Observable con indicadores de evaluaciones, aprobaciones y cuotas.
   * @throws {HttpErrorResponse} Cuando la consulta de estadísticas falla.
   */
  obtenerEstadisticas(llamadoId: number): Observable<ApiResponse<EstadisticasTribunal>> {
    return this.http.get<ApiResponse<EstadisticasTribunal>>(
      `${this.apiUrl}/llamado/${llamadoId}/estadisticas`
    );
  }

  /**
   * @description RF-11: Lista las pruebas configuradas para un llamado con información de evaluaciones registradas.
   * @param {number} llamadoId Identificador del llamado.
   * @returns {Observable<ApiResponse<PruebaDto[]>>} Observable con los metadatos de las pruebas.
   * @throws {HttpErrorResponse} Cuando la consulta no puede realizarse correctamente.
   */
  obtenerPruebasDelLlamado(llamadoId: number): Observable<ApiResponse<PruebaDto[]>> {
    return this.http.get<ApiResponse<PruebaDto[]>>(
      `${this.apiUrl}/llamado/${llamadoId}/pruebas`
    );
  }

  /**
   * @description RF-11: Registra la evaluación de una prueba asignando un puntaje entre 0 y el máximo permitido.
   * @param {CalificarPruebaDto} dto Datos de la prueba a calificar, incluyendo puntaje obtenido y observaciones.
   * @returns {Observable<ApiResponse<EvaluacionPrueba>>} Observable con la evaluación de la prueba registrada.
   * @throws {HttpErrorResponse} Cuando la calificación no puede guardarse por errores de negocio o de red.
   */
  calificarPrueba(dto: CalificarPruebaDto): Observable<ApiResponse<EvaluacionPrueba>> {
    return this.http.post<ApiResponse<EvaluacionPrueba>>(`${this.apiUrl}/calificar-prueba`, dto);
  }

  /**
   * @description RF-12: Valora un mérito individual verificando documentación y asignando puntaje según la tabla de evaluación.
   * @param {ValorarMeritoDto} dto Información del mérito del postulante junto con la documentación a validar.
   * @returns {Observable<ApiResponse<any>>} Observable con la evaluación del mérito procesada por el tribunal.
   * @throws {HttpErrorResponse} Cuando la valoración falla o la API devuelve un error.
   */
  valorarMerito(dto: ValorarMeritoDto): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${this.apiUrl}/valorar-merito`, dto);
  }

  /**
   * @description RF-12: Valora en lote múltiples méritos asociados a una inscripción.
   * @param {number} inscripcionId Identificador de la inscripción evaluada.
   * @param {ValorarMeritoDto[]} meritos Listado de méritos a valorar con puntajes y verificaciones.
   * @returns {Observable<ApiResponse<any[]>>} Observable con las evaluaciones generadas.
   * @throws {HttpErrorResponse} Cuando ocurre un error durante la valoración masiva.
   */
  valorarMeritos(inscripcionId: number, meritos: ValorarMeritoDto[]): Observable<ApiResponse<any[]>> {
    return this.http.post<ApiResponse<any[]>>(
      `${this.apiUrl}/inscripcion/${inscripcionId}/valorar-meritos`,
      meritos
    );
  }

  /**
   * @description RF-14: Genera listas de ordenamiento aplicando reglas de desempate y sorteo aleatorio cuando corresponde.
   * @param {GenerarOrdenamientoDto} dto Parámetros de generación del ordenamiento (llamado, puntaje mínimo, cuotas y banderas de definitividad).
   * @returns {Observable<ApiResponse<any>>} Observable con el resultado de la generación del ordenamiento y estadísticas asociadas.
   * @throws {HttpErrorResponse} Cuando se produce un error al generar las listas de prelación.
   */
  generarOrdenamiento(dto: GenerarOrdenamientoDto): Observable<ApiResponse<any>> {
    return this.http.post<ApiResponse<any>>(`${this.apiUrl}/generar-ordenamiento`, dto);
  }

  /**
   * @description RF-15: Obtiene las listas de prelación generadas para un llamado determinado.
   * @param {number} llamadoId Identificador del llamado del que se requieren los ordenamientos.
   * @returns {Observable<ApiResponse<Ordenamiento[]>>} Observable con la colección de ordenamientos disponibles.
   * @throws {HttpErrorResponse} Cuando la consulta de ordenamientos falla.
   */
  obtenerOrdenamientos(llamadoId: number): Observable<ApiResponse<Ordenamiento[]>> {
    return this.http.get<ApiResponse<Ordenamiento[]>>(
      `${this.apiUrl}/llamado/${llamadoId}/ordenamientos`
    );
  }

  /**
   * @description RF-15: Obtiene el detalle de un ordenamiento específico con las posiciones resultantes.
   * @param {number} ordenamientoId Identificador del ordenamiento requerido.
   * @returns {Observable<ApiResponse<OrdenamientoDetalle>>} Observable con las posiciones y datos adicionales.
   * @throws {HttpErrorResponse} Cuando el detalle de ordenamiento no está disponible.
   */
  obtenerDetalleOrdenamiento(ordenamientoId: number): Observable<ApiResponse<OrdenamientoDetalle>> {
    return this.http.get<ApiResponse<OrdenamientoDetalle>>(
      `${this.apiUrl}/ordenamiento/${ordenamientoId}`
    );
  }

  /**
   * @description RF-15: Publica un ordenamiento para dejarlo disponible a otros actores.
   * @param {number} ordenamientoId Identificador del ordenamiento a publicar.
   * @returns {Observable<ApiResponse<boolean>>} Observable que indica si la publicación fue exitosa.
   * @throws {HttpErrorResponse} Cuando el backend no puede publicar el ordenamiento.
   */
  publicarOrdenamiento(ordenamientoId: number): Observable<ApiResponse<boolean>> {
    return this.http.post<ApiResponse<boolean>>(
      `${this.apiUrl}/ordenamiento/${ordenamientoId}/publicar`,
      {}
    );
  }
}
