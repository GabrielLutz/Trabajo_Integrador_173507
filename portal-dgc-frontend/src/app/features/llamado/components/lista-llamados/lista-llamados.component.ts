import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ErrorMessageComponent } from '../../../../shared/components/error-message/error-message.component';
import { LoadingComponent } from '../../../../shared/components/loading/loading.component';
import { ApiResponse } from '../../../../core/models/api-response.model';
import { Llamado } from '../../../../core/models/llamado.model';
import { LlamadoService } from '../../../../core/services/llamado.service';

@Component({
  selector: 'app-lista-llamados',
  standalone: true,
  imports: [CommonModule, LoadingComponent, ErrorMessageComponent],
  templateUrl: './lista-llamados.component.html',
  styleUrls: ['./lista-llamados.component.scss']
})
export class ListaLlamadosComponent implements OnInit {
  llamados: Llamado[] = [];
  loading = false;
  error = '';

  constructor(
    private readonly llamadoService: LlamadoService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.cargarLlamados();
  }

  cargarLlamados(): void {
    this.loading = true;
    this.llamadoService.obtenerLlamadosActivos().subscribe({
      next: (response: ApiResponse<Llamado[]>) => {
        if (response.success && response.data) {
          this.llamados = response.data;
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
}
