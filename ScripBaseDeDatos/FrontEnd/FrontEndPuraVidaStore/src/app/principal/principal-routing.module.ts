import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MantenimientosComponent } from './mantenimientos/mantenimientos.component';
const routes: Routes = [
  {
    path: 'mantenimientos',
    component: MantenimientosComponent,
    loadChildren: () => import('./mantenimientos/mantenimientos.module').then(m => m.MantenimientosModule)
  },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrincipalRoutingModule { }
