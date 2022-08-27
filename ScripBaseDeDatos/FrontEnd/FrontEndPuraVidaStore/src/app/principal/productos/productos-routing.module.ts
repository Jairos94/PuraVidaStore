import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductosComponent } from './productos.component';

const routes: Routes = [
  {
    path: '',
    component: ProductosComponent,
    children:[
      { path: 'tipo-producto', loadChildren: () => import('./tipo-producto/tipo-producto.module').then(m => m.TipoProductoModule) }

    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductosRoutingModule { }
