using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Proveedores
{
    public long PvdId { get; set; }

    public string PvdProveedorNmbre { get; set; } = null!;

    public string? PvdProveedorCorreo { get; set; }

    public string? PvdProveedorNumeroTelefono { get; set; }

    public int? PvdCodigoPais { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Tracking> Trackins { get; set; } = new List<Tracking>();
}
