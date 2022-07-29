import { NgModule } from '@angular/core';

//prime ng
import { AccordionModule } from 'primeng/accordion';
import { AutoCompleteModule } from 'primeng/autocomplete';
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
    AccordionModule,
    AutoCompleteModule,
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
