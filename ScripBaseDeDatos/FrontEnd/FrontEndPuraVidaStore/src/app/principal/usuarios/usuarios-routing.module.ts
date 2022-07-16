import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditarNuevoComponent } from './editar-nuevo/editar-nuevo.component';

const routes: Routes = [
  {path:'mantenimiento-usuario/:id',component:EditarNuevoComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuariosRoutingModule { }
