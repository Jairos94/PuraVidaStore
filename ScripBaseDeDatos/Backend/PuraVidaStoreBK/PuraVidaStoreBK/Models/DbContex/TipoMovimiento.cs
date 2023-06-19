using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class TipoMovimiento
{
    public int TpmId { get; set; }

    public string TpmDescripcion { get; set; } = null!;

    public virtual ICollection<MotivosMovimiento> MotivosMovimientos { get; set; } = new List<MotivosMovimiento>();
}
