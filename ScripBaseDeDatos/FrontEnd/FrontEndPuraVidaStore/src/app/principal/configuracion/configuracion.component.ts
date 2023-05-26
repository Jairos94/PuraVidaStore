
import { Component, OnInit } from '@angular/core';
import { ImpuestosModel } from 'src/app/models/impuestos-model';
import { ParametrosEmailModel } from 'src/app/models/parametros-email-model';
import { ParametrosGlobalesModel } from 'src/app/models/parametros-globales-model';
import { ParametrosImpuestoModel } from 'src/app/models/parametros-impuesto-model';

@Component({
  selector: 'app-configuracion',
  templateUrl: './configuracion.component.html',
  styleUrls: ['./configuracion.component.css']
})
export class ConfiguracionComponent implements OnInit{

  emial:ParametrosEmailModel={
    preId: 0,
    preHost: '',
    prePuerto: 0,
    preUser: '',
    preClave: '',
    preSsl: false,
    preIdParametroGlobal: null,
    pre: null
  };

  listaImpuestos:ParametrosImpuestoModel[]=[];

  parametrosGlobales:ParametrosGlobalesModel={
    prgId: 0,
    prgUndsHabilitarMayorista: 0,
    prgUndsAgregarMayorista: 0,
    prgHabilitarImpuestos: false,
    prgImpustosIncluidos: false,
    parametrosEmail: this.emial,
    impuestosPorParametros: this.listaImpuestos
  }

constructor() {}

  ngOnInit(): void {
  }

}
