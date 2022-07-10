import { Component, OnInit } from '@angular/core';
import { MegaMenuItem, MenuItem} from 'primeng/api';
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

  ngOnInit(): void {
    this.usuario=activo.usuarioPrograma.usuario;
    if(activo.usuarioPrograma.idRol===1)
    {
      this.items = [
        {label: 'Punto de venta', icon: 'pi pi-fw pi-wallet'},
        {label: 'Usuarios', icon: 'pi pi-fw pi-user',routerLink: './usuarios'},
        {label: 'Edit', icon: 'pi pi-fw pi-pencil'},
        {label: 'Documentation', icon: 'pi pi-fw pi-file'},
        {label: 'Settings', icon: 'pi pi-fw pi-cog'}
    ];
    }else{
      this.items = [
        {label: 'Calendar', icon: 'pi pi-fw pi-calendar'},
        {label: 'Edit', icon: 'pi pi-fw pi-pencil'},
        {label: 'Documentation', icon: 'pi pi-fw pi-file'},
        {label: 'Settings', icon: 'pi pi-fw pi-cog'}
    ];
    }
    
  }

}
