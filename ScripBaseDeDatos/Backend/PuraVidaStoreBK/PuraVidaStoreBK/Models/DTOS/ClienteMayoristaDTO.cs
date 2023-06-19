﻿using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class ClienteMayoristaDTO
    {
        public long ClmId { get; set; }

        public long ClmIdPersona { get; set; }

        public DateTime ClmFechaCreacion { get; set; }

        public DateTime ClmFechaVencimiento { get; set; }

        public string? ClmCorreo { get; set; }

        public string? ClmTelefono { get; set; }

        public virtual PersonaDto? ClmIdPersonaNavigation { get; set; }
        public virtual ICollection<HistorialClienteMayoristaDTO>? HistorialClienteMayorista { get; set; }
    }
}
