import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PrincipalRoutingModule } from './principal-routing.module';
import { PrincipalComponent } from './principal.component';
import { PrimeNgPrincipalModule } from './prime-ng-principal.module';
import { PipePrincipalPipe } from './pipes/pipe-principal.pipe';


@NgModule({
  declarations: [
    PrincipalComponent,
    PipePrincipalPipe
  ],
  imports: [
    CommonModule,
    PrincipalRoutingModule,
    PrimeNgPrincipalModule
  ]
})
export class PrincipalModule { }
