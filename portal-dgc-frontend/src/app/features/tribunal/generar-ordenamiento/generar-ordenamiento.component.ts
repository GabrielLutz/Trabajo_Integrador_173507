import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators, ReactiveFormsModule, NonNullableFormBuilder } from '@angular/forms';
import { TribunalService } from '../../../core/services/tribunal.service';
import { GenerarOrdenamientoDto, InscripcionParaEvaluar } from '../../../core/models/tribunal.models';

@Component({
  selector: 'app-generar-ordenamiento',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './generar-ordenamiento.component.html',
  styleUrls: ['./generar-ordenamiento.component.scss']
})
export class GenerarOrdenamientoComponent implements OnInit {
  llamadoId!: number;
  form: FormGroup<{
    puntajeMinimoAprobacion: FormControl<number>;
    aplicarCuotas: FormControl<boolean>;
    esDefinitivo: FormControl<boolean>;
  }>;

  // Wizard steps
  pasoActual = 1;
  totalPasos = 3;

  // Estados
  generando = false;
  error: string | null = null;
  resultado: any = null;

  // Preview
  previewInscripciones: InscripcionParaEvaluar[] = [];
  cargandoPreview = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: NonNullableFormBuilder,
    private tribunalService: TribunalService
  ) {
    this.form = this.fb.group({
      puntajeMinimoAprobacion: [70, [Validators.required, Validators.min(0), Validators.max(100)]],
      aplicarCuotas: [true],
      esDefinitivo: [false]
    });
  }

  /**
   * Recupera el llamado desde la ruta y carga la previsualización inicial (RF-14).
   */
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.llamadoId = +params['llamadoId'];
      this.cargarPreview();
    });
  }

  /**
   * Obtiene inscripciones evaluadas para mostrar un preview antes de generar el ordenamiento.
   */
  cargarPreview(): void {
    this.cargandoPreview = true;

    this.tribunalService.obtenerInscripcionesParaEvaluar(this.llamadoId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          // Filtrar solo inscripciones con evaluación completa
          this.previewInscripciones = response.data
            .filter(i => i.pruebasEvaluadas === i.pruebasTotales && i.puntajeTotal !== null)
            .sort((a, b) => (b.puntajeTotal || 0) - (a.puntajeTotal || 0));
        }
        this.cargandoPreview = false;
      },
      error: (err) => {
        console.error(err);
        this.cargandoPreview = false;
      }
    });
  }

  /**
   * Avanza al siguiente paso del asistente.
   */
  siguientePaso(): void {
    if (this.pasoActual < this.totalPasos) {
      this.pasoActual++;
    }
  }

  /**
   * Retrocede un paso en el asistente.
   */
  pasoAnterior(): void {
    if (this.pasoActual > 1) {
      this.pasoActual--;
    }
  }

  /**
   * Invoca el endpoint de generación de ordenamiento aplicando los parámetros elegidos (RF-14).
   */
  generarOrdenamiento(): void {
    if (this.form.invalid) return;

    this.generando = true;
    this.error = null;

    const dto: GenerarOrdenamientoDto = {
      llamadoId: this.llamadoId,
      aplicarCuotas: this.aplicarCuotas,
      puntajeMinimoAprobacion: this.puntajeMinimoAprobacion,
      esDefinitivo: this.esDefinitivo
    };

    this.tribunalService.generarOrdenamiento(dto).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.resultado = response.data;
          this.pasoActual = 3; // Ir a resumen
        } else {
          this.error = response.message;
        }
        this.generando = false;
      },
      error: (err) => {
        this.error = 'Error al generar ordenamiento';
        console.error(err);
        this.generando = false;
      }
    });
  }

  /**
   * Navega al detalle del ordenamiento recién generado.
   */
  verOrdenamiento(ordenamientoId: number): void {
    this.router.navigate(['/tribunal/ordenamiento', ordenamientoId]);
  }

  /**
   * Regresa al dashboard del tribunal.
   */
  volver(): void {
    this.router.navigate(['/tribunal/dashboard']);
  }

  /**
   * Cantidad de postulantes que superan el puntaje mínimo configurado.
   */
  getCantidadAprobados(): number {
    const minimo = this.puntajeMinimoAprobacion;
    return this.previewInscripciones.filter(i => (i.puntajeTotal || 0) >= minimo).length;
  }

  /**
   * Cantidad de aprobados por universo de acción afirmativa para los indicadores.
   */
  getCantidadPorUniverso(universo: 'afro' | 'trans' | 'disc'): number {
    const minimo = this.puntajeMinimoAprobacion;
    const filtrados = this.previewInscripciones.filter(i => (i.puntajeTotal || 0) >= minimo);

    switch (universo) {
      case 'afro': return filtrados.filter(i => i.esAfrodescendiente).length;
      case 'trans': return filtrados.filter(i => i.esTrans).length;
      case 'disc': return filtrados.filter(i => i.tieneDiscapacidad).length;
      default: return 0;
    }
  }

  /**
   * Puntaje mínimo ingresado en el formulario.
   */
  get puntajeMinimoAprobacion(): number {
    return this.form.controls.puntajeMinimoAprobacion.value;
  }

  /**
   * Indica si se deben aplicar cuotas en la generación del ordenamiento.
   */
  get aplicarCuotas(): boolean {
    return this.form.controls.aplicarCuotas.value;
  }

  /**
   * Indica si el ordenamiento a generar es definitivo.
   */
  get esDefinitivo(): boolean {
    return this.form.controls.esDefinitivo.value;
  }

  /**
   * Determina si la inscripción supera el umbral para ser considerada aprobada.
   */
  esAprobado(inscripcion: InscripcionParaEvaluar): boolean {
    return (inscripcion.puntajeTotal ?? 0) >= this.puntajeMinimoAprobacion;
  }
}
