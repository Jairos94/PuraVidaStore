import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HistorialMayoristaModel } from 'src/app/models/historial-mayorista-model';
import { MayoristaModel } from 'src/app/models/mayorista-model';
import { PersonaModel } from 'src/app/models/persona-model';
import { MayoristaService } from 'src/app/services/mayorista.service';
import { PersonaServiceService } from 'src/app/services/persona-service.service';

@Component({
  selector: 'app-agregar-cliente',
  templateUrl: './agregar-cliente.component.html',
  styleUrls: ['./agregar-cliente.component.css'],
})
export class AgregarClienteComponent implements OnInit {
  parametroId: any;
  persona: PersonaModel = {
    psrId: 0,
    psrIdentificacion: '',
    psrNombre: '',
    psrApellido1: '',
    psrApellido2: '',
  };

  cliente: MayoristaModel = {
    clmId: 0,
    clmIdPersona: 0,
    clmFechaCreacion: Date.now.toString(),
    clmFechaVencimiento: '',
    clmCorreo: null,
    clmTelefono: null,
    cantidadTiempo: 0,
    idTipoTiempo: 0,
    clmIdPersonaNavigation: this.persona,
    facturas:  null,
    historialClienteMayorista:  null,
  };
  historial: HistorialMayoristaModel[] = [];

  mayoritaForm = new FormGroup({
    cedula: new FormControl(this.persona.psrIdentificacion,[Validators.required]),
    nombre: new FormControl(this.persona.psrNombre,[Validators.required]),
    primerApellido: new FormControl(this.persona.psrApellido1,[Validators.required]),
    segundoApellido: new FormControl(this.persona.psrApellido2,[Validators.required]),
    correo: new FormControl(this.cliente.clmCorreo, [
      Validators.required,
      Validators.email,
    ]),
    telefono: new FormControl(this.cliente.clmTelefono,[Validators.required]),
    fechaVencimiento:new FormControl(this.cliente.clmFechaVencimiento)
  });

  constructor(
    private route: ActivatedRoute,
    private servicioMayorista: MayoristaService,
    private servicioPersona:PersonaServiceService
  ) {}

  ngOnInit(): void {
    this.parametroId = this.route.snapshot.paramMap.get('id');
    if (this.parametroId > 0) {
      this.consultarCliente();
    }
  }

  consultarCliente() {
    this.servicioMayorista
      .obtenerMayoristaPorCedula(this.parametroId)
      .subscribe({
        next: (x) => {
          this.cliente = x;
          if (this.cliente.clmIdPersonaNavigation) {
            this.persona = this.cliente.clmIdPersonaNavigation;
            this.mayoritaForm.patchValue({
              cedula: this.persona.psrIdentificacion,
              nombre: this.persona.psrNombre,
              primerApellido: this.persona.psrApellido1,
              segundoApellido: this.persona.psrApellido2,
            });
          }
          if (this.cliente.historialClienteMayorista) {
            this.historial = [];
            this.historial = this.cliente.historialClienteMayorista;
          }
          this.mayoritaForm.patchValue({
            correo: this.cliente.clmCorreo,
            telefono: this.cliente.clmTelefono,
            fechaVencimiento:this.cliente.clmFechaVencimiento
          });
        },
        error: (_e) => console.log(_e),
      });

  }

  guardar() {
    console.log('Se precion[o el bot[on ');
  }

  buscarCedula(){

      this.servicioPersona.buscarPersonaPorCedula(this.mayoritaForm.get('cedula')?.value!).subscribe({
        next:x=>
        {
        if(x.psrId>0){
          this.persona=x;
          this.mayoritaForm.patchValue({
            cedula: this.persona.psrIdentificacion,
            nombre: this.persona.psrNombre,
            primerApellido: this.persona.psrApellido1,
            segundoApellido: this.persona.psrApellido2,
          });
        }



        },
        error:_e=>{console.log(_e);
        }
      });
    }


}
