import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SharedModule } from '../../shared/shared.module';
import { DetalleInscripcionComponent } from './components/detalle-inscripcion/detalle-inscripcion.component';
import { FormularioInscripcionComponent } from './components/formulario-inscripcion/formulario-inscripcion.component';
import { InscripcionRoutingModule } from './inscripcion-routing.module';

@NgModule({
  declarations: [FormularioInscripcionComponent, DetalleInscripcionComponent],
  imports: [CommonModule, ReactiveFormsModule, FormsModule, InscripcionRoutingModule, SharedModule]
})
export class InscripcionModule {}
