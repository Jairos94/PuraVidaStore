using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class FacturaResumen
{
    public long FtrId { get; set; }

    public long FtrFactura { get; set; }

    public double FtrMontoTotal { get; set; }

    public double? FtrMontoPagado { get; set; }

    public double? FtrCambio { get; set; }

    public virtual Factura FtrFacturaNavigation { get; set; } = null!;
}
