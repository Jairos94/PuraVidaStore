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
      { path: 'editar-nuevo/:id', component: EditarNuevoComponent },
      { path: '', component: ListaUsuariosComponent },
      { path: '**', redirectTo: '/' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuariosRoutingModule { }
