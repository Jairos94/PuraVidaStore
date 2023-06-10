using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class EstadoPedido
{
    public int EtpId { get; set; }

    public string EtpDescripcion { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Tracking> Trackins { get; set; } = new List<Tracking>();
}
