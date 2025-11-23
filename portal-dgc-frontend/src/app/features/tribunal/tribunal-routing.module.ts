import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardTribunalComponent } from './dashboard-tribunal/dashboard-tribunal.component';
import { ListaInscripcionesComponent } from './lista-inscripciones/lista-inscripciones.component';
import { DetalleEvaluacionComponent } from './detalle-evaluacion/detalle-evaluacion.component';
import { GenerarOrdenamientoComponent } from './generar-ordenamiento/generar-ordenamiento.component';
import { VerOrdenamientoComponent } from './ver-ordenamiento/ver-ordenamiento.component';

import { ListaOrdenamientosComponent } from './lista-ordenamientos/lista-ordenamientos.component';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    component: DashboardTribunalComponent
  },
  {
    path: 'llamado/:llamadoId/inscripciones',
    component: ListaInscripcionesComponent
  },
  {
    path: 'inscripcion/:inscripcionId/evaluar',
    component: DetalleEvaluacionComponent
  },
  {
    path: 'llamado/:llamadoId/generar-ordenamiento',
    component: GenerarOrdenamientoComponent
  },
  {
    path: 'llamado/:llamadoId/ordenamientos',
    component: ListaOrdenamientosComponent
  },
  {
    path: 'ordenamiento/:ordenamientoId',
    component: VerOrdenamientoComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TribunalRoutingModule {}
