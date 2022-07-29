import { Component, Input, OnInit } from '@angular/core';
import { activo } from 'src/app/activo';
import { PersonaModel } from 'src/app/models/persona-model';
import { PersonaServiceService } from 'src/app/services/persona-service.service';

@Component({
  selector: 'app-personas',
  templateUrl: './personas.component.html',
  styleUrls: ['./personas.component.css']
})
export class PersonasComponent implements OnInit {
  //@Input()  cedula: string = '';; // decorate the property with @Input()
 
  @Input()  persona!: PersonaModel;
  listaPersonas: PersonaModel[] = [];

  constructor(
    private servicio: PersonaServiceService, //todo servicio de persona
  ) { }

  ngOnInit(): void {
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
    }

  }

   validacion(){
  
  this.persona=activo.personaInteractiva
   
  }

  public guardarUsuario(){

    if(this.persona.psrId > 0){

    }
    else{}
  }
}
