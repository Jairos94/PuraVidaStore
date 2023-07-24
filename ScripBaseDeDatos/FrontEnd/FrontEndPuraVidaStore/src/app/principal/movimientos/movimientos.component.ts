import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { activo } from 'src/app/activo';

@Component({
  selector: 'app-movimientos',
  templateUrl: './movimientos.component.html',
  styleUrls: ['./movimientos.component.css'],
})
export class MovimientosComponent implements OnInit {
  constructor() {}
  items: MenuItem[] = [];

  ngOnInit(): void {
    this.items = [
      {
        label: 'Inventarios',
        icon: 'pi pi-pw pi-verified',
        routerLink: 'inventarios',
      },
      {
        label: 'Ingresar productos',
        icon: 'pi pi-pw pi-plus',
        routerLink: 'ingreso-productos',
        visible: activo.esAministrador(),
      },
      {
        label: 'Ajustes',
        icon: 'pi pi-pw pi-cog',
        routerLink: 'ajustes',
        visible: activo.esAministrador(),
        items: [
          {
            label: 'Razón del ajuste',
            icon: 'pi pi-pw pi-comment',
            routerLink: 'ajustes/razon-ajuste',
          },
          {
            label: 'Realizar ajustes',
            icon: 'pi pi-pw pi-play',
            routerLink: 'ajustes/realizar-ajuste',
          },
        ],
      },
      {
        label: 'Traslados',
        icon: 'pi pi-pw pi-sort-alt',
        routerLink: 'traslados',
        visible: activo.esAministrador(),
      },
      {
        label: 'Reportes',
        icon: 'pi pi-pw pi-chart-line',
        routerLink: 'reportes'
      },
    ];
  }
}
