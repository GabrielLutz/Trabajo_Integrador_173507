import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InscripcionSimple } from '../../../../core/models/inscripcion.model';
import { InscripcionService } from '../../../../core/services/inscripcion.service';

@Component({
  selector: 'app-mis-inscripciones',
  standalone: false,
  templateUrl: './mis-inscripciones.component.html',
  styleUrls: ['./mis-inscripciones.component.scss']
})
export class MisInscripcionesComponent implements OnInit {
  inscripciones: InscripcionSimple[] = [];
  loading = false;
  error = '';
  private readonly postulanteId = 1;

  constructor(
    private readonly inscripcionService: InscripcionService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.cargarInscripciones();
  }

  cargarInscripciones(): void {
    this.loading = true;
    this.error = '';

    this.inscripcionService
      .obtenerInscripcionesPorPostulante(this.postulanteId)
      .subscribe({
        next: (response) => {
          if (response.success && response.data) {
            this.inscripciones = response.data;
          } else {
            this.inscripciones = [];
            this.error = response.message || 'No fue posible obtener tus inscripciones.';
          }
          this.loading = false;
        },
        error: (err) => {
          this.loading = false;
          this.error = err.error?.message || 'Ocurrió un error al cargar tus inscripciones.';
        }
      });
  }

  verDetalle(inscripcionId: number): void {
    this.router.navigate(['/inscripcion', inscripcionId]);
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

  trackByInscripcion(_: number, inscripcion: InscripcionSimple): number {
    return inscripcion.id;
  }

}
