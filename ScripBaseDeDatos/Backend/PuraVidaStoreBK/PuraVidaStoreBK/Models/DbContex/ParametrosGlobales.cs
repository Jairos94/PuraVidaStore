using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class ParametrosGlobales
{
    public int PrgId { get; set; }

    public int PrgUndsHabilitarMayorista { get; set; }

    public int PrgUndsAgregarMayorista { get; set; }

    public bool PrgHabilitarImpuestos { get; set; }

    public bool PrgImpustosIncluidos { get; set; }

    public int PrgIdBodega { get; set; }

    public virtual ICollection<ImpuestosPorParametro> ImpuestosPorParametros { get; set; } = new List<ImpuestosPorParametro>();

    public virtual ParametrosEmail? ParametrosEmail { get; set; }

    public virtual Bodega PrgIdBodegaNavigation { get; set; } = null!;
}
