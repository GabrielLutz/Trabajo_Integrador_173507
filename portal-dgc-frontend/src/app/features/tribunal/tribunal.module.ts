import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { TribunalRoutingModule } from './tribunal-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { DashboardTribunalComponent } from './dashboard-tribunal/dashboard-tribunal.component';
import { ListaInscripcionesComponent } from './lista-inscripciones/lista-inscripciones.component';
import { DetalleEvaluacionComponent } from './detalle-evaluacion/detalle-evaluacion.component';
import { GenerarOrdenamientoComponent } from './generar-ordenamiento/generar-ordenamiento.component';
import { VerOrdenamientoComponent } from './ver-ordenamiento/ver-ordenamiento.component';

@NgModule({
  declarations: [
    DashboardTribunalComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    ListaInscripcionesComponent,
    DetalleEvaluacionComponent,
    GenerarOrdenamientoComponent,
    VerOrdenamientoComponent,
    TribunalRoutingModule,
    SharedModule
  ]
})
export class TribunalModule {}
