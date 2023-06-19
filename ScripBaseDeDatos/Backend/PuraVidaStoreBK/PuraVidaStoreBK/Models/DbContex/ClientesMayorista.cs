using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class ClientesMayorista
{
    public long ClmId { get; set; }

    public long ClmIdPersona { get; set; }

    public DateTime ClmFechaCreacion { get; set; }

    public DateTime ClmFechaVencimiento { get; set; }

    public string? ClmCorreo { get; set; }

    public string? ClmTelefono { get; set; }

    public virtual Persona ClmIdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<HistorialClienteMayorista> HistorialClienteMayorista { get; set; } = new List<HistorialClienteMayorista>();
}
