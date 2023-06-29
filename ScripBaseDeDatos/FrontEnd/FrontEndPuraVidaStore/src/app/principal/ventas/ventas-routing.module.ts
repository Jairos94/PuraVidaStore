import { ImpuestosComponent } from './impuestos/impuestos.component';
import { FacturacionComponent } from './facturacion/facturacion.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VentasComponent } from './ventas.component';

const routes: Routes = [
  {
    path: '',
    component: VentasComponent,
    children: [
      { path: '', component: FacturacionComponent },
      { path: 'facturacion', component: FacturacionComponent },
      { path: 'impuestos', component: ImpuestosComponent },

      {path:'**',redirectTo:'/'}
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VentasRoutingModule {}
