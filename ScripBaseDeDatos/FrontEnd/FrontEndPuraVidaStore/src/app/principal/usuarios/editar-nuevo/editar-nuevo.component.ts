import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PersonaModel } from 'src/app/models/persona-model';
import { RolModel } from 'src/app/models/rol-model';
import { UsuarioModel } from 'src/app/models/usuario-model';
import { PersonaServiceService } from 'src/app/services/persona-service.service';
import { RolServiceService } from 'src/app/services/rol-service.service';
import { UsuarioServiceService } from 'src/app/services/usuario-service.service';

@Component({
  selector: 'app-editar-nuevo',
  templateUrl: './editar-nuevo.component.html',
  styleUrls: ['./editar-nuevo.component.css']
})
export class EditarNuevoComponent implements OnInit {
  personaM: PersonaModel = {
    psrId: 0,
    psrIdentificacion: '',
    psrNombre: '',
    psrApellido1: '',
    psrApellido2: ''
  };
  
  rolM: RolModel = {
    rluID: 0,
    rluDescripcion: ''
  }

  usuarioEdtitar: UsuarioModel = {
    idUsuario: 0,
    password: '',
    usuario: '',
    email: '',
    idPersona: 0,
    idRol: 0,
    persona: this.personaM,
    Rol: this.rolM

  };
  esAgregar: boolean = false;
  listaPersonas: PersonaModel[] = [];

  listaRoles: RolModel[] = [];
  titulo: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private servicio: UsuarioServiceService,
    private servicioRol: RolServiceService,
    private servicioPersona: PersonaServiceService,
  ) { }

  ngOnInit(): void {
    const parametroId = this.route.snapshot.paramMap.get('id');
    this.validacion(parametroId);
    this.gestionInicio(parametroId);

  }


  validacion(parameto: any) {
    if (parameto > 0) {
      this.servicio.usuarioPorId(parameto).subscribe((x => {
        this.usuarioEdtitar = x
        this.personaM = x.persona
      }),
        (_error => console.log(_error)));
    }
  }

  gestionInicio(parametro: any) {

    this.servicioRol.listaRoles().subscribe((x => {
      this.listaRoles = x;

    }), (_error => console.log(_error)
    ));

    if (parametro != '0') {
      this.titulo = 'Editar usuario'
      this.esAgregar = false;
    } else {
      this.titulo = 'Agregar usuario';
      this.esAgregar = true;
    }
  }





}
