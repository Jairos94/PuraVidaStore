import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsuariosRoutingModule } from './usuarios-routing.module';
import { UsuariosComponent } from './usuarios.component';
import { ListaUsuariosComponent } from './lista-usuarios/lista-usuarios.component';
import { EditarNuevoComponent } from './editar-nuevo/editar-nuevo.component';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';

@NgModule({
  declarations: [
    UsuariosComponent,
    ListaUsuariosComponent,
    EditarNuevoComponent
  ],
  imports: [
    CommonModule,
    PrimeNgPrincipalModule,
    UsuariosRoutingModule,
  ]
})
export class UsuariosModule { }
