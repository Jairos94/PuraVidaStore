import { NgModule } from '@angular/core';


//Primereng
import {AccordionModule} from 'primeng/accordion'; 
import {InputTextModule} from 'primeng/inputtext';
import {PasswordModule} from 'primeng/password';
import {ButtonModule} from 'primeng/button';
import {MegaMenuModule} from 'primeng/megamenu';


@NgModule({
  declarations: [],
  exports:[
    AccordionModule,
    InputTextModule,
    PasswordModule,
    ButtonModule,
    MegaMenuModule
  ]

})
export class PrimeNgModule { }
