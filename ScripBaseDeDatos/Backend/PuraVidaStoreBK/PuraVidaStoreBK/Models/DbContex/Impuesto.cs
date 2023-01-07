using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class Impuesto
    {
        public int ImpId { get; set; }
        public string? ImpDescripcion { get; set; }
        public double? ImpPorcentaje { get; set; }
    }
}
