import { PersonaModel } from 'src/app/models/persona-model';
import { Component, OnInit } from '@angular/core';
import { DetalleFacturaModel } from 'src/app/models/detalle-factura-model';
import { MayoristaModel } from 'src/app/models/mayorista-model';
import { ProductoModel } from 'src/app/models/producto-model';
import { Archivo } from 'src/app/utils/Archivos';
import { ProductoServiceService } from 'src/app/services/producto-service.service';

@Component({
  selector: 'app-facturacion',
  templateUrl: './facturacion.component.html',
  styleUrls: ['./facturacion.component.css'],
})
export class FacturacionComponent implements OnInit {
  total: number = 0;
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
  fecha = new Date();
  personaMayorista: PersonaModel = {
    psrId: 0,
    psrIdentificacion: '',
    psrNombre: '',
    psrApellido1: '',
    psrApellido2: '',
  };
  mayorista: MayoristaModel = {
    clmId: 0,
    clmIdPersona: 0,
    clmFechaCreacion: this.fecha,
    clmFechaVencimiento: this.fecha,
    clmCorreo: '',
    clmTelefono: '',
    clmIdPersonaNavigation: this.personaMayorista,
  };

  constructor(private servicioPorducto: ProductoServiceService) {}

  ngOnInit(): void {}

  buscarProductoPorCodigodBarras() {
    this.servicioPorducto
      .ObtenerProductoPorCodigo(this.buscadorCodigoBarras)
      .subscribe({
        next: (x) => {
          this.limpiarDetalle();
          this.productoBuscado = x;
          this.cargarListaDetalle();
          this.sumarTotal();
        },
        error: (_e) => {
          console.log(_e);
        },
      });
  }

  sumarTotal() {
    this.total=0
    this.listaDtealle.forEach(x=>{
      this.total= this.total+(x.dtfCantidad*x.dtfPrecio)
    });
  }

  cargarListaDetalle() {
    this.buscadorCodigoBarras = '';
    this.detalleSeleccionado.dtfIdProducto = this.productoBuscado.prdId;

    if (this.mayorista.clmId > 0) {
      this.detalleSeleccionado.dtfPrecio =
        this.productoBuscado.prdPrecioVentaMayorista;
    } else {
      this.detalleSeleccionado.dtfPrecio =
        this.productoBuscado.prdPrecioVentaMinorista;
    }

    this.detalleSeleccionado.dtfCantidad = 1;
    this.detalleSeleccionado.dtfIdProducto1 = this.productoBuscado;

    let ingresado = false;

    if (this.listaDtealle.length > 0) {
      this.listaDtealle.forEach((x) => {
        if (x.dtfIdProducto === this.detalleSeleccionado.dtfIdProducto) {
          x.dtfCantidad = x.dtfCantidad + this.detalleSeleccionado.dtfCantidad;
          ingresado = true;
        }
      });

      if (!ingresado) {
        this.listaDtealle.push(this.detalleSeleccionado);
      }
    } else {
      this.listaDtealle.push(this.detalleSeleccionado);
    }
  }

  limpiarDetalle() {
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
