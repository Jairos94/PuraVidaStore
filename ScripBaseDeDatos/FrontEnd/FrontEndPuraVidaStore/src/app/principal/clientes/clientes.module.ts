import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientesRoutingModule } from './clientes-routing.module';
import { ClientesComponent } from './clientes.component';
import { MayoristasComponent } from './mayoristas/mayoristas.component';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';
import { AgregarClienteComponent } from './agregar-cliente/agregar-cliente.component';


@NgModule({
  declarations: [
    ClientesComponent,
    MayoristasComponent,
    AgregarClienteComponent
  ],
  imports: [
    CommonModule,
    ClientesRoutingModule,
    PrimeNgPrincipalModule
  ]
})
export class ClientesModule { }
