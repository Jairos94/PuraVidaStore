import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductosRoutingModule } from './productos-routing.module';
import { ProductosComponent } from './productos.component';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';


@NgModule({
  declarations: [
    ProductosComponent,
  ],
  imports: [
    CommonModule,
    ProductosRoutingModule,
    PrimeNgPrincipalModule
  ]
})
export class ProductosModule { }
