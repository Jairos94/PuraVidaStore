import { HiltorialMayoristaModel } from "./hiltorial-mayorista-model";
import { PersonaModel } from "./persona-model";

export interface MayoristaModel {
  clmId: number;
  clmIdPersona: number;
  clmFechaCreacion: string;
  clmFechaVencimiento: string;
  clmCorreo: string | null;
  clmTelefono: string | null;
  clmIdPersonaNavigation: PersonaModel | null;
  historialClienteMayorista: HiltorialMayoristaModel[] | null;
}
