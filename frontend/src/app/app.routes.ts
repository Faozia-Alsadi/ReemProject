import { Routes } from '@angular/router';
import { authGuard } from './core/auth-guard';

export const routes: Routes = [
  { path: 'login', loadComponent: () => import('./pages/login/login').then(m => m.Login) },
  {
    path: '',
    loadComponent: () => import('./layout/shell/shell').then(m => m.Shell),
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard) },
      { path: 'projects',  loadComponent: () => import('./pages/projects/projects').then(m => m.Projects) },
      { path: 'users',     loadComponent: () => import('./pages/users/users').then(m => m.Users) },
    ]
  },
  { path: '**', redirectTo: '' }
];
