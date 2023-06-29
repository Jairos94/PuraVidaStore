import { ProductoServiceService } from 'src/app/services/producto-service.service';
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
    private motivoServicio: MovimientosService,
    private productoServicio: ProductoServiceService
  ) {}
  buscador: string = '';
  cantidad: number = 0;
  listaMotivoMovimiento: MotivoMovimientoModel[] = [];

  tipoMovimimentoSeleccionado:TipoMovimientoModel={
    tpmId:0,
    tpmDescripcion:''
  }
  motivoSeleccionado: MotivoMovimientoModel = {
    mtmId: 0,
    mtmDescripcion: '',
    mtmIdTipoMovimiento: 0,
    mtmIdTipoMovimientoNavigation: this.tipoMovimimentoSeleccionado,
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

  limpiar() {
    this.buscador = '';
    this.cantidad = 0;
    this.productoSeleccionado = {
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
    this.motivoSeleccionado = {
      mtmId: 0,
      mtmDescripcion: '',
      mtmIdTipoMovimiento: 0,
      mtmIdTipoMovimientoNavigation: this.tipoMovimimentoSeleccionado,
    };
  }

  buscarProductoPorCodigo() {
    this.productoServicio.ObtenerProductoPorCodigo(this.buscador).subscribe({
      next: (x) => {
        this.limpiar();
        this.productoSeleccionado = x;

      },
      error: (_e) => {
        this.showError()
        console.log(_e);

      },
    });
  }

guardarAjuste(){
  this.inventarioIngresar.producto=this.productoSeleccionado;
  this.inventarioIngresar.cantidadExistencia=this.cantidad;
  this.motivoServicio.GuardarMovimiento(this.inventarioIngresar,this.motivoSeleccionado.mtmId).subscribe({
    next:x=>{
      this.showSuccess();
      this.limpiar();
      //ruta
    },
    error:_e=>{console.log(_e);
    }
  })
}

showSuccess() {
  this.messageService.add({severity:'success', summary: 'Éxito al guardar', detail: 'Se guardó satisfactoriamente el registro'});
}

  showError() {
    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: 'No se encontró resultados con valor suministrado',
    });
  }
}
