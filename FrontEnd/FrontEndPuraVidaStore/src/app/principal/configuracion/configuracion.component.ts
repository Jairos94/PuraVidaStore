import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { activo } from 'src/app/activo';
import { ImpuestosModel } from 'src/app/models/impuestos-model';
import { ParametrosEmailModel } from 'src/app/models/parametros-email-model';
import { ParametrosGlobalesModel } from 'src/app/models/parametros-globales-model';
import { ParametrosImpuestoModel } from 'src/app/models/parametros-impuesto-model';
import { TiempoParaRenovarModel } from 'src/app/models/tiempo-para-renovar-model';
import { ImpuestoService } from 'src/app/services/impuesto.service';
import { ParametrosService } from 'src/app/services/parametros.service';

@Component({
  selector: 'app-configuracion',
  templateUrl: './configuracion.component.html',
  styleUrls: ['./configuracion.component.css'],
  providers: [MessageService],
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
    preIdParametroGlobal: 0,
  };

  listaImpuestos: ParametrosImpuestoModel[] = [];
  ListaImpuestosConsultados: ImpuestosModel[] = [];
  ImpuestosSugerencia: ImpuestosModel[] = [];
  ImpuestosAgregar: ImpuestosModel[] = []; //Impuestos que se agregan a la tabla
  ListTiempos: TiempoParaRenovarModel[] = [];
  CorreoHbilitado: boolean = false;

  parametrosGlobales: ParametrosGlobalesModel = {
    prgId: 0,
    prgUndsHabilitarMayorista: 1,
    prgUndsAgregarMayorista: 1,
    prgHabilitarImpuestos: false,
    prgImpustosIncluidos: false,
    prgIdBodega: 0,
    prgIdTiempo: 0,
    prgCantidadTiempo: 0,
    prgImpresora: null,
    prgNombreNegocio: null,
    prgCedula:  null,
    parametrosEmail: this.emial,
    prgLeyenda:null,
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
    tiempoParaRenovar: new FormControl(
      this.parametrosGlobales.prgCantidadTiempo,
      [Validators.required]
    ),
    tiempoParaRenovarId: new FormControl(
      this.parametrosGlobales.prgIdTiempo,
      [Validators.required]
    ),
    habilitarImpuestos: new FormControl(
      this.parametrosGlobales.prgHabilitarImpuestos
    ),
    impuestosIncluidos: new FormControl(
      this.parametrosGlobales.prgImpustosIncluidos
    ),
    habilitarCorreo: new FormControl(true),
    host: new FormControl(this.emial.preHost),
    puerto: new FormControl(this.emial.prePuerto),
    usuario: new FormControl(this.emial.preUser),
    clave: new FormControl(this.emial.preClave),
    ssl: new FormControl(this.emial.preSsl),
    impresora: new FormControl<string | null>(null), // O asegúrate de que el tipo permita 'undefined' si es necesario
    leyenda: new FormControl<string | null>(null) // O asegúrate de que el tipo permita 'undefined' si es necesario

  });
  listaImpresoras:string[]=[];

  constructor(
    private impuestoServicio: ImpuestoService,
    private parametrosServicio: ParametrosService,
    private messageService: MessageService,
    private ruta: Router,
  ) {}

  ngOnInit(): void {
    this.CargarImpuestos();
    this.obtenerParametros();
    this.obtenerTiempoParaRenovar();
    this.CargarImpresoras();
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

  CargarImpresoras(){
    this.parametrosServicio.ObtenerImpresoras().subscribe(
      {
        next: (x) => {
          this.listaImpresoras = [];
          this.listaImpresoras = x;
        },
        error: (_e) => {
          console.log(_e);
        },
      })
  }

  obtenerTiempoParaRenovar() {
    this.parametrosServicio.ObtenerListaParaRenovar().subscribe({
      next: (x) => {
        this.ListTiempos = x;
      },
      error: (_e) => console.log(_e),
    });
  }

  FiltrarImpuestos(evento: any) {
    //in a real application, make a request to a remote url with the query and return filtered results,
    let filtrado: any[] = [];
    let query = evento.query;

    for (let i = 0; i < this.ListaImpuestosConsultados.length; i++) {
      let Impuesto = this.ListaImpuestosConsultados[i];
      if (
        Impuesto.impDescripcion.toLowerCase().indexOf(query.toLowerCase()) == 0
      ) {
        filtrado.push(Impuesto);
      }
    }
    this.ImpuestosSugerencia = filtrado;

    if (filtrado.length == 1) {
      let DatoExiste = false;
      this.ImpuestosAgregar.forEach((x) => {
        if (x.impId == filtrado[0].impId) {
          DatoExiste = true;
        }
      });
      if (!DatoExiste) {
        this.ImpuestosAgregar.push(filtrado[0]);
      }
    }
  }

  obtenerParametros() {
    this.parametrosServicio
      .ObtenerPametros(activo.bodegaIngreso.bdgId)
      .subscribe({
        next: (x) => {
          if (x != null) {
            this.parametrosGlobales = x;
            this.CargarDatosAlForm(this.parametrosGlobales);
            if (this.parametrosGlobales.parametrosEmail != null) {
              this.cargarDatosEmail(this.parametrosGlobales.parametrosEmail);
            }
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

    let contadorImpuestos: number = 0;
    let huboError: boolean = false;

    this.parametrosGlobales.prgIdBodega = activo.bodegaIngreso.bdgId;
    this.parametrosGlobales.prgUndsHabilitarMayorista = this.ParametrosForm.get(
      'unidadesHabilitarMayorista'
    )?.value!;
    this.parametrosGlobales.prgUndsAgregarMayorista = this.ParametrosForm.get(
      'unidadesAgregarMayorista'
    )?.value!;
    this.parametrosGlobales.prgHabilitarImpuestos =
      this.ParametrosForm.get('habilitarImpuestos')?.value!;
    this.parametrosGlobales.prgImpustosIncluidos =
      this.ParametrosForm.get('impuestosIncluidos')?.value!;
      this.parametrosGlobales.prgCantidadTiempo =
      this.ParametrosForm.get('tiempoParaRenovar')?.value!;
      this.parametrosGlobales.prgIdTiempo =
      this.ParametrosForm.get('tiempoParaRenovarId')?.value!;
      this.parametrosGlobales.prgImpresora = this.ParametrosForm.get('impresora')?.value!;
      this.parametrosGlobales.prgLeyenda = this.ParametrosForm.get('leyenda')?.value!;

    if (this.CorreoHbilitado) {
      this.emial.preHost = this.ParametrosForm.get('host')?.value!;
      this.emial.prePuerto = this.ParametrosForm.get('puerto')?.value!;
      this.emial.preUser = this.ParametrosForm.get('usuario')?.value!;
      this.emial.preClave = this.ParametrosForm.get('clave')?.value!;
      this.emial.preSsl = this.ParametrosForm.get('ssl')?.value!;
      this.emial.preIdParametroGlobal = this.parametrosGlobales.prgId;

      if (this.emial.preHost == null || this.emial.preHost == '') {
        huboError = this.error();
      }
      if (this.emial.prePuerto == null || this.emial.prePuerto == 0) {
        huboError = this.error();
      }
      if (this.emial.preUser == null || this.emial.preUser == '') {
        huboError = this.error();
      }
      if (this.emial.preClave == null || this.emial.preClave == '') {
        huboError = this.error();
      }

      if (!huboError) {
        this.parametrosGlobales.parametrosEmail = this.emial;
      }
    } else {
      this.parametrosGlobales.parametrosEmail = null;
    }

    if (this.ImpuestosAgregar.length > 0) {
      let listaImpuestos: ParametrosImpuestoModel[] = [];
      let idsAgregados = new Set<number>(); // Usar un Set para rastrear IDs únicos
    
      this.ImpuestosAgregar.forEach((x) => {
        if (!idsAgregados.has(x.impId)) { // Verificar si el ID ya ha sido agregado
          let nuevoImpuesto: ParametrosImpuestoModel = {
            impPid: 0,
            impPidParametroGlobal: this.parametrosGlobales.prgId,
            impPidImpuesto: x.impId,
            impPidImpuestoNavigation: null,
            impPidParametroGlobalNavigation: null,
          };
          listaImpuestos.push(nuevoImpuesto);
          idsAgregados.add(x.impId); // Agregar el ID al Set
        }
      });
    
      this.parametrosGlobales.impuestosPorParametros = listaImpuestos; // Asignar la lista sin duplicados
    }
    else {
      this.parametrosGlobales.impuestosPorParametros = null;
    }

    if (!huboError) {
      this.parametrosServicio
        .GuardarParametros(this.parametrosGlobales)
        .subscribe({
          next: (x) => {
            this.CargarDatosAlForm(x);
            activo.parametrosGlobales=x;
            this.ruta.navigate(['./principal'])
          },
          error: (_e) => {
            console.log(_e);
          },
        });
    } else {
      this.error();
    }
  }

  CargarDatosAlForm(datos: ParametrosGlobalesModel) {
    this.ParametrosForm.patchValue({
      unidadesHabilitarMayorista: datos.prgUndsHabilitarMayorista,
      unidadesAgregarMayorista: datos.prgUndsAgregarMayorista,
      habilitarImpuestos: datos.prgHabilitarImpuestos,
      impuestosIncluidos: datos.prgImpustosIncluidos,
      tiempoParaRenovar: datos.prgCantidadTiempo,
      tiempoParaRenovarId: datos.prgIdTiempo,
      impresora: datos.prgImpresora ?? null, // Asigna null si el valor es undefined
      leyenda: datos.prgLeyenda ?? null, // Asigna null si el valor es undefined
    });
  }
  

  cargarDatosEmail(datos: ParametrosEmailModel) {
    this.ParametrosForm.patchValue({
      host: datos.preHost,
      puerto: datos.prePuerto,
      usuario: datos.preUser,
      clave: datos.preClave,
      ssl: datos.preSsl,
    });
  }

  AsignarCorreo() {
    this.CorreoHbilitado = this.ParametrosForm.get('habilitarCorreo')?.value!;
  }

  error(): boolean {
    this.messageService.add({
      severity: 'error',
      summary: 'No se puede guardar',
      detail: 'Se requiere ingresar todos los datos',
    });
    return true;
  }

  eliminarLista(impuesto: ImpuestosModel) {
    const index = this.ImpuestosAgregar.indexOf(impuesto, 0);
    if (index > -1) {
      this.ImpuestosAgregar.splice(index, 1);
    }
    this.ImpuestosAgregar.sort();
  }
}
