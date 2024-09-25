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

    public int? PrgIdTiempo { get; set; }

    public int? PrgCantidadTiempo { get; set; }

    public string? PrgImpresora { get; set; }

    public string? PrgNombreNegocio { get; set; }

    public string? PrgCedula { get; set; }

    public virtual ICollection<ImpuestosPorParametro> ImpuestosPorParametros { get; set; } = new List<ImpuestosPorParametro>();

    public virtual ParametrosEmail? ParametrosEmail { get; set; }

    public virtual Bodega PrgIdBodegaNavigation { get; set; } = null!;

    public virtual TiempoParaRenovar? PrgIdTiempoNavigation { get; set; }
}
