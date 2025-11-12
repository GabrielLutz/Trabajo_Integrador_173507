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

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.llamadoId = +params['llamadoId'];
      this.cargarPreview();
    });
  }

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

  siguientePaso(): void {
    if (this.pasoActual < this.totalPasos) {
      this.pasoActual++;
    }
  }

  pasoAnterior(): void {
    if (this.pasoActual > 1) {
      this.pasoActual--;
    }
  }

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

  verOrdenamiento(ordenamientoId: number): void {
    this.router.navigate(['/tribunal/ordenamiento', ordenamientoId]);
  }

  volver(): void {
    this.router.navigate(['/tribunal/dashboard']);
  }

  getCantidadAprobados(): number {
    const minimo = this.puntajeMinimoAprobacion;
    return this.previewInscripciones.filter(i => (i.puntajeTotal || 0) >= minimo).length;
  }

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

  get puntajeMinimoAprobacion(): number {
    return this.form.controls.puntajeMinimoAprobacion.value;
  }

  get aplicarCuotas(): boolean {
    return this.form.controls.aplicarCuotas.value;
  }

  get esDefinitivo(): boolean {
    return this.form.controls.esDefinitivo.value;
  }

  esAprobado(inscripcion: InscripcionParaEvaluar): boolean {
    return (inscripcion.puntajeTotal ?? 0) >= this.puntajeMinimoAprobacion;
  }
}
