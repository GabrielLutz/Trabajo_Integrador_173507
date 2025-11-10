import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SharedModule } from '../../shared/shared.module';
import { DatosPersonalesComponent } from './components/datos-personales/datos-personales.component';
import { MisInscripcionesComponent } from './components/mis-inscripciones/mis-inscripciones.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { PostulanteRoutingModule } from './postulante-routing.module';

@NgModule({
  declarations: [DatosPersonalesComponent, MisInscripcionesComponent, PerfilComponent],
  imports: [CommonModule, ReactiveFormsModule, FormsModule, PostulanteRoutingModule, SharedModule]
})
export class PostulanteModule {}
