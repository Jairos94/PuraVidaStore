import { PersonaModel } from "./persona-model";

export interface MayoristaModel {
  clmId: number;
        clmIdPersona: number;
        clmFechaCreacion: Date;
        clmFechaVencimiento: Date;
        clmCorreo: string;
        clmTelefono: string;
        clmIdPersonaNavigation: PersonaModel;
}
