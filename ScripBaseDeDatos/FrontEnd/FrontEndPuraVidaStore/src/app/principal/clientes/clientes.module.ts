import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientesRoutingModule } from './clientes-routing.module';
import { ClientesComponent } from './clientes.component';
import { MayoristasComponent } from './mayoristas/mayoristas.component';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';


@NgModule({
  declarations: [
    ClientesComponent,
    MayoristasComponent
  ],
  imports: [
    CommonModule,
    ClientesRoutingModule,
    PrimeNgPrincipalModule
  ]
})
export class ClientesModule { }
