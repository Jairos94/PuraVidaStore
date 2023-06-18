import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { activo } from 'src/app/activo';
import { ImpuestosModel } from 'src/app/models/impuestos-model';
import { ParametrosEmailModel } from 'src/app/models/parametros-email-model';
import { ParametrosGlobalesModel } from 'src/app/models/parametros-globales-model';
import { ParametrosImpuestoModel } from 'src/app/models/parametros-impuesto-model';
import { ImpuestoService } from 'src/app/services/impuesto.service';
import { ParametrosService } from 'src/app/services/parametros.service';

@Component({
  selector: 'app-configuracion',
  templateUrl: './configuracion.component.html',
  styleUrls: ['./configuracion.component.css'],
  providers: [MessageService]
})
export class ConfiguracionComponent implements OnInit {
  buscador: string = '';
  emial: ParametrosEmailModel = {
    preId: 0,
    preHost: '',
    prePuerto: 0,
    preUser: '',
    preClave: '',
    preSsl: false,
    preIdParametroGlobal: null,
    pre: null,
  };

  listaImpuestos: ParametrosImpuestoModel[] = [];
  ListaImpuestosConsultados: ImpuestosModel[] = [];
  ImpuestosSugerencia: ImpuestosModel[] = [];
  ImpuestosAgregar: ImpuestosModel[] = []; //Impuestos que se agregan a la tabla

  parametrosGlobales: ParametrosGlobalesModel = {
    prgId: 0,
    prgUndsHabilitarMayorista: 1,
    prgUndsAgregarMayorista: 1,
    prgHabilitarImpuestos: false,
    prgImpustosIncluidos: false,
    prgIdBodega: 0,
    parametrosEmail: this.emial,
    impuestosPorParametros: this.listaImpuestos,
    prgIdBodegaNavigation: null,
  };

  ParametrosForm = new FormGroup({
    unidadesHabilitarMayorista: new FormControl(
      this.parametrosGlobales.prgUndsHabilitarMayorista,
      [Validators.required]
    ),
    unidadesAgregarMayorista: new FormControl(
      this.parametrosGlobales.prgUndsAgregarMayorista,
      [Validators.required]
    ),
    habilitarImpuestos: new FormControl(
      this.parametrosGlobales.prgHabilitarImpuestos
    ),
    impuestosIncluidos: new FormControl(
      this.parametrosGlobales.prgImpustosIncluidos
    ),
    habilitarCorreo:new FormControl(true),
    host: new FormControl(this.emial.preHost),
    puerto: new FormControl(this.emial.prePuerto),
    usuario: new FormControl(this.emial.preUser),
    clave: new FormControl(this.emial.preClave),
    ssl: new FormControl(this.emial.preSsl),
  });

  constructor(
    private impuestoServicio: ImpuestoService,
    private parametrosServicio: ParametrosService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.CargarImpuestos();
    this.obtenerParametros();
  }

  CargarImpuestos() {
    this.impuestoServicio.listaImpuestos().subscribe({
      next: (x) => {
        this.ListaImpuestosConsultados = [];
        this.ListaImpuestosConsultados = x;
      },
      error: (_e) => {
        console.log(_e);
      },
    });
  }

  AgregarAlista() {
    let ingrado: Boolean = false;
    this.ImpuestosAgregar.forEach((x) => {
      if (x.impId === this.ImpuestosSugerencia[0].impId) {
        ingrado = true;
      }
    });
    if (this.buscador != '' && this.ImpuestosSugerencia.length === 1) {
      if (!ingrado) {
        this.ImpuestosAgregar.push(this.ImpuestosSugerencia[0]);
        this.ImpuestosSugerencia = [];
      }
      this.buscador = '';
    }
  }

  FiltrarImpuestos(evento: any) {
    //in a real application, make a request to a remote url with the query and return filtered results,
    let filtrado: any[] = [];
    let query = evento.query;
    console.log(evento);

    for (let i = 0; i < this.ListaImpuestosConsultados.length; i++) {
      let Impuesto = this.ListaImpuestosConsultados[i];
      if (
        Impuesto.impDescripcion.toLowerCase().indexOf(query.toLowerCase()) == 0
      ) {
        filtrado.push(Impuesto);
      }
    }

    this.ImpuestosSugerencia = filtrado;
  }

  obtenerParametros() {
    this.parametrosServicio
      .ObtenerPametros(activo.bodegaIngreso.bdgId)
      .subscribe({
        next: (x) => {
          if (x != null) {
            this.parametrosGlobales = x;
            this.CargarDatosAlForm(this.parametrosGlobales);
            this.ImpuestosAgregar = [];
            this.parametrosGlobales.impuestosPorParametros?.forEach(
              (impuesto) => {
                if (impuesto.impPidImpuestoNavigation != null) {
                  this.ImpuestosAgregar.push(impuesto.impPidImpuestoNavigation);
                }
              }
            );
            if (this.parametrosGlobales.parametrosEmail != null) {
              this.emial = this.parametrosGlobales.parametrosEmail;
            }
          }
        },
        error: (_e) => {
          console.log(_e);
        },
      });
  }

  guardar() {

    this.parametrosGlobales.prgUndsHabilitarMayorista=this.ParametrosForm.get('unidadesHabilitarMayorista')?.value!;
    this.parametrosGlobales.prgUndsAgregarMayorista=this.ParametrosForm.get('unidadesAgregarMayorista')?.value!;
    this.parametrosGlobales.prgHabilitarImpuestos=this.ParametrosForm.get('habilitarImpuestos')?.value!;
    this.parametrosGlobales.prgImpustosIncluidos=this.ParametrosForm.get('impuestosIncluidos')?.value!;
    this.emial.preIdParametroGlobal=this.parametrosGlobales.prgId;


    let contadorImpuestos: 0;
    if (this.ImpuestosAgregar.length > 0) {
      let contadorImpuesto: number = 0;
      this.ImpuestosAgregar.forEach((x) => {
        contadorImpuesto++;
        let nuevoImpuesto: ParametrosImpuestoModel = {
          impPid: contadorImpuesto,
          impPidParametroGlobal: this.parametrosGlobales.prgId,
          impPidImpuesto: x.impId,
          impPidImpuestoNavigation: null,
          impPidParametroGlobalNavigation: null,
        };
        this.parametrosGlobales.impuestosPorParametros?.push(nuevoImpuesto);
      });
    }
  }

  CargarDatosAlForm(datos: ParametrosGlobalesModel) {
    this.ParametrosForm.patchValue({
      unidadesHabilitarMayorista: datos.prgUndsHabilitarMayorista,
      unidadesAgregarMayorista: datos.prgUndsAgregarMayorista,
      habilitarImpuestos: datos.prgHabilitarImpuestos,
      impuestosIncluidos: datos.prgImpustosIncluidos,
      host: datos.parametrosEmail?.preHost,
      puerto: datos.parametrosEmail?.prePuerto,
      usuario: datos.parametrosEmail?.preUser,
      clave: datos.parametrosEmail?.preClave,
      ssl: datos.parametrosEmail?.preSsl,
    });
  }
}
