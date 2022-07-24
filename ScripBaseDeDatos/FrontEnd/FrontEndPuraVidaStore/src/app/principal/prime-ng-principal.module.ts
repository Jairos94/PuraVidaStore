import { NgModule } from '@angular/core';

//prime ng
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { MenuModule } from 'primeng/menu';
import { MenubarModule } from 'primeng/menubar';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { RippleModule } from 'primeng/ripple';
import { SidebarModule } from 'primeng/sidebar';

@NgModule({
  exports: [
    ButtonModule,
    InputTextModule,
    MenuModule,
    MenubarModule,
    TableModule,
    ToastModule,
    RippleModule,
    SidebarModule
  ]
})
export class PrimeNgPrincipalModule { }
