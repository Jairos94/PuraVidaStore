import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductosComponent } from './productos.component';
import { TipoProductoComponent } from './tipo-producto/tipo-producto.component';

const routes: Routes = [
  {
    path: '',
    component: ProductosComponent,
    children: [
      { path: 'tipo-producto',component: TipoProductoComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductosRoutingModule { }
