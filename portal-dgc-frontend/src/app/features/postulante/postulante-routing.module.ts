import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DatosPersonalesComponent } from './components/datos-personales/datos-personales.component';
import { MisInscripcionesComponent } from './components/mis-inscripciones/mis-inscripciones.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'mis-inscripciones',
    pathMatch: 'full'
  },
  {
    path: 'mis-inscripciones',
    component: MisInscripcionesComponent
  },
  {
    path: 'datos-personales',
    component: DatosPersonalesComponent
  },
  {
    path: '**',
    redirectTo: 'mis-inscripciones'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostulanteRoutingModule {}
