import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiResponse } from '../../../../core/models/api-response.model';
import { LlamadoDetalle } from '../../../../core/models/llamado.model';
import { LlamadoService } from '../../../../core/services/llamado.service';

@Component({
  selector: 'app-detalle-llamado',
  standalone: false,
  templateUrl: './detalle-llamado.component.html',
  styleUrls: ['./detalle-llamado.component.scss']
})
export class DetalleLlamadoComponent implements OnInit {
  llamado: LlamadoDetalle | null = null;
  loading = false;
  error = '';
  tabActiva: 'informacion' | 'requisitos' | 'puntuables' | 'apoyos' = 'informacion';

  // Para depuración: fecha/hora actual
  now: Date = new Date();

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly llamadoService: LlamadoService
  ) {}

  /**
   * Obtiene el identificador de ruta y carga el detalle del llamado (RF-04).
   */
  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.cargarLlamado(id);
    }
  }

  /**
   * Pide al backend la información completa del llamado.
   */
  cargarLlamado(id: number): void {
    this.loading = true;
    this.llamadoService.obtenerLlamadoDetalle(id).subscribe({
      next: (response: ApiResponse<LlamadoDetalle>) => {
        if (response.success && response.data) {
          this.llamado = response.data;
        }
        this.loading = false;
      },
      error: () => {
        this.error = 'Error al cargar el llamado';
        this.loading = false;
      }
    });
  }

  /**
   * Cambia la pestaña visible en el detalle del llamado.
   */
  cambiarTab(tab: 'informacion' | 'requisitos' | 'puntuables' | 'apoyos'): void {
    this.tabActiva = tab;
  }

  /**
   * Inicia el proceso de inscripción cuando el llamado está habilitado (RF-05).
   */
  inscribirse(): void {
    if (this.llamado && this.puedeInscribirse) {
      this.router.navigate(['/inscripcion', 'nuevo'], {
        queryParams: { llamadoId: this.llamado.id }
      });
    }
  }

  /**
   * Regresa al listado general de llamados.
   */
  volver(): void {
    this.router.navigate(['/llamados']);
  }

  /**
   * Valida si el postulante puede acceder al flujo de inscripción.
   */
  get puedeInscribirse(): boolean {
    if (!this.llamado) {
      return false;
    }
    // Habilitado si el estado es 'activo' o 'abierto' y la fecha de cierre es futura
    const estado = (this.llamado.estado || '').toLowerCase();
    const esHabilitado = estado === 'activo' || estado === 'abierto';
    const cierre = new Date(this.llamado.fechaCierre);
    const ahora = new Date();
    return esHabilitado && cierre.getTime() > ahora.getTime();
  }

  /**
   * Determina la clase visual del badge según el estado del llamado.
   */
  getEstadoBadgeClass(): string {
    const estado = this.llamado?.estado?.toLowerCase() ?? '';

    if (estado === 'activo' || estado === 'abierto') {
      return 'badge-success';
    }

    if (['cerrado', 'inactivo', 'finalizado'].includes(estado)) {
      return 'badge-danger';
    }

    if (['pendiente', 'proximamente'].includes(estado)) {
      return 'badge-warning';
    }

    return 'badge-secondary';
  }
}
