using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class ParametrosGlobale
    {
        public ParametrosGlobale()
        {
            ImpustosIncluidos = new HashSet<ImpustosIncluido>();
        }

        public int PrgId { get; set; }
        public int PrgUndsHabilitarMayorista { get; set; }
        public int PrgUndsAgregarMayorista { get; set; }
        public bool PrgHabilitarImpuestos { get; set; }
        public bool PrgImpustosIncluidos { get; set; }

        public virtual ICollection<ImpustosIncluido> ImpustosIncluidos { get; set; }
    }
}
