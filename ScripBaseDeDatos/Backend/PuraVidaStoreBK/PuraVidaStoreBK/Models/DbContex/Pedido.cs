using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Pedido
{
    public int PddId { get; set; }

    public DateTime PddFecha { get; set; }

    public int PddIdUsuario { get; set; }

    public string? PddRazonCancelada { get; set; }

    public int PddProveedor { get; set; }

    public int PddEstado { get; set; }

    public virtual ICollection<DetalleProductoPedido> DetalleProductoPedidos { get; set; } = new List<DetalleProductoPedido>();

    public virtual ICollection<OtrosCargo> OtrosCargos { get; set; } = new List<OtrosCargo>();

    public virtual EstadoPedido PddEstadoNavigation { get; set; } = null!;

    public virtual Usuario PddIdUsuarioNavigation { get; set; } = null!;

    public virtual Proveedores PddProveedorNavigation { get; set; } = null!;

    public virtual ICollection<Tracking> Trackins { get; set; } = new List<Tracking>();
}
