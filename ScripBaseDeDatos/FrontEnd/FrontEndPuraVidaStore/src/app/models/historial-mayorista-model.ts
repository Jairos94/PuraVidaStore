import { MayoristaModel } from "./mayorista-model";

export interface HistorialMayoristaModel {
  hcmId: number;
  hcmIdCliente: number;
  hcmFechaVencimiento: string;
  hcmFechaActualizacion: string;
  hcmIdClienteNavigation: MayoristaModel | null;

}
