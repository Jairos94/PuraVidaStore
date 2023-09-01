import { ImpuestosPorFacturaModel } from './../../../models/impuestos-por-factura-model';
import { FacturaModel } from 'src/app/models/factura-model';
import { Component, OnInit } from '@angular/core';
import { activo } from 'src/app/activo';
import { EstructuraFacturaModel } from 'src/app/models/estructura-factura-model';
import { PersonaModel } from 'src/app/models/persona-model';
import { UsuarioModel } from 'src/app/models/usuario-model';
import { FacturaResumenModel } from 'src/app/models/factura-resumen-model';
import { VentasService } from 'src/app/services/ventas.service';

@Component({
  selector: 'app-anular-facturas',
  templateUrl: './anular-facturas.component.html',
  styleUrls: ['./anular-facturas.component.css'],
})
export class AnularFacturasComponent implements OnInit {
  listaFacturas: EstructuraFacturaModel[] = [];
  listaImpuestosFactura: ImpuestosPorFacturaModel[] = [];
  habilitarModal: boolean = false;
  habilitarRazon: boolean = false;
  razonAnular: string = '';
  buscador:string='';
  facturaResumen: FacturaResumenModel = {
    ftrId: 0,
    ftrFactura: 0,
    ftrMontoTotal: 0,
    ftrMontoImpuestos: 0,
    ftrMontoPagado: null,
    ftrCambio: null,
    ftrFacturaNavigation: null,
  };
  personaMayorista: PersonaModel = {
    psrId: 0,
    psrIdentificacion: '',
    psrNombre: '',
    psrApellido1: '',
    psrApellido2: '',
  };
  usurioFactura: UsuarioModel = {
    usrId: 0,
    usrUser: '',
    clave: '',
    usrEmail: '',
    usrIdRol: 0,
    usrIdPersona: 0,
    usrActivo: null,
    persona: null,
    rol: null,
  };
  facturaseleccionada: FacturaModel = {
    ftrId: 0,
    ftrFecha: '',
    ftrIdUsuario: 0,
    ftrMayorista: 0,
    ftrEstatusId: 0,
    ftrBodega: 0,
    ftrFormaPago: 0,
    ftrEsFacturaNula: false,
    ftrCodigoFactura: '',
    ftrCorreoEnvio:'',
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

  constructor(private ventas: VentasService) {}

  ngOnInit(): void {
    this.ObtenerFacturas();
  }

  ObtenerFacturas() {
    this.ventas.FacturasMes(activo.bodegaIngreso.bdgId).subscribe({
      next: (x) => {
        this.listaFacturas = [];
        this.listaFacturas = x;
      },
      error: (_e) => console.log(),
    });
  }

  consultarFactura(codigoFactura: string) {
    this.ventas.FacturasPorCodigo(codigoFactura).subscribe({
      next: (x) => {
        this.limpiarFacturaSeleccionada();
        this.facturaseleccionada = x;
        this.personaMayorista =
          this.facturaseleccionada.ftrMayoristaNavigation?.clmIdPersonaNavigation!;
        this.usurioFactura = this.facturaseleccionada.ftrIdUsuarioNavigation!;
        this.listaImpuestosFactura =
          this.facturaseleccionada.impuestosPorFacturas!;
        this.facturaseleccionada.facturaResumen?.forEach((x) => {
          this.facturaResumen = x;
        });
        this.habilitarModal = true;
      },
      error: (_e) => console.log(_e),
    });
  }

  limpiarFacturaSeleccionada() {
    this.razonAnular = '';
    this.facturaseleccionada = {
      ftrId: 0,
      ftrFecha: '',
      ftrIdUsuario: 0,
      ftrMayorista: 0,
      ftrEstatusId: 0,
      ftrBodega: 0,
      ftrFormaPago: 0,
      ftrEsFacturaNula: false,
      ftrCodigoFactura: '',
      ftrCorreoEnvio:'',
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
    this.personaMayorista = {
      psrId: 0,
      psrIdentificacion: '',
      psrNombre: '',
      psrApellido1: '',
      psrApellido2: '',
    };
    this.usurioFactura = {
      usrId: 0,
      usrUser: '',
      clave: '',
      usrEmail: '',
      usrIdRol: 0,
      usrIdPersona: 0,
      usrActivo: null,
      persona: null,
      rol: null,
    };

    this.facturaResumen = {
      ftrId: 0,
      ftrFactura: 0,
      ftrMontoTotal: 0,
      ftrMontoImpuestos: 0,
      ftrMontoPagado: null,
      ftrCambio: null,
      ftrFacturaNavigation: null,
    };
    this.listaImpuestosFactura = [];
  }

  anularFactura() {
    this.ventas
      .AnularFacturas(
        this.facturaseleccionada.ftrId,
        activo.usuarioPrograma.usrId,
        this.razonAnular
      )
      .subscribe({
        next: (x) => {
          this.limpiarFacturaSeleccionada();
          this.habilitarRazon=false;
          this.habilitarModal= false;
          this.ObtenerFacturas();
        },
      });
  }
}
