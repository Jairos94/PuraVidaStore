import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { activo } from '../activo';
import { UsuarioModel } from '../models/usuario-model';

@Component({
  selector: 'app-principal',
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})
export class PrincipalComponent implements OnInit {
  items: MenuItem[] = [];
  usuario:UsuarioModel=activo.usuarioPrograma;
  constructor() {
  }

  ngOnInit(): void {
    this.items = [
      {
        label: 'ventas',
        icon: 'pi pi-money-bill',
        routerLink: 'ventas'
      },
      {
        label: 'Prodctos',
        icon: 'pi pi-box',
        routerLink: 'productos'
      },
      {
        label: 'Movimientos',
        icon: 'pi pi-sort-amount-down',
        routerLink: 'movimientos'
      },

      {
        label: 'Usuarios',
        icon: 'pi pi-users',
        routerLink: 'usuarios',
        visible: activo.esAministrador(),
        items: [
          {
            label: 'Agregar usuario',
            icon: 'pi pi-user-plus',
            routerLink:'usuarios/editar-nuevo/0'
          },
      ]
      }
    ];
  }

}
