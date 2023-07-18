import { FacturaReporteListaModel } from "./factura-reporte-lista-model";

export interface FacturaReporteModel {
  montoTotal: number;
  montoTotalNulas: number;
  lista: FacturaReporteListaModel[];
}
