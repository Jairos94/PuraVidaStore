import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { activo } from 'src/app/activo';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent {
  items: MenuItem[] = [];

  ngOnInit(): void {
    this.items = [
      {
        label: 'Agregar Cliente Mayorista',
        icon: 'pi pi-fw pi-file',
        routerLink: 'editar-nuevo/0',
        visible:activo.esAministrador()
      },
      {
        label: 'Lista de clientes Mayoristas',
        icon: 'pi pi-fw pi-file',
        routerLink: 'mayorista',
      }

    ];
  }
}
