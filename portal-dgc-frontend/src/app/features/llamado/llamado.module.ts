import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../../shared/shared.module';
import { DetalleLlamadoComponent } from './components/detalle-llamado/detalle-llamado.component';
import { ListaLlamadosComponent } from './components/lista-llamados/lista-llamados.component';
import { LlamadoRoutingModule } from './llamado-routing.module';

@NgModule({
  declarations: [ListaLlamadosComponent, DetalleLlamadoComponent],
  imports: [CommonModule, LlamadoRoutingModule, SharedModule]
})
export class LlamadoModule {}
