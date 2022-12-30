import { InventariosComponent } from './inventarios/inventarios.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IngresosComponent } from './ingresos/ingresos.component';
import { MovimientosComponent } from './movimientos.component';

const routes: Routes = [{ path: '', component: MovimientosComponent,children:[
  {path:'ingreso-productos',component:IngresosComponent},
  {path:'inventarios',component:InventariosComponent},

  {path:'**',redirectTo:'inventarios'}
] }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MovimientosRoutingModule { }
