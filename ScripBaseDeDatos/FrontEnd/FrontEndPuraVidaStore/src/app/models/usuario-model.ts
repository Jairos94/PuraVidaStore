export interface UsuarioModel {
    idUsuario: number | null;
    usuario: string;
    password: string;
    email: string;
    idRol: number;
    idPersona: number;
}