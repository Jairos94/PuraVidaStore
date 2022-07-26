using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex2
{
    public partial class FacturaResuman
    {
        public int FtrId { get; set; }
        public int FtrFactura { get; set; }
        public double MontoTotal { get; set; }

        public virtual Factura FtrFacturaNavigation { get; set; } = null!;
    }
}
