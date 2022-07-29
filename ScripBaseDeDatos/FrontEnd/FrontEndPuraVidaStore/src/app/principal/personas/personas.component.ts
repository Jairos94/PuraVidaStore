import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { activo } from 'src/app/activo';
import { PersonaModel } from 'src/app/models/persona-model';
import { PersonaServiceService } from 'src/app/services/persona-service.service';

@Component({
  selector: 'app-personas',
  templateUrl: './personas.component.html',
  styleUrls: ['./personas.component.css']
})
export class PersonasComponent implements OnInit {
  persona!: PersonaModel;
  listaPersonas: PersonaModel[] = [];
  cedula: string = '';

  //! Validación para los formularios
  personaForm = new FormGroup({
    identificacion: new FormControl(this.persona?.psrIdentificacion, [Validators.required]),
    nombre: new FormControl(this.persona?.psrNombre, [Validators.required]),
    apellido1: new FormControl(this.persona?.psrApellido1, [Validators.required]),
    apellido2: new FormControl(this.persona?.psrApellido2, [Validators.required]),
  });

  constructor(
    private servicio: PersonaServiceService, //todo servicio de persona
  ) { }

  ngOnInit(): void {
    this.validacion()// todo valida si es persona para editar
  }

  //? Metodo para cuando se ingresa en la pantalla y aparezcan las sugerencias
  search(event?: any) {

    this.servicio.listaPersonasCedula(event.query).subscribe((
      x => {
        this.listaPersonas = x
        if (this.listaPersonas.length === 1) {
          this.persona = this.listaPersonas[0];
          activo.personaInteractiva = this.persona;
          this.cambios();
        }else{
          this.personaForm.patchValue({
           
            nombre: '',
            apellido1: '',
            apellido2: ''
          });
        }
      }
    ),
      (_error => console.log(_error)));

   

  }

  //? Metodo para cargar los datos a la pantalla
  cambios() {
    if (this.persona.psrId > 0) {
      activo.personaInteractiva = this.persona;
      this.persona = this.listaPersonas[0];
      this.personaForm.setValue({
        identificacion: this.persona.psrIdentificacion,
        nombre: this.persona.psrNombre,
        apellido1: this.persona.psrApellido1,
        apellido2: this.persona.psrApellido2
      });
    }

  }

  validacion() {
    if (activo.ConsultaIdPersona > 0) {
        this.servicio.buscarPersonaId(activo.ConsultaIdPersona).subscribe((x => {
        this.persona = x;
      }), (_error => (console.log(_error))
      ))
    }
    
    this.cambios();
  }
}
