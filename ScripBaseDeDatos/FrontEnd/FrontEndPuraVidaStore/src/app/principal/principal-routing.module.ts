import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsuariosComponent } from './usuarios/usuarios.component';
const routes: Routes = [
  
    
      {
        path: 'usuarios',
        component: UsuariosComponent,
      },
      {
        path: 'ventas',
        component: UsuariosComponent,
        loadChildren: () => import('../principal/ventas/ventas.module').then(m => m.VentasModule)
      },
      {
        path: '**', redirectTo: '/menu'
      },
    
  
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrincipalRoutingModule { }
