import { RealizarAjusteComponent } from './realizar-ajuste/realizar-ajuste.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AjustesComponent } from './ajustes.component';
import { RazonAjustesComponent } from './razon-ajustes/razon-ajustes.component';

const routes: Routes = [{ path: '', component: AjustesComponent,children:
[
  {path:'',component:RealizarAjusteComponent},
  {path:'realizar-ajuste',component:RealizarAjusteComponent},
  {path:'razon-ajuste',component:RazonAjustesComponent},
  {path:'**',redirectTo:'/'},


] }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AjustesRoutingModule { }
