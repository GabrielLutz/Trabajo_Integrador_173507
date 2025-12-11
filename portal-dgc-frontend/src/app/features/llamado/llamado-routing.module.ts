import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetalleLlamadoComponent } from './components/detalle-llamado/detalle-llamado.component';
import { ListaLlamadosComponent } from './components/lista-llamados/lista-llamados.component';

const routes: Routes = [
  {
    path: '',
    component: ListaLlamadosComponent
  },
  {
    path: ':id',
    component: DetalleLlamadoComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LlamadoRoutingModule { }
