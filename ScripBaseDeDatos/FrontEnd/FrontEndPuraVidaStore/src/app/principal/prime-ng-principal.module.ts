import { NgModule } from '@angular/core';

//prime ng
import { ButtonModule } from 'primeng/button';
import { MenubarModule } from 'primeng/menubar';
import { InputTextModule } from 'primeng/inputtext';

@NgModule({
  exports: [
    ButtonModule,
    MenubarModule,
    InputTextModule
  ]
})
export class PrimeNgPrincipalModule { }
