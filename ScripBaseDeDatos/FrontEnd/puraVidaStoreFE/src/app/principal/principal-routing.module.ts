import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PrincipalComponent } from './principal.component';

const routes: Routes = [
  { path: '', component: PrincipalComponent },
  
  { path: 'usuarios', loadChildren: () => import('../principal/usuarios/usuarios.module').then(m => m.UsuariosModule) },
  { path: 'ventas', loadChildren: () => import('../principal/ventas/ventas.module').then(m => m.VentasModule) },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrincipalRoutingModule { }
