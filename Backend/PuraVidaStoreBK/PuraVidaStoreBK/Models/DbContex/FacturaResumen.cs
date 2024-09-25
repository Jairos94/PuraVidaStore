using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class FacturaResumen
{
    public long FtrId { get; set; }

    public long FtrFactura { get; set; }

    public decimal FtrMontoTotal { get; set; }

    public decimal FtrMontoImpuestos { get; set; }

    public decimal? FtrMontoPagado { get; set; }

    public decimal? FtrCambio { get; set; }

    public virtual Factura FtrFacturaNavigation { get; set; } = null!;
}
