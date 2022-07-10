import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PrincipalComponent } from './principal/principal.component';
import { UsuariosComponent } from './principal/usuarios/usuarios.component';
import { VentasComponent } from './principal/ventas/ventas.component';

const routes: Routes = [

  { path: 'login', component: LoginComponent },
  { path: 'principal',
  component: PrincipalComponent, // this is the component with the <router-outlet> in the template
  children: [
    {
      path: 'usuarios', // child route path
      component: UsuariosComponent, // child route component that the router renders
    },
    {
      path: 'ventas', // child route path
      component: VentasComponent, // child route component that the router renders
    },
    
  ],
},
  {
    path: 'principal',
    component: PrincipalComponent,
    loadChildren: () => import('./principal/principal.module').then(m => m.PrincipalModule)
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // redirect to `first-component`
  { path: '**', component: LoginComponent },  // Wildcard route for a 404 page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
