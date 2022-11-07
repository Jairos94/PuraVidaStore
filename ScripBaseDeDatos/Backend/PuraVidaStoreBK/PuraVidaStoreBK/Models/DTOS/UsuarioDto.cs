namespace PuraVidaStoreBK.Models.DTOS
{
    public class UsuarioDto
    {
        public int UsrId { get; set; }
        public string UsrUser { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string? UsrEmail { get; set; }
        public int UsrIdRol { get; set; }
        public int UsrIdPersona { get; set; }
        public bool? UsrActivo { get; set; }

        public PersonaDto? Persona { get; set; }
        public RolUsuarioDto? Rol { get; set; }
    }
}
