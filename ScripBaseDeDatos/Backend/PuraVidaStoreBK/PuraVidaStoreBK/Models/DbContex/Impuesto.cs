using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Impuesto
{
    public int ImpId { get; set; }

    public string? ImpDescripcion { get; set; }

    public double? ImpPorcentaje { get; set; }

    public bool? ImpActivo { get; set; }

    public virtual ICollection<ImpuestosPorFactura> ImpuestosPorFacturas { get; set; } = new List<ImpuestosPorFactura>();

    public virtual ICollection<ImpuestosPorParametro> ImpuestosPorParametros { get; set; } = new List<ImpuestosPorParametro>();
}
