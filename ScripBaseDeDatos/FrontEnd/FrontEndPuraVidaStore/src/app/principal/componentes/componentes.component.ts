import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api/menuitem';
import { activo } from 'src/app/activo';

@Component({
  selector: 'app-componentes',
  templateUrl: './componentes.component.html',
  styleUrls: ['./componentes.component.css']
})
export class ComponentesComponent implements OnInit {

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
