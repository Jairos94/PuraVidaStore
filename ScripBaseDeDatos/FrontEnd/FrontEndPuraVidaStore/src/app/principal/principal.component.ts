import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { activo } from '../activo';
import { PersonaModel } from '../models/persona-model';
import { UsuarioModel } from '../models/usuario-model';

@Component({
  selector: 'app-principal',
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css'],
})
export class PrincipalComponent implements OnInit {
  items: MenuItem[] = [];
  botonConfiguracion: MenuItem[] = [];
  NombreUsuario: PersonaModel = {
    psrId: 0,
    psrIdentificacion: '',
    psrNombre: '',
    psrApellido1: '',
    psrApellido2: '',
  };
  bodegaActiva = activo.bodegaIngreso;
  usuario: UsuarioModel = activo.usuarioPrograma;

  constructor() {}

  ngOnInit(): void {
    if (this.usuario.persona != null) {
      this.NombreUsuario = this.usuario.persona;
    }
    this.items = [
      {
        label: 'ventas',
        icon: 'pi pi-money-bill',
        routerLink: 'ventas',
      },
      {
        label: 'Prodctos',
        icon: 'pi pi-box',
        routerLink: 'productos',
      },
      {
        label: 'Movimientos',
        icon: 'pi pi-sort-amount-down',
        routerLink: 'movimientos',
      },

      {
        label: 'Usuarios',
        icon: 'pi pi-users',
        routerLink: 'usuarios',
        visible: activo.esAministrador(),
      },
      {
        label: 'Bodega',
        icon: 'pi pi-home',
        routerLink: 'bodegas',
        visible: activo.esAministrador(),
      },
      {
        label: 'Clientes',
        icon: 'pi pi-comments',
        routerLink: 'clientes',
      },
    ];

    this.botonConfiguracion = [
      {
        label: 'Configurar perfil',
        icon: 'pi pi-user-edit',
        routerLink:'editar-perfil/'+this.usuario.usrId
      },
      {
        label: 'Configurar opciones del sistema',
        icon: 'pi pi-desktop',
        routerLink:'configuracion',
        visible:activo.esAministrador()
      },
      {
        label: 'Salir',
        icon: 'pi pi-power-off',
        routerLink:'/'
      },
    ];
  }
}
