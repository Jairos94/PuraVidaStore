using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Moneda
{
    public int MndId { get; set; }

    public string MndCodigo { get; set; } = null!;

    public string MndDescripcion { get; set; } = null!;

    public virtual ICollection<DetalleProductoPedido> DetalleProductoPedidos { get; set; } = new List<DetalleProductoPedido>();

    public virtual ICollection<OtrosCargo> OtrosCargos { get; set; } = new List<OtrosCargo>();

    public virtual ICollection<Tracking> Trackins { get; set; } = new List<Tracking>();
}
