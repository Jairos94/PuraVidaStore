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
  titulo: string = '';
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
  }
  Guardar() {
    this.servicioImpuesto.guardarImpuesto(this.impuestoSelecionado).subscribe({
      next: (x) => {
        this.obtenerLista();
        this.showSuccess();
      },
      error: (_e) => {
        console.log(_e);
      },
    });
    this.limpiar();
    this.displayModal = false;
  }

  cambioDeshabilitar() {
    if (this.impuestoSelecionado.impDescripcion!='' && this.impuestoSelecionado.impPorcentaje>0)
    {
     this.deshabilitarBotonGuardar=false;
    }
    else{
      this.deshabilitarBotonGuardar=true;
    }

  }

  showSuccess() {
    this.messageService.add({
      severity: 'success',
      summary: 'Se guardó satisfactoriamente',
      detail: 'El dato ingresado se registro con éxito',
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
