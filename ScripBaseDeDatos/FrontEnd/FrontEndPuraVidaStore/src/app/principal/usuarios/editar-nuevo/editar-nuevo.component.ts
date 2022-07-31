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

  usuarioEdtitar!: UsuarioModel;
  esAgregar: boolean = false;
  personaM!: PersonaModel;
  listaPersonas: PersonaModel[] = [];

  //#region variables de persona nueva
  identificacion: string = '';
  nombre: string = '';
  primerApellido: string = '';
  segundoApellido: string = '';
  //#endregion

  listaRoles: RolModel[] = [];
  titulo: string = '';
  busqueda = '';

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
        this.cargarDatos(x)
      }),
        (_error => console.log(_error)));


    }
  }

  cargarDatos(usuario: UsuarioModel) {
    this.usuarioEdtitar = usuario
    this.personaM = usuario.persona
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



  limpiar() {
    //this.personaM.psrIdentificacion = '';
    this.personaM.psrNombre = '';
    this.personaM.psrApellido1 = '';
    this.personaM.psrApellido2 = '';


    this.identificacion = ""
    this.nombre = ""
    this.primerApellido = ""
    this.segundoApellido = ""
  }

}
