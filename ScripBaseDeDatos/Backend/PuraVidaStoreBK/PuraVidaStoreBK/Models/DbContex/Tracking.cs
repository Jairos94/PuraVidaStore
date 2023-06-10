using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Tracking
{
    public int TrkId { get; set; }

    public DateTime TrkFecha { get; set; }

    public string TrKtrackin { get; set; } = null!;

    public int TrkMoneda { get; set; }

    public double? TrkCostoMoneda { get; set; }

    public double? TrkValorMoneda { get; set; }

    public int? TrkIdPedido { get; set; }

    public double? TrkPesoProveedor { get; set; }

    public double? TrkPesoReal { get; set; }

    public double? TrkMedidaLargoCm { get; set; }

    public double? TrkMedidaAnchoCm { get; set; }

    public double? TrkMedidaAlturaCm { get; set; }

    public int TrkEstado { get; set; }

    public int TrkProveedor { get; set; }

    public virtual ICollection<TrackingsAsociado> TrackinsAsociados { get; set; } = new List<TrackingsAsociado>();

    public virtual EstadoPedido TrkEstadoNavigation { get; set; } = null!;

    public virtual Pedido? TrkIdPedidoNavigation { get; set; }

    public virtual Moneda TrkMonedaNavigation { get; set; } = null!;

    public virtual Proveedores TrkProveedorNavigation { get; set; } = null!;
}
