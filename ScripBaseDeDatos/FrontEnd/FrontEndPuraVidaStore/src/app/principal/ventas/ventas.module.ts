import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VentasRoutingModule } from './ventas-routing.module';
import { VentasComponent } from './ventas.component';
import { FacturacionComponent } from './facturacion/facturacion.component';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';
import { FormsModule } from '@angular/forms';
import { ImpuestosComponent } from './impuestos/impuestos.component';


@NgModule({
  declarations: [
    VentasComponent,
    FacturacionComponent,
    ImpuestosComponent
  ],
  imports: [
    CommonModule,
    VentasRoutingModule,
    PrimeNgPrincipalModule,
    FormsModule
  ]
})
export class VentasModule { }
