import { PersonaModel } from "./persona-model";
import { RolModel } from "./rol-model";

export interface UsuarioModel {
    usrId: number;
    usrUser: string;
    clave: string;
    usrEmail: string | null;
    usrIdRol: number;
    usrIdPersona: number;
    usrActivo: boolean | null;
    persona: PersonaModel |null
    rol: RolModel |null
}
