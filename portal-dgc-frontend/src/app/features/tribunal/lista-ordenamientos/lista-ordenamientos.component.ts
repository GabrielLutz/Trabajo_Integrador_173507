import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TribunalService } from '../../../core/services/tribunal.service';
import { Ordenamiento } from '../../../core/models/tribunal.models';

import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-lista-ordenamientos',
  templateUrl: './lista-ordenamientos.component.html',
  styleUrls: ['./lista-ordenamientos.component.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class ListaOrdenamientosComponent implements OnInit {
  ordenamientos: Ordenamiento[] = [];
  llamadoId!: number;
  error: string | null = null;

  constructor(
    private tribunalService: TribunalService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.llamadoId = +params['llamadoId'];
      this.cargarOrdenamientos();
    });
  }

  cargarOrdenamientos(): void {
    this.tribunalService.obtenerOrdenamientos(this.llamadoId).subscribe({
      next: (response) => {
        this.ordenamientos = response.data || [];
      },
      error: () => {
        this.error = 'Error al cargar los ordenamientos';
      }
    });
  }

  verDetalle(ordenamientoId: number): void {
    this.router.navigate(['/tribunal/ordenamiento', ordenamientoId]);
  }

  volverAlDashboard(): void {
    this.router.navigate(['/tribunal/dashboard']);
  }
}
