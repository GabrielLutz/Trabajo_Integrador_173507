import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorMessageComponent } from '../../../../shared/components/error-message/error-message.component';
import { LoadingComponent } from '../../../../shared/components/loading/loading.component';
import { ApiResponse } from '../../../../core/models/api-response.model';
import { LlamadoDetalle } from '../../../../core/models/llamado.model';
import { LlamadoService } from '../../../../core/services/llamado.service';

@Component({
  selector: 'app-detalle-llamado',
  standalone: true,
  imports: [CommonModule, LoadingComponent, ErrorMessageComponent],
  templateUrl: './detalle-llamado.component.html',
  styleUrls: ['./detalle-llamado.component.scss']
})
export class DetalleLlamadoComponent implements OnInit {
  llamado: LlamadoDetalle | null = null;
  loading = false;
  error = '';
  tabActiva: 'informacion' | 'requisitos' | 'puntuables' | 'apoyos' = 'informacion';

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly llamadoService: LlamadoService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.cargarLlamado(id);
    }
  }

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

  cambiarTab(tab: 'informacion' | 'requisitos' | 'puntuables' | 'apoyos'): void {
    this.tabActiva = tab;
  }

  inscribirse(): void {
    if (this.llamado) {
      this.router.navigate(['/inscripcion', 'nuevo'], {
        queryParams: { llamadoId: this.llamado.id }
      });
    }
  }

  volver(): void {
    this.router.navigate(['/llamados']);
  }
}
