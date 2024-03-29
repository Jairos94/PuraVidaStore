import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductosRoutingModule } from './productos-routing.module';
import { ProductosComponent } from './productos.component';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';
import { TipoProductoComponent } from './tipo-producto/tipo-producto.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConsultaProductoComponent } from './consulta-producto/consulta-producto.component';


@NgModule({
  declarations: [
    ProductosComponent,
    TipoProductoComponent,
    ConsultaProductoComponent,
  ],
  imports: [
    CommonModule,
    ProductosRoutingModule,
    PrimeNgPrincipalModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class ProductosModule { }
