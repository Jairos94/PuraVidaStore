import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//prime ng
import {ButtonModule} from 'primeng/button';
import {InputTextModule} from 'primeng/inputtext';
import {MegaMenuModule} from 'primeng/megamenu';
import { PanelMenuModule } from 'primeng/panelmenu';
import {TabMenuModule} from 'primeng/tabmenu';
import {ToastModule} from 'primeng/toast';
import {SidebarModule} from 'primeng/sidebar';
import { MessageModule } from 'primeng/message';
import {MenubarModule} from 'primeng/menubar';
@NgModule({
  declarations: [],
  
  exports:[
    BrowserAnimationsModule,
    ButtonModule,
    InputTextModule,
    MegaMenuModule,
    TabMenuModule,
    PanelMenuModule,
    ToastModule,
    SidebarModule,
    MessageModule,
    MenubarModule
  ]
})
export class PrimeNGModule { }
