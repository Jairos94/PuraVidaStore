import { MessageService } from 'primeng/api';
import { TipoMovimientoModel } from './../../../../models/tipo-movimiento-model';
import { MovimientosService } from './../../../../services/movimientos.service';
import { Component, OnInit } from '@angular/core';
import { MotivoMovimientoModel } from 'src/app/models/motivo-movimiento-model';

@Component({
  selector: 'app-razon-ajustes',
  templateUrl: './razon-ajustes.component.html',
  styleUrls: ['./razon-ajustes.component.css'],
  providers: [MessageService],
})
export class RazonAjustesComponent implements OnInit {
  constructor(
    private servicio: MovimientosService,
    private messageService: MessageService
  ) {}

  listaMotivosMovimientos: MotivoMovimientoModel[] = [];
  encabezado: string = '';
  buscador: string = '';
  displayModal: boolean = false;
  listaTipoMovimiento: TipoMovimientoModel[] = [];
  TipoMovimientoSeleccionado: TipoMovimientoModel = {
    tpmId: 1,
    tpmDescripcion: 'Positivo',
  };
  motivo: MotivoMovimientoModel = {
    mtmId: 0,
    mtmDescripcion: '',
    mtmIdTipoMovimiento: 0,
    mtmIdTipoMovimientoNavigation: null,
  };

  ngOnInit(): void {
    this.obtenerListaARazonesAjuste();
    this.ObtenerListaTipoAjuste();
  }

  showSuccess() {
    this.messageService.add({
      severity: 'success',
      summary: 'Éxito al guardar',
      detail: 'Se guardó la razón del ajuste satisfactoriamente',
    });
  }

  sujerencias() {
    this.servicio.SugerenciasMotivoMovimientos(this.buscador).subscribe({
      next: (x) => {
        if (x.length > 0) {
          this.listaMotivosMovimientos = [];
          this.listaMotivosMovimientos = x;
        } else {
          this.obtenerListaARazonesAjuste();
        }
      },
      error: (_e) => {
        this.obtenerListaARazonesAjuste();
        console.log(_e);
      },
    });
  }

  obtenerListaARazonesAjuste() {
    this.servicio.obtenerMotivosMovimientos().subscribe({
      next: (x) => {
        this.listaMotivosMovimientos = [];
        this.listaMotivosMovimientos = x;
      },
      error: (_e) => {
        console.log(_e);
      },
    });
  }

  limpiar() {
    this.TipoMovimientoSeleccionado = {
      tpmId: 1,
      tpmDescripcion: 'Positivo',
    };

    this.motivo = {
      mtmId: 0,
      mtmDescripcion: '',
      mtmIdTipoMovimiento: 0,
      mtmIdTipoMovimientoNavigation: null,
    };
  }

  showModalDialog() {
    this.encabezado = 'Agregar nueva razón de ajuste';
    this.displayModal = true;
  }

  ObtenerListaTipoAjuste() {
    this.servicio.obtenerTipoMovimiento().subscribe({
      next: (x) => {

        this.listaTipoMovimiento = [];
        this.listaTipoMovimiento = x;
      },
      error: (_e) => {
        console.log(_e);
      },
    });
  }

  Cancelar() {
    this.limpiar();
    this.displayModal = false;
  }

  editarAjuste(ajusteSeleccionado: MotivoMovimientoModel) {
    this.limpiar();
    this.motivo = ajusteSeleccionado;
    this.encabezado = 'Editar razón de ajuste';
    this.TipoMovimientoSeleccionado =
      this.motivo.mtmIdTipoMovimientoNavigation!;
    this.displayModal = true;
  }

  GuardarMotivoMovimiento() {
    this.motivo.mtmIdTipoMovimientoNavigation = null;
    this.motivo.mtmIdTipoMovimiento = this.TipoMovimientoSeleccionado.tpmId;
    this.servicio.GuardarMotivoMovimiento(this.motivo).subscribe({
      next: (x) => {
        this.showSuccess();
        this.limpiar();
        this.obtenerListaARazonesAjuste();

        this.displayModal = false;
      },
      error: (_e) => {
        console.log(_e);
      },
    });
  }
}
