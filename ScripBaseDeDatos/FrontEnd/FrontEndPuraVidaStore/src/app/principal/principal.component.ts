import { Component, OnInit } from '@angular/core';
import { MegaMenuItem} from 'primeng/api';
import { activo } from '../activo';


@Component({
  selector: 'app-principal',
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})
export class PrincipalComponent implements OnInit {
  

  constructor() { }
  usuario: string = '';
  items: MegaMenuItem[] = [];

  ngOnInit(): void {
    this.usuario=activo.usuarioPrograma.usuario;
    this.items = [
      {
          label: 'Usuarios', icon: 'pi pi-fw pi-users',
          items: [
          ]
      },
  ]
  }

}
