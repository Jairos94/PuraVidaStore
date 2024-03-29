import { MayoristaModel } from './mayorista-model';
import { UsuarioModel } from './usuario-model';
import { EstatusModel } from "./estatus-model";
import { FormaPagoModel } from "./forma-pago-model";
import { DetalleFacturaModel } from './detalle-factura-model';
import { FacturaResumenModel } from './factura-resumen-model';
import { ImpuestosPorFacturaModel } from './impuestos-por-factura-model';
import { BodegaModel } from './bodega-model';
import { HistorialMayoristaModel } from './historial-mayorista-model';

export interface FacturaModel {
  ftrId: number;
  ftrFecha: string;
  ftrIdUsuario: number;
  ftrMayorista: number | null;
  ftrEstatusId: number;
  ftrBodega: number;
  ftrFormaPago: number;
  ftrEsFacturaNula: boolean | null;
  ftrCodigoFactura: string | null;
  ftrCorreoEnvio: string | null;
  detalleFacturas: DetalleFacturaModel[] | null;
  facturaResumen: FacturaResumenModel[] | null;
  ftrBodegaNavigation: BodegaModel | null;
  ftrEstatus: EstatusModel | null;
  ftrFormaPagoNavigation: FormaPagoModel | null;
  ftrIdUsuarioNavigation: UsuarioModel | null;
  ftrMayoristaNavigation: MayoristaModel | null;
  historialFacturasAnulada: HistorialMayoristaModel[] | null;
  impuestosPorFacturas: ImpuestosPorFacturaModel[] | null;
}

