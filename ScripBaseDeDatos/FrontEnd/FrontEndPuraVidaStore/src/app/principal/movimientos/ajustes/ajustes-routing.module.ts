import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AjustesComponent } from './ajustes.component';
import { RazonAjustesComponent } from './razon-ajustes/razon-ajustes.component';
import { RealizarAjusteComponent } from './realizar-ajuste/realizar-ajuste.component';

const routes: Routes = [{ path: '', component: AjustesComponent,children:
[
  {path:'realizar-ajuste',component:RealizarAjusteComponent},
  {path:'razon-ajuste',component:RazonAjustesComponent},
  {path:'',component:RealizarAjusteComponent},
  {path:'**',redirectTo:'/'},


] }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AjustesRoutingModule { }
