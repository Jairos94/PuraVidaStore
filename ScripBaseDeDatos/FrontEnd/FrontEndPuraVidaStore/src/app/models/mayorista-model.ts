import { FacturaModel } from './factura-model';
import { HistorialMayoristaModel } from './historial-mayorista-model';
import { PersonaModel } from './persona-model';

export interface MayoristaModel {
  clmId: number;
  clmIdPersona: number;
  clmFechaCreacion: string;
  clmFechaVencimiento: string | null;
  clmCorreo: string | null;
  clmTelefono: string | null;
  cantidadTiempo: number;
  idTipoTiempo: number;
  clmIdPersonaNavigation: PersonaModel | null;
  facturas: FacturaModel[] | null;
  historialClienteMayorista: HistorialMayoristaModel[] | null;
}
