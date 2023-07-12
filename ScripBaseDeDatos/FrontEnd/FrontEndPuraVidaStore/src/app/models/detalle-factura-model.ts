import { FacturaModel } from "./factura-model";
import { ProductoModel } from "./producto-model";

export interface DetalleFacturaModel {
  dtfId: number;
  dtfIdProducto: number;
  dtfIdFactura: number;
  dtfLinea: number | null;
  dtfPrecio: number;
  dtfMontoImpuestos: number;
  dtfDescuento: number | null;
  dtfCantidad: number;
  dtfIdProducto1: ProductoModel | null;
}
