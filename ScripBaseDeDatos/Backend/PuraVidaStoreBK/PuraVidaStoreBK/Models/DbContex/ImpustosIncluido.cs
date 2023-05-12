using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class ImpustosIncluido
    {
        public int IicId { get; set; }
        public int? IicIdImpuesto { get; set; }
        public int? IicIdConfiguracion { get; set; }

        public virtual ParametrosGlobale? IicIdConfiguracionNavigation { get; set; }
        public virtual Impuesto? IicIdImpuestoNavigation { get; set; }
    }
}
