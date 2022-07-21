import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListaUsuariosComponent } from './lista-usuarios/lista-usuarios.component';
import { NuevoEditarComponent } from './nuevo-editar/nuevo-editar.component';
import { UsuariosComponent } from './usuarios.component';

const routes: Routes = [
  { path: '', 
  component: UsuariosComponent ,
  children: 
  [
    { path: 'nuevo-editar/:id', component: NuevoEditarComponent },
    { path: 'lista-usuarios', component: ListaUsuariosComponent },
    { path: '', redirectTo: 'lista-usuarios'}
  ]
},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuariosRoutingModule { }
