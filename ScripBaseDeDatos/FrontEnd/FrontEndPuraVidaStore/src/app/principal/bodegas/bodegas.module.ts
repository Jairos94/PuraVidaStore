import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BodegasRoutingModule } from './bodegas-routing.module';
import { BodegasComponent } from './bodegas.component';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';
import { AgregarEditarBodegaComponent } from './agregar-editar-bodega/agregar-editar-bodega.component';
import { ListaBodegasComponent } from './lista-bodegas/lista-bodegas.component';


@NgModule({
  declarations: [
    BodegasComponent,
    AgregarEditarBodegaComponent,
    ListaBodegasComponent
  ],
  imports: [
    CommonModule,
    BodegasRoutingModule,
    PrimeNgPrincipalModule
  ]
})
export class BodegasModule { }
