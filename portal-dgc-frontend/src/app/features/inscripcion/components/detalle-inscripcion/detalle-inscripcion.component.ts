import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EMPTY, Subject } from 'rxjs';
import { switchMap, takeUntil } from 'rxjs/operators';
import { InscripcionResponse } from '../../../../core/models/inscripcion.model';
import { InscripcionService } from '../../../../core/services/inscripcion.service';

@Component({
  selector: 'app-detalle-inscripcion',
  standalone: false,
  templateUrl: './detalle-inscripcion.component.html',
  styleUrls: ['./detalle-inscripcion.component.scss']
})
export class DetalleInscripcionComponent implements OnInit, OnDestroy {
  inscripcion: InscripcionResponse | null = null;
  loading = false;
  error = '';

  private readonly destroy$ = new Subject<void>();

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly inscripcionService: InscripcionService
  ) {}

  ngOnInit(): void {
    this.route.paramMap
      .pipe(
        takeUntil(this.destroy$),
        switchMap((params) => {
          const idParam = params.get('id');
          const inscripcionId = idParam ? Number(idParam) : NaN;

          if (!inscripcionId || Number.isNaN(inscripcionId)) {
            this.router.navigate(['/perfil', 'mis-inscripciones']);
            return EMPTY;
          }

          this.loading = true;
          this.error = '';

          return this.inscripcionService.obtenerInscripcion(inscripcionId);
        })
      )
      .subscribe({
        next: (response) => {
          this.loading = false;

          if (response.success && response.data) {
            this.inscripcion = response.data;
          } else {
            this.inscripcion = null;
            this.error = response.message || 'No se encontró la inscripción solicitada.';
          }
        },
        error: (err) => {
          this.loading = false;
          this.inscripcion = null;
          this.error = err.error?.message || 'Ocurrió un error al cargar la inscripción.';
        }
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  irAListado(): void {
    this.router.navigate(['/perfil', 'mis-inscripciones']);
  }

  getEstadoBadgeClass(estado: string): string {
    const normalized = estado?.toLowerCase() ?? '';

    if (['aprobada', 'aceptada', 'confirmada'].includes(normalized)) {
      return 'badge-success';
    }

    if (['pendiente', 'en evaluación', 'en progreso'].includes(normalized)) {
      return 'badge-info';
    }

    if (['observada', 'requiere acción'].includes(normalized)) {
      return 'badge-warning';
    }

    if (normalized === 'rechazada') {
      return 'badge-danger';
    }

    return 'badge-secondary';
  }

  tieneAutodefinicion(): boolean {
    const autodefinicion = this.inscripcion?.autodefinicion;
    return !!autodefinicion && (
      autodefinicion.esAfrodescendiente || autodefinicion.esTrans || autodefinicion.tieneDiscapacidad
    );
  }

}
