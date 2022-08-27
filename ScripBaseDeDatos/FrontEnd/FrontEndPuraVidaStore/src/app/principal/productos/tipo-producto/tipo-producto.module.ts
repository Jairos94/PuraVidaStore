import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TipoProductoRoutingModule } from './tipo-producto-routing.module';
import { TipoProductoComponent } from './tipo-producto.component';
import { ListaTipoProductoComponent } from './lista-tipo-producto/lista-tipo-producto.component';
import { PrimeNgPrincipalModule } from '../../prime-ng-principal.module';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    TipoProductoComponent,
    ListaTipoProductoComponent
  ],
  imports: [
    CommonModule,
    TipoProductoRoutingModule,
    PrimeNgPrincipalModule,
    FormsModule
  ]
})
export class TipoProductoModule { }
