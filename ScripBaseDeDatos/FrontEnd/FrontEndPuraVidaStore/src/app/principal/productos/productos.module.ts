import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductosRoutingModule } from './productos-routing.module';
import { ProductosComponent } from './productos.component';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';
import { TipoProductoComponent } from './tipo-producto/tipo-producto.component';
import { MantenimientoProductosComponent } from './mantenimiento-productos/mantenimiento-productos.component';


@NgModule({
  declarations: [
    ProductosComponent,
    TipoProductoComponent,
    MantenimientoProductosComponent,
  ],
  imports: [
    CommonModule,
    ProductosRoutingModule,
    PrimeNgPrincipalModule
  ]
})
export class ProductosModule { }
