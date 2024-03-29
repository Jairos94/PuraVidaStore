﻿namespace PuraVidaStoreBK.Models
{
    public class UsuarioModel
    {
        public System.Nullable<int> IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string password { get; set; }
        public  string?  email { get; set; }
        public int IdRol { get; set; }
        public int IdPersona { get; set; }
        public bool? Activo { get; set; }
        public PersonaModel persona { get; set; }
        public RolModel Rol { get; set; }
    }
}
