import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BodegasComponent } from './bodegas.component';
import { ListaBodegasComponent } from './lista-bodegas/lista-bodegas.component';

const routes: Routes = [{ path: '', 
component: BodegasComponent ,
children:[
  {path:'lista-bodegas',component:ListaBodegasComponent},
  {path:'**',redirectTo:'lista-bodega'}

]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BodegasRoutingModule { }
