import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import {  timer } from 'rxjs';
import { activo } from 'src/app/activo';
import { PersonaModel } from 'src/app/models/persona-model';
import { RolModel } from 'src/app/models/rol-model';
import { UsuarioModel } from 'src/app/models/usuario-model';
import { PersonaServiceService } from 'src/app/services/persona-service.service';
import { RolServiceService } from 'src/app/services/rol-service.service';
import { UsuarioServiceService } from 'src/app/services/usuario-service.service';
import { EncripDesencrip } from 'src/app/utils/EncripDesencrip';

@Component({
  selector: 'app-editar-nuevo',
  templateUrl: './editar-nuevo.component.html',
  styleUrls: ['./editar-nuevo.component.css'],
  providers: [MessageService]
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
    rluId: 0,
    rluDescripcion: ''
  }

  usuarioEdtitar: UsuarioModel = {
    usrId: 0,
    clave: '',
    usrUser: '',
    usrEmail: '',
    usrIdPersona: 0,
    usrIdRol: 0,
    persona: this.personaM,
    rol: this.rolM,
    usrActivo: true
  };


  usuarioForm = new FormGroup({
    cedula: new FormControl('', [Validators.required]),
    nombre: new FormControl('', [Validators.required]),
    apellido1: new FormControl('', [Validators.required]),
    apellido2: new FormControl('', [Validators.required]),
    usuario: new FormControl('', [Validators.required]),
    clave: new FormControl('', [Validators.required]),
    correo: new FormControl('', [Validators.email, Validators.required]),
    rol: new FormControl(2),

  });


  transacionExitosa: boolean = false;
  esAgregar: boolean = false;
  listaPersonas: PersonaModel[] = [];

  listaRoles: RolModel[] = [];
  titulo: string = '';

  constructor(
    private route: ActivatedRoute,
    private ruta: Router,
    private servicio: UsuarioServiceService,
    private servicioRol: RolServiceService,
    private servicioPersona: PersonaServiceService,
    private messageService: MessageService,
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

        if(usuario.persona!=null){
          this.personaM = usuario.persona
        }

        if(usuario.rol!=null){
          this.rolM = usuario.rol
        }
        
        
        this.usuarioForm.patchValue({
          cedula: this.personaM.psrIdentificacion,
          nombre: this.personaM.psrNombre,
          apellido1: this.personaM.psrApellido1,
          apellido2: this.personaM.psrApellido2,
          usuario: this.usuarioEdtitar.usrUser,
          correo: this.usuarioEdtitar.usrEmail,
          rol: this.usuarioEdtitar.usrIdRol,
        })

        var password = EncripDesencrip.decryptUsingAES256(this.usuarioEdtitar.clave).replace('"', '').replace('"', '')


        this.usuarioForm.patchValue({ clave: password })
      }
      ),
        (_error => console.log(_error)));

    } else {
      this.usuarioEdtitar.usrId = 0;
      this.personaM.psrId = 0;
      this.rolM = this.listaRoles[1]
    }


    if (parametro != '0') {
      this.titulo = 'Editar usuario '
      this.esAgregar = false;
    } else {
      this.titulo = 'Agregar usuario';
      this.esAgregar = true;
    }

    this.cambioRol();
  }

  cargarListaRoles() {
    this.servicioRol.listaRoles().subscribe((x => {
      this.listaRoles = x
    }), (_error => console.log(_error)
    ));
  }

  guardar() {
    this.personaM.psrIdentificacion = this.usuarioForm.get('cedula')?.value!;
    this.personaM.psrNombre = this.usuarioForm.get('nombre')?.value!;
    this.personaM.psrApellido1 = this.usuarioForm.get('apellido1')?.value!;
    this.personaM.psrApellido2 = this.usuarioForm.get('apellido2')?.value!;

    this.usuarioEdtitar.usrUser = this.usuarioForm.get('usuario')?.value!;
    this.usuarioEdtitar.clave = EncripDesencrip.encryptUsingAES256(this.usuarioForm.get('clave')?.value!);
    this.usuarioEdtitar.usrEmail = this.usuarioForm.get('correo')?.value!;
    this.usuarioEdtitar.usrIdRol = this.usuarioForm.get('rol')?.value!;

    this.usuarioEdtitar.usrIdPersona = this.personaM.psrId
    this.usuarioEdtitar.persona = this.personaM;


    this.servicio.GuardarUsuario(this.usuarioEdtitar, this.esAgregar).subscribe((x => {
      this.transacionExitosa = true;
      this.showOk('Se guardó con exitó al usuario', x.usrUser);
      timer(3000).subscribe(n => {
        this.irUsuarios()
      });
      
    }), (_e => {
      this.showError('Se presentó un error', _e.error)
      this.transacionExitosa = false
    }));
   
    //this.irUsuarios()

  }


  irUsuarios() {
    timer(0,0).subscribe()
    //./usuarios/lista-usuarios
    this.ruta.navigate(['./principal/usuarios'])
  }
  cambioRol() {

    const email = this.usuarioForm.get('correo')
    const id = this.usuarioForm.get('rol')?.value;
    const req = this.usuarioForm.controls.correo.hasValidator(Validators.required)
    if (id === 1) {
      if (!req) {
        this.usuarioForm.controls.correo.addValidators(Validators.required)
        this.usuarioForm.controls.correo.enable
      }
    }
    else {
      if (req) {
        this.usuarioForm.controls.correo.removeValidators(Validators.required);
      }
      this.usuarioForm.controls.correo.disable
    }
    this.usuarioForm.controls.correo
  }

  showOk(encabezado: string, mensaje: string) {
    this.messageService.add({ severity: 'success', summary: encabezado, detail: mensaje });
  }

  showError(encabezado: string, mensaje: string) {
    this.messageService.add({ severity: 'error', summary: encabezado, detail: mensaje });
  }

}
