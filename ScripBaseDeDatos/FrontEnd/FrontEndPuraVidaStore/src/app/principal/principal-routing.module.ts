import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsuariosComponent } from './usuarios/usuarios.component';
const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'usuarios',
        component: UsuariosComponent,
        loadChildren: () => import('../principal/usuarios/usuarios.module').then(m => m.UsuariosModule)
      },
      {
        path: 'ventas',
        component: UsuariosComponent,
        loadChildren: () => import('../principal/ventas/ventas.module').then(m => m.VentasModule)
      },
      

      {
        path: '',
        redirectTo:'ventas'
      },
    ]
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrincipalRoutingModule { }
