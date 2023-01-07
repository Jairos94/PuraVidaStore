import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VentasRoutingModule } from './ventas-routing.module';
import { VentasComponent } from './ventas.component';
import { FacturacionComponent } from './facturacion/facturacion.component';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';


@NgModule({
  declarations: [
    VentasComponent,
    FacturacionComponent
  ],
  imports: [
    CommonModule,
    VentasRoutingModule,
    PrimeNgPrincipalModule
  ]
})
export class VentasModule { }
