import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PrincipalRoutingModule } from './principal-routing.module';
import { MantenimientosComponent } from './mantenimientos/mantenimientos.component';
import { ComponentesComponent } from './componentes/componentes.component';


@NgModule({
  declarations: [
    MantenimientosComponent,
    ComponentesComponent,
  ],
  imports: [
    CommonModule,
    PrincipalRoutingModule
  ]
})
export class PrincipalModule { }
