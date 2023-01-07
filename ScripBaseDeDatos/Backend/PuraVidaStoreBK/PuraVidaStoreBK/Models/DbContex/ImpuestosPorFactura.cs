using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class ImpuestosPorFactura
    {
        public int IpfId { get; set; }
        public int? IpfIdFactura { get; set; }

        public virtual Factura? IpfIdFacturaNavigation { get; set; }
    }
}
