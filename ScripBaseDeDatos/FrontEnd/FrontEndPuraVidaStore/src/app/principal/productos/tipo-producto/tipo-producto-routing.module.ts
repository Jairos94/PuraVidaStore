import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListaTipoProductoComponent } from './lista-tipo-producto/lista-tipo-producto.component';
import { TipoProductoComponent } from './tipo-producto.component';

const routes: Routes = [{ 
  path: '',
   component: TipoProductoComponent,
   children:[
    {path:'lista-tipo-producto',component:ListaTipoProductoComponent},
    {path:'**', redirectTo:'lista-tipo-producto'}
   ]
 }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TipoProductoRoutingModule { }
