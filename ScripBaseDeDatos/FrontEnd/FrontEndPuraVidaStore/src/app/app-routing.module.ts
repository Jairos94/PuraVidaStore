import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
<<<<<<< HEAD

const routes: Routes = [

  { path: 'login', component: LoginComponent },
  
  { path: '**', redirectTo:'login'},
=======
import { PrimeNGModule } from './prime-ng.module';
const routes: Routes = [

  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },

  { path: 'principal', loadChildren: () => import('./principal/principal.module').then(m => m.PrincipalModule) },
  { path: '**', component: LoginComponent },  // Wildcard route for a 404 page
>>>>>>> feature/Cambios
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
