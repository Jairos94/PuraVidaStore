using System;
using System.Collections.Generic;

namespace bkPuraVidaStoreMinimal.Models
{
    public partial class DetalleFactura
    {
        public int DtfId { get; set; }
        public int DtfIdProducto { get; set; }
        public int DtfIdFactura { get; set; }
        public double DtfPrecio { get; set; }
        public int? DtfDescuento { get; set; }

        public virtual Producto DtfIdProducto1 { get; set; } = null!;
        public virtual Factura DtfIdProductoNavigation { get; set; } = null!;
    }
}
