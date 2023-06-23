import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientesComponent } from './clientes.component';
import { MayoristasComponent } from './mayoristas/mayoristas.component';
import { AgregarClienteComponent } from './agregar-cliente/agregar-cliente.component';

const routes: Routes = [
  {
    path: '',
    component: ClientesComponent,
    children: [
      { path: '', component: MayoristasComponent },
      { path: 'mayorista', component: MayoristasComponent },
      { path: 'editar-nuevo/:id', component: AgregarClienteComponent },
      { path: '**', redirectTo: '/' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientesRoutingModule {}
