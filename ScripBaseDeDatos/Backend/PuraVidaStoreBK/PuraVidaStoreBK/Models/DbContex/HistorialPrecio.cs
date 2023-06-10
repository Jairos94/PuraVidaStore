using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class HistorialPrecio
{
    public int HlpId { get; set; }

    public int HlpIdProducto { get; set; }

    public DateTime HlpFecha { get; set; }

    public int HlpIdUsuario { get; set; }

    public double? HlpPrecioMayorista { get; set; }

    public double? HlpPrecioMinorista { get; set; }

    public double? HlpPrecioMayoristaAnterior { get; set; }

    public double? HlpPrecioMinoristaAnterior { get; set; }

    public virtual Producto HlpIdProductoNavigation { get; set; } = null!;

    public virtual Usuario HlpIdUsuarioNavigation { get; set; } = null!;
}
