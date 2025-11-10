import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/llamados',
    pathMatch: 'full'
  },
  {
    path: 'llamados',
    loadChildren: () => import('./features/llamado/llamado.module').then((m) => m.LlamadoModule)
  },
  {
    path: 'inscripcion',
    loadChildren: () => import('./features/inscripcion/inscripcion.module').then((m) => m.InscripcionModule)
  },
  {
    path: 'perfil',
    loadChildren: () => import('./features/postulante/postulante.module').then((m) => m.PostulanteModule)
  },
  {
    path: '**',
    redirectTo: '/llamados'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
