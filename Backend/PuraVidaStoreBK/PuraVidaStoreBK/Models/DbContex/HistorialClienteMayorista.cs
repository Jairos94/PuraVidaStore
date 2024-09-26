using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class HistorialClienteMayorista
{
    public long HcmId { get; set; }

    public long HcmIdCliente { get; set; }

    public DateTime HcmFechaVencimiento { get; set; }

    public DateTime HcmFechaActualizacion { get; set; }

    public virtual ClientesMayorista HcmIdClienteNavigation { get; set; } = null!;
}
