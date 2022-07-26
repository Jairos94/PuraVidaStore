using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex2
{
    public partial class Bodega
    {
        public Bodega()
        {
            Facturas = new HashSet<Factura>();
            Movimientos = new HashSet<Movimiento>();
        }

        public int BdgId { get; set; }
        public string BdgDescripción { get; set; } = null!;

        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
