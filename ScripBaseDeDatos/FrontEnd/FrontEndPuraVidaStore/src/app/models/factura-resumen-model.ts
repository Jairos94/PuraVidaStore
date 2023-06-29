import { FacturaModel } from "./factura-model";

export interface FacturaResumenModel {
  ftrId: number;
  ftrFactura: number;
  ftrMontoTotal: number;
  ftrMontoPagado: number | null;
  ftrCambio: number | null;
  ftrFacturaNavigation: FacturaModel | null;
}
