import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { activo } from 'src/app/activo';


@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css']
})
export class ProductosComponent implements OnInit {

  constructor() { }
  items: MenuItem[] = [];
  ngOnInit(): void {
    this.items = [
      {
        label: 'Productos',
        icon: 'pi pi-pw pi-file',
        routerLink: 'productos',
        items: [
          {
            label: 'Agregar',
            icon: 'pi pi-pw pi-file',
            routerLink: 'productos/agregar-editar/0',
            visible:activo.esAministrador()
          },
          {
            label: 'Lista de productos',
            icon: 'pi pi-pw pi-file',
            routerLink: 'productos/lista-productos',
          },
        ]
      },
      {

        label: 'Tipo de producto',
        icon: 'pi pi-pw pi-file',
        routerLink: 'tipo-producto',
      },
      {

        label: 'Consultar producto',
        icon: 'pi pi-pw pi-file',
        routerLink: 'consulta-producto',
      },

    ];
  }

}
