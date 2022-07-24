import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditarNuevoComponent } from './editar-nuevo/editar-nuevo.component';
import { ListaUsuariosComponent } from './lista-usuarios/lista-usuarios.component';
import { UsuariosComponent } from './usuarios.component';

const routes: Routes = [
  {
    path: '',
    component: UsuariosComponent,
    children: [
      { path: 'lista-usuarios', component: ListaUsuariosComponent },
      { path: 'editar-nuevo', component: EditarNuevoComponent },
      { path: '**', redirectTo: 'lista-usuarios' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuariosRoutingModule { }
