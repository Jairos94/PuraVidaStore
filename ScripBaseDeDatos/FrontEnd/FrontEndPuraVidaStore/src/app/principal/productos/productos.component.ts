import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';


@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css']
})
export class ProductosComponent implements OnInit {

  constructor() { }
  items: MenuItem[]=[];
  ngOnInit(): void {
    this.items = [
      {
          label: 'Tipo de producto',
          icon: 'pi pi-pw pi-file',
          routerLink: 'tipo-producto',
      },
  ];
  }

}
