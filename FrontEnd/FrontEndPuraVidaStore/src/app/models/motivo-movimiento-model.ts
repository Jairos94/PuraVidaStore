import { TipoMovimientoModel } from "./tipo-movimiento-model";

export interface MotivoMovimientoModel {
        mtmId: number;
        mtmDescripcion: string;
        mtmIdTipoMovimiento: number;
        mtmIdTipoMovimientoNavigation: TipoMovimientoModel | null;
}
