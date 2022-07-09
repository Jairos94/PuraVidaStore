import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

//prime ng
import {ButtonModule} from 'primeng/button';
import {InputTextModule} from 'primeng/inputtext';
@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports:[
    InputTextModule,
    ButtonModule,
  ]
})
export class PrimeNGModule { }
