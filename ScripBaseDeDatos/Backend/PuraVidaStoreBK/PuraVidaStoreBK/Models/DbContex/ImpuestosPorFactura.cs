using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class ImpuestosPorFactura
{
    public long IpfId { get; set; }

    public long IpfIdFactura { get; set; }

    public int IpfIdImpuesto { get; set; }

    public virtual Factura IpfIdFacturaNavigation { get; set; } = null!;

    public virtual Impuesto IpfIdImpuestoNavigation { get; set; } = null!;
}
