using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class Impuesto
    {
        public Impuesto()
        {
            ImpuestosPorFacturas = new HashSet<ImpuestosPorFactura>();
            ImpustosIncluidos = new HashSet<ImpustosIncluido>();
        }

        public int ImpId { get; set; }
        public string? ImpDescripcion { get; set; }
        public double? ImpPorcentaje { get; set; }
        public bool? ImpActivo { get; set; }

        public virtual ICollection<ImpuestosPorFactura> ImpuestosPorFacturas { get; set; }
        public virtual ICollection<ImpustosIncluido> ImpustosIncluidos { get; set; }
    }
}
