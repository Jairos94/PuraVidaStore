import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-ventas',
  templateUrl: './ventas.component.html',
  styleUrls: ['./ventas.component.css'],
})
export class VentasComponent implements OnInit {
  constructor() {}
  items: MenuItem[] = [];
  ngOnInit(): void {
    this.items = [
      {
        label: 'Facturación',
        icon: 'pi pi-fw pi-file',
        routerLink: 'facturacion',
      },
    ];
  }
}
