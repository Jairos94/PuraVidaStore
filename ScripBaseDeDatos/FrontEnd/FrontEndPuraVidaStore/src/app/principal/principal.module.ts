import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsuariosModule } from './usuarios/usuarios.module';
import { PrincipalRoutingModule } from './principal-routing.module';
import { ComponentesComponent } from './componentes/componentes.component';
import { PrimeNGModule } from '../prime-ng.module';
import { VentasModule } from './ventas/ventas.module';
import { UsuariosComponent } from './usuarios/usuarios.component';


@NgModule({
  declarations: [
    ComponentesComponent,
    UsuariosComponent
  ],
  imports: [
    CommonModule,
    PrincipalRoutingModule,
    PrimeNGModule,
    VentasModule
  ]
})
export class PrincipalModule { }
