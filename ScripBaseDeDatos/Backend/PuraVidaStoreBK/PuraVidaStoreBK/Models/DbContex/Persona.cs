using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Persona
{
    public long PsrId { get; set; }

    public string PsrIdentificacion { get; set; } = null!;

    public string PsrNombre { get; set; } = null!;

    public string PsrApellido1 { get; set; } = null!;

    public string PsrApellido2 { get; set; } = null!;

    public virtual ICollection<ClientesMayorista> ClientesMayorista { get; set; } = new List<ClientesMayorista>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
