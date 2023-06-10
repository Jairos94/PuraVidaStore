using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class TrackingsAsociado
{
    public int TraId { get; set; }

    public int TraIdTrackin { get; set; }

    public int TraIdTrackinPrincial { get; set; }

    public virtual Tracking TraIdTrackinNavigation { get; set; } = null!;
}
