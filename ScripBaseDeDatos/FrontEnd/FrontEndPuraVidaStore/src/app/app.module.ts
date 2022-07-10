import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { PrimeNGModule } from './prime-ng.module';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {AccordionModule} from 'primeng/accordion';
import { LoginComponent } from './login/login.component';
import { PrincipalComponent } from './principal/principal.component';   //accordion and accordion tab

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PrincipalComponent,
  ],
  imports: [
    AccordionModule,
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    PrimeNGModule,
    BrowserAnimationsModule,

  ],
  exports:[
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
