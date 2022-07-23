import { NgModule } from '@angular/core';

//prime ng
import { ButtonModule } from 'primeng/button';
import { MenubarModule } from 'primeng/menubar';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';

@NgModule({
  exports: [
    ButtonModule,
    MenubarModule,
    InputTextModule,
    TableModule,
  ]
})
export class PrimeNgPrincipalModule { }
