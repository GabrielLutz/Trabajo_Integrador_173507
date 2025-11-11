import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Postulante } from '../../../../core/models/postulante.model';
import { PostulanteService } from '../../../../core/services/postulante.service';

@Component({
  selector: 'app-perfil',
  standalone: false,
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit, OnDestroy {
  postulante?: Postulante;
  loading = false;
  error = '';

  private readonly postulanteId = 1;
  private readonly destroy$ = new Subject<void>();

  constructor(
    private readonly postulanteService: PostulanteService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.cargarPerfil();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  cargarPerfil(): void {
    this.loading = true;
    this.error = '';

    this.postulanteService
      .obtenerPostulante(this.postulanteId)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          if (response.success && response.data) {
            this.postulante = response.data;
          } else {
            this.error = 'No se encontraron datos del postulante.';
          }
          this.loading = false;
        },
        error: () => {
          this.error = 'Error al cargar los datos del perfil.';
          this.loading = false;
        }
      });
  }

  editarDatos(): void {
    this.router.navigate(['/perfil', 'editar']);
  }

  get iniciales(): string {
    if (!this.postulante) {
      return '';
    }
    const nombres = `${this.postulante.nombre} ${this.postulante.apellido}`.trim().split(' ');
    const letras = nombres
      .filter((parte) => parte.length > 0)
      .map((parte) => parte.charAt(0).toUpperCase());
    return letras.slice(0, 2).join('');
  }

  get nombreCompleto(): string {
    if (!this.postulante) {
      return '';
    }
    return `${this.postulante.nombre} ${this.postulante.apellido}`.trim();
  }

  get generoDescripcion(): string {
    if (!this.postulante) {
      return '';
    }
    if (this.postulante.genero === 'Otro' && this.postulante.generoOtro) {
      return this.postulante.generoOtro;
    }
    return this.postulante.genero;
  }

  formatearFecha(fecha: Date | string): string {
    const date = fecha instanceof Date ? fecha : new Date(fecha);
    if (Number.isNaN(date.getTime())) {
      return '-';
    }
    return new Intl.DateTimeFormat('es-UY', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric'
    }).format(date);
  }

  mostrarValor(valor?: string | null): string {
    return valor && valor.trim().length ? valor : '-';
  }

}
