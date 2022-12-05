import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BodegasRoutingModule } from './bodegas-routing.module';
import { BodegasComponent } from './bodegas.component';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';
import { ListaBodegasComponent } from './lista-bodegas/lista-bodegas.component';


@NgModule({
  declarations: [
    BodegasComponent,
    ListaBodegasComponent
  ],
  imports: [
    CommonModule,
    BodegasRoutingModule,
    PrimeNgPrincipalModule,
  ]
})
export class BodegasModule { }
