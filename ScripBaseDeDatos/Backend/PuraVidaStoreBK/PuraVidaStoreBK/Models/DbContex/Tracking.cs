using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Tracking
{
    public long TrkId { get; set; }

    public DateTime TrkFecha { get; set; }

    public string TrKtrackin { get; set; } = null!;

    public int TrkMoneda { get; set; }

    public decimal? TrkCostoMoneda { get; set; }

    public decimal? TrkValorMoneda { get; set; }

    public long? TrkIdPedido { get; set; }

    public decimal? TrkPesoProveedor { get; set; }

    public decimal? TrkPesoReal { get; set; }

    public decimal? TrkMedidaLargoCm { get; set; }

    public decimal? TrkMedidaAnchoCm { get; set; }

    public decimal? TrkMedidaAlturaCm { get; set; }

    public int TrkEstado { get; set; }

    public long TrkProveedor { get; set; }

    public virtual ICollection<TrackingsAsociado> TrackingsAsociados { get; set; } = new List<TrackingsAsociado>();

    public virtual EstadoPedido TrkEstadoNavigation { get; set; } = null!;

    public virtual Pedido? TrkIdPedidoNavigation { get; set; }

    public virtual Moneda TrkMonedaNavigation { get; set; } = null!;

    public virtual Proveedores TrkProveedorNavigation { get; set; } = null!;
}
