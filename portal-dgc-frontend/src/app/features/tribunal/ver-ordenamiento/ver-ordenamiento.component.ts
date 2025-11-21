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

  /**
   * Recupera el identificador desde la ruta y carga el ordenamiento correspondiente (RF-15).
   */
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.ordenamientoId = +params['ordenamientoId'];
      this.cargarOrdenamiento();
    });
  }

  /**
   * Obtiene del backend el detalle completo del ordenamiento y sus posiciones.
   */
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

  /**
   * Filtra las posiciones por nombre o cédula para facilitar la búsqueda.
   */
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

  /**
   * Solicita la publicación definitiva del ordenamiento (RF-15).
   */
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

  /**
   * Marcador temporal para exportar el ordenamiento a PDF.
   */
  exportarPDF(): void {
    alert('Funcionalidad de exportar PDF en desarrollo');
  }

  /**
   * Marcador temporal para exportar el ordenamiento a Excel.
   */
  exportarExcel(): void {
    alert('Funcionalidad de exportar Excel en desarrollo');
  }

  /**
   * Regresa al panel principal del tribunal.
   */
  volver(): void {
    this.router.navigate(['/tribunal/dashboard']);
  }

  /**
   * Clase CSS a aplicar según el tipo de ordenamiento.
   */
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

  /**
   * Clase CSS según el estado de publicación del ordenamiento.
   */
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

  /**
   * Puntaje más alto encontrado en el ordenamiento.
   */
  get puntajeMaximo(): number {
    if (!this.ordenamiento || this.ordenamiento.posiciones.length === 0) {
      return 0;
    }

    return this.ordenamiento.posiciones[0]?.puntajeTotal ?? 0;
  }

  /**
   * Puntaje promedio de todas las posiciones del ordenamiento.
   */
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

  /**
   * Total de posiciones que aplican a algún cupo o cuota especial.
   */
  get cantidadCuotasAplicadas(): number {
    if (!this.ordenamiento) {
      return 0;
    }

    return this.ordenamiento.posiciones.filter((p) => p.aplicaCuota).length;
  }
}
