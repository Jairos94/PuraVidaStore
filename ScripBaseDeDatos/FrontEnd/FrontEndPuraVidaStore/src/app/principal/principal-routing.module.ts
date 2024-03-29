import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ConfiguracionComponent } from './configuracion/configuracion.component';
import { PrincipalComponent } from './principal.component';
import { EditarPerfilComponent } from './editar-perfil/editar-perfil.component';

const routes: Routes = [
  {
    path: '',
    component: PrincipalComponent,
    children: [
      {
        path: 'usuarios',
        loadChildren: () =>
          import('./usuarios/usuarios.module').then((m) => m.UsuariosModule),
      },
      {
        path: 'ventas',
        loadChildren: () =>
          import('./ventas/ventas.module').then((m) => m.VentasModule),
      },
      {
        path: 'productos',
        loadChildren: () =>
          import('./productos/productos.module').then((m) => m.ProductosModule),
      },
      {
        path: 'movimientos',
        loadChildren: () =>
          import('./movimientos/movimientos.module').then(
            (m) => m.MovimientosModule
          ),
      },
      {
        path: 'bodegas',
        loadChildren: () =>
          import('./bodegas/bodegas.module').then((m) => m.BodegasModule),
      },
      {
        path: 'clientes',
        loadChildren: () =>
          import('./clientes/clientes.module').then((m) => m.ClientesModule),
      },
      { path: 'configuracion', component: ConfiguracionComponent },
      { path: 'editar-perfil/:id', component: EditarPerfilComponent },
      { path: '**', redirectTo: 'ventas' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PrincipalRoutingModule {}
