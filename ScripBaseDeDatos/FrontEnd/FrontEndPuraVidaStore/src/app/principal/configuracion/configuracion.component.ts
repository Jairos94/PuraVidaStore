import { Component, OnInit } from '@angular/core';
import { ImpuestosModel } from 'src/app/models/impuestos-model';
import { ParametrosEmailModel } from 'src/app/models/parametros-email-model';
import { ParametrosGlobalesModel } from 'src/app/models/parametros-globales-model';
import { ParametrosImpuestoModel } from 'src/app/models/parametros-impuesto-model';
import { ImpuestoService } from 'src/app/services/impuesto.service';

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

  constructor(private impuestoServicio: ImpuestoService) {}

  ngOnInit(): void {
    this.CargarImpuestos();
  }

  CargarImpuestos() {
    this.impuestoServicio.listaImpuestos().subscribe({
      next: (x) => {
        this.ListaImpuestosConsultados = [];
        this.ListaImpuestosConsultados = x;
      },
      error: (_e) => {},
    });
  }

  AgregarAlista() {
    let ingrado: Boolean = false;
   this.ImpuestosAgregar.forEach(x=>{
    if(x.impId===this.ImpuestosSugerencia[0].impId){
      ingrado=true;
    }
   });
    if (this.buscador != '' && this.ImpuestosSugerencia.length === 1) {

      if (!ingrado) {
        this.ImpuestosAgregar.push(this.ImpuestosSugerencia[0]);
        this.ImpuestosSugerencia = [];
      }
      this.buscador='';
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
}
