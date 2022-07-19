import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditarNuevoComponent } from './editar-nuevo/editar-nuevo.component';
import { ListaUsuariosComponent } from './lista-usuarios/lista-usuarios.component';

const routes: Routes = [
  {
    path:'',
    children:[
      {path:'nuevo-editar',component:EditarNuevoComponent},
      {path:'lista-Usuarios',component:ListaUsuariosComponent},
      {path:'',redirectTo:'lista-Usuarios'}
    ]
  },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuariosRoutingModule { }
