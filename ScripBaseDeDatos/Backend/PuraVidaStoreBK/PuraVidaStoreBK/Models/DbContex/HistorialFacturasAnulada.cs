using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class HistorialFacturasAnulada
{
    public long HlfId { get; set; }

    public int HlfIdUsuario { get; set; }

    public long HlfIdFctura { get; set; }

    public string HlfRazon { get; set; } = null!;

    public virtual Factura HlfIdFcturaNavigation { get; set; } = null!;

    public virtual Usuario HlfIdUsuarioNavigation { get; set; } = null!;
}
