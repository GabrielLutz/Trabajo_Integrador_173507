import { Routes } from '@angular/router';

export const routes: Routes = [
	{
		path: '',
		pathMatch: 'full',
		redirectTo: 'llamados'
	},
	{
		path: 'llamados',
		loadChildren: () => import('./features/llamado/llamado.module').then((m) => m.LlamadoModule)
	},
	{
		path: 'postulante',
		loadChildren: () => import('./features/postulante/postulante.module').then((m) => m.PostulanteModule)
	},
	{
		path: 'inscripcion',
		loadChildren: () => import('./features/inscripcion/inscripcion.module').then((m) => m.InscripcionModule)
	},
	{
		path: '**',
		redirectTo: 'llamados'
	}
];
