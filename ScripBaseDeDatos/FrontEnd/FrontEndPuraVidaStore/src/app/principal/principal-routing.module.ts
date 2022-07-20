import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ComponentesComponent } from './componentes/componentes.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
const routes: Routes = [
  
    
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

      { path: '/menu', component: ComponentesComponent },
      {
        path: '**', redirectTo: '/menu'
      },
    
  
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrincipalRoutingModule { }
