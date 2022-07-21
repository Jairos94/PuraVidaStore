import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsuariosRoutingModule } from './usuarios-routing.module';
import { UsuariosComponent } from './usuarios.component';
import { NuevoEditarComponent } from './nuevo-editar/nuevo-editar.component';
import { ListaUsuariosComponent } from './lista-usuarios/lista-usuarios.component';


@NgModule({
  declarations: [
    UsuariosComponent,
    NuevoEditarComponent,
    ListaUsuariosComponent
  ],
  imports: [
    CommonModule,
    UsuariosRoutingModule
  ]
})
export class UsuariosModule { }
