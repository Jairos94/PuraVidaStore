import { PrimeNgPrincipalModule } from './../prime-ng-principal.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MovimientosRoutingModule } from './movimientos-routing.module';
import { MovimientosComponent } from './movimientos.component';
import { IngresosComponent } from './ingresos/ingresos.component';
import { InventariosComponent } from './inventarios/inventarios.component';


@NgModule({
  declarations: [
    MovimientosComponent,
    IngresosComponent,
    InventariosComponent
  ],
  imports: [
    CommonModule,
    MovimientosRoutingModule,
    PrimeNgPrincipalModule
  ]
})
export class MovimientosModule { }
