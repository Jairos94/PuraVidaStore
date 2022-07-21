import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';

const routes: Routes = [

  { path: 'login', component: LoginComponent },

  { path: 'principal', loadChildren: () => import('./principal/principal.module').then(m => m.PrincipalModule) },

  { path: 'usuarios', loadChildren: () => import('./principal/usuarios/usuarios.module').then(m => m.UsuariosModule) },

  { path: 'ventas', loadChildren: () => import('./principal/ventas/ventas.module').then(m => m.VentasModule) },
  { path: '**', component: LoginComponent },  // Wildcard route for a 404 page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
