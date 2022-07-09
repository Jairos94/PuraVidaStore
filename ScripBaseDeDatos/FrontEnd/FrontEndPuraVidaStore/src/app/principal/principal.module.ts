import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsuariosModule } from './usuarios/usuarios.module';
import { PrincipalRoutingModule } from './principal-routing.module';
import { ComponentesComponent } from './componentes/componentes.component';
import { PrimeNGModule } from '../prime-ng.module';


@NgModule({
  declarations: [
    ComponentesComponent,
  ],
  imports: [
    CommonModule,
    PrincipalRoutingModule,
    PrimeNGModule,
    UsuariosModule
  ]
})
export class PrincipalModule { }
