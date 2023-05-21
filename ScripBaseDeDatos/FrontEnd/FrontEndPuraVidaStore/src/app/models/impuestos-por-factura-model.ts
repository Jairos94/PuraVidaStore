import { FacturaModel } from "./factura-model";
import { ImpuestosModel } from "./impuestos-model";

export interface ImpuestosPorFacturaModel {
  ipfId: number;
    ipfIdFactura: number;
    ipfIdImpuesto: number;
    ipfIdFactura1: ImpuestosModel | null;
    ipfIdFacturaNavigation: FacturaModel | null;
}
