import { Component, OnInit } from '@angular/core';
import { DetalleFacturaModel } from 'src/app/models/detalle-factura-model';
import { ProductoModel } from 'src/app/models/producto-model';
import { Archivo } from 'src/app/utils/Archivos';

@Component({
  selector: 'app-facturacion',
  templateUrl: './facturacion.component.html',
  styleUrls: ['./facturacion.component.css'],
})
export class FacturacionComponent implements OnInit {
  buscadorCodigoBarras: string = '';
  verModal: boolean = false;
  listaDtealle: DetalleFacturaModel[] = [];

  productoBuscado: ProductoModel = {
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

  detalleSeleccionado: DetalleFacturaModel = {
    dtfId: 0,
    dtfIdProducto: 0,
    dtfIdFactura: 0,
    dtfPrecio: 0,
    dtfDescuento: 0,
    dtfCantidad: 0,
    dtfIdProducto1: this.productoBuscado,
    dtfIdProductoNavigation: null,
  };

  constructor() {}

  ngOnInit(): void {}


  limpiarDetalle(){
    this.productoBuscado = {
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

    this.detalleSeleccionado = {
      dtfId: 0,
      dtfIdProducto: 0,
      dtfIdFactura: 0,
      dtfPrecio: 0,
      dtfDescuento: 0,
      dtfCantidad: 0,
      dtfIdProducto1: this.productoBuscado,
      dtfIdProductoNavigation: null,
    };
  }

  leerArchivo(imagen: any): string {
    if (imagen == null) {
      imagen = '';
    }
    return Archivo.lectorImagen(imagen);
  }

  showResponsiveDialog() {
    this.verModal = true;
  }
}
