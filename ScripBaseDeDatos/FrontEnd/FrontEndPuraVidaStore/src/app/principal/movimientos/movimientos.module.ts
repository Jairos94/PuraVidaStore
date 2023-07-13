import { PrimeNgPrincipalModule } from './../prime-ng-principal.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MovimientosRoutingModule } from './movimientos-routing.module';
import { MovimientosComponent } from './movimientos.component';
import { IngresosComponent } from './ingresos/ingresos.component';
import { InventariosComponent } from './inventarios/inventarios.component';
import { FormsModule } from '@angular/forms';
import { TrasladosComponent } from './traslados/traslados.component';
import { ReportesComponent } from './reportes/reportes.component';


@NgModule({
  declarations: [
    MovimientosComponent,
    IngresosComponent,
    InventariosComponent,
    TrasladosComponent,
    ReportesComponent,
  ],
  imports: [
    CommonModule,
    MovimientosRoutingModule,
    PrimeNgPrincipalModule,
    FormsModule
  ]
})
export class MovimientosModule { }
