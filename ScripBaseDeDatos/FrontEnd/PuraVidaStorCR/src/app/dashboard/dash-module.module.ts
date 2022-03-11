import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { PrimeNgModule } from '../prime-ng.module';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';



@NgModule({
  declarations: [
    DashboardComponent,
    NavbarComponent,
    SidebarComponent],
  imports: [
    CommonModule,
    PrimeNgModule
  ],
  exports: [
    DashboardComponent,
    NavbarComponent,
    SidebarComponent
  ]
})
export class DashModuleModule { }
