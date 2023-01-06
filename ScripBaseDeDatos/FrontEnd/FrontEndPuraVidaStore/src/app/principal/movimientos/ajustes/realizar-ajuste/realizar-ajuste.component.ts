import { MotivoMovimientoModel } from 'src/app/models/motivo-movimiento-model';
import { MovimientosService } from 'src/app/services/movimientos.service';
import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { ProductoModel } from 'src/app/models/producto-model';
import { TipoMovimientoModel } from 'src/app/models/tipo-movimiento-model';
import { InventariosModel } from 'src/app/models/inventarios-model';
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
    private motivoServicio: MovimientosService
  ) {}
  buscador: string = '';
  cantidad: number = 0;
  listaMotivoMovimiento: MotivoMovimientoModel[] = [];
  motivoSeleccionado: MotivoMovimientoModel = {
    mtmId: 0,
    mtmDescripcion: '',
    mtmIdTipoMovimiento: 0,
    mtmIdTipoMovimientoNavigation: null,
  };
  productoSeleccionado: ProductoModel = {
    prdId: 0,
    prdNombre: '',
    prdPrecioVentaMayorista: 0,
    prdPrecioVentaMinorista: 0,
    prdCodigo: '',
    prdUnidadesMinimas: 0,
    prdIdTipoProducto: 0,
    prdCodigoProvedor: '',
    pdrVisible: true,
    pdrFoto: null,
    pdrTieneExistencias: false,
    prdIdTipoProductoNavigation: null,
  };
  inventarioIngresar: InventariosModel = {
    producto: this.productoSeleccionado,
    cantidadExistencia: this.cantidad,
  };

  ngOnInit(): void {
    this.cargarTipoAjustes();
  }

  leerArchivo(imagen: any): string {
    if (imagen == null) {
      imagen = '';
    }
    return Archivo.lectorImagen(imagen);
  }

  cargarTipoAjustes() {
    this.motivoServicio.obtenerMotivosMovimientos().subscribe({
      next: (x) => {
        this.listaMotivoMovimiento = [];
        this.listaMotivoMovimiento = x;
      },
      error: (_e) => {
        console.log(_e);
      },
    });
  }

  showError() {
    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Message Content',
    });
  }
}
