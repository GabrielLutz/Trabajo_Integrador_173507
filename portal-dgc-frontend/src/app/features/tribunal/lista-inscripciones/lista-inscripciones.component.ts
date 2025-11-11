import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TribunalService } from '../../../core/services/tribunal.service';
import { InscripcionParaEvaluar } from '../../../core/models/tribunal.models';

@Component({
  selector: 'app-lista-inscripciones',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './lista-inscripciones.component.html',
  styleUrls: ['./lista-inscripciones.component.scss']
})
export class ListaInscripcionesComponent implements OnInit {
  inscripciones: InscripcionParaEvaluar[] = [];
  inscripcionesFiltradas: InscripcionParaEvaluar[] = [];
  loading = false;
  error: string | null = null;
  llamadoId!: number;

  filtroNombre = '';
  filtroDepartamento = '';
  filtroEstado = '';
  filtroUniverso = '';

  departamentos: string[] = [];
  estados: string[] = [];

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly tribunalService: TribunalService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.llamadoId = +params['llamadoId'];
      this.cargarInscripciones();
    });
  }

  cargarInscripciones(): void {
    this.loading = true;
    this.error = null;

    this.tribunalService.obtenerInscripcionesParaEvaluar(this.llamadoId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.inscripciones = response.data;
          this.inscripcionesFiltradas = [...this.inscripciones];
          this.extraerFiltros();
        } else {
          this.error = response.message;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error al cargar inscripciones';
        console.error(err);
        this.loading = false;
      }
    });
  }

  extraerFiltros(): void {
    this.departamentos = [...new Set(this.inscripciones.map((i) => i.departamento))];
    this.estados = [...new Set(this.inscripciones.map((i) => i.estadoInscripcion))];
  }

  aplicarFiltros(): void {
    this.inscripcionesFiltradas = this.inscripciones.filter((inscripcion) => {
      const cumpleNombre =
        !this.filtroNombre ||
        inscripcion.nombreCompleto.toLowerCase().includes(this.filtroNombre.toLowerCase()) ||
        inscripcion.cedulaIdentidad.includes(this.filtroNombre);

      const cumpleDepartamento = !this.filtroDepartamento || inscripcion.departamento === this.filtroDepartamento;

      const cumpleEstado = !this.filtroEstado || this.getEstadoEvaluacion(inscripcion) === this.filtroEstado;

      const cumpleUniverso =
        !this.filtroUniverso ||
        (this.filtroUniverso === 'afro' && inscripcion.esAfrodescendiente) ||
        (this.filtroUniverso === 'trans' && inscripcion.esTrans) ||
        (this.filtroUniverso === 'discapacidad' && inscripcion.tieneDiscapacidad);

      return cumpleNombre && cumpleDepartamento && cumpleEstado && cumpleUniverso;
    });
  }

  limpiarFiltros(): void {
    this.filtroNombre = '';
    this.filtroDepartamento = '';
    this.filtroEstado = '';
    this.filtroUniverso = '';
    this.inscripcionesFiltradas = [...this.inscripciones];
  }

  getEstadoEvaluacion(inscripcion: InscripcionParaEvaluar): string {
    if (inscripcion.pruebasEvaluadas === 0 && inscripcion.meritosEvaluados === 0) {
      return 'Pendiente';
    }
    if (
      inscripcion.pruebasEvaluadas < inscripcion.pruebasTotales ||
      inscripcion.meritosEvaluados < inscripcion.meritosTotales
    ) {
      return 'En Proceso';
    }
    return 'Completa';
  }

  getEstadoBadgeClass(estado: string): string {
    switch (estado) {
      case 'Pendiente':
        return 'badge-warning';
      case 'En Proceso':
        return 'badge-info';
      case 'Completa':
        return 'badge-success';
      default:
        return 'badge-secondary';
    }
  }

  getProgresoEvaluacion(inscripcion: InscripcionParaEvaluar): number {
    const totalItems = inscripcion.pruebasTotales + inscripcion.meritosTotales;
    const evaluados = inscripcion.pruebasEvaluadas + inscripcion.meritosEvaluados;
    return totalItems > 0 ? Math.round((evaluados / totalItems) * 100) : 0;
  }

  verDetalle(inscripcionId: number): void {
    this.router.navigate(['/tribunal/inscripcion', inscripcionId, 'evaluar']);
  }

  volver(): void {
    this.router.navigate(['/tribunal/dashboard']);
  }
}
