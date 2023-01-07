using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class Impuesto
    {
        public Impuesto()
        {
            ImpuestosPorFacturas = new HashSet<ImpuestosPorFactura>();
        }

        public int ImpId { get; set; }
        public string? ImpDescripcion { get; set; }
        public double? ImpPorcentaje { get; set; }

        public virtual ICollection<ImpuestosPorFactura> ImpuestosPorFacturas { get; set; }
    }
}
