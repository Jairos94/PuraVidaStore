import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductosRoutingModule } from './productos-routing.module';
import { ProductosComponent } from './productos.component';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';
import { TipoProductoComponent } from './tipo-producto/tipo-producto.component';


@NgModule({
  declarations: [
    ProductosComponent,
    TipoProductoComponent,
  ],
  imports: [
    CommonModule,
    ProductosRoutingModule,
    PrimeNgPrincipalModule
  ]
})
export class ProductosModule { }
