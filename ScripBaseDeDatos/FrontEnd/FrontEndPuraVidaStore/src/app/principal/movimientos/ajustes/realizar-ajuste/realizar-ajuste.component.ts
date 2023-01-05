import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { InventariosModel } from 'src/app/models/inventarios-model';
import { MotivoMovimientoModel } from 'src/app/models/motivo-movimiento-model';
import { ProductoModel } from 'src/app/models/producto-model';
import { MovimientosService } from 'src/app/services/movimientos.service';
import { ProductoServiceService } from 'src/app/services/producto-service.service';
import { Archivo } from 'src/app/utils/Archivos';

@Component({
  selector: 'app-realizar-ajuste',
  templateUrl: './realizar-ajuste.component.html',
  styleUrls: ['./realizar-ajuste.component.css'],
  providers: [MessageService],
})
export class RealizarAjusteComponent implements OnInit {
  constructor(
    private messageService: MessageService,
    private servicioMovimientos: MovimientosService,
    private servicioProducto: ProductoServiceService,

  ) {}

  productoSeleccionado: ProductoModel = {
    prdId: 0,
    prdNombre: '',
    prdPrecioVentaMayorista: 0,
    prdPrecioVentaMinorista: 0,
    prdCodigo: '',
    prdUnidadesMinimas: 0,
    prdIdTipoProducto: 0,
    prdCodigoProvedor: '',
    pdrVisible: false,
    pdrFoto: null,
    pdrTieneExistencias: false,
    prdIdTipoProductoNavigation: null,
  };
  cantidad: number = 0;
  buscador: string = '';

  inventarioIngresar: InventariosModel = {
    producto: this.productoSeleccionado,
    cantidadExistencia: this.cantidad,
  };

  listaMotivosMovimiento: MotivoMovimientoModel[] = [];
  motivoMovimientoSeleccionado: MotivoMovimientoModel = {
    mtmId: 0,
    mtmDescripcion: '',
    mtmIdTipoMovimiento: 0,
    mtmIdTipoMovimientoNavigation: null,
  };

  ngOnInit(): void {
    this.ObtenerListaTipoAjuste();
  }

  leerArchivo(imagen: any): string {
    if (imagen == null) {
      imagen = '';
    }
    return Archivo.lectorImagen(imagen);
  }

  limpiar() {
    this.productoSeleccionado = {
      prdId: 0,
      prdNombre: '',
      prdPrecioVentaMayorista: 0,
      prdPrecioVentaMinorista: 0,
      prdCodigo: '',
      prdUnidadesMinimas: 0,
      prdIdTipoProducto: 0,
      prdCodigoProvedor: '',
      pdrVisible: false,
      pdrFoto: null,
      pdrTieneExistencias: false,
      prdIdTipoProductoNavigation: null,
    };
    this.cantidad = 0;
    this.buscador = '';

    this.inventarioIngresar = {
      producto: this.productoSeleccionado,
      cantidadExistencia: this.cantidad,
    };

    this.listaMotivosMovimiento = [];
    this.motivoMovimientoSeleccionado = {
      mtmId: 0,
      mtmDescripcion: '',
      mtmIdTipoMovimiento: 0,
      mtmIdTipoMovimientoNavigation: null,
    };
  }

  buscarProductoPorcodigo() {
    this.servicioProducto.ObtenerProductoPorCodigo(this.buscador).subscribe({
      next: (x) => {
        console.log(x);
      },
      error: (_e) => {
        this.showError();
        console.log(_e);

      },
    });
  }

  showError() {

    this.messageService.add({severity:'error', summary: 'Error', detail: 'Message Content'});
  }

  ObtenerListaTipoAjuste() {
    this.servicioMovimientos.obtenerMotivosMovimientos().subscribe({
      next: (x) => {
        this.listaMotivosMovimiento = [];
        this.listaMotivosMovimiento = x;
      },
      error: (_e) => {
        console.log(_e);
      },
    });
  }
}
