import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//prime ng
import {ButtonModule} from 'primeng/button';
import {InputTextModule} from 'primeng/inputtext';
import {MegaMenuModule} from 'primeng/megamenu';
import { PanelMenuModule } from 'primeng/panelmenu';
import {ToastModule} from 'primeng/toast';
import {SidebarModule} from 'primeng/sidebar';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports:[
    BrowserAnimationsModule,
    ButtonModule,
    InputTextModule,
    MegaMenuModule,
    
    PanelMenuModule,
    ToastModule,
    SidebarModule,
    
  ]
})
export class PrimeNGModule { }
