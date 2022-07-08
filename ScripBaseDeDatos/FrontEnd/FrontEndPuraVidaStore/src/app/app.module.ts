import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {AccordionModule} from 'primeng/accordion';
import { LoginComponent } from './login/login.component';
import { PrincipalComponent } from './principal/principal.component';   //accordion and accordion tab
//import {MenuItem} from 'primeng/api';   

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PrincipalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AccordionModule,

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
