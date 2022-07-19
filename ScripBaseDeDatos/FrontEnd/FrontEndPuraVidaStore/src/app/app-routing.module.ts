import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PrincipalComponent } from './principal/principal.component';

const routes: Routes = [

  { path: 'login', component: LoginComponent },
  {
    path: 'principal',
    component: PrincipalComponent,
    loadChildren: () => import('./principal/principal.module').then(m => m.PrincipalModule)
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' }, // redirect to `first-component`
  { path: '**', redirectTo: 'login'},  // Wildcard route for a 404 page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
