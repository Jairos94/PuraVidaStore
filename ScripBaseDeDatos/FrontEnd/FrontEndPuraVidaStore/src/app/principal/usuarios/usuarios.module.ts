
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { UsuariosRoutingModule } from './usuarios-routing.module';
import { UsuariosComponent } from './usuarios.component';
import { ListaUsuariosComponent } from './lista-usuarios/lista-usuarios.component';
import { EditarNuevoComponent } from './editar-nuevo/editar-nuevo.component';



import { FormsModule } from '@angular/forms';
import { PrimeNgPrincipalModule } from '../prime-ng-principal.module';
import { PrincipalModule } from '../principal.module';

@NgModule({
  declarations: [
    UsuariosComponent,
    ListaUsuariosComponent,
    EditarNuevoComponent
  ],
  imports: [
    CommonModule,

    //prime ng
    FormsModule,
    PrimeNgPrincipalModule,
    UsuariosRoutingModule,
    PrincipalModule
  ]
})
export class UsuariosModule { }
