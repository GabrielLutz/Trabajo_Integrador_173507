import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DetalleEvaluacion, CalificarPruebaDto, ValorarMeritoDto, PruebaDto } from '../../../core/models/tribunal.models';
import { TribunalService } from '../../../core/services/tribunal.service';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-detalle-evaluacion',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './detalle-evaluacion.component.html',
  styleUrls: ['./detalle-evaluacion.component.scss']
})
export class DetalleEvaluacionComponent implements OnInit {
  detalle: DetalleEvaluacion | null = null;
  pruebas: PruebaDto[] = [];
  loading = false;
  error: string | null = null;
  mensajeExito: string | null = null;
  guardandoPrueba = false;
  guardandoMeritos = false;
  inscripcionId!: number;
  seccionActiva: 'requisitos' | 'pruebas' | 'meritos' = 'pruebas';
  formCalificarPrueba: FormGroup;
  formValorarMeritos: FormGroup;
  pruebaSeleccionada: PruebaDto | null = null;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly fb: FormBuilder,
    private readonly tribunalService: TribunalService
  ) {
    this.formCalificarPrueba = this.fb.group({
      puntajeObtenido: ['', [Validators.required, Validators.min(0)]],
      observaciones: ['']
    });

    this.formValorarMeritos = this.fb.group({});
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.inscripcionId = +params['inscripcionId'];
      this.cargarDetalle();
    });
  }

  cargarDetalle(): void {
    this.loading = true;
    this.error = null;

    this.tribunalService.obtenerDetalleEvaluacion(this.inscripcionId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.detalle = response.data;
          this.cargarPruebasDisponibles();
          this.inicializarFormularioMeritos();
        } else {
          this.error = response.message;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error al cargar detalle de evaluación';
        console.error(err);
        this.loading = false;
      }
    });
  }

  cargarPruebasDisponibles(): void {
    if (!this.detalle) {
      return;
    }

    const llamadoId = 1; // MVP: valor fijo hasta conectar con datos reales

    this.tribunalService.obtenerPruebasDelLlamado(llamadoId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.pruebas = response.data;
        }
      },
      error: (err) => console.error('Error al cargar pruebas del llamado', err)
    });
  }

  inicializarFormularioMeritos(): void {
    if (!this.detalle) {
      return;
    }

    const controls: Record<string, unknown> = {};

    this.detalle.meritos.forEach((merito) => {
      controls[`puntaje_${merito.meritoPostulanteId}`] = [
        merito.puntajeAsignado ?? 0,
        [Validators.required, Validators.min(0), Validators.max(merito.puntajeMaximo)]
      ];
      controls[`verificado_${merito.meritoPostulanteId}`] = [merito.documentacionVerificada ?? false];
      controls[`observaciones_${merito.meritoPostulanteId}`] = [merito.observaciones ?? ''];
    });

    this.formValorarMeritos = this.fb.group(controls);
  }

  seleccionarPrueba(prueba: PruebaDto): void {
    this.pruebaSeleccionada = prueba;

    const evaluacionExistente = this.detalle?.pruebas.find((p) => p.pruebaId === prueba.id);

    if (evaluacionExistente) {
      this.formCalificarPrueba.patchValue({
        puntajeObtenido: evaluacionExistente.puntajeObtenido,
        observaciones: evaluacionExistente.observaciones ?? ''
      });
    } else {
      this.formCalificarPrueba.reset({
        puntajeObtenido: '',
        observaciones: ''
      });
    }

    const ctrl = this.formCalificarPrueba.get('puntajeObtenido');
    if (ctrl) {
      ctrl.setValidators([Validators.required, Validators.min(0), Validators.max(prueba.puntajeMaximo)]);
      ctrl.updateValueAndValidity();
    }
  }

  calificarPrueba(): void {
    if (this.formCalificarPrueba.invalid || !this.pruebaSeleccionada) {
      this.formCalificarPrueba.markAllAsTouched();
      return;
    }

    this.guardandoPrueba = true;
    this.mensajeExito = null;

    const dto: CalificarPruebaDto = {
      inscripcionId: this.inscripcionId,
      pruebaId: this.pruebaSeleccionada.id,
      puntajeObtenido: this.formCalificarPrueba.value.puntajeObtenido,
      observaciones: this.formCalificarPrueba.value.observaciones
    };

    this.tribunalService.calificarPrueba(dto).subscribe({
      next: (response) => {
        if (response.success) {
          this.mensajeExito = 'Prueba calificada correctamente.';
          this.cargarDetalle();
          this.pruebaSeleccionada = null;
          this.formCalificarPrueba.reset({ puntajeObtenido: '', observaciones: '' });
          setTimeout(() => (this.mensajeExito = null), 3000);
        }
        this.guardandoPrueba = false;
      },
      error: (err) => {
        this.error = 'Error al calificar prueba';
        console.error(err);
        this.guardandoPrueba = false;
      }
    });
  }

  valorarTodosMeritos(): void {
    if (this.formValorarMeritos.invalid || !this.detalle) {
      this.formValorarMeritos.markAllAsTouched();
      return;
    }

    this.guardandoMeritos = true;
    this.mensajeExito = null;

    const meritosDto: ValorarMeritoDto[] = this.detalle.meritos.map((merito) => ({
      meritoPostulanteId: merito.meritoPostulanteId,
      puntajeAsignado: this.formValorarMeritos.value[`puntaje_${merito.meritoPostulanteId}`],
      documentacionVerificada: this.formValorarMeritos.value[`verificado_${merito.meritoPostulanteId}`],
      observaciones: this.formValorarMeritos.value[`observaciones_${merito.meritoPostulanteId}`]
    }));

    this.tribunalService.valorarMeritos(this.inscripcionId, meritosDto).subscribe({
      next: (response) => {
        if (response.success) {
          this.mensajeExito = 'Méritos valorados correctamente.';
          this.cargarDetalle();
          setTimeout(() => (this.mensajeExito = null), 3000);
        }
        this.guardandoMeritos = false;
      },
      error: (err) => {
        this.error = 'Error al valorar méritos';
        console.error(err);
        this.guardandoMeritos = false;
      }
    });
  }

  cambiarSeccion(seccion: 'requisitos' | 'pruebas' | 'meritos'): void {
    this.seccionActiva = seccion;
  }

  getPuntajeMaximoMerito(meritoId: number): number {
    const merito = this.detalle?.meritos.find((m) => m.meritoPostulanteId === meritoId);
    return merito?.puntajeMaximo ?? 0;
  }

  estaPruebaEvaluada(pruebaId: number): boolean {
    return this.detalle?.pruebas?.some((p) => p.pruebaId === pruebaId) ?? false;
  }

  volver(): void {
    this.router.navigate(['/tribunal/llamado', 1, 'inscripciones']); // MVP: llamadoId fijo hasta vincular datos
  }
}
