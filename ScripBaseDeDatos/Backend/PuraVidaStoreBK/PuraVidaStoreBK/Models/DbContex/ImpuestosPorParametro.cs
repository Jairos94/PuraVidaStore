using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class ImpuestosPorParametro
    {
        public long ImpPid { get; set; }
        public int? ImpPidParametroGlobal { get; set; }
        public int? ImpPidImpuesto { get; set; }

        public virtual ParametrosGlobales? ImpPidParametroGlobalNavigation { get; set; }
    }
}
