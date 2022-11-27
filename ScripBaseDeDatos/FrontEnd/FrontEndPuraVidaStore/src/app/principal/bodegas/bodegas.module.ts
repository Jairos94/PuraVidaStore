import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BodegasRoutingModule } from './bodegas-routing.module';
import { BodegasComponent } from './bodegas.component';


@NgModule({
  declarations: [
    BodegasComponent
  ],
  imports: [
    CommonModule,
    BodegasRoutingModule
  ]
})
export class BodegasModule { }
