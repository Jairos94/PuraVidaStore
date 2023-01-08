import { PersonaModel } from 'src/app/models/persona-model';
import { Component, OnInit } from '@angular/core';
import { DetalleFacturaModel } from 'src/app/models/detalle-factura-model';
import { MayoristaModel } from 'src/app/models/mayorista-model';
import { ProductoModel } from 'src/app/models/producto-model';
import { Archivo } from 'src/app/utils/Archivos';
import { ProductoServiceService } from 'src/app/services/producto-service.service';
import { MessageService } from 'primeng/api';
import { FacturaModel } from 'src/app/models/factura-model';
import { FormaPagoModel } from 'src/app/models/forma-pago-model';
import { VentasService } from 'src/app/services/ventas.service';

@Component({
  selector: 'app-facturacion',
  templateUrl: './facturacion.component.html',
  styleUrls: ['./facturacion.component.css'],
  providers: [MessageService],
})
export class FacturacionComponent implements OnInit {
  pagoCon: number = 0;
  cambio: number = 0;
  totalCantidad: number = 0;
  total: number = 0;
  buscadorCodigoBarras: string = '';
  mayoristaDeshabilitado: boolean = true;
  verModal: boolean = false;
  verModalPago: boolean = false;
  listaDtealle: DetalleFacturaModel[] = [];
  listaFormaPago: FormaPagoModel[] = [];

  formaPagoSeleccionado: FormaPagoModel = {
    frpId: 0,
    frpDescripcion: '',
  };

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

  facturaResumen: FacturaModel = {
    ftrId: 0,
    ftrFecha: '',
    ftrIdUsuario: 0,
    ftrMayorista: null,
    ftrEstatusId: 0,
    ftrBodega: 0,
    ftrFormaPago: 0,
    ftrEsFacturaNula: false,
    ftrCodigoFactura: null,
    ftrEstatus: null,
    ftrFormaPagoNavigation: null,
    ftrIdUsuarioNavigation: null,
    ftrMayoristaNavigation: null,
    detalleFacturas: null, //Array
    facturaResumen: null, //Aray
    impuestosPorFacturas: null, //Array
  };

  constructor(
    private servicioPorducto: ProductoServiceService,
    private servicioVenta: VentasService,
    private messageService: MessageService
  ) {}

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
          this.showError(
            'No hay resultados',
            'No se encontró resultados con los datos suministrados'
          );
        },
      });
  }

  obtenerFormaPago() {
    this.servicioVenta
      .listaFormaPago()
      .subscribe({ next: (x) =>
        {
         this.listaFormaPago=[];
         this.listaFormaPago = x
        }, error: (_e) => {console.log(_e);
      } });
  }

  sumarTotal() {
    this.total = 0;
    this.totalCantidad = 0;
    this.listaDtealle.forEach((x) => {
      this.total = this.total + x.dtfCantidad * x.dtfPrecio;
      this.totalCantidad = this.totalCantidad + x.dtfCantidad;
      if (this.totalCantidad > 3) {
        this.mayoristaDeshabilitado = false;
      } else {
        this.mayoristaDeshabilitado = true;
      }
    });
  }

  eliminarDeLaLista(id: number) {
    this.listaDtealle.forEach((x: DetalleFacturaModel, i: number) => {
      if (x.dtfId === id) {
        this.listaDtealle.splice(i, 1);
      }
    });

    this.listaDtealle.sort();
    this.sumarTotal();
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

  hacerCambio() {
    if (this.pagoCon > this.total) {
      this.cambio = this.pagoCon - this.total;
    }
  }
  showResponsiveDialogPago() {
    this.verModalPago = true;
  }

  showResponsiveDialog() {
    this.verModal = true;
  }

  showError(encabezado: string, mensaje: string) {
    this.messageService.add({
      severity: 'error',
      summary: encabezado,
      detail: mensaje,
    });
  }

  showSuccess() {
    this.messageService.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Message Content',
    });
  }
}
