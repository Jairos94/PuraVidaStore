import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { activo } from 'src/app/activo';

@Component({
  selector: 'app-movimientos',
  templateUrl: './movimientos.component.html',
  styleUrls: ['./movimientos.component.css']
})
export class MovimientosComponent implements OnInit {

  constructor() { }
  items: MenuItem[] = [];

  ngOnInit(): void {

    this.items = [
      {
        label: 'Inventarios',
        icon: 'pi pi-pw pi-file',
        routerLink: 'inventarios'
      },
      {
        label: 'Ingresar productos',
        icon: 'pi pi-pw pi-file',
        routerLink: 'ingreso-productos',
        visible: activo.esAministrador()
      },
      {
        label: 'Ajustes',
        icon: 'pi pi-pw pi-file',
        routerLink: 'ajustes',
        visible: activo.esAministrador()
      },
    ];


  }

}
