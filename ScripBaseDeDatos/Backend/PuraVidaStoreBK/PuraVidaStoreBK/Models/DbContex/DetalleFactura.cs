using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class DetalleFactura
{
    public long DtfId { get; set; }

    public long DtfIdProducto { get; set; }

    public long DtfIdFactura { get; set; }

    public double DtfPrecio { get; set; }

    public int? DtfDescuento { get; set; }

    public int? DtfCantidad { get; set; }

    public virtual Producto DtfIdProducto1 { get; set; } = null!;

    public virtual Factura DtfIdProductoNavigation { get; set; } = null!;
}
