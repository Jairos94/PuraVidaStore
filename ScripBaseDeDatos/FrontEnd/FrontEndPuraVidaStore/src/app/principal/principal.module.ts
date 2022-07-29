import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PrincipalRoutingModule } from './principal-routing.module';
import { PrincipalComponent } from './principal.component';
import { PrimeNgPrincipalModule } from './prime-ng-principal.module';
import { PipePrincipalPipe } from './pipes/pipe-principal.pipe';
import { PersonasComponent } from './personas/personas.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    PrincipalComponent,
    PipePrincipalPipe,
    PersonasComponent
  ],
  imports: [
    CommonModule,
    PrincipalRoutingModule,
    PrimeNgPrincipalModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  exports:[
    PersonasComponent
  ]
})
export class PrincipalModule { }
