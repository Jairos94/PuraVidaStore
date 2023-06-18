import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
    prgUndsHabilitarMayorista: 0,
    prgUndsAgregarMayorista: 0,
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
    ImpuestosIncluidos: new FormControl(
      this.parametrosGlobales.prgImpustosIncluidos
    ),
    host: new FormControl(this.emial.preHost ),
    puerto: new FormControl(this.emial.prePuerto),
    usuario: new FormControl(this.emial.preUser ),
    clave: new FormControl(this.emial.preClave ),
    ssl: new FormControl(this.emial.preSsl),
  });

  constructor(
    private impuestoServicio: ImpuestoService,
    private parametrosServicio: ParametrosService
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
    console.log('Entro al boton');

    if (this.ImpuestosAgregar.length > 0) {
      let contadorImpuesto: number = 0;
      this.ImpuestosAgregar.forEach((x) => {
        let nuevoImpuesto: ParametrosImpuestoModel = {
          impPid: 0,
          impPidParametroGlobal: this.parametrosGlobales.prgId,
          impPidImpuesto: x.impId,
          impPidImpuestoNavigation: null,
          impPidParametroGlobalNavigation: null,
        };
        this.parametrosGlobales.impuestosPorParametros?.push(nuevoImpuesto);
      });
    }
  }

  CargarDatosAlForm(datos:ParametrosGlobalesModel) {
this.ParametrosForm.patchValue();
  }
}
