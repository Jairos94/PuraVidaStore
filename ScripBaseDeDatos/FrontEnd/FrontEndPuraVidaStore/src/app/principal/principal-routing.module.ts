import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PrincipalComponent } from './principal.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { VentasComponent } from './ventas/ventas.component';

const routes: Routes = [{ 
  path: '', 
  component: PrincipalComponent,
  children:[
    {path:'usuarios',component:UsuariosComponent},
    {path:'ventas',component:VentasComponent},
    {path:'**',redirectTo:'ventas'}
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrincipalRoutingModule { }
