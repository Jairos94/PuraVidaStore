import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { PrimeNGModule } from './prime-ng.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Base64 } from 'js-base64';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { LoginComponent } from './login/login.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Interceptor } from './services/interceptor';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    PrimeNGModule,
    BrowserAnimationsModule,
    Base64

  ],
  exports:[
    PrimeNGModule
  ],
  providers:[{
    provide: HTTP_INTERCEPTORS,
    useClass: Interceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
