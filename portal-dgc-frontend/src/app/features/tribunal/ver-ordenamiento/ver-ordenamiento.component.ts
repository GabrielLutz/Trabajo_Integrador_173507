import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OrdenamientoDetalle, PosicionOrdenamiento } from '../../../core/models/tribunal.models';
import { TribunalService } from '../../../core/services/tribunal.service';

@Component({
  selector: 'app-ver-ordenamiento',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './ver-ordenamiento.component.html',
  styleUrls: ['./ver-ordenamiento.component.scss']
})
export class VerOrdenamientoComponent implements OnInit {
  ordenamiento: OrdenamientoDetalle | null = null;
  ordenamientoId!: number;
  loading = false;
  error: string | null = null;

  // Publicación
  publicando = false;
  mensajeExito: string | null = null;

  // Filtros
  filtroNombre = '';
  posicionesFiltradas: PosicionOrdenamiento[] = [];

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly tribunalService: TribunalService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.ordenamientoId = +params['ordenamientoId'];
      this.cargarOrdenamiento();
    });
  }

  cargarOrdenamiento(): void {
    this.loading = true;
    this.error = null;

    this.tribunalService.obtenerDetalleOrdenamiento(this.ordenamientoId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.ordenamiento = response.data;
          this.posicionesFiltradas = [...this.ordenamiento.posiciones];
        } else {
          this.error = response.message;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error al cargar ordenamiento';
        console.error(err);
        this.loading = false;
      }
    });
  }

  filtrarPosiciones(): void {
    if (!this.ordenamiento) return;

    if (!this.filtroNombre.trim()) {
      this.posicionesFiltradas = [...this.ordenamiento.posiciones];
      return;
    }

    const filtro = this.filtroNombre.toLowerCase();
    this.posicionesFiltradas = this.ordenamiento.posiciones.filter(
      (pos) =>
        pos.nombreCompleto.toLowerCase().includes(filtro) ||
        pos.cedulaIdentidad.includes(filtro)
    );
  }

  publicarOrdenamiento(): void {
    if (
      !confirm(
        '¿Está seguro que desea publicar este ordenamiento? Esta acción no se puede deshacer.'
      )
    ) {
      return;
    }

    this.publicando = true;
    this.mensajeExito = null;

    this.tribunalService.publicarOrdenamiento(this.ordenamientoId).subscribe({
      next: (response) => {
        if (response.success) {
          this.mensajeExito = '✅ Ordenamiento publicado correctamente';
          this.cargarOrdenamiento();

          setTimeout(() => (this.mensajeExito = null), 3000);
        }
        this.publicando = false;
      },
      error: (err) => {
        this.error = 'Error al publicar ordenamiento';
        console.error(err);
        this.publicando = false;
      }
    });
  }

  exportarPDF(): void {
    alert('Funcionalidad de exportar PDF en desarrollo');
  }

  exportarExcel(): void {
    alert('Funcionalidad de exportar Excel en desarrollo');
  }

  volver(): void {
    this.router.navigate(['/tribunal/dashboard']);
  }

  getTipoBadgeClass(tipo: string): string {
    switch (tipo.toLowerCase()) {
      case 'general':
        return 'tipo-general';
      case 'afrodescendiente':
        return 'tipo-afro';
      case 'trans':
        return 'tipo-trans';
      case 'discapacidad':
        return 'tipo-disc';
      default:
        return 'tipo-general';
    }
  }

  getEstadoBadgeClass(estado: string): string {
    switch (estado.toLowerCase()) {
      case 'preliminar':
        return 'estado-preliminar';
      case 'definitivo':
        return 'estado-definitivo';
      case 'publicado':
        return 'estado-publicado';
      default:
        return 'estado-preliminar';
    }
  }

  get puntajeMaximo(): number {
    if (!this.ordenamiento || this.ordenamiento.posiciones.length === 0) {
      return 0;
    }

    return this.ordenamiento.posiciones[0]?.puntajeTotal ?? 0;
  }

  get puntajePromedio(): number {
    if (!this.ordenamiento || this.ordenamiento.posiciones.length === 0) {
      return 0;
    }

    const total = this.ordenamiento.posiciones.reduce(
      (sum, p) => sum + (p.puntajeTotal ?? 0),
      0
    );

    return total / this.ordenamiento.posiciones.length;
  }

  get cantidadCuotasAplicadas(): number {
    if (!this.ordenamiento) {
      return 0;
    }

    return this.ordenamiento.posiciones.filter((p) => p.aplicaCuota).length;
  }
}
