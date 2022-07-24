import { PersonaModel } from "./persona-model";
import { RolModel } from "./rol-model";

export interface UsuarioModel {
    idUsuario: number | null;
    usuario: string;
    password: string;
    email: string;
    idRol: number;
    idPersona: number;
<<<<<<< HEAD
    persona: PersonaModel;
    Rol: RolModel;
=======
    persona: PersonaModel
    Rol: RolModel
>>>>>>> feature/Cambios
}
