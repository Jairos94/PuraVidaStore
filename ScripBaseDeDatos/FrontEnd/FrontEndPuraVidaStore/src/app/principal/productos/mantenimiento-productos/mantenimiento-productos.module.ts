import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MantenimientoProductosRoutingModule } from './mantenimiento-productos-routing.module';
import { MantenimientoProductosComponent } from './mantenimiento-productos.component';
import { ListaProductosComponent } from './lista-productos/lista-productos.component';
import { AgregarEditarComponent } from './agregar-editar/agregar-editar.component';
import { PrimeNgPrincipalModule } from '../../prime-ng-principal.module';


@NgModule({
  declarations: [
    MantenimientoProductosComponent,
    ListaProductosComponent,
    AgregarEditarComponent
  ],
  imports: [
    CommonModule,
    MantenimientoProductosRoutingModule,
    PrimeNgPrincipalModule
  ]
})
export class MantenimientoProductosModule { }
