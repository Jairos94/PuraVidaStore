import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PrincipalComponent } from './principal.component';

const routes: Routes = [{
  path: '',
  component: PrincipalComponent,
  children: [
    { path: 'usuarios', loadChildren: () => import('./usuarios/usuarios.module').then(m => m.UsuariosModule) },
    { path: 'ventas', loadChildren: () => import('./ventas/ventas.module').then(m => m.VentasModule) },
    { path: 'productos', loadChildren: () => import('./productos/productos.module').then(m => m.ProductosModule) },
    { path: 'movimientos', loadChildren: () => import('./movimientos/movimientos.module').then(m => m.MovimientosModule) },
    { path: 'bodegas', loadChildren: () => import('./bodegas/bodegas.module').then(m => m.BodegasModule) },
    { path: '**', redirectTo: 'ventas' },
  ]
},


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrincipalRoutingModule { }
