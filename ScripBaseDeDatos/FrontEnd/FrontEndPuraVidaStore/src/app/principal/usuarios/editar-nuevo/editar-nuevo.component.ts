import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { PersonaModel } from 'src/app/models/persona-model';
import { RolModel } from 'src/app/models/rol-model';
import { UsuarioModel } from 'src/app/models/usuario-model';
import { PersonaServiceService } from 'src/app/services/persona-service.service';
import { RolServiceService } from 'src/app/services/rol-service.service';
import { UsuarioServiceService } from 'src/app/services/usuario-service.service';
import { validaciones } from 'src/app/utils/validaciones';

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


  usuarioForm = new FormGroup({
    cedula: new FormControl('', [Validators.required]),
    nombre: new FormControl('', [Validators.required]),
    apellido1: new FormControl('', [Validators.required]),
    apellido2: new FormControl('', [Validators.required]),
    usuario: new FormControl('', [Validators.required]),
    clave: new FormControl('', [Validators.required]),
    correo: new FormControl('', [Validators.required,
                                 Validators.email,
    
    ]),
    rol: new FormControl(2),

  });



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

  }


  async validacion(parametro: any) {
    this.cargarListaRoles();
    if (parametro > 0) {
      await this.servicio.usuarioPorId(parametro).pipe().subscribe((usuario => {
        this.usuarioEdtitar = usuario
        this.personaM = usuario.persona
        this.rolM = usuario.Rol
        this.usuarioForm.patchValue({
          cedula: this.personaM.psrIdentificacion,
          nombre: this.personaM.psrNombre,
          apellido1: this.personaM.psrApellido1,
          apellido2: this.personaM.psrApellido2,
          usuario: this.usuarioEdtitar.usuario,
          correo: this.usuarioEdtitar.email,
          rol: this.usuarioEdtitar.idRol,
        })


      }),
        (_error => console.log(_error)));
    } else {
      this.rolM = this.listaRoles[1]
    }
    console.log(this.listaRoles);

    if (parametro != '0') {
      this.titulo = 'Editar usuario '
      this.esAgregar = false;
    } else {
      this.titulo = 'Agregar usuario';
      this.esAgregar = true;
    }
  }

  cargarListaRoles() {
    this.servicioRol.listaRoles().subscribe((x => {
      this.listaRoles = x
    }), (_error => console.log(_error)
    ));
  }

  guardar() {
    console.log(this.usuarioForm);


  }

  cambioRol() {
    const valor = this.usuarioForm.get('rol')?.value;
    console.log(valor);
    
    if(valor!=undefined){
      this.usuarioEdtitar.idRol=valor !;
    }
    this.usuarioForm.hasValidator
      console.log(this.usuarioForm.errors);
    
    
}

}
