import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PrincipalRoutingModule } from './principal-routing.module';
import { PrincipalComponent } from './principal.component';
import { PrimeNgPrincipalModule } from './prime-ng-principal.module';
import { PipePrincipalPipe } from './pipes/pipe-principal.pipe';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConfiguracionComponent } from './configuracion/configuracion.component';
import { EditarPerfilComponent } from './editar-perfil/editar-perfil.component';


@NgModule({
  declarations: [
    PrincipalComponent,
    PipePrincipalPipe,
    ConfiguracionComponent,
    EditarPerfilComponent
  ],
  imports: [
    CommonModule,
    PrincipalRoutingModule,
    PrimeNgPrincipalModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  exports:[
  ]
})
export class PrincipalModule { }
