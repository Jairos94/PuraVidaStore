import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) },
  { path: 'principal', loadChildren: () => import('./principal/principal.module').then(m => m.PrincipalModule) },
  { path: 'usuarios', loadChildren: () => import('./principal/usuarios/usuarios.module').then(m => m.UsuariosModule) },
  { path: 'ventas', loadChildren: () => import('./principal/ventas/ventas.module').then(m => m.VentasModule) },
  {path:'**',redirectTo:'login'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
