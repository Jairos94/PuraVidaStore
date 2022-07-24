import { NgModule } from '@angular/core';

//prime ng
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { MenubarModule } from 'primeng/menubar';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { RippleModule } from 'primeng/ripple';

@NgModule({
  exports: [
    ButtonModule,
    MenubarModule,
    InputTextModule,
    TableModule,
    ToastModule,
    RippleModule,
  ]
})
export class PrimeNgPrincipalModule { }
