using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class HistorialPrecio
{
    public long HlpId { get; set; }

    public long HlpIdProducto { get; set; }

    public DateTime HlpFecha { get; set; }

    public int HlpIdUsuario { get; set; }

    public decimal? HlpPrecioMayorista { get; set; }

    public decimal? HlpPrecioMinorista { get; set; }

    public decimal? HlpPrecioMayoristaAnterior { get; set; }

    public decimal? HlpPrecioMinoristaAnterior { get; set; }

    public virtual Producto HlpIdProductoNavigation { get; set; } = null!;

    public virtual Usuario HlpIdUsuarioNavigation { get; set; } = null!;
}
