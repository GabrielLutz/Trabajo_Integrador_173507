import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DatosPersonalesComponent } from './components/datos-personales/datos-personales.component';
import { MisInscripcionesComponent } from './components/mis-inscripciones/mis-inscripciones.component';
import { PerfilComponent } from './components/perfil/perfil.component';

const routes: Routes = [
  {
    path: '',
    component: PerfilComponent
  },
  {
    path: 'mis-inscripciones',
    component: MisInscripcionesComponent
  },
  {
    path: 'editar',
    component: DatosPersonalesComponent
  },
  {
    path: 'datos-personales',
    redirectTo: 'editar'
  },
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PostulanteRoutingModule {}
