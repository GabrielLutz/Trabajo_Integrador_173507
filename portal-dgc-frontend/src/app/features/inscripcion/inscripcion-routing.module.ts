import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormularioInscripcionComponent } from './components/formulario-inscripcion/formulario-inscripcion.component';
import { DetalleInscripcionComponent } from './components/detalle-inscripcion/detalle-inscripcion.component';

const routes: Routes = [
  {
    path: 'nuevo',
    component: FormularioInscripcionComponent
  },
  {
    path: ':id',
    component: DetalleInscripcionComponent
  },
  {
    path: '',
    redirectTo: 'nuevo',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InscripcionRoutingModule { }
