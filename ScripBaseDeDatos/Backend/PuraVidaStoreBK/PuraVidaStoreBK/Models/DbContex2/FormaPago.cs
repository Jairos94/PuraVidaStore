using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex2
{
    public partial class FormaPago
    {
        public FormaPago()
        {
            Facturas = new HashSet<Factura>();
        }

        public int FrpId { get; set; }
        public string FrpDescripcion { get; set; } = null!;

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
