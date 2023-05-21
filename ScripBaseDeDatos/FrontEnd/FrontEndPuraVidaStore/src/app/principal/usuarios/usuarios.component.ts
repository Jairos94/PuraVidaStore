import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { activo } from 'src/app/activo';
import { UsuarioModel } from 'src/app/models/usuario-model';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {

  constructor(private router: Router ) { }
  items: MenuItem[]=[];
  usuarioActual:UsuarioModel=activo.usuarioPrograma;
  ngOnInit(): void {
    console.log(this.router.url);

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
