import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { DashModuleModule } from './dashboard/dash-module.module';
import { PrimeNgModule } from './prime-ng.module';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DashModuleModule,
    PrimeNgModule,
    

  ],
  exports: [
    LoginComponent,
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
