using System;
using System.Collections.Generic;

namespace bkPuraVidaStoreMinimal.Models
{
    public partial class FacturaResuman
    {
        public int FtrId { get; set; }
        public int FtrFactura { get; set; }
        public double MontoTotal { get; set; }

        public virtual Factura FtrFacturaNavigation { get; set; } = null!;
    }
}
