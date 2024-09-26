import { activo } from 'src/app/activo';
import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { ImpuestosModel } from 'src/app/models/impuestos-model';
import { ImpuestoService } from 'src/app/services/impuesto.service';

@Component({
  selector: 'app-impuestos',
  templateUrl: './impuestos.component.html',
  styleUrls: ['./impuestos.component.css'],
  providers: [MessageService],
})
export class ImpuestosComponent implements OnInit {
  listaImpuestos: ImpuestosModel[] = [];
  displayModal: boolean = false;
  deshabilitarBotonGuardar: boolean = true;
  esEliminar: boolean = false;
  buscador:string='';
  titulo: string = '';
  esAdministrador:boolean=activo.esAministrador();
  impuestoSelecionado: ImpuestosModel = {
    impId: 0,
    impDescripcion: '',
    impPorcentaje: 0,
    impActivo: true,
  };

  constructor(
    private messageService: MessageService,
    private servicioImpuesto: ImpuestoService
  ) {}

  ngOnInit(): void {
    this.obtenerLista();
  }

  obtenerLista() {
    this.servicioImpuesto.listaImpuestos().subscribe({
      next: (x) => {
        this.listaImpuestos = [];
        this.listaImpuestos = x;
      },
      error: (_e) => {
        console.log(_e);
      },
    });
  }

  limpiar() {
    this.impuestoSelecionado = {
      impId: 0,
      impDescripcion: '',
      impPorcentaje: 0,
      impActivo: true,
    };
    this.esEliminar = false;
  }

  Guardar() {
    this.servicioImpuesto.guardarImpuesto(this.impuestoSelecionado).subscribe({
      next: (x) => {
        this.limpiar();
        this.obtenerLista();

      },
      error: (_e) => {
        console.log(_e);
      },
    });

    if (this.esEliminar) {
      this.showError();
    } else {
      this.showSuccess();
    }
    this.limpiar();
    this.displayModal = false;
  }

  cambioDeshabilitar() {
    if (
      this.impuestoSelecionado.impDescripcion != '' &&
      this.impuestoSelecionado.impPorcentaje > 0
    ) {
      this.deshabilitarBotonGuardar = false;
    } else {
      this.deshabilitarBotonGuardar = true;
    }
  }

  editarUsuario(impuesto: ImpuestosModel, esEliminar: boolean) {
    this.impuestoSelecionado = impuesto;
    this.esEliminar = esEliminar;
    if (esEliminar) {
      this.impuestoSelecionado.impActivo = false;
      this.Guardar();
    } else {
      this.cambioDeshabilitar();
      this.showModalDialog();
    }
  }

  filtrar(){
    this.servicioImpuesto.listaImpuestosPorDescripcion(this.buscador).subscribe({
      next:x=>{
        if(x.length>0)
        {
         this.listaImpuestos=[];
         this.listaImpuestos=x;
        }
        else{this.obtenerLista()
        }
      },
      error:_e=>{
        this.obtenerLista();
      }
    })
  }

  showSuccess() {
    this.messageService.add({
      severity: 'success',
      summary: 'Se guardó satisfactoriamente',
      detail: 'El dato ingresado se registro con éxito',
    });
  }

  showError() {
    this.messageService.add({
      severity: 'error',
      summary: 'Cambio de estado',
      detail: 'Se cambio el estado del registro',
    });
  }

  showModalDialog() {
    if (this.impuestoSelecionado.impId > 0) {
      this.titulo = 'Editar Impuesto';
    } else {
      this.limpiar();
      this.titulo = 'Agregar Impuesto';
    }
    this.displayModal = true;
  }
}
