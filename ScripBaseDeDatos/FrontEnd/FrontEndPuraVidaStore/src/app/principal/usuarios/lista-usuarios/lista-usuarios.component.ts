import { Component, OnInit } from '@angular/core';
import { UsuarioModel } from 'src/app/models/usuario-model';
import { UsuarioServiceService } from 'src/app/services/usuario-service.service';

//prime ng
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-lista-usuarios',
  templateUrl: './lista-usuarios.component.html',
  styleUrls: ['./lista-usuarios.component.css']
})
export class ListaUsuariosComponent implements OnInit {
  listaUsuario: UsuarioModel[] = [];

  constructor(private servicio: UsuarioServiceService,
    private primengConfig: PrimeNGConfig) { }

  ngOnInit(): void {
    this.llenarUsuarios();
    this.primengConfig.ripple = true;
  }

  llenarUsuarios() {
    this.servicio.listaUsuarios().subscribe(
      (lista => {
        this.listaUsuario = lista;
        console.log(this.listaUsuario);
        
      }),
      (_e => { console.log(_e); }));

  }



}
