import { TipoMovimientoModel } from './../../../../models/tipo-movimiento-model';
import { MovimientosService } from './../../../../services/movimientos.service';
import { Component, OnInit } from '@angular/core';
import { MotivoMovimientoModel } from 'src/app/models/motivo-movimiento-model';

@Component({
  selector: 'app-razon-ajustes',
  templateUrl: './razon-ajustes.component.html',
  styleUrls: ['./razon-ajustes.component.css'],
})
export class RazonAjustesComponent implements OnInit {
  constructor(private servicio: MovimientosService) {}

  listaMotivosMovimientos: MotivoMovimientoModel[] = [];
  encabezado: string = '';
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
        console.log(x);

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
}
