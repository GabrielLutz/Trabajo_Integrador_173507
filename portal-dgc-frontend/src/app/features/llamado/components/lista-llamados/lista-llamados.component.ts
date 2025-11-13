import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { ApiResponse } from '../../../../core/models/api-response.model';
import { Llamado } from '../../../../core/models/llamado.model';
import { LlamadoService } from '../../../../core/services/llamado.service';

@Component({
  selector: 'app-lista-llamados',
  standalone: false,
  templateUrl: './lista-llamados.component.html',
  styleUrls: ['./lista-llamados.component.scss']
})
export class ListaLlamadosComponent implements OnInit {
  llamadosActivos: Llamado[] = [];
  llamadosInactivos: Llamado[] = [];
  loading = false;
  error = '';
  estadoSeleccionado: 'activos' | 'inactivos' = 'activos';

  constructor(
    private readonly llamadoService: LlamadoService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.cargarLlamados();
  }

  cargarLlamados(): void {
    this.loading = true;
    forkJoin({
      activos: this.llamadoService.obtenerLlamadosActivos(),
      inactivos: this.llamadoService.obtenerLlamadosInactivos()
    }).subscribe({
      next: ({ activos, inactivos }) => {
        this.llamadosActivos = activos.success && activos.data ? activos.data : [];
        this.llamadosInactivos = inactivos.success && inactivos.data ? inactivos.data : [];

        if (!activos.success && !inactivos.success) {
          this.error = 'No pudimos obtener los llamados en este momento.';
        }

        this.loading = false;
      },
      error: () => {
        this.error = 'Error al cargar llamados';
        this.loading = false;
      }
    });
  }

  verDetalle(llamadoId: number): void {
    this.router.navigate(['/llamados', llamadoId]);
  }

  inscribirse(llamadoId: number): void {
    this.router.navigate(['/inscripcion', 'nuevo'], {
      queryParams: { llamadoId }
    });
  }

  getDiasRestantes(fechaCierre: Date | string): number {
    const hoy = new Date();
    const cierre = new Date(fechaCierre);
    const diffTime = cierre.getTime() - hoy.getTime();
    return Math.ceil(diffTime / (1000 * 60 * 60 * 24));
  }

  seleccionarEstado(estado: 'activos' | 'inactivos'): void {
    this.estadoSeleccionado = estado;
  }

  get llamadosVisibles(): Llamado[] {
    return this.estadoSeleccionado === 'activos'
      ? this.llamadosActivos
      : this.llamadosInactivos;
  }
}
