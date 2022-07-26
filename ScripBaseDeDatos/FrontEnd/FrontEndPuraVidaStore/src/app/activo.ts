import { PersonaModel } from "./models/persona-model";
import { UsuarioModel } from "./models/usuario-model"

export class activo {
    public static usuarioPrograma: UsuarioModel;
    public static personaInteractiva:PersonaModel;

    //!valida si es usuario está logeado con el fin de validar en los componentes sino devolver al login
    public esUsuario() {
        if (activo.usuarioPrograma != null) { return true; } else { return false; }
    }

    public static esAministrador() {
        if (activo.usuarioPrograma.idRol === 1) {
            return true;
        } else {
            return false;
        }
    }
}