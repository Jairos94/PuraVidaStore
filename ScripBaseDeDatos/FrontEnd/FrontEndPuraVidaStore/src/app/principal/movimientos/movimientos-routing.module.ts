import { InventariosComponent } from './inventarios/inventarios.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IngresosComponent } from './ingresos/ingresos.component';
import { MovimientosComponent } from './movimientos.component';
import { TrasladosComponent } from './traslados/traslados.component';
import { ReporteMovimientosComponent } from './reporte-movimientos/reporte-movimientos.component';

const routes: Routes = [
  {
    path: '',
    component: MovimientosComponent,
    children: [
      { path: 'ingreso-productos', component: IngresosComponent },
      { path: 'inventarios', component: InventariosComponent },
      { path: 'traslados', component: TrasladosComponent },
      { path: 'reportes', component: ReporteMovimientosComponent },
      {
        path: 'ajustes',
        loadChildren: () =>
          import('./ajustes/ajustes.module').then((m) => m.AjustesModule),
      },

      { path: '', component: InventariosComponent },
      { path: '**', redirectTo:'/'},

    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MovimientosRoutingModule {}
