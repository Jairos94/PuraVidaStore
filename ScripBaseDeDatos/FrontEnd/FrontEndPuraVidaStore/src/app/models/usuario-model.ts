import { PersonaModel } from "./persona-model";
import { RolModel } from "./rol-model";

export interface UsuarioModel {
    idUsuario: number | null;
    usuario: string;
    password: string;
    email: string;
    idRol: number;
    idPersona: number;
    activo: boolean | null;
    persona: PersonaModel
    Rol: RolModel
}
