using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class ClienteMayoristaDTO
    {
        public long ClmId { get; set; }

        public long ClmIdPersona { get; set; }

        public DateTime ClmFechaCreacion { get; set; }

        public DateTime? ClmFechaVencimiento { get; set; } = null!;

        public string? ClmCorreo { get; set; }

        public string? ClmTelefono { get; set; }
        public int CantidadTiempo { get; set; }
        public int IdTipoTiempo { get; set; }

        public virtual PersonaDto? ClmIdPersonaNavigation { get; set; } = null!;

        public virtual ICollection<HistorialClienteMayoristaDTO>? HistorialClienteMayorista { get; set; } = new List<HistorialClienteMayoristaDTO>();
    }
}
