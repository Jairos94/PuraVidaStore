import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { VentasComponent } from './ventas/ventas.component';
const routes: Routes = [
  {path: 'usuarios',component: UsuariosComponent},
  {path: 'ventas',component: VentasComponent},
  { path: '',   redirectTo: './ventas', pathMatch: 'full' }, // redirect to `first-component`
  { path: '**',  redirectTo: './ventas', pathMatch: 'full' },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrincipalRoutingModule { }
