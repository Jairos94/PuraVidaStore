import { UsuarioModel } from "./models/usuario-model"

export class activo {
    public static usuarioPrograma: UsuarioModel;

    //!valida si es usuario está logeado con el fin de validar en los componentes sino devolver al login
    public esUsuario()
    {
        if (activo.usuarioPrograma !=null ) 
        {return true;} else {return false;}
    }
}