import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsuariosModule } from './usuarios/usuarios.module';
import { PrincipalRoutingModule } from './principal-routing.module';
import { ComponentesComponent } from './componentes/componentes.component';
import { PrimeNGModule } from '../prime-ng.module';
import { VentasComponent } from './ventas/ventas.component';
import { VentasModule } from './ventas/ventas.module';


@NgModule({
  declarations: [
    ComponentesComponent,
    
  ],
  imports: [
    CommonModule,
    PrincipalRoutingModule,
    PrimeNGModule,
    UsuariosModule,
    VentasModule
  ]
})
export class PrincipalModule { }
