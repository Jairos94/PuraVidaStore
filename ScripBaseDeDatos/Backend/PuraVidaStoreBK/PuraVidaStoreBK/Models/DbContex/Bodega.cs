using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Bodega
{
    public int BdgId { get; set; }

    public string BdgDescripcion { get; set; } = null!;

    public bool? BdgVisible { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual ICollection<ParametrosGlobales> ParametrosGlobales { get; set; } = new List<ParametrosGlobales>();
}
