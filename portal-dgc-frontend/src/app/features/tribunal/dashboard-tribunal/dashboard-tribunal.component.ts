import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TribunalService } from '../../../core/services/tribunal.service';
import { EstadisticasTribunal } from '../../../core/models/tribunal.models';

@Component({
  selector: 'app-dashboard-tribunal',
  standalone: false,
  templateUrl: './dashboard-tribunal.component.html',
  styleUrls: ['./dashboard-tribunal.component.scss']
})
export class DashboardTribunalComponent implements OnInit {
  estadisticas: EstadisticasTribunal | null = null;
  loading = false;
  error: string | null = null;

  // MVP: se toma un llamado fijo; luego se obtendra del usuario autenticado
  llamadoId = 1;

  constructor(
    private readonly tribunalService: TribunalService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.cargarEstadisticas();
  }

  cargarEstadisticas(): void {
    this.loading = true;
    this.error = null;

    this.tribunalService.obtenerEstadisticas(this.llamadoId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.estadisticas = response.data;
        } else {
          this.error = response.message || 'No se pudieron obtener las estadisticas.';
        }
        this.loading = false;
      },
      error: (err) => {
        console.error('Error al cargar estadisticas del tribunal', err);
        this.error = 'Error al cargar estadisticas';
        this.loading = false;
      }
    });
  }

  irAInscripciones(): void {
    this.router.navigate(['/tribunal/llamado', this.llamadoId, 'inscripciones']);
  }

  irAGenerarOrdenamiento(): void {
    this.router.navigate(['/tribunal/llamado', this.llamadoId, 'generar-ordenamiento']);
  }

  calcularPorcentaje(valor: number, total: number): number {
    return total > 0 ? Math.round((valor / total) * 100) : 0;
  }

  obtenerTotalGeneral(): number {
    if (!this.estadisticas) {
      return 0;
    }

    const { totalInscripciones, totalAfrodescendientes, totalTrans, totalDiscapacidad } = this.estadisticas;
    return Math.max(
      0,
      totalInscripciones - totalAfrodescendientes - totalTrans - totalDiscapacidad
    );
  }
}
