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
        label:'Productos',
        icon: 'pi pi-pw pi-file',
      },
      {
        
          label: 'Tipo de producto',
          icon: 'pi pi-pw pi-file',
          routerLink: 'tipo-producto',
          items:[
            {
              label: 'Lista de tipo productos',
              icon: 'pi pi-pw pi-file',
              routerLink: 'tipo-producto/lista-tipo-producto',
            },
            {
              label: 'Agregar tipo producto',
              icon: 'pi pi-pw pi-file',
            },
          ]
      },
      
  ];
  }

}
