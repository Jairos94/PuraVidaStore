import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientesComponent } from './clientes.component';
import { MayoristasComponent } from './mayoristas/mayoristas.component';

const routes: Routes = [
  {
    path: '',
    component: ClientesComponent,
    children: [
      { path: '', component: MayoristasComponent },
      { path: 'mayorista', component: MayoristasComponent },
      { path: '**', redirectTo: '/' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientesRoutingModule {}
