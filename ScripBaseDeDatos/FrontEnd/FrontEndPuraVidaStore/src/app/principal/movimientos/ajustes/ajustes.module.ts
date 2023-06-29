
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AjustesRoutingModule } from './ajustes-routing.module';
import { AjustesComponent } from './ajustes.component';
import { RazonAjustesComponent } from './razon-ajustes/razon-ajustes.component';
import { FormsModule } from '@angular/forms';
import { RealizarAjusteComponent } from './realizar-ajuste/realizar-ajuste.component';
import { PrimeNgPrincipalModule } from '../../prime-ng-principal.module';

@NgModule({
  declarations: [
    AjustesComponent,
    RazonAjustesComponent,
    RealizarAjusteComponent
  ],
  imports: [
    CommonModule,
    AjustesRoutingModule,
    PrimeNgPrincipalModule,
    FormsModule,
  ]
})
export class AjustesModule { }
