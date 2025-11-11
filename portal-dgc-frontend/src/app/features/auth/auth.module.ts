import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegistroComponent } from './registro/registro.component';

@NgModule({
  declarations: [LoginComponent, RegistroComponent],
  imports: [CommonModule, ReactiveFormsModule, RouterModule.forChild([
    { path: '', component: LoginComponent },
    { path: 'registro', component: RegistroComponent }
  ])]
})
export class AuthModule {}
