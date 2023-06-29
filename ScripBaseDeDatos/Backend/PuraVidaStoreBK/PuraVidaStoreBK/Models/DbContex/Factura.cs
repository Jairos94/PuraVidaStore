using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Factura
{
    public long FtrId { get; set; }

    public DateTime FtrFecha { get; set; }

    public int FtrIdUsuario { get; set; }

    public long? FtrMayorista { get; set; }

    public int FtrEstatusId { get; set; }

    public int FtrBodega { get; set; }

    public int FtrFormaPago { get; set; }

    public bool? FtrEsFacturaNula { get; set; }

    public string? FtrCodigoFactura { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual ICollection<FacturaResumen> FacturaResumen { get; set; } = new List<FacturaResumen>();

    public virtual Bodega FtrBodegaNavigation { get; set; } = null!;

    public virtual EstatusFactura FtrEstatus { get; set; } = null!;

    public virtual FormaPago FtrFormaPagoNavigation { get; set; } = null!;

    public virtual Usuario FtrIdUsuarioNavigation { get; set; } = null!;

    public virtual ClientesMayorista? FtrMayoristaNavigation { get; set; }

    public virtual ICollection<HistorialFacturasAnulada> HistorialFacturasAnulada { get; set; } = new List<HistorialFacturasAnulada>();

    public virtual ICollection<ImpuestosPorFactura> ImpuestosPorFacturas { get; set; } = new List<ImpuestosPorFactura>();
}
