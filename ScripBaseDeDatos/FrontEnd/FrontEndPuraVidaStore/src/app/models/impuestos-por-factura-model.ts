import { FacturaModel } from "./factura-model";
import { ImpuestosModel } from "./impuestos-model";

export interface ImpuestosPorFacturaModel {
  ipfId: number;
    ipfIdFactura: number;
    ipfIdImpuesto: number;
    ipfIdFactura1: ImpuestosModel | null;
    ipfPorcentaje: number | null;
    ipfIdFacturaNavigation: FacturaModel | null;
}
