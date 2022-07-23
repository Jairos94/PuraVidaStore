import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';

//prime ng
import {TableModule} from 'primeng/table';

@NgModule({
  exports: [
    BrowserAnimationsModule,
    BrowserModule,

    //prime ng
    TableModule
  ]
})
export class PrimeNgUsuariosModule { }
