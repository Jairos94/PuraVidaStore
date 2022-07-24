import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//prime ng
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
<<<<<<< HEAD
import {MenubarModule} from 'primeng/menubar';
@NgModule({
  declarations: [],
  
  exports:[
=======
import { PanelMenuModule } from 'primeng/panelmenu';
import { ToastModule } from 'primeng/toast';
import { SidebarModule } from 'primeng/sidebar';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
>>>>>>> feature/Cambios
    BrowserAnimationsModule,
    ButtonModule,
    InputTextModule,
    MessageModule,
    PanelMenuModule,
    ToastModule,
    SidebarModule,
<<<<<<< HEAD
    MessageModule,
    MenubarModule
=======

>>>>>>> feature/Cambios
  ]
})
export class PrimeNGModule { }
