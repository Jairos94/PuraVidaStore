using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class ParametrosGlobales
    {
        public ParametrosGlobales()
        {
            ImpuestosPorParametros = new HashSet<ImpuestosPorParametro>();
        }

        public int PrgId { get; set; }
        public int PrgUndsHabilitarMayorista { get; set; }
        public int PrgUndsAgregarMayorista { get; set; }
        public bool PrgHabilitarImpuestos { get; set; }
        public bool PrgImpustosIncluidos { get; set; }

        public virtual ParametrosEmail? ParametrosEmail { get; set; }
        public virtual ICollection<ImpuestosPorParametro> ImpuestosPorParametros { get; set; }
    }
}
