import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-bodegas',
  templateUrl: './bodegas.component.html',
  styleUrls: ['./bodegas.component.css']
})
export class BodegasComponent implements OnInit {

  constructor() { }
  items: MenuItem[]=[];
  ngOnInit(): void {
    this.items = [
      {
          label: 'Lista de Bodegas',
          icon: 'pi pi-home',
          routerLink:'lista-bodegas',
      },
      {
          label: 'Agregar Bodega',
          icon: 'pi pi-home',
          routerLink:'Edtitar-Agregar/0',
      },
  ];
  }

}
