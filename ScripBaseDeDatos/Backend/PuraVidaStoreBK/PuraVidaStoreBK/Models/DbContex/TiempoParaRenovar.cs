using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class TiempoParaRenovar
{
    public int TrpId { get; set; }

    public string TrrDescricpcion { get; set; } = null!;

    public virtual ICollection<ParametrosGlobales> ParametrosGlobales { get; set; } = new List<ParametrosGlobales>();
}
