import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {

  constructor() { }
  items: MenuItem[]=[];
  ngOnInit(): void {
    this.items = [
      {
          label: 'Lista de usuarios',
          icon: 'pi pi-users',
          routerLink:'lista-usuarios',
      },
      {
          label: 'Agregar usuario',
          icon: 'pi pi-user-plus',
          routerLink:'editar-nuevo/0',
      },
  ];
  }

}
