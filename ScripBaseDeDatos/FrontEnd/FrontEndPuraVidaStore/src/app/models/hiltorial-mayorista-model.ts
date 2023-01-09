import { MayoristaModel } from "./mayorista-model";

export interface HiltorialMayoristaModel {
  hcmId: number;
  hcmIdCliente: number;
  hcmFechaVencimiento: string;
  hcmFechaActualizacion: string;
  hcmIdClienteNavigation: MayoristaModel | null;

}
