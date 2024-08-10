import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/home/home.component').then((m) => m.HomeComponent),
  },
  {
    path: 'simulation',
    loadComponent: () => import('./pages/simulation/simulation.component').then((m) => m.SimulationComponent),
  },
//   {
//     path: '**',
//     redirectTo: '',
//   },
];
