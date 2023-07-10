import { MayoristaService } from './../../../services/mayorista.service';
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
import { FacturaResumenModel } from 'src/app/models/factura-resumen-model';
import { ParametrosGlobalesModel } from 'src/app/models/parametros-globales-model';
import { activo } from 'src/app/activo';

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
  montoTotalImpuestos: number = 0;
  subTotal: number = 0;
  total: number = 0;
  buscadorCodigoBarras: string = '';
  buscadorCedulaOId: string = '';
  esCambioTabla: boolean = false;
  pagarDeshabilitado: boolean = true;
  cambioDeshabilitar: boolean = false;
  mayoristaDeshabilitado: boolean = true;
  verModal: boolean = false;
  verModalPago: boolean = false;
  verMayoristaModal: boolean = false;
  parametrosGlobales: ParametrosGlobalesModel = activo.parametrosGlobales;
  listaDtealle: DetalleFacturaModel[] = [];
  listaFormaPago: FormaPagoModel[] = [];

  formaPagoSeleccionado: FormaPagoModel = {
    frpId: 1,
    frpDescripcion: 'Efectivo',
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
    dtfLinea: 0,
    dtfPrecio: 0,
    dtfMontoImpuestos: 0,
    dtfDescuento: 0,
    dtfCantidad: 0,
    dtfIdProducto1: null,
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
    clmFechaCreacion: this.fecha.toString(),
    clmFechaVencimiento: this.fecha.toString(),
    clmCorreo: '',
    clmTelefono: '',
    cantidadTiempo: 0,
    idTipoTiempo: 0,
    clmIdPersonaNavigation: this.personaMayorista,
    facturas: null,
    historialClienteMayorista: null,
  };

  factura: FacturaModel = {
    ftrId: 0,
    ftrFecha: '',
    ftrIdUsuario: 0,
    ftrMayorista: 0,
    ftrEstatusId: 0,
    ftrBodega: 0,
    ftrFormaPago: 0,
    ftrEsFacturaNula: false,
    ftrCodigoFactura: '',
    detalleFacturas: null,
    facturaResumen: null,
    ftrBodegaNavigation: null,
    ftrEstatus: null,
    ftrFormaPagoNavigation: null,
    ftrIdUsuarioNavigation: null,
    ftrMayoristaNavigation: null,
    historialFacturasAnulada: null,
    impuestosPorFacturas: null,
  };

  facturaResumen: FacturaResumenModel = {
    ftrId: 0,
    ftrFactura: 0,
    ftrMontoTotal: 0,
    ftrMontoPagado: 0,
    ftrCambio: 0,
    ftrFacturaNavigation: null,
  };

  constructor(
    private servicioPorducto: ProductoServiceService,
    private servicioVenta: VentasService,
    private servicioMayorista: MayoristaService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.obtenerFormaPago();
  }

  cambioTabla(detalle: DetalleFacturaModel) {
    let producto: ProductoModel = detalle.dtfIdProducto1!;
    this.listaDtealle.forEach(x=>
      {
        if(this.parametrosGlobales.prgHabilitarImpuestos && this.parametrosGlobales.impuestosPorParametros?.length! > 0)
        {

          let sumaImpuestosMayorista:number=0;
          let sumaImpuestosMinorista:number=0;
          this.parametrosGlobales.impuestosPorParametros?.forEach(impuesto=>
            {
              let porcentaje = impuesto.impPidImpuestoNavigation?.impPorcentaje!/100;
              sumaImpuestosMayorista = sumaImpuestosMayorista + (producto.prdPrecioVentaMayorista*porcentaje);
              sumaImpuestosMinorista = sumaImpuestosMinorista + (producto.prdPrecioVentaMinorista*porcentaje);
            });

          if(this.parametrosGlobales.prgImpustosIncluidos)
          {
            if(this.mayorista.clmId>0)
            {
              x.dtfPrecio=producto.prdPrecioVentaMayorista-sumaImpuestosMayorista;
              x.dtfMontoImpuestos=sumaImpuestosMayorista;
            }else{
              x.dtfPrecio=producto.prdPrecioVentaMinorista-sumaImpuestosMinorista;
              x.dtfMontoImpuestos=sumaImpuestosMinorista;
            }
          }
          else
          {
            if(this.mayorista.clmId>0)
            {
              x.dtfPrecio=producto.prdPrecioVentaMayorista;
              x.dtfMontoImpuestos=sumaImpuestosMayorista;
            }else{
              x.dtfPrecio=producto.prdPrecioVentaMinorista;
              x.dtfMontoImpuestos=sumaImpuestosMinorista;
            }

          }
        }
        else
        {
          x.dtfMontoImpuestos=0
          if(this.mayorista.clmId>0){
            x.dtfPrecio=producto.prdPrecioVentaMayorista
          }else{
            x.dtfPrecio=producto.prdPrecioVentaMinorista
          }
        }
      });
      this.sumarTotal();
  }

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
    this.servicioVenta.listaFormaPago().subscribe({
      next: (x) => {
        this.listaFormaPago = [];
        this.listaFormaPago = x;
        this.formaPagoSeleccionado;
      },
      error: (_e) => {
        console.log(_e);
      },
    });
  }

  sumarTotal() {
    this.total = 0;
    this.subTotal = 0;
    this.totalCantidad = 0;
    this.pagarDeshabilitado = false;
    this.montoTotalImpuestos = 0;

    //regorrido para obtener el subtotal
    this.listaDtealle.forEach((x, i) => {
      let sumaImpuestoMayorista: number = 0;
      let sumaImpuestoMinorista: number = 0;
      if (
        this.parametrosGlobales.prgHabilitarImpuestos &&
        this.parametrosGlobales.impuestosPorParametros != null
      ) {
        //!si se la da ctr +f se debe acomodar
        this.parametrosGlobales.impuestosPorParametros.forEach((impuesto) => {
          let porcentaje = impuesto.impPidImpuestoNavigation?.impPorcentaje! / 100;
          sumaImpuestoMayorista =sumaImpuestoMayorista + porcentaje * x.dtfIdProducto1?.prdPrecioVentaMayorista!;
          sumaImpuestoMinorista =sumaImpuestoMinorista +porcentaje * x.dtfIdProducto1?.prdPrecioVentaMinorista!;
        });

        if (this.parametrosGlobales.prgImpustosIncluidos) {
          if (x.dtfIdProducto1 != null) {
            if (this.mayorista.clmId > 0) {
              //!si se la da ctr +f se debe acomodar
              x.dtfPrecio =
                x.dtfIdProducto1.prdPrecioVentaMayorista -
                sumaImpuestoMayorista;
              x.dtfMontoImpuestos = sumaImpuestoMayorista;
            } else {
              //!si se la da ctr +f se debe acomodar
              x.dtfPrecio =
                x.dtfIdProducto1.prdPrecioVentaMinorista -
                sumaImpuestoMinorista;
              x.dtfMontoImpuestos = sumaImpuestoMinorista;
            }
          }
        } else {
          if (x.dtfIdProducto1 != null) {
            if (this.mayorista.clmId > 0) {
              x.dtfPrecio = x.dtfIdProducto1.prdPrecioVentaMayorista;
              x.dtfMontoImpuestos = sumaImpuestoMayorista;
            } else {
              x.dtfPrecio = x.dtfIdProducto1.prdPrecioVentaMinorista;
              x.dtfMontoImpuestos = sumaImpuestoMinorista;
            }
          }
        }
      } else {
        if (x.dtfIdProducto1 != null) {
          if (
            this.mayorista.clmId >
            this.parametrosGlobales.prgUndsHabilitarMayorista
          ) {
            //!si se la da ctr +f se debe acomodar
            x.dtfPrecio = x.dtfIdProducto1.prdPrecioVentaMayorista;
            x.dtfMontoImpuestos = sumaImpuestoMayorista;
          } else {
            //!si se la da ctr +f se debe acomodar
            x.dtfPrecio = x.dtfIdProducto1.prdPrecioVentaMinorista;
            x.dtfMontoImpuestos = sumaImpuestoMinorista;
          }
        }
      }

      this.subTotal = this.subTotal + x.dtfCantidad * x.dtfPrecio;
      this.totalCantidad = this.totalCantidad + x.dtfCantidad;

      if (
        this.totalCantidad >= this.parametrosGlobales.prgUndsHabilitarMayorista
      ) {
        this.mayoristaDeshabilitado = false;
      } else {
        this.mayoristaDeshabilitado = true;
      }
    });

    this.listaDtealle.forEach((x) => {
      this.montoTotalImpuestos =
        (this.montoTotalImpuestos + x.dtfMontoImpuestos) * x.dtfCantidad;
    });
    this.total = this.subTotal + this.montoTotalImpuestos;

    if (this.mayoristaDeshabilitado) {
      this.limpiarMayorista();
    }

    if (this.listaDtealle.length <= 0) {
      this.cancelar();
    }
  }

  ConsultarClienteMayorista() {
    this.servicioMayorista
      .obtenerMayoristaPorCedula(this.buscadorCedulaOId)
      .subscribe(
        (x) => {
          console.log(x);
          this.limpiarMayorista();
          this.buscadorCedulaOId = '';
          this.mayorista = x;
          this.sumarTotal();

          if (x == null) {
            this.limpiarMayorista();
          }

          if (this.mayorista.clmIdPersonaNavigation != null) {
            this.personaMayorista = this.mayorista.clmIdPersonaNavigation;
          }
        },
        (_e) => {
          this.limpiarMayorista();
          console.log(_e);
        }
      );
    this.verMayoristaModal = false;
  }
  deshabilitarCambio() {
    if (this.formaPagoSeleccionado.frpId > 1) {
      this.cambio = 0;
      this.pagoCon = 0;
      this.cambioDeshabilitar = true;
    } else {
      this.cambioDeshabilitar = false;
    }
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

          x.dtfCantidad = Number(x.dtfCantidad)+ this.detalleSeleccionado.dtfCantidad;
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

  limpiarMayorista() {
    this.personaMayorista = {
      psrId: 0,
      psrIdentificacion: '',
      psrNombre: '',
      psrApellido1: '',
      psrApellido2: '',
    };

    this.mayorista = {
      clmId: 0,
      clmIdPersona: 0,
      clmFechaCreacion: this.fecha.toString(),
      clmFechaVencimiento: this.fecha.toString(),
      clmCorreo: '',
      clmTelefono: '',
      cantidadTiempo: 0,
      idTipoTiempo: 0,
      clmIdPersonaNavigation: this.personaMayorista,
      facturas: null,
      historialClienteMayorista: null,
    };
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
      dtfLinea: 0,
      dtfPrecio: 0,
      dtfMontoImpuestos: 0,
      dtfDescuento: 0,
      dtfCantidad: 0,
      dtfIdProducto1: null,
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

  cancelar() {
    this.limpiarMayorista();
    this.subTotal=0;
    this.montoTotalImpuestos=0;
    this.pagoCon = 0;
    this.cambio = 0;
    this.pagarDeshabilitado = true;
    this.totalCantidad = 0;
    this.cambioDeshabilitar = false;
    this.total = 0;
    this.mayoristaDeshabilitado = true;
    this.verModal = false;
    this.verModalPago = false;
    this.listaDtealle = [];
    this.limpiarDetalle();

    this.formaPagoSeleccionado = {
      frpId: 1,
      frpDescripcion: 'Efectivo',
    };
    this.personaMayorista = {
      psrId: 0,
      psrIdentificacion: '',
      psrNombre: '',
      psrApellido1: '',
      psrApellido2: '',
    };

    this.factura = {
      ftrId: 0,
      ftrFecha: '',
      ftrIdUsuario: 0,
      ftrMayorista: 0,
      ftrEstatusId: 0,
      ftrBodega: 0,
      ftrFormaPago: 0,
      ftrEsFacturaNula: false,
      ftrCodigoFactura: '',
      detalleFacturas: null,
      facturaResumen: null,
      ftrBodegaNavigation: null,
      ftrEstatus: null,
      ftrFormaPagoNavigation: null,
      ftrIdUsuarioNavigation: null,
      ftrMayoristaNavigation: null,
      historialFacturasAnulada: null,
      impuestosPorFacturas: null,
    };
  }

  showResponsiveDialogPago() {
    this.verModalPago = true;
  }

  showResponsiveDialogMayorista() {
    this.verMayoristaModal = true;
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
