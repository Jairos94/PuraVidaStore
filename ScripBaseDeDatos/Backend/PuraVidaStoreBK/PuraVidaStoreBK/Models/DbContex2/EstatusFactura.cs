using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex2
{
    public partial class EstatusFactura
    {
        public EstatusFactura()
        {
            Facturas = new HashSet<Factura>();
        }

        public int EtfId { get; set; }
        public string EsfDescripcion { get; set; } = null!;

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
