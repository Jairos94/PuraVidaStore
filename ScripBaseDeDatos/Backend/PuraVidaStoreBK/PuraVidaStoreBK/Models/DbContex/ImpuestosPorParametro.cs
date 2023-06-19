using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class ImpuestosPorParametro
{
    public int ImpPid { get; set; }

    public int ImpPidParametroGlobal { get; set; }

    public int ImpPidImpuesto { get; set; }

    public virtual Impuesto ImpPidImpuestoNavigation { get; set; } = null!;

    public virtual ParametrosGlobales ImpPidParametroGlobalNavigation { get; set; } = null!;
}
