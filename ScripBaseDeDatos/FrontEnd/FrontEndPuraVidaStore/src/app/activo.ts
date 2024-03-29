import { BodegaModel } from "./models/bodega-model";
import { ImpuestosModel } from "./models/impuestos-model";
import { ParametrosGlobalesModel } from "./models/parametros-globales-model";
import { PersonaModel } from "./models/persona-model";
import { UsuarioModel } from "./models/usuario-model"

export class activo {
    public static usuarioPrograma: UsuarioModel;
    public static personaInteractiva: PersonaModel;
    public static token: string;
    public static bodegaIngreso: BodegaModel;
    public static ConsultaIdPersona: number;//? valida cuando hay una persona por editar
    public static parametrosGlobales:ParametrosGlobalesModel;
    public static listaImpuestos:ImpuestosModel[];

    //!valida si es usuario está logeado con el fin de validar en los componentes sino devolver al login
    public esUsuario() {
        if (activo.usuarioPrograma != null) { return true; } else { return false; }
    }

    public static esAministrador() {
        if (activo.usuarioPrograma.usrIdRol === 1) {
            return true;
        } else {
            return false;
        }
    }
    public static limpiarPersona() {
        activo.personaInteractiva.psrId = 0;
        activo.personaInteractiva.psrIdentificacion = '';
        activo.personaInteractiva.psrNombre = '';
        activo.personaInteractiva.psrApellido1 = '';
        activo.personaInteractiva.psrApellido2 = '';

    }
}
