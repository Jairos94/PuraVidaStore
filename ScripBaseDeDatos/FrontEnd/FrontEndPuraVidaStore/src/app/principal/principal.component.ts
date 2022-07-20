import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api/menuitem';
import { activo } from '../activo';


@Component({
  selector: 'app-principal',
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})
export class PrincipalComponent implements OnInit {


  constructor() { }

  
  usuario: string = '';
  items: MenuItem[] = [];
  administrador: boolean = false;

  ngOnInit(): void {
this.usuario = activo.usuarioPrograma.usuario;
    if (activo.usuarioPrograma.idRol === 1) {
      this.administrador = true;
      this.items = [
        {
          label: 'ventas',
          icon: 'pi pi-money-bill',
          routerLink: "./ventas",
        },

        {
          label: 'Usuarios',
          icon: 'pi pi-users',
          routerLink: "./usuarios",
        },
      ];
    } else {
      this.administrador = false;
      this.items = [
        {
          label: 'Edit',
        }
      ];
    }
  }

}
