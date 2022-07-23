using System;
using System.Collections.Generic;

namespace bkPuraVidaStoreMinimal.Models
{
    public partial class ClientesMayorista
    {
        public ClientesMayorista()
        {
            Facturas = new HashSet<Factura>();
            HistorialClienteMayorista = new HashSet<HistorialClienteMayoristum>();
        }

        public int ClmId { get; set; }
        public int ClmIdPersona { get; set; }
        public DateTime ClmFechaCreacion { get; set; }
        public DateTime ClmFechaVencimiento { get; set; }
        public string? ClmCorreo { get; set; }
        public string? ClmTelefono { get; set; }

        public virtual Persona ClmIdPersonaNavigation { get; set; } = null!;
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<HistorialClienteMayoristum> HistorialClienteMayorista { get; set; }
    }
}
