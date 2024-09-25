import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AgregarEditarComponent } from './agregar-editar/agregar-editar.component';
import { ListaProductosComponent } from './lista-productos/lista-productos.component';
import { MantenimientoProductosComponent } from './mantenimiento-productos.component';

const routes: Routes = [
  {
    path: '', component: MantenimientoProductosComponent,
    children:[
      {path:'agregar-editar/:id',component:AgregarEditarComponent},
      {path:'lista-productos',component:ListaProductosComponent},
      {path:'',component:ListaProductosComponent},
      {path:'**',redirectTo:'/'}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MantenimientoProductosRoutingModule { }
