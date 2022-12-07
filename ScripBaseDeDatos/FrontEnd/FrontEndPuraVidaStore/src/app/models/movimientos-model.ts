import { MotivoMovimientoModel } from "./motivo-movimiento-model";
import { ProductoModel } from "./producto-model";

export interface MovimientosModel {
    mvmId: number;
    mvmIdProducto: number;
    mvmCantidad: number;
    mvmFecha: string | null;
    mvmIdMotivoMovimiento: number;
    mvmIdUsuario: number;
    mvmIdBodega: number;
    mvmIdProductoNavigation: ProductoModel | null;
    mvmIdMotivoMovimientoNavigation: MotivoMovimientoModel | null;
}
