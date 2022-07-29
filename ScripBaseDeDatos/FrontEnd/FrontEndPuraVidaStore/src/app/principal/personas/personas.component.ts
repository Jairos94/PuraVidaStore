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
  personaForm = new FormGroup({
    identificacion: new FormControl(this.persona?.psrIdentificacion, [Validators.required]),
    nombre: new FormControl(this.persona?.psrNombre, [Validators.required]),
    apellido1: new FormControl(this.persona?.psrApellido1, [Validators.required]),
    apellido2: new FormControl(this.persona?.psrApellido2, [Validators.required]),
  });

  constructor(private servicio: PersonaServiceService) { }

  ngOnInit(): void {
  }
  search(event?: any) {

    this.servicio.listaPersonasCedula(event.query).subscribe((
      x => {
        this.listaPersonas = x
        if (this.listaPersonas.length === 1) {
          this.persona = this.listaPersonas[0];
          activo.personaInteractiva=this.persona;
          this.cambios();
        }
      }
    ),
      (_error => console.log(_error)));



  }
  cambios() {
    if (this.persona.psrId > 0) {
      console.log('entro al if');
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

}
