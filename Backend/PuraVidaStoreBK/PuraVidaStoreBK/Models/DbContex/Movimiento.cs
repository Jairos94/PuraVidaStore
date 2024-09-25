using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Movimiento
{
    public long MvmId { get; set; }

    public long MvmIdProducto { get; set; }

    public int MvmCantidad { get; set; }

    public DateTime MvmFecha { get; set; }

    public int MvmIdMotivoMovimiento { get; set; }

    public int MvmIdUsuario { get; set; }

    public int MvmIdBodega { get; set; }

    public virtual Bodega MvmIdBodegaNavigation { get; set; } = null!;

    public virtual MotivosMovimiento MvmIdMotivoMovimientoNavigation { get; set; } = null!;

    public virtual Producto MvmIdProductoNavigation { get; set; } = null!;

    public virtual Usuario MvmIdUsuarioNavigation { get; set; } = null!;
}
