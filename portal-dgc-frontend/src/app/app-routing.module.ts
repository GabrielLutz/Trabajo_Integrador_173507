import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  // Public auth entry points
  {
    path: 'login',
    loadChildren: () => import('./features/auth/auth.module').then((m) => m.AuthModule)
  },
  // Default to login so user must authenticate first
  {
    path: '',
    redirectTo: '/login',
    pathMatch: 'full'
  },
  // Protected lazy modules
  {
    path: 'llamados',
    loadChildren: () => import('./features/llamado/llamado.module').then((m) => m.LlamadoModule),
    // protect lazy loaded module
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'inscripcion',
    loadChildren: () => import('./features/inscripcion/inscripcion.module').then((m) => m.InscripcionModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'perfil',
    loadChildren: () => import('./features/postulante/postulante.module').then((m) => m.PostulanteModule),
    canLoad: [AuthGuard],
    canActivate: [AuthGuard]
  },
  {
    path: 'tribunal',
    loadChildren: () => import('./features/tribunal/tribunal.module').then((m) => m.TribunalModule)
  },
  {
    path: '**',
    redirectTo: '/login'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
